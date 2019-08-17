﻿using eWebShop.WebUI.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eWebShop.WebUI.Controllers.Api
{
    public class CatalogController : BaseController
    {
        private readonly ICatalogViewModelService _catalogViewModelService;

        public CatalogController(ICatalogViewModelService catalogViewModelService) => _catalogViewModelService = catalogViewModelService;

        [HttpGet]
        public async Task<IActionResult> List(int? brandFilterApplied, int? typesFilterApplied, int? page)
        {
            var itemsPage = 10;
            var catalogModel = await _catalogViewModelService.GetCatalogItems(page ?? 0, itemsPage, brandFilterApplied, typesFilterApplied);
            return Ok(catalogModel);
        }
    }
}
