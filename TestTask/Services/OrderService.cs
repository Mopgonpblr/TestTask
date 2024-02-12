using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{

    public class OrderService : IOrderService
    {
        public Task<Order> GetOrder()
        {
            return Task.Run(() =>
            {
                var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test")
        .Options;
                using ApplicationDbContext db = new(contextOptions);
                Order max = db.Orders.First();
                foreach (Order order in db.Orders)
                {
                    if (order.Price > max.Price)
                        max = order;
                }
                return max;
            });
        }

        public Task<List<Order>> GetOrders()
        {
            return Task.Run(() =>
            {

                var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test").Options;
                using ApplicationDbContext db = new(contextOptions);
                var orders = from order in db.Orders
                             where order.Quantity > 10
                             select order;

                return orders.ToList<Order>();
            });
        }
    }

}
