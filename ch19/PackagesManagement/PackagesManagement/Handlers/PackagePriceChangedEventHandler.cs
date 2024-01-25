using DDD.ApplicationLayer;
using PackagesManagementDomain.Aggregates;
using PackagesManagementDomain.Events;
using PackagesManagementDomain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackagesManagement.Handlers
{
    public class PackagePriceChangedEventHandler(IPackageEventRepository repo) :
        IEventHandler<PackagePriceChangedEvent>
    {
        
        public Task HandleAsync(PackagePriceChangedEvent ev)
        {
            repo.New(PackageEventType.CostChanged, ev.PackageId, ev.OldVersion, ev.NewVersion, ev.NewPrice);
            return Task.CompletedTask;
        }
    }
}
