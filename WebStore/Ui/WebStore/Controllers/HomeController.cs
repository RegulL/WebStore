using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.ViewModels;
using WebStore.Infrastructure;
using WebStore.Interfaces;


namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IValueService _valueService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(/*IProductService productService,*/ IValueService valueService, ILogger<HomeController> logger)
        {
            //_productService = productService;
            _valueService = valueService;
            _logger = logger;
        }

        [SimpleActionFilter]
        public async Task<IActionResult> Index(/*int? categoryId, int? brandId*/)
        {
            _logger?.LogInformation(message: "Index action requsted");
            _logger?.LogCritical(message: "Critical! All cats are beautiful!");
            _logger?.LogError(message: "Error! All cats are beautiful!");
            _logger?.LogWarning(message: "Warning! All cats are beautiful!");
            _logger?.LogDebug(message: "Debug! All cats are beautiful!");

            var values = await _valueService.GetAsync();

            #region Products filtred and show
            //var products = _productService.GetProducts(new ProductFilter
            //{ 
            //    BrandId = brandId, 
            //    CategoryId = categoryId 
            //});

            //var model = new CatalogViewModel()
            //{
            //    BrandId = brandId,
            //    CategoryId = categoryId,
            //    Products = products.Select(p => new ProductViewModel()
            //    {
            //        Id = p.Id,
            //        ImageUrl = p.ImageUrl,
            //        Name = p.Name,
            //        Order = p.Order,
            //        Price = p.Price,
            //        BrandName = p.Brand?.Name ?? string.Empty
            //    }).OrderBy(p => p.Order).ToList()
            //};
            #endregion


            return View(values);
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

        public IActionResult CheckOut()
        {
            return View();
        }

        public IActionResult ErrorStatus(string id)
        {
            if (id == "404")
                return RedirectToAction("Error404");

            return Content($"Статуcный код ошибки: {id}");
        }
    }
}