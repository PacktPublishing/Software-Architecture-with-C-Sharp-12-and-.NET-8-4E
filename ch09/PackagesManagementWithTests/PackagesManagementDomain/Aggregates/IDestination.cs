using DDD.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace PackagesManagementDomain.Aggregates
{
    public interface IDestination: IEntity<int>
    {
        void FullUpdate(IDestination o);
        string Name { get; }
        string Country { get;}
        string Description { get;}
        
    }
}
