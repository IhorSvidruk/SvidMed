﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SvidMed.Services.Data.Specializations;
using SvidMed.Web.ViewModels.Administration.SpecializationViewModels;

namespace SvidMed.Web.Areas.Administration.Controllers
{
    public class SpecializationController : AdministrationController
    {
        private readonly ISpecializationService _specializationService;

        public SpecializationController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var viewModel = await _specializationService.GetAllAsync<SpecializationViewModel>();

            return View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(SpecializationCreateFormModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            await _specializationService.CreateAsync(input);

            return Redirect(nameof(All));
        }

        [Authorize]
        public async Task<IActionResult> Delete(int specializationId) // id is pageNumber
        {
            var checkIfSpecExists = await _specializationService.DeleteAsync(specializationId);

            if (checkIfSpecExists == false)
            {
                return new StatusCodeResult(404);
            }

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public async Task<IActionResult> Edit(int specializationId)
        {
            var viewModel = await _specializationService.GetSpecializationByIdAsync<SpecializationEditFormModel>(specializationId);

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(SpecializationEditFormModel input)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit), new { input.Id });
            }

            var checkIfSpecExists = await _specializationService.EditAsync(input.Id, input.Name, input.Description);

            if (checkIfSpecExists == false)
            {
                return new StatusCodeResult(404);
            }

            return RedirectToAction(nameof(All));
        }
    }
}
