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
        public Order CreateOrder(OrderViewModel orderViewModel, CartViewModel cartViewModel, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            using ( var trans = _context.Database.BeginTransaction())
            {
                var order = new Order()
                {
                    Address = orderViewModel.Address,
                    Name = orderViewModel.Name,
                    Date = DateTime.Now,
                    Phone = orderViewModel.Phone,
                    User = user
                };

                _context.Orders.Add(order);

                foreach (var item in cartViewModel.Items)
                {
                    var productVM = item.Key;
                    var product = _context.Products.FirstOrDefault(p => p.Id == productVM.Id);

                    if (product == null)
                        throw new InvalidOperationException(message: "Товар не найден!");

                    var OrderItem = new OrderItem()
                    {
                        Price = product.Price,
                        Quantity = item.Value,
                        Order = order,
                        Product = product
                    };

                    _context.OrderItems.Add(OrderItem);
                }
                _context.SaveChanges();
                trans.Commit();
                return order;
            }
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders
                .Include(navigationPropertyPath: o => o.User)
                .Include(navigationPropertyPath: o => o.OrderItems)
                .FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<Order> GetUserOrders(string userName)
        {
            return _context.Orders
                .Include(navigationPropertyPath: o => o.User)
                .Include(navigationPropertyPath: o => o.OrderItems)
                .Where(o => o.User.UserName == userName)
                .ToList();
        }
    }
}
