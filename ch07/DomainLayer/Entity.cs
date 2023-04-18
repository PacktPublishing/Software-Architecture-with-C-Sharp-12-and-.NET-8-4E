using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDD.DomainLayer
{
    public abstract class Entity<K>: IEntity<K>
    {
        
        public virtual K Id { get; protected set; }
        public bool IsTransient()
        {
            return Object.Equals(Id, default(K));
            
        }
        public override bool Equals(object obj)
        {
            return obj is Entity<K> entity &&
              Equals(entity); 
        }

        public bool Equals(IEntity<K> other)
        {
            if (other == null || 
                other.IsTransient() || this.IsTransient())
                return false;

            return Object.Equals(Id, other.Id);
        }

        int? _requestedHashCode;
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = HashCode.Combine(Id);
                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }
        public static bool operator ==(Entity<K> left, Entity<K> right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null));
            else
                return left.Equals(right);
        }
        public static bool operator !=(Entity<K> left, Entity<K> right)
        {
            return !(left == right);
        }
        [NotMapped]
        public List<IEventNotification> DomainEvents { get; private set; }
        public void AddDomainEvent(IEventNotification evt)
        {
            DomainEvents ??= new List<IEventNotification>();
            DomainEvents.Add(evt);
        }
        public void RemoveDomainEvent(IEventNotification evt)
        {
            DomainEvents?.Remove(evt);
        }
    }
}
