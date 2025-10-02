using ShopDongHo.Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ShopDongHo.Models.Repositories
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(ShopDbContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetActiveProducts()
        {
            return _dbSet.Include(p => p.Category)
                        .Where(p => p.IsActive)
                        .OrderByDescending(p => p.CreatedDate)
                        .ToList();
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return _dbSet.Include(p => p.Category)
                        .Where(p => p.IsActive && p.CategoryId == categoryId)
                        .OrderByDescending(p => p.CreatedDate)
                        .ToList();
        }

        public IEnumerable<Product> SearchProducts(string keyword)
        {
            keyword = keyword?.ToLower() ?? "";
            return _dbSet.Include(p => p.Category)
                        .Where(p => p.IsActive &&
                                   (p.Name.ToLower().Contains(keyword) ||
                                    p.Brand.ToLower().Contains(keyword) ||
                                    p.Description.ToLower().Contains(keyword)))
                        .OrderByDescending(p => p.CreatedDate)
                        .ToList();
        }

        public IEnumerable<string> GetAllBrands()
        {
            return _dbSet.Where(p => p.IsActive && !string.IsNullOrEmpty(p.Brand))
                        .Select(p => p.Brand)
                        .Distinct()
                        .OrderBy(b => b)
                        .ToList();
        }

        public Product GetProductWithCategory(int id)
        {
            return _dbSet.Include(p => p.Category)
                        .FirstOrDefault(p => p.Id == id);
        }
    }
}
