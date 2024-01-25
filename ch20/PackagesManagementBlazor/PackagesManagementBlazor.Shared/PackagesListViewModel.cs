using System.Collections.Generic;

namespace PackagesManagementBlazor.Shared
{
    public class PackagesListViewModel
    {
        public required IReadOnlyCollection<PackageInfosViewModel> 
            Items { get; set; }
    }
}
