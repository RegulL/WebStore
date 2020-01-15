using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;

namespace WebStore.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Category> GetCategories();

        IEnumerable<Brand> GetBrands();

        IEnumerable<ProductDto> GetProducts(ProductFilter filter);

        ProductDto GetProductById(int id);

        Category GetCategoryById(int id);

        Brand GetBrandById(int id);
    }
}
