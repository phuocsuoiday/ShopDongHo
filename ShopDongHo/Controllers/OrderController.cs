using ShopDongHo.Models.Entities;
using ShopDongHo.Models.Repositories;
using ShopDongHo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopDongHo.Controllers
{
    public class OrderController : Controller
    {
        private readonly ShopDbContext _context;
        private readonly OrderRepository _orderRepo;
        private readonly ProductRepository _productRepo;

        public OrderController()
        {
            _context = new ShopDbContext();
            _orderRepo = new OrderRepository(_context);
            _productRepo = new ProductRepository(_context);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Cart/Checkout.cshtml", model);
            }

            var cart = Session["Cart"] as List<CartItemViewModel> ?? new List<CartItemViewModel>();

            if (!cart.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng trống.";
                return RedirectToAction("Index", "Product");
            }

            foreach (var item in cart)
            {
                var product = _productRepo.GetById(item.ProductId);
                if (product == null || !product.IsActive || product.Stock < item.Quantity)
                {
                    TempData["ErrorMessage"] = $"Sản phẩm {item.ProductName} không đủ số lượng trong kho.";
                    return RedirectToAction("Index", "Cart");
                }
            }

            var order = new Order
            {
                UserId = Session["UserId"] as int?,
                CustomerName = model.CustomerName,
                CustomerEmail = model.CustomerEmail,
                CustomerPhone = model.CustomerPhone,
                CustomerAddress = model.CustomerAddress,
                Note = model.Note,
                OrderDate = DateTime.Now,
                Status = "Pending",
                TotalAmount = cart.Sum(c => c.Price * c.Quantity)
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var item in cart)
            {
                var product = _productRepo.GetById(item.ProductId);

                var orderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price
                };

                _context.OrderDetails.Add(orderDetail);

                product.Stock -= item.Quantity;
                _productRepo.Update(product);
            }

            _context.SaveChanges();

            Session["Cart"] = new List<CartItemViewModel>();

            return RedirectToAction("Confirm", new { id = order.Id });
        }

        public ActionResult Confirm(int id)
        {
            var order = _orderRepo.GetOrderWithDetails(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            var currentUserId = Session["UserId"] as int?;
            if (order.UserId.HasValue && currentUserId != order.UserId)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        public ActionResult History()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = (int)Session["UserId"];
            var orders = _orderRepo.GetOrdersByUserId(userId);

            return View(orders);
        }

        public ActionResult Detail(int id)
        {
            var order = _orderRepo.GetOrderWithDetails(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var currentUserId = (int)Session["UserId"];
            var isAdmin = Session["Role"]?.ToString() == "Admin";
            if (order.UserId != currentUserId && !isAdmin)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
