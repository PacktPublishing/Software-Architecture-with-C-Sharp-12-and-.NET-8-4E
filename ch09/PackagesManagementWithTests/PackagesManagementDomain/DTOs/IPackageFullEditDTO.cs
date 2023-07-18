using System;
using System.Collections.Generic;
using System.Text;

namespace PackagesManagementDomain.DTOs
{
    public interface IPackageFullEditDTO
    {
        int Id { get; set; }
        string Name { get; set; }

        string Description { get; }
        decimal Price { get; set; }
        int DurationInDays { get; }
        DateTime? StartValidityDate { get; }
        DateTime? EndValidityDate { get; }
        int DestinationId { get; }
    }
}
