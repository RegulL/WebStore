using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.ViewComponents
{
    public class Brands:ViewComponent
    {
        private readonly IProductService _productService;
        public Brands(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Brands = GetBrands();
            return View(Brands);
        }

        private List<BrandViewModel> GetBrands()
        {
            var brands = _productService.GetBrands();
            var brandvm = new List<BrandViewModel>();
            foreach (var b in brands)
            {
                brandvm.Add(new BrandViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Order = b.Order,
                    Sum = _productService.GetProducts(new ProductFilter() { CategoryId = null, BrandId = b.Id }).Count()
                });
            }
            brandvm = brandvm.OrderBy(b => b.Order).ToList();
            return brandvm;
        }
    }
}
