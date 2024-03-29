﻿using DDD.ApplicationLayer;
using PackagesManagementDomain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PackagesManagement.Commands
{
    public class UpdatePackageCommand(IPackageFullEditDTO updates) : ICommand
    {
        public IPackageFullEditDTO Updates { get; private set; }= updates;
    }
}
