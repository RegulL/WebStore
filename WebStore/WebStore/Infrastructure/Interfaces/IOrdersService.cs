using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew.Entities;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IOrdersService
    {
        IEnumerable<Order> GetUserOrders(string userName);

        Order GetOrderById(int id);

        Order CreateOrder(OrderViewModel orderViewModel, CartViewModel cartViewModel, string userName);
    }
}
