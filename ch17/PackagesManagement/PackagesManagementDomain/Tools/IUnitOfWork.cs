using System.Threading.Tasks;

namespace DDD.DomainLayer
{
    public interface IUnitOfWork
    {
        Task<bool> SaveEntitiesAsync();
        Task StartAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
