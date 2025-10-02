using ShopDongHo.Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ShopDongHo.Models.Repositories
{
    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(ShopDbContext context) : base(context)
        {
        }

        public IEnumerable<Order> GetOrdersByUserId(int userId)
        {
            return _dbSet.Include(o => o.OrderDetails)
                        .Where(o => o.UserId == userId)
                        .OrderByDescending(o => o.OrderDate)
                        .ToList();
        }

        public Order GetOrderWithDetails(int id)
        {
            return _dbSet.Include(o => o.OrderDetails.Select(od => od.Product))
                        .Include(o => o.User)
                        .FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<Order> GetRecentOrders(int count = 10)
        {
            return _dbSet.Include(o => o.OrderDetails)
                        .OrderByDescending(o => o.OrderDate)
                        .Take(count)
                        .ToList();
        }

        public IEnumerable<Order> GetOrdersByStatus(string status)
        {
            return _dbSet.Include(o => o.OrderDetails)
                        .Where(o => o.Status == status)
                        .OrderByDescending(o => o.OrderDate)
                        .ToList();
        }
    }
}
