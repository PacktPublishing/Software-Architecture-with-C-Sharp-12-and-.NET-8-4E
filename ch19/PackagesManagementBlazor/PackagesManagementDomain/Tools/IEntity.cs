using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.DomainLayer
{
    public interface IEntity<K>
        where K : IEquatable<K>
    {
        K Id { get; }
        bool IsTransient();
        List<IEventNotification> DomainEvents { get; }
        void AddDomainEvent(IEventNotification evt);
        void RemoveDomainEvent(IEventNotification evt);
        
    }
}
