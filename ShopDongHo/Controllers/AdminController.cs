using ShopDongHo.Filters;
using ShopDongHo.Helpers;
using ShopDongHo.Models.Entities;
using ShopDongHo.Models.Repositories;
using ShopDongHo.Models.ViewModels;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopDongHo.Controllers
{
    [AdminAuthorize]
    public class AdminController : Controller
    {
        private readonly ShopDbContext _context;
        private readonly ProductRepository _productRepo;
        private readonly CategoryRepository _categoryRepo;
        private readonly OrderRepository _orderRepo;
        private readonly UserRepository _userRepo;

        public AdminController()
        {
            _context = new ShopDbContext();
            _productRepo = new ProductRepository(_context);
            _categoryRepo = new CategoryRepository(_context);
            _orderRepo = new OrderRepository(_context);
            _userRepo = new UserRepository(_context);
        }

        public ActionResult Dashboard()
        {
            ViewBag.TotalProducts = _productRepo.Count();
            ViewBag.TotalCategories = _categoryRepo.Count();
            ViewBag.TotalOrders = _orderRepo.Count();
            ViewBag.TotalUsers = _userRepo.Count();

            var recentOrders = _orderRepo.GetRecentOrders(10);
            ViewBag.RecentOrders = recentOrders;

            return View();
        }

        public ActionResult Products(int page = 1)
        {
            var pageSize = 20;
            var products = _productRepo.GetAll()
                .OrderByDescending(p => p.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)System.Math.Ceiling(_productRepo.Count() / (double)pageSize);

            return View(products);
        }

        public ActionResult CreateProduct()
        {
            ViewBag.Categories = _categoryRepo.GetAll().ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(AdminProductViewModel model, HttpPostedFileBase imageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryRepo.GetAll().ToList();
                return View(model);
            }

            var product = new Product
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Brand = model.Brand,
                Price = model.Price,
                OriginalPrice = model.OriginalPrice,
                Stock = model.Stock,
                Description = model.Description,
                IsActive = model.IsActive
            };

            if (imageFile != null && imageFile.ContentLength > 0)
            {
                try
                {
                    product.Image = ImageHelper.SaveProductImage(imageFile);
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    ViewBag.Categories = _categoryRepo.GetAll().ToList();
                    return View(model);
                }
            }

            _productRepo.Add(product);
            TempData["SuccessMessage"] = "Thêm sản phẩm thành công!";

            return RedirectToAction("Products");
        }

        public ActionResult EditProduct(int id)
        {
            var product = _productRepo.GetById(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            var model = new AdminProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Brand = product.Brand,
                Price = product.Price,
                OriginalPrice = product.OriginalPrice,
                Image = product.Image,
                Stock = product.Stock,
                Description = product.Description,
                IsActive = product.IsActive
            };

            ViewBag.Categories = _categoryRepo.GetAll().ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(AdminProductViewModel model, HttpPostedFileBase imageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryRepo.GetAll().ToList();
                return View(model);
            }

            var product = _productRepo.GetById(model.Id);

            if (product == null)
            {
                return HttpNotFound();
            }

            product.Name = model.Name;
            product.CategoryId = model.CategoryId;
            product.Brand = model.Brand;
            product.Price = model.Price;
            product.OriginalPrice = model.OriginalPrice;
            product.Stock = model.Stock;
            product.Description = model.Description;
            product.IsActive = model.IsActive;

            if (imageFile != null && imageFile.ContentLength > 0)
            {
                try
                {
                    if (!string.IsNullOrEmpty(product.Image))
                    {
                        ImageHelper.DeleteProductImage(product.Image);
                    }
                    product.Image = ImageHelper.SaveProductImage(imageFile);
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    ViewBag.Categories = _categoryRepo.GetAll().ToList();
                    return View(model);
                }
            }

            _productRepo.Update(product);
            TempData["SuccessMessage"] = "Cập nhật sản phẩm thành công!";

            return RedirectToAction("Products");
        }

        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            var product = _productRepo.GetById(id);

            if (product == null)
            {
                return Json(new { success = false, message = "Sản phẩm không tồn tại" });
            }

            if (!string.IsNullOrEmpty(product.Image))
            {
                ImageHelper.DeleteProductImage(product.Image);
            }

            _productRepo.Delete(product);

            return Json(new { success = true, message = "Đã xóa sản phẩm" });
        }

        public ActionResult Categories()
        {
            var categories = _categoryRepo.GetAll().OrderBy(c => c.Name).ToList();
            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                TempData["ErrorMessage"] = "Tên danh mục không được để trống.";
                return RedirectToAction("Categories");
            }

            var category = new Category
            {
                Name = name.Trim(),
                Description = description?.Trim()
            };

            _categoryRepo.Add(category);
            TempData["SuccessMessage"] = "Thêm danh mục thành công!";

            return RedirectToAction("Categories");
        }

        [HttpPost]
        public ActionResult DeleteCategory(int id)
        {
            var category = _categoryRepo.GetById(id);

            if (category == null)
            {
                return Json(new { success = false, message = "Danh mục không tồn tại" });
            }

            var hasProducts = _productRepo.Any(p => p.CategoryId == id);

            if (hasProducts)
            {
                return Json(new { success = false, message = "Không thể xóa danh mục có sản phẩm" });
            }

            _categoryRepo.Delete(category);

            return Json(new { success = true, message = "Đã xóa danh mục" });
        }

        public ActionResult Orders(string status, int page = 1)
        {
            var pageSize = 20;
            var orders = string.IsNullOrEmpty(status)
                ? _orderRepo.GetAll().OrderByDescending(o => o.OrderDate)
                : _orderRepo.GetOrdersByStatus(status).OrderByDescending(o => o.OrderDate);

            var totalOrders = orders.Count();
            var orderList = orders.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)System.Math.Ceiling(totalOrders / (double)pageSize);
            ViewBag.SelectedStatus = status;

            return View(orderList);
        }

        public ActionResult OrderDetail(int id)
        {
            var order = _orderRepo.GetOrderWithDetails(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        [HttpPost]
        public ActionResult UpdateOrderStatus(int id, string status)
        {
            var order = _orderRepo.GetById(id);

            if (order == null)
            {
                return Json(new { success = false, message = "Đơn hàng không tồn tại" });
            }

            order.Status = status;
            _orderRepo.Update(order);

            return Json(new { success = true, message = "Đã cập nhật trạng thái đơn hàng" });
        }

        public ActionResult Users(int page = 1)
        {
            var pageSize = 20;
            var users = _userRepo.GetAll()
                .OrderByDescending(u => u.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)System.Math.Ceiling(_userRepo.Count() / (double)pageSize);

            return View(users);
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
