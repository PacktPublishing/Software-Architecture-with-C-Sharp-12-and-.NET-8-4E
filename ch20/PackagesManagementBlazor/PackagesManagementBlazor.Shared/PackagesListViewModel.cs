using System.Collections.Generic;

namespace PackagesManagementBlazor.Shared
{
    public class PackagesListViewModel
    {
        public IEnumerable<PackageInfosViewModel> 
            Items { get; set; }
    }
}
