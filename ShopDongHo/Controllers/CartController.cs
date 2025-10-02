using ShopDongHo.Models.Entities;
using ShopDongHo.Models.Repositories;
using ShopDongHo.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopDongHo.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopDbContext _context;
        private readonly ProductRepository _productRepo;

        public CartController()
        {
            _context = new ShopDbContext();
            _productRepo = new ProductRepository(_context);
        }

        public ActionResult Index()
        {
            var cart = Session["Cart"] as List<CartItemViewModel> ?? new List<CartItemViewModel>();
            return View(cart);
        }

        [HttpPost]
        public ActionResult AddToCart(int id, int quantity = 1)
        {
            var product = _productRepo.GetById(id);

            if (product == null || !product.IsActive)
            {
                TempData["ErrorMessage"] = "Sản phẩm không tồn tại hoặc đã ngừng kinh doanh.";
                return RedirectToAction("Index", "Product");
            }

            if (product.Stock < quantity)
            {
                TempData["ErrorMessage"] = "Số lượng sản phẩm trong kho không đủ.";
                return RedirectToAction("Detail", "Product", new { id });
            }

            var cart = Session["Cart"] as List<CartItemViewModel> ?? new List<CartItemViewModel>();
            var existingItem = cart.FirstOrDefault(c => c.ProductId == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItemViewModel
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Image = product.Image,
                    Price = product.Price,
                    Quantity = quantity
                });
            }

            Session["Cart"] = cart;
            TempData["SuccessMessage"] = $"Đã thêm {product.Name} vào giỏ hàng.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateCart(int productId, int quantity)
        {
            if (quantity <= 0)
            {
                return Json(new { success = false, message = "Số lượng không hợp lệ" });
            }

            var product = _productRepo.GetById(productId);
            if (product != null && product.Stock < quantity)
            {
                return Json(new { success = false, message = "Số lượng vượt quá tồn kho" });
            }

            var cart = Session["Cart"] as List<CartItemViewModel> ?? new List<CartItemViewModel>();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);

            if (item != null)
            {
                item.Quantity = quantity;
            }

            Session["Cart"] = cart;

            return Json(new
            {
                success = true,
                cartTotal = cart.Sum(c => c.Price * c.Quantity),
                cartItemCount = cart.Sum(c => c.Quantity)
            });
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int productId)
        {
            var cart = Session["Cart"] as List<CartItemViewModel> ?? new List<CartItemViewModel>();
            cart.RemoveAll(c => c.ProductId == productId);
            Session["Cart"] = cart;

            TempData["SuccessMessage"] = "Đã xóa sản phẩm khỏi giỏ hàng.";

            return RedirectToAction("Index");
        }

        public ActionResult Checkout()
        {
            var cart = Session["Cart"] as List<CartItemViewModel> ?? new List<CartItemViewModel>();

            if (!cart.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng của bạn đang trống.";
                return RedirectToAction("Index", "Product");
            }

            var model = new CheckoutViewModel();

            if (Session["UserId"] != null)
            {
                var userRepo = new UserRepository(_context);
                var user = userRepo.GetById((int)Session["UserId"]);

                if (user != null)
                {
                    model.CustomerName = user.FullName ?? user.Username;
                    model.CustomerEmail = user.Email;
                    model.CustomerPhone = user.Phone;
                    model.CustomerAddress = user.Address;
                }
            }

            return View(model);
        }

        public ActionResult ClearCart()
        {
            Session["Cart"] = new List<CartItemViewModel>();
            TempData["SuccessMessage"] = "Đã xóa tất cả sản phẩm trong giỏ hàng.";
            return RedirectToAction("Index");
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
