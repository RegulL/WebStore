using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.Helper;
using WebStore.Interfaces;

namespace WebStore.Services.SQL
{
    public class SQLProductService : IProductService
    {
        private readonly WebStoreContext _context;
        public SQLProductService(WebStoreContext context)
        {
            _context = context;
        }

       

        public IEnumerable<Brand> GetBrands()
        {
            return _context.Brands.ToList();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
        
        public Brand GetBrandById(int id)       
        {
            return _context.Brands.FirstOrDefault(b => b.Id == id);            
        }
        
        public Category GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public ProductDto GetProductById(int id)
        {
            var product = _context.Products
                .Include(navigationPropertyPath: p => p.Category)
                .Include(navigationPropertyPath: p => p.Brand)
                .FirstOrDefault(p => p.Id == id);
            if (product == null) 
            {
                return null;
            }
            return product.ToDto();
        }

        public IEnumerable<ProductDto> GetProducts(ProductFilter filter)
        {
            var query = _context.Products
                .Include(navigationPropertyPath: p => p.Category)
                .Include(navigationPropertyPath: p => p.Brand)
                .AsQueryable();
            if (filter.BrandId.HasValue)
                query = query.Where(c => c.BrandId.HasValue && c.BrandId.Value.Equals(filter.BrandId.Value));
            if (filter.CategoryId.HasValue)
                query = query.Where(c => c.CategoryId.Equals(filter.CategoryId.Value));
            return query.Select(q => q.ToDto()).ToList();
        }

    }
}
