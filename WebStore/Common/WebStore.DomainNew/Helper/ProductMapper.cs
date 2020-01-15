using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.DomainNew.Dto;
using WebStore.DomainNew.Dto.Order;
using WebStore.DomainNew.Entities;

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
                Brand = p.BrandId.HasValue ? new BrandDto { Id = p.Brand.Id, Name = p.Brand.Name } : null,
                Category = new CategoryDto 
                {
                    Id = p.CategoryId,
                    Name = p.Category.Name
                }
            };

        public static OrderDto OrderToDto(this Order o) =>
            new OrderDto
            {
                Id = o.Id,
                Name = o.Name,
                Address = o.Address,
                Date = o.Date,
                Phone = o.Phone,
                OrderItems = o.OrderItems.Select(oi => new OrderItemDto() 
                {
                    Id = oi.Id,
                    Price = oi.Price,
                    Quantity = oi.Quantity
                })
            };
    }
}
