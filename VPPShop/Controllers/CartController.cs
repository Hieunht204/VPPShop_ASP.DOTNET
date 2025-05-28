﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using VPPShop.Entities;
using VPPShop.Helpers;
using VPPShop.Models;
using VPPShop.Services;

namespace VPPShop.Controllers
{
    public class CartController : Controller
    {
        private readonly PaypalClient _paypalClient;
        private readonly TmdtContext _context;
        public CartController(TmdtContext context, PaypalClient paypalClient)
        {
            _paypalClient = paypalClient;
            _context = context;
        }
        public List<CartViewModel> Cart => HttpContext.Session.Get<List<CartViewModel>>(MySetttings.CART_KEY) ?? new List<CartViewModel>();

        [Authorize]
        public IActionResult Index()
        {
            return View(Cart);
        }

        [Authorize]
        public IActionResult AddToCart(string id, int quantity = 1)
        {
            var myCart = Cart;
            var cartItem = myCart.FirstOrDefault(p => p.ProductId == id);

            if (cartItem == null)
            {
                var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
                if (product == null)
                {
                    return Redirect("/404");
                }

                cartItem = new CartViewModel
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    UnitPrice = (double)product.UnitPrice,
                    Image = product.Image ?? string.Empty,
                    Quantity = quantity,
                    Discount = product.Discount
                };
                myCart.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            HttpContext.Session.Set(MySetttings.CART_KEY, myCart);
            return Json(new
            {
                success = true,
                message = "Sản phẩm đã được thêm vào giỏ hàng.",
                totalItems = myCart.Sum(p => p.Quantity)
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult UpdateQuantity(string productId, int quantity)
        {
            var myCart = Cart;
            var item = myCart.FirstOrDefault(p => p.ProductId == productId);

            if (item != null)
            {
                item.Quantity = quantity;
                HttpContext.Session.Set(MySetttings.CART_KEY, myCart);

                double itemTotal = (double)item.Total;
                double cartTotal = (double)myCart.Sum(p => p.Total);

                return Json(new
                {
                    success = true,
                    quantity = item.Quantity,
                    itemTotal = itemTotal.ToString("N0", new System.Globalization.CultureInfo("vi-VN")),
                    cartTotal = cartTotal.ToString("N0", new System.Globalization.CultureInfo("vi-VN"))
                });
            }
            return Json(new { success = false });
        }

        [Authorize]
        public IActionResult RemoveCart(string id)
        {
            var myCart = Cart;
            var cartItem = myCart.FirstOrDefault(p => p.ProductId == id);

            if (cartItem != null)
            {
                myCart.Remove(cartItem);
                HttpContext.Session.Set(MySetttings.CART_KEY, myCart);
            }
            return RedirectToAction("Index");
        }

        #region Checkout(Thanh toán)
        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            if (Cart.Count == 0)
            {
                return RedirectToAction("Index", "Product");
            }
            ViewBag.PaypalClientId = _paypalClient.ClientId;
            return View(Cart);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customerId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetttings.CLAIM_CUSTOMERID).Value;
                var customer = new Customer();
                if (model.SamePerson)
                {
                    customer = _context.Customers.SingleOrDefault(c => c.CustomerId == customerId);

                }

                var invoiceIds = _context.Invoices
                    .Where(i => i.InvoiceId.StartsWith("inv"))
                    .Select(i => i.InvoiceId)
                    .ToList();

                int maxNumber = invoiceIds
                    .Select(id =>
                    {
                        var match = Regex.Match(id, @"inv(\d+)$");
                        return match.Success && int.TryParse(match.Groups[1].Value, out int n) ? n : 0;
                    })
                    .Max();

                var invoiceId = "inv" + (maxNumber + 1);

                var invoice = new Invoice
                {
                    InvoiceId = invoiceId,
                    CustomerId = customerId,
                    Fullname = model.FullName ?? customer.Fullname,
                    Address = model.Address ?? customer.Address,
                    Phonenumber = model.PhoneNumber ?? customer.Phonenumber,
                    OrderDate = DateTime.Now,
                    PaymentMethod = "COD",
                    ShippingMethod = "",
                    StatusId = "st01",
                    Note = model.Note,
                };

                _context.Database.BeginTransaction();
                try
                {

                    _context.Database.CommitTransaction();
                    _context.Add(invoice);
                    _context.SaveChanges();

                    var invoiceDetailIds = _context.InvoiceDetails
                        .Where(i => i.InvoiceDetailId.StartsWith("ind"))
                        .Select(i => i.InvoiceDetailId)
                        .ToList();

                    int maxInvoiceDetailNumber = invoiceDetailIds
                        .Select(id =>
                        {
                            var match = Regex.Match(id, @"ind(\d+)$");
                            return match.Success && int.TryParse(match.Groups[1].Value, out int n) ? n : 0;
                        })
                        .Max();

                    var invoiceDetails = new List<InvoiceDetail>();
                    foreach (var item in Cart)
                    {
                        var invoiceDetailId = "ind" + (maxInvoiceDetailNumber + 1);
                        invoiceDetails.Add(new InvoiceDetail
                        {
                            InvoiceDetailId = invoiceDetailId,
                            InvoiceId = invoice.InvoiceId,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                            ProductId = item.ProductId,
                            Discount = item.Discount,
                        });
                    }
                    _context.InvoiceDetails.AddRange(invoiceDetails);
                    _context.SaveChanges();

                    HttpContext.Session.Set<List<CartViewModel>>(MySetttings.CART_KEY, new List<CartViewModel>());

                    return View("Success");
                }
                catch
                {

                }

                return RedirectToAction("Index", "Product");
            }
            return View(Cart);
        }
        #region Paypal(Thanh toán Paypal)
        [Authorize]
        [HttpPost("/Cart/create-paypal-order")]
        public async Task<IActionResult> CreatePaypalOrder(CancellationToken cancellationToken)
        {
            //Thông tin đơn hàng gửi qua Paypal
            var totalVND = Cart.Sum(p => p.Total).ToString();
            var exchangeRate = 25000.0;
            //var totalUSD = ((double)totalVND / exchangeRate).ToString("0.00");
            var currency = "USD";
            var referenceOrderID = "DH" + DateTime.Now.Ticks.ToString();

            try
            {
                var response = await _paypalClient.CreateOrder(totalVND, currency, referenceOrderID);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = new { ex.GetBaseException().Message };
                return BadRequest(error);
            }
        }

        [Authorize]
        [HttpPost("/Cart/capture-paypal-order")]
        public async Task<IActionResult> CapturePaypalOder(string orderID, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _paypalClient.CaptureOrder(orderID);

                // Lưu database đơn hàng
                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = new { ex.GetBaseException().Message };
                return BadRequest(error);
            }
        }

        [Authorize]
        public IActionResult PaymentSuccess()
        {
            return View("Success");
        }

        #endregion

        #endregion
    }
}
