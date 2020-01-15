using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Controllers;
using WebStore.Interfaces;
using Xunit;

namespace WebStore.Tests
{
    public class HomeControllerTests
    {
        //A-A-A
        //Arrange
        //Act
        //Assert

        private HomeController controller;

        public HomeControllerTests()
        {
            var mockService = new Mock<IValueService>();
            mockService.Setup(
                c => c.GetAsync()
                ).ReturnsAsync(new List<string> { "1", "2" });
            
            controller = new HomeController(null, mockService.Object, null);
            
        }

        //[Fact]
        //public async Task Index_Method_Returns_View_With_Values()
        //{
        //    var result = await controller.Index();

        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsAssignableFrom<IEnumerable<string>>(viewResult.ViewData.Model);
        //    Assert.Equal(2, model.Count());
        //}

        [Fact]
        public void ContactUs_Returns_View() 
        {
            var result = controller.ContactUs();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Error_404_Redirect_to_Error404() 
        {
            var result = controller.ErrorStatus(id: "404");

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirect.ControllerName);
            Assert.Equal("Error404", redirect.ActionName);
        }

        [Fact]
        public void ErrorStatus_Another_Returns_Content_Result() 
        {
            var result = controller.ErrorStatus(id: "500");

            var contentResult = Assert.IsType<ContentResult>(result);
            Assert.Equal("Статуcный код ошибки: 500", contentResult.Content);
        }

        [Fact]
        public void Checkout_Returns_View() 
        {
            var result = controller.CheckOut();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Error_Returns_View()
        {
            var result = controller.Error();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ProductDetails_Returns_View()
        {
            var result = controller.ProductDetails();
            Assert.IsType<ViewResult>(result);
        }

    }
}
