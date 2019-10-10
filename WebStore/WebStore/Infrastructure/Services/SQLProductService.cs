﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL;
using WebStore.Domain.Entities;
using WebStore.Domain.Filters;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services
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

        public Product GetProductById(int id)
        {
            return _context.Products
                .Include(navigationPropertyPath: p => p.Category)
                .Include(navigationPropertyPath: p => p.Brand)
                .FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetProducts(ProductFilter filter)
        {
            var query = _context.Products
                .Include(navigationPropertyPath: p => p.Category)
                .Include(navigationPropertyPath: p => p.Brand)
                .AsQueryable();
            if (filter.BrandId.HasValue)
                query = query.Where(c => c.BrandId.HasValue && c.BrandId.Value.Equals(filter.BrandId.Value));
            if (filter.CategoryId.HasValue)
                query = query.Where(c => c.CategoryId.Equals(filter.CategoryId.Value));
            return query.ToList();
        }
    }
}
