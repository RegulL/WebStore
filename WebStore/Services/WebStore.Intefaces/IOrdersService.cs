using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew.Dto.Order;
using WebStore.DomainNew.Entities;
using WebStore.DomainNew.ViewModels;

namespace WebStore.Interfaces
{
    public interface IOrdersService
    {
        IEnumerable<OrderDto> GetUserOrders(string userName);

        OrderDto GetOrderById(int id);

        OrderDto CreateOrder(CreateOrderModel orderModel, string userName);
    }
}
