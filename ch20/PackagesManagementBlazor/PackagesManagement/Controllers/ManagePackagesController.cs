using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DDD.ApplicationLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PackagesManagement.Commands;
using PackagesManagement.Models;
using PackagesManagement.Models.Packages;
using PackagesManagement.Queries;
using PackagesManagementDomain.IRepositories;

namespace PackagesManagement.Controllers
{
    [Authorize(Roles= "Admins")]
    public class ManagePackagesController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index([FromServices] IPackagesListQuery query)
        {
            var results = await query.GetAllPackages();
            var vm = new PackagesListViewModel { Items = results };
            return View(vm);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Create(
            PackageFullEditViewModel vm,
            [FromServices] ICommandHandler<CreatePackageCommand> command)
        {
            if (ModelState.IsValid) { 
                await command.HandleAsync(new CreatePackageCommand(vm));
                return RedirectToAction(
                    nameof(ManagePackagesController.Index));
            }
            else
                return View("Edit", vm);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(
            int id,
            [FromServices] IPackageRepository repo)
        {
            if (id == 0) return RedirectToAction(
                nameof(ManagePackagesController.Index));
            var aggregate = await repo.Get(id);
            if (aggregate == null) return RedirectToAction(
                nameof(ManagePackagesController.Index));
            var vm = new PackageFullEditViewModel(aggregate);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(
            PackageFullEditViewModel vm,
            [FromServices] ICommandHandler<UpdatePackageCommand> command)
        {
            if (ModelState.IsValid)
            {
                await command.HandleAsync(new UpdatePackageCommand(vm));
                return RedirectToAction(
                    nameof(ManagePackagesController.Index));
            }
            else
                return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(
            int id,
            [FromServices] ICommandHandler<DeletePackageCommand> command)
        {
            if (id>0)
            {
                await command.HandleAsync(new DeletePackageCommand(id));
                
            }
            return RedirectToAction(
                    nameof(ManagePackagesController.Index));
        }
    }
}
