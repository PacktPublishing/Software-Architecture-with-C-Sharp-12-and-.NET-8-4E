using DDD.ApplicationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackagesManagement.Commands
{
    public class DeletePackageCommand(int id) : ICommand
    {

        public int PackageId { get; private set; } = id;
    }
}
