using System.Threading.Tasks;
using DDD.DomainLayer;

namespace DDD.ApplicationLayer
{
    public interface IEventHandler
    {
    }
    public interface IEventHandler<T>: IEventHandler
    where T : IEventNotification
    {
        Task HandleAsync(T ev);
    }
}
