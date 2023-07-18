using DDD.DomainLayer;
using PackagesManagementDomain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PackagesManagementDomain.IRepositories
{
    public interface IDestinationRepository:IRepository<IDestination>
    {
        Task<IDestination> Get(int id);
        IDestination New();
    }
}
