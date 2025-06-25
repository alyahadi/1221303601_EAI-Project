using AandN_Website.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace AandN_Website.Controllers
{
    public class CartController : Controller
    {
        // Simple example: use session
        public ActionResult Index()
        {
            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            return View(cart);
        }

        [System.Web.Http.HttpPost]
        public ActionResult AddToCart(string productId, string name, decimal price, int quantity)
        {
            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            cart.Add(new CartItem
            {
                ProductID = productId,
                Name = name,
                Price = price,
                Quantity = quantity
            });
            Session["Cart"] = cart;
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int index)
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart != null && index >= 0 && index < cart.Count)
                cart.RemoveAt(index);

            return RedirectToAction("Index");
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Checkout()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var client = new IntegrationServiceRef.IntegrationServiceSoapClient();

            var order = new IntegrationServiceRef.OrderDto
            {
                CustomerName = model.CustomerName,
                PhoneNumber = model.PhoneNumber,
                BillingAddress = model.BillingAddress,
                ShippingAddress = model.ShippingAddress,
                PromotionCode = model.PromotionCode,
                PaymentMethod = model.PaymentMethod,
                CartTotal = model.UnitPrice * model.Quantity,
                Items = new[]
                {
                new IntegrationServiceRef.OrderItemDto
                {
                    ProductID = model.ProductID,
                    Quantity = model.Quantity,
                    UnitPrice = model.UnitPrice
                }
            }
            };

            var confirmation = client.SubmitOrder(order);
            return View("OrderConfirmation", confirmation);
        }


    }

    public class CartItem
    {
        public string ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

}