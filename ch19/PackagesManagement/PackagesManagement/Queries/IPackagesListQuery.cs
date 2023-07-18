using DDD.ApplicationLayer;
using PackagesManagement.Models.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackagesManagement.Queries
{
    public interface IPackagesListQuery: IQuery
    {
        Task<IEnumerable<PackageInfosViewModel>> GetAllPackages();
        
    }
}
