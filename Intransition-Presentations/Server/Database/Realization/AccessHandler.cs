using Instend.Server.Database.Abstraction;
using Itrantion.Server.Database;
using Itrantion.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Instend.Server.Database.Realization
{
    public class AccessHandler : IAccessHandler
    {
        private readonly DatabaseContext _context;

        public AccessHandler(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> IsUserHasEditPermission(Guid presentationId, string username)
        {
            var presentation = await _context.Presentations
                .FirstOrDefaultAsync(x => x.Id == presentationId);

            if (presentation == null)
                return false;

            if (presentation.Owner == username)
                return true;

            var permission = await _context.Permissions
                .FirstOrDefaultAsync(x => x.Username == username && x.Permission == Permissions.ReadAndEdit.ToString());

            return permission != null;
        }
    }
}