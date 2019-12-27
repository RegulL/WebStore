using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Filters;
using WebStore.DomainNew.ViewModels;
using WebStore.Infrastructure;
using WebStore.Interfaces;


namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IProductService productService, ILogger<HomeController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [SimpleActionFilter]
        public IActionResult Index(int? categoryId, int? brandId)
        {
            //throw new Exception("Oops...");

            _logger.LogInformation(message: "Index action requsted");
            _logger.LogCritical(message: "Critical! All cats are beautiful!");
            _logger.LogError(message: "Error! All cats are beautiful!");
            _logger.LogWarning(message: "Warning! All cats are beautiful!");
            _logger.LogDebug(message: "Debug! All cats are beautiful!");

            var products = _productService.GetProducts(new ProductFilter
            { BrandId = brandId, CategoryId = categoryId });

            var model = new CatalogViewModel()
            {
                BrandId = brandId,
                CategoryId = categoryId,
                Products = products.Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Order = p.Order,
                    Price = p.Price,
                    BrandName = p.Brand?.Name ?? string.Empty
                }).OrderBy(p => p.Order).ToList()
            };

            return View(model);
        }

        public IActionResult Shop()
        {
            return View();
        }

        public IActionResult ProductDetails()
        {
            return View();
        }

        public async Task<IActionResult> Error404()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }


        public IActionResult ErrorStatus(string id)
        {
            if (id == "404")
                return RedirectToAction("NotFound");

            return Content($"Статуcный код ошибки: {id}");
        }
    }
}