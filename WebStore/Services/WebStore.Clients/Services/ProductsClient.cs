using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;
using WebStore.Interfaces;

namespace WebStore.Clients.Services
{
    public class ProductsClient : BaseClient, IProductService
    {
        public ProductsClient(IConfiguration configuration):base (configuration)
        {

        }
        protected override string ServiceAddress { get; } = "api/products";

        public IEnumerable<Brand> GetBrands()
        {
            string url = $"{ServiceAddress}/brands";
            return Get<List<Brand>>(url);
        }

        public IEnumerable<Category> GetCategories()
        {
            string url = $"{ServiceAddress}/categories";
            return Get<List<Category>>(url);
        }

        public ProductDto GetProductById(int id)
        {
            string url = $"{ServiceAddress}/{id}";
            return Get<ProductDto>(url);
        }

        public IEnumerable<ProductDto> GetProducts(ProductFilter filter)
        {
            string url = $"{ServiceAddress}";
            var response = Post(url, filter);
            return response.Content.ReadAsAsync<IEnumerable<ProductDto>>().Result;
        }
    }
}
