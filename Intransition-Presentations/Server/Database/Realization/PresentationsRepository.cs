using Instend.Server.Database.Abstraction;
using Itrantion.Server.Database.Abstraction;
using Itrantion.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Itrantion.Server.Database.Realization
{
    public class PresentationsRepository : IPresentationsRepository
    {
        private readonly DatabaseContext _context;

        private readonly IUsersRepository _userRepository;

        private readonly ISlidesRepository _slidesRepository;

        public PresentationsRepository
        (
            DatabaseContext context, 
            IUsersRepository userRepository, 
            ISlidesRepository slidesRepository
        )
        {
            _context = context;
            _userRepository = userRepository;
            _slidesRepository = slidesRepository;
        }

        public async Task<PresentationModel[]> GetPresentation(int page) 
            => await _context.Presentations
                    .OrderByDescending(x => x.Date)
                    .Skip(page)
                    .Take(3)
                    .ToArrayAsync();

        public async Task ChangeName(Guid id, string name)
        {
            await _context.Presentations
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.Name, name));

            await _context.SaveChangesAsync();
        }

        public async Task EditPermissions(Guid id, string user, string username, Permissions permission)
        {
            var presentation = await _context.Presentations
                .FirstOrDefaultAsync(x => x.Id == id);

            if (presentation == null || presentation.Owner != username)
                return;

            var permissionModel = await _context.Permissions
                .FirstOrDefaultAsync(x => x.Username == user && x.PresentationId == id);

            if (permissionModel != null && permissionModel.Permission == permission.ToString()) 
                return;

            if (permissionModel != null)
            {
                permissionModel.Permission = permission.ToString();

                _context.Entry(permissionModel).State = EntityState.Modified;    
                await _context.SaveChangesAsync(); return;
            }

            var newPermission = new PermissionModel()
            {
                PresentationId = id,
                Username = user,
                Permission = permission.ToString()
            };

            await _context.AddAsync(newPermission);
            await _context.SaveChangesAsync();
        }

        public async Task<UserModel?> RegisterNewConnection(string username, string connectionId, Guid presentationId)
        {
            var user = await _userRepository.Login(username, 0);

            if (user.error != null || user.instance == null)
                return null;

            var result = await _context.Connections
                .Where(x => x.User == username && x.Presentation == presentationId)
                .ExecuteUpdateAsync(x => x.SetProperty(x => x.ConnectionId, connectionId));

            if (result == 0)
            {
                await _context.Connections
                    .AddAsync(new UserConnection(username, connectionId, presentationId));
            }

            await _context.SaveChangesAsync();
            return user.instance;
        }

        public async Task<(Guid? presentationId, string? username)> DeleteConnection(string connectionId)
        {
            var connection = await _context.Connections
                .FirstOrDefaultAsync(x => x.ConnectionId == connectionId);

            if (connection != null)
            {
                _context.Connections.Remove(connection);
                await _context.SaveChangesAsync();

                return (connection.Presentation, connection.User);
            }

            return (null, null);
        }

        public class PresentationResult
        {
            public PresentationModel? Presentation { get; set; }
            public SlideModel[]? Slides { get; set; }
        }

        public static SlideModel SetSlideItems(SlideModel slide, DatabaseContext context)
        {
            slide.texts = context.Texts
                .Where(text => text.SlideId == slide.Id)
                .ToArray();

            return slide;
        }

        public async Task<PresentationResult> GetModel(Guid id)
        {
            var result = await _context.Presentations
                .Where(x => x.Id == id)
                .Select(presentation => new
                {
                    Presentation = presentation,
                    Slides = _context.Slides
                        .Where(slide => slide.PresentationId == presentation.Id)
                        .Select(slide => SetSlideItems(slide, _context))
                        .ToArray()
                })
                .FirstOrDefaultAsync();

            if (result == null)
                return new PresentationResult();

            result.Presentation.permissions = await _context.Permissions
                .Where(x => x.PresentationId == result.Presentation.Id)
                .ToListAsync();

            result.Presentation.connectedUsers = await _context.Connections
                    .Where(x => x.Presentation == id)
                    .Join(_context.Users,
                        (link) => link.User,
                        (user) => user.Username,
                        (link, user) => user)
                    .ToListAsync();

            return new PresentationResult() {
                Presentation = result!.Presentation,
                Slides = result.Slides,
            };
        }

        public async Task<(string? error, PresentationModel? instance)> Create(string nickname, int type, string name = "No name")
        {
            var searchUserResult = await _context.Users.FirstOrDefaultAsync(x => x.Username == nickname);

            if (searchUserResult == null)
                return ("Username not found", null);

            var presentation = PresentationModel.Create(name, nickname, DateTime.Now, type);

            if (presentation.error != null || presentation.instance == null)
                return presentation;

            var slide = new SlideModel(0, presentation.instance.Id);

            await _context.AddAsync(presentation.instance);
            await _context.AddAsync(slide);

            await _context.SaveChangesAsync();

            if (type > 0)
            {
                await _slidesRepository.AddText(new TextModel()
                {
                    SlideId = slide.Id,
                    Text = "Presentation",
                    Top = 194,
                    Left = 90,
                    Height = 138,
                    Width = 532,
                    SheetHeight = 556,
                    FontWeight = "700",
                    SheetWidth = 988,
                    FontSize = "62px",
                });

                await _slidesRepository.AddText(new TextModel()
                {
                    SlideId = slide.Id,
                    Text = "Author 1, Author 2",
                    Top = 281,
                    Left = 90,
                    Height = 79,
                    Width = 461,
                    SheetHeight = 556,
                    SheetWidth = 988,
                    FontSize = "38px",
                });
            }

            return presentation;
        }
    }
}