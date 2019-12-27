using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebStore.DomainNew.Dto.Order;
using WebStore.Interfaces;

namespace WebStore.Clients.Services
{
    public class OrdersClient:BaseClient, IOrdersService
    {
        protected override string ServiceAddress { get; } = "api/orders";
        public OrdersClient(IConfiguration configuration):base(configuration)
        {

        }

        public IEnumerable<OrderDto> GetUserOrders(string userName)
        {
            var url = $"{ServiceAddress}/user/{userName}";
            var result = Get<List<OrderDto>>(url);
            return result;  
        }

        public OrderDto GetOrderById(int id)
        {
            var url = $"{ServiceAddress}/{id}";
            var result = Get<OrderDto>(url);
            return result;
        }

        public OrderDto CreateOrder(CreateOrderModel orderModel, string userName)
        {
            var url = $"{ServiceAddress}/{userName}";
            var response = Post(url, orderModel);
            var result = response.Content.ReadAsAsync<OrderDto>().Result;
            return result;
        }
    }
}
