using ShopDongHo.Helpers;
using ShopDongHo.Models.Entities;
using ShopDongHo.Models.Repositories;
using ShopDongHo.Models.ViewModels;
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
            var cart = SessionHelper.GetCart();
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

            var cartItem = new CartItemViewModel
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Image = product.Image,
                Price = product.Price,
                Quantity = quantity
            };

            SessionHelper.AddToCart(cartItem);
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

            SessionHelper.UpdateCartItem(productId, quantity);

            return Json(new
            {
                success = true,
                cartTotal = SessionHelper.GetCartTotal(),
                cartItemCount = SessionHelper.GetCartItemCount()
            });
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int productId)
        {
            SessionHelper.RemoveFromCart(productId);
            TempData["SuccessMessage"] = "Đã xóa sản phẩm khỏi giỏ hàng.";

            return RedirectToAction("Index");
        }

        public ActionResult Checkout()
        {
            var cart = SessionHelper.GetCart();

            if (!cart.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng của bạn đang trống.";
                return RedirectToAction("Index", "Product");
            }

            var model = new CheckoutViewModel();

            if (AuthHelper.IsAuthenticated())
            {
                var userRepo = new UserRepository(_context);
                var user = userRepo.GetById(AuthHelper.GetCurrentUserId().Value);

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
            SessionHelper.ClearCart();
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
