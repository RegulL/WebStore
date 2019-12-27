using System;
using System.Collections.Generic;
using System.Text;
using WebStore.DomainNew.ViewModels;

namespace WebStore.DomainNew.Dto.Order
{
    public class CreateOrderModel
    {
        public OrderViewModel OrderViewModel { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }
}
