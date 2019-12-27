using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using WebStore.Controllers;
using WebStore.DomainNew.Dto.Order;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;
using Xunit;

namespace WebStore.Tests
{
    public class CartControllerTests
    {
        Mock<ICartService> mockCartService;
        Mock<IOrdersService> mockOrdersService;
        CartController controller;
        public CartControllerTests()
        {
            mockCartService = new Mock<ICartService>();
            mockOrdersService = new Mock<IOrdersService>();
            controller = new CartController(mockCartService.Object, mockOrdersService.Object);
        }

        [Fact]
        public void CheckOut_ModelState_Invalid_Returns_ViewModel() 
        {
            controller.ModelState.AddModelError(key: "Error", errorMessage: "InvalidModel");

            var result = controller.CheckOut(new OrderViewModel { Name = "test" });

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<OrderDetailsViewModel>(viewResult.ViewData.Model);
            Assert.Equal("test", model.OrderViewModel.Name);
        }

        [Fact]
        public void CheckOut_Calls_Service_And_Return_Redirect()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
            }));

            mockCartService
                .Setup(c => c.TransformCart())
                .Returns(new CartViewModel
                {
                    Items = new Dictionary<ProductViewModel, int>()
                    {
                        { new ProductViewModel(), 1 }
                    }
                });

            // setting up ordersService
            mockOrdersService
                .Setup(c => c.CreateOrder(
                    It.IsAny<CreateOrderModel>(),
                    It.IsAny<string>()))
                .Returns(new OrderDto { Id = 1 });

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            var result = controller.CheckOut(new OrderViewModel
            {
                Name = "test",
                Address = "",
                Phone = ""
            });


            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectResult.ControllerName);
            Assert.Equal("OrderConfirmed", redirectResult.ActionName);
            Assert.Equal(1, redirectResult.RouteValues["id"]);
        }

    }
}
