using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Filters;
using WebStore.Interfaces;

namespace WebStore.Areas.Admin.Controllers
{
    [Area(areaName:"Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductList()
        {
            return View(model: _productService.GetProducts(new ProductFilter()));
        }
    }
}