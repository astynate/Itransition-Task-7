namespace Instend.Server.Database.Abstraction
{
    public interface IAccessHandler
    {
        Task<bool> IsUserHasEditPermission(Guid presentationId, string username);
    }
}