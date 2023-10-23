﻿using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace CRUD.Controllers
{
    [Route("[controller]")]
    public class CountriesController : Controller
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        [Route("UploadFromExcel")]
        public IActionResult UploadFromExcel()
        {
            return View();
        }
        [HttpPost]
        [Route("UploadFromExcel")]

        public async Task<IActionResult> UploadFromExcel(IFormFile excelFile)
        {
            if (excelFile == null || excelFile.Length == 0)
            {
                ViewBag.ErrorMessage = "Please select an xlsx file";
                return View();
            }

            if (Path.GetExtension(excelFile.FileName).Equals(".xslsx", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.ErrorMessage = "Please select an xlsx file";
                return View();
            }

            int countriesInserted = await _countriesService.UploadCountriesFromExcelFile(excelFile);

            ViewBag.Message = $"{countriesInserted} Countries Uploaded";
            return View();
        }
    }
}
