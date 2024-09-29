using Itrantion.Server.Models;
using static Itrantion.Server.Database.Realization.PresentationsRepository;

namespace Itrantion.Server.Database.Abstraction
{
    public interface IPresentationsRepository
    {
        Task ChangeName(Guid id, string name);
        Task<(string? error, PresentationModel? instance)> Create(string nickname, int type, string name = "No name");
        Task<(Guid? presentationId, string? username)> DeleteConnection(string connectionId);
        Task EditPermissions(Guid id, string user, string username, Permissions permission);
        Task<PresentationModel[]> GetPresentation(int page);
        Task<PresentationResult> GetModel(Guid id);
        Task<UserModel?> RegisterNewConnection(string username, string connectionId, Guid presentationId);
    }
}