using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.ViewComponents
{
    public class Categories: ViewComponent
    {
        private readonly IProductService _productService;

        public Categories(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Categories = GetCategories();
            return View(Categories);
        }

        private List<CategoryViewModel> GetCategories()
        {
            var categories = _productService.GetCategories();

            var parentSections = categories.Where(p => !p.ParentId.HasValue).ToArray();
            var parentCategories = new List<CategoryViewModel>();
            foreach (var parCat in parentSections)
            {
                parentCategories.Add(new CategoryViewModel()
                {
                    Id = parCat.Id,
                    Name = parCat.Name,
                    Order = parCat.Order,
                    ParentCategory = null
                });
            }

            foreach (var CVM in parentCategories)
            {
                var childCat = categories.Where(c => c.ParentId == CVM.Id);
                foreach (var childCategory in childCat)
                {
                    CVM.ChildCategories.Add(new CategoryViewModel()
                    {
                        Id = childCategory.Id,
                        Name = childCategory.Name,
                        Order = childCategory.Order,
                        ParentCategory = CVM
                    });
                }
                CVM.ChildCategories = CVM.ChildCategories.OrderBy(c => c.Order).ToList();
            }

            parentCategories = parentCategories.OrderBy(c => c.Order).ToList();
            return parentCategories;
        }
    }
}
