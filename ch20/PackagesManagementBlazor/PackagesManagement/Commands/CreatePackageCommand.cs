using DDD.ApplicationLayer;
using PackagesManagementDomain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackagesManagement.Commands
{
    public class CreatePackageCommand: ICommand
    {
        public CreatePackageCommand(IPackageFullEditDTO values)
        {
            Values = values;
        }
        public IPackageFullEditDTO Values { get; private set; }
    }
}
