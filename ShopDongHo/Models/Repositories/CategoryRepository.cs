using ShopDongHo.Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ShopDongHo.Models.Repositories
{
    public class CategoryRepository : Repository<Category>
    {
        public CategoryRepository(ShopDbContext context) : base(context)
        {
        }

        public IEnumerable<Category> GetCategoriesWithProducts()
        {
            return _dbSet.Include(c => c.Products)
                        .OrderBy(c => c.Name)
                        .ToList();
        }

        public Category GetCategoryWithProducts(int id)
        {
            return _dbSet.Include(c => c.Products)
                        .FirstOrDefault(c => c.Id == id);
        }
    }
}
