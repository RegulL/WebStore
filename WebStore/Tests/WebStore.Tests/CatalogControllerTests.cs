using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Controllers;
using Xunit;
using Moq;
using WebStore.Interfaces;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.Dto;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.ViewModels;
using System.Linq;

namespace WebStore.Tests
{
    public class CatalogControllerTests
    {
        private CatalogController controller;
        public CatalogControllerTests()
        {
            // Arrange
            var mockObject = new Mock<IProductService>();

            //ProductDto product1 = new ProductDto()
            //{
            //    Id = 1,
            //    Name = "testproduct",
            //    ImageUrl = "testproduct.bmp",
            //    Order = 0,
            //    Price = 1000,
            //    Brand = new BrandDto()
            //    {
            //        Id = 1,
            //        Name = "testbrand"
            //    }
            //};
            //ProductDto product2 = new ProductDto()
            //{
            //    Id = 2,
            //    Name = "testproduct2",
            //    ImageUrl = "testproduct2.bmp",
            //    Order = 1,
            //    Price = 2000,
            //    Brand = new BrandDto()
            //    {
            //        Id = 2,
            //        Name = "testbrand2"
            //    }
            //};
            //ProductDto product3 = new ProductDto()
            //{
            //    Id = 3,
            //    Name = "testproduct3",
            //    ImageUrl = "testproduct3.bmp",
            //    Order = 2,
            //    Price = 1500,
            //    Brand = new BrandDto()
            //    {
            //        Id = 1,
            //        Name = "testbrand"
            //    }
            //};
            //mockObject.Setup(
            //    p => p.GetProducts(new ProductFilter() { BrandId = null, CategoryId = null })
            //    ).Returns(
            //    new List<ProductDto>
            //    {
            //        product1,
            //        product2,
            //        product3
            //    });

            mockObject.Setup(
                p => p.GetProductById(1)).Returns(
                new ProductDto()
                {
                    Id = 1,
                    Name = "testproduct",
                    ImageUrl = "testproduct.bmp",
                    Order = 0,
                    Price = 1000,
                    Brand = new BrandDto()
                    {
                        Id = 1,
                        Name = "testbrand"
                    }
                });

            controller = new CatalogController(mockObject.Object);
        }

        [Fact]
        public void ProductDetails_Returns_View_With_Correct_Item()
        {
            //Arrange
            const int id = 1;
            // Act
            var result = controller.ProductDetails(id);
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(viewResult.ViewData.Model);
            Assert.Equal("testbrand", model.BrandName);
        }

        [Fact]
        public void ProductDetails_Returns_NotFound()
        {            
            // Act

            // Assert
            // ответ от контроллера должен быть типа NotFoundResult
            //Arrange
            int id = 2;
            // Act
            var result = controller.ProductDetails(id);

            // Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

        // проверяем корректную работу метода controller.Shop()
        [Fact]
        public void Shop_Method_Returns_Correct_View()
        {
          
            var result = controller.Shop(2, 1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CatalogViewModel>(viewResult.ViewData.Model);
            Assert.Equal(1, model.BrandId);
        }
    }
}
