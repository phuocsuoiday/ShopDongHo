using ShopDongHo.Filters;
using ShopDongHo.Helpers;
using ShopDongHo.Models.Entities;
using ShopDongHo.Models.Repositories;
using ShopDongHo.Models.ViewModels;
using System;
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

            var cart = SessionHelper.GetCart();

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
                UserId = AuthHelper.GetCurrentUserId(),
                CustomerName = model.CustomerName,
                CustomerEmail = model.CustomerEmail,
                CustomerPhone = model.CustomerPhone,
                CustomerAddress = model.CustomerAddress,
                Note = model.Note,
                OrderDate = DateTime.Now,
                Status = "Pending",
                TotalAmount = SessionHelper.GetCartTotal()
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

            SessionHelper.ClearCart();

            return RedirectToAction("Confirm", new { id = order.Id });
        }

        public ActionResult Confirm(int id)
        {
            var order = _orderRepo.GetOrderWithDetails(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            var currentUserId = AuthHelper.GetCurrentUserId();
            if (order.UserId.HasValue && currentUserId != order.UserId)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        [AuthorizeRole]
        public ActionResult History()
        {
            var userId = AuthHelper.GetCurrentUserId().Value;
            var orders = _orderRepo.GetOrdersByUserId(userId);

            return View(orders);
        }

        [AuthorizeRole]
        public ActionResult Detail(int id)
        {
            var order = _orderRepo.GetOrderWithDetails(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            var currentUserId = AuthHelper.GetCurrentUserId().Value;
            if (order.UserId != currentUserId && !AuthHelper.IsAdmin())
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
