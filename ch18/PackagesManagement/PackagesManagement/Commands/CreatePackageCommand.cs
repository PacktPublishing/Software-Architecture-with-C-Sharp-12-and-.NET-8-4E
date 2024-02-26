using DDD.ApplicationLayer;
using PackagesManagementDomain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackagesManagement.Commands
{
    public class CreatePackageCommand(IPackageFullEditDTO values) : ICommand
    {

        public IPackageFullEditDTO Values { get; private set; } = values;
    }
}
