using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;


namespace WebStore.ViewComponents
{
    public class Categories: ViewComponent
    {
        private readonly IProductService _productService;

        public Categories(IProductService productService)
        {
            _productService = productService;
        }


        public async Task<IViewComponentResult> InvokeAsync(string sectionId)
        {
            int.TryParse(sectionId, out var sectionIdInt);

            var sections = GetCategory(sectionIdInt, out var parentSectionId);
            return View(new CategoryCompleteViewModel
            {
                Categories = sections,
                CurrentCategoryId = sectionIdInt,
                CurrentParentCategoryId = parentSectionId
            });

        }

        private List<CategoryViewModel> GetCategory(int? sectionId, out int? parentSectionId)
        {
            parentSectionId = null;

            var categories = _productService.GetCategories();

            var parentCategories = categories.Where(x => !x.ParentId.HasValue).ToArray();
            var parentSections = new List<CategoryViewModel>();
            foreach (var parentCategory in parentCategories)
            {
                parentSections.Add(new CategoryViewModel()
                {
                    Id = parentCategory.Id,
                    Name = parentCategory.Name,
                    Order = parentCategory.Order,
                    ParentCategory = null
                });
            }
            foreach (var sectionViewModel in parentSections)
            {
                var childCategories = categories.Where(c => c.ParentId == sectionViewModel.Id);
                foreach (var childCategory in childCategories)
                {
                    
                    if (childCategory.Id == sectionId)
                        parentSectionId = sectionViewModel.Id;

                    sectionViewModel.ChildCategories.Add(new CategoryViewModel()
                    {
                        Id = childCategory.Id,
                        Name = childCategory.Name,
                        Order = childCategory.Order,
                        ParentCategory = sectionViewModel
                    });
                }

                sectionViewModel.ChildCategories = sectionViewModel.ChildCategories
                    .OrderBy(c => c.Order)
                    .ToList();
            }

            parentSections = parentSections.OrderBy(c => c.Order).ToList();

            return parentSections;
        }
    }
}
