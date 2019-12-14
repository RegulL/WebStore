using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities;
using WebStore.DomainNew.Dto;

namespace WebStore.DomainNew.Helper
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(this Product p) =>
            new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Brand = p.BrandId.HasValue ? new BrandDto { Id = p.Brand.Id, Name = p.Brand.Name } : null
            };
    }
}
