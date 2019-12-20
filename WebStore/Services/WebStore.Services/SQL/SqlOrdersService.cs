using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL;
using WebStore.DomainNew.Entities;
using Microsoft.EntityFrameworkCore;
using WebStore.Interfaces;
using WebStore.DomainNew.ViewModels;
using WebStore.DomainNew.Dto.Order;
using WebStore.DomainNew.Helper;

namespace WebStore.Services.SQL
{
    public class SqlOrdersService : IOrdersService
    {
        private readonly WebStoreContext _context;
        private readonly UserManager<User> _userManager;

        public SqlOrdersService(WebStoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public OrderDto CreateOrder(CreateOrderModel orderModel, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            using ( var trans = _context.Database.BeginTransaction())
            {
                var order = new Order()
                {
                    Address = orderModel.OrderViewModel.Address,
                    Name = orderModel.OrderViewModel.Name,
                    Date = DateTime.Now,
                    Phone = orderModel.OrderViewModel.Phone,
                    User = user
                };

                _context.Orders.Add(order);

                foreach (var item in orderModel.OrderItems)
                {
                    var product = _context.Products.FirstOrDefault(p => p.Id.Equals(item.Id));

                    if (product == null)
                        throw new InvalidOperationException(message: "Товар не найден!");

                    var OrderItem = new OrderItem()
                    {
                        Order = order,
                        Price = product.Price,
                        Quantity = item.Quantity,                       
                        Product = product
                    };

                    _context.OrderItems.Add(OrderItem);
                }
                _context.SaveChanges();
                trans.Commit();
                return GetOrderById(order.Id);
            }
        }

        public OrderDto GetOrderById(int id)
        {
            var order = _context.Orders
                .Include(navigationPropertyPath: o => o.User)
                .Include(navigationPropertyPath: o => o.OrderItems)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
                throw new ArgumentNullException(nameof(order));
            return order.OrderToDto();
        }

        public IEnumerable<OrderDto> GetUserOrders(string userName)
        {
            var list = _context.Orders
                .Include(navigationPropertyPath: o => o.User)
                .Include(navigationPropertyPath: o => o.OrderItems)
                .Where(o => o.User.UserName == userName)
                .ToList();

            return list.Select(o => o.OrderToDto()).ToList();
        }
    }
}
