using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew.Filters;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.Services
{
    public class CartService : ICartService
    {
        private readonly ICartStore cartStore;
        private IProductService ProductService { get; }

        public CartService(IProductService productService, ICartStore cartstore)
        {
            cartStore = cartstore;
            ProductService = productService;
        }

        public void DecrementFromCart(int id)
        {
            var cart = cartStore.Cart;

            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                if (item.Quantity > 0)
                    item.Quantity--;

                if (item.Quantity == 0)
                    cart.Items.Remove(item);
            }

            cartStore.Cart = cart;
        }

        public void RemoveFromCart(int id)
        {
            var cart = cartStore.Cart;

            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                cart.Items.Remove(item);
            }

            cartStore.Cart = cart;
        }

        public void RemoveAll()
        {
            cartStore.Cart.Items.Clear();
        }

        public void AddToCart(int id)
        {
            var cart = cartStore.Cart;

            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                cart.Items.Add(new CartItem() { ProductId = id, Quantity = 1 });
            }

            cartStore.Cart = cart;
        }

        public CartViewModel TransformCart()
        {
            var products = ProductService.GetProducts(new ProductFilter()
            {
                Ids = cartStore.Cart.Items.Select(i => i.ProductId).ToList()
            }).Select(p => new ProductViewModel()
            {
                Id = p.Id,
                ImageUrl = p.ImageUrl,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                BrandName = p.Brand != null ? p.Brand.Name : string.Empty
            }).ToList();

            var r = new CartViewModel
            {
                Items = cartStore.Cart.Items.ToDictionary(
                    x => products.First(y => y.Id == x.ProductId),
                    x => x.Quantity)
            };

            return r;

        }
    }
}
