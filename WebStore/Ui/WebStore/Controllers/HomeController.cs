using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Filters;
using WebStore.DomainNew.ViewModels;
using WebStore.Infrastructure;
using WebStore.Interfaces;


namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IValueService _valueService;

        public HomeController(IProductService productService, IValueService valueService)
        {
            _productService = productService;
            _valueService = valueService;
        }

        [SimpleActionFilter]
        public IActionResult Index(int? categoryId, int? brandId)
        {
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
            var values = await _valueService.GetAsync();
            return View(values);
        }
        public IActionResult ContactUs()
        {
            return View();
        }
    }
}