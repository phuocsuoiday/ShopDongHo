using ShopDongHo.Models.Entities;
using ShopDongHo.Models.Repositories;
using ShopDongHo.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace ShopDongHo.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShopDbContext _context;
        private readonly ProductRepository _productRepo;
        private readonly CategoryRepository _categoryRepo;

        public ProductController()
        {
            _context = new ShopDbContext();
            _productRepo = new ProductRepository(_context);
            _categoryRepo = new CategoryRepository(_context);
        }

        public ActionResult Index(int? categoryId, string brand, decimal? minPrice, decimal? maxPrice, string search, int page = 1)
        {
            var pageSize = 12;
            var products = _productRepo.GetActiveProducts();

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(brand))
            {
                products = products.Where(p => p.Brand == brand);
            }

            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice.Value);
            }

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                products = products.Where(p =>
                    p.Name.ToLower().Contains(search) ||
                    p.Brand.ToLower().Contains(search) ||
                    (p.Description != null && p.Description.ToLower().Contains(search))
                );
            }

            var totalProducts = products.Count();
            var totalPages = (int)System.Math.Ceiling(totalProducts / (double)pageSize);

            var productList = products
                .OrderByDescending(p => p.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new ProductViewModel
            {
                Products = productList,
                Categories = _categoryRepo.GetAll().ToList(),
                Brands = _productRepo.GetAllBrands().ToList(),
                SelectedCategoryId = categoryId,
                SelectedBrand = brand,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                SearchKeyword = search,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize
            };

            ViewBag.QueryString = BuildQueryString(categoryId, brand, minPrice, maxPrice, search);

            return View(viewModel);
        }

        public ActionResult Detail(int id)
        {
            var product = _productRepo.GetProductWithCategory(id);

            if (product == null || !product.IsActive)
            {
                return HttpNotFound();
            }

            var relatedProducts = _productRepo.GetProductsByCategory(product.CategoryId)
                .Where(p => p.Id != id)
                .Take(4)
                .ToList();

            ViewBag.RelatedProducts = relatedProducts;

            return View(product);
        }

        public ActionResult Search(string keyword)
        {
            return RedirectToAction("Index", new { search = keyword });
        }

        private string BuildQueryString(int? categoryId, string brand, decimal? minPrice, decimal? maxPrice, string search)
        {
            var queryParts = new System.Collections.Generic.List<string>();

            if (categoryId.HasValue)
                queryParts.Add($"&categoryId={categoryId}");

            if (!string.IsNullOrEmpty(brand))
                queryParts.Add($"&brand={brand}");

            if (minPrice.HasValue)
                queryParts.Add($"&minPrice={minPrice}");

            if (maxPrice.HasValue)
                queryParts.Add($"&maxPrice={maxPrice}");

            if (!string.IsNullOrEmpty(search))
                queryParts.Add($"&search={search}");

            return string.Join("", queryParts);
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
