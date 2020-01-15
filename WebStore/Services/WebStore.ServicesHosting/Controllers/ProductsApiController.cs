using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.ServicesHosting.Controllers
{
    [Route("api/products")]
    [Produces(contentType: ("application/json"))]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductService
    {
        private readonly IProductService _productService;
        public ProductsApiController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet(template: "brands")]
        public IEnumerable<Brand> GetBrands()
        {
            return _productService.GetBrands();
        }

        [HttpGet(template:"categories")]
        public IEnumerable<Category> GetCategories()
        {
            return _productService.GetCategories();
        }


        public ProductDto GetProductById(int id)
        {
            return _productService.GetProductById(id);
        }


        [HttpPost]
        [ActionName(name: "Post")]
        public IEnumerable<ProductDto> GetProducts([FromBody]ProductFilter filter)
        {
            return _productService.GetProducts(filter);
        }
    }
}