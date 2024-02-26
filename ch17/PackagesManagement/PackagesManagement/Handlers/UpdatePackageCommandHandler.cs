using DDD.ApplicationLayer;
using DDD.DomainLayer;
using Microsoft.EntityFrameworkCore;
using PackagesManagement.Commands;
using PackagesManagementDomain.Aggregates;
using PackagesManagementDomain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackagesManagement.Handlers
{
    public class UpdatePackageCommandHandler(IPackageRepository repo, IEventMediator mediator) : ICommandHandler<UpdatePackageCommand>
    {
        
        public async Task HandleAsync(UpdatePackageCommand command)
        {
            bool done = false;
            IPackage? model;
            while (!done)
            {
                try
                {
                    model = await repo.GetAsync(command.Updates.Id);
                    if (model == null) return;
                    model.FullUpdate(command.Updates);
                    await mediator.TriggerEvents(model.DomainEvents);
                    await repo.UnitOfWork.SaveEntitiesAsync();
                    done = true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    //add here some logging
                }
            }
        }
    }
}
