using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using WebStore.DomainNew.Filters;
using WebStore.Interfaces;

namespace WebStore.Controllers
{
    public class SitemapController : ControllerBase
    {
        private readonly IProductService _productService;

        public SitemapController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index() 
        {
            var nodes = new List<SitemapNode>
            {
                new SitemapNode(Url.Action("Index", controller:"Home")),
                new SitemapNode(Url.Action("Shop", controller:"Catalog")),
                new SitemapNode(Url.Action("BlogSingle", controller:"Home")),
                new SitemapNode(Url.Action("Blog", controller:"Home")),
                new SitemapNode(Url.Action("ContactUs", controller:"Home"))
            };


            var sections = _productService.GetCategories();
            foreach (var section in sections)
            {
                if (section.ParentId.HasValue)
                    nodes.Add(new SitemapNode(Url.Action(
                        "Shop",
                        "Catalog",
                        new { sectionId = section.Id })));
            }

            var brands = _productService.GetBrands();
            foreach (var brand in brands)
            {
                nodes.Add(new SitemapNode(Url.Action(
                    "Shop",
                    "Catalog",
                    new { brandId = brand.Id })));
            }

            var products = _productService.GetProducts(new ProductFilter());
            foreach (var productDto in products)
            {
                nodes.Add(new SitemapNode(Url.Action(
                    "ProductDetails",
                    "Catalog",
                    new { id = productDto.Id })));
            }

            return new SitemapProvider()
                .CreateSitemap(new SitemapModel(nodes));
        }

    }
}