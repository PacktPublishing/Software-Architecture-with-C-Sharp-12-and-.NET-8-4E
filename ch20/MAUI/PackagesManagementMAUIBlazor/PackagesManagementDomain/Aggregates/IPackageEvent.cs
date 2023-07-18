using DDD.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace PackagesManagementDomain.Aggregates
{
    public enum PackageEventType {Deleted, CostChanged}
    public interface IPackageEvent: IEntity<long>
    {
        PackageEventType Type { get; }
        int PackageId { get; }
        decimal NewPrice { get; }
        long? OldVersion { get;}
        long? NewVersion { get;}
    }
}
