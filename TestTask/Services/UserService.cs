using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TestTask.Controllers;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services
{
    public class UserService : IUserService
    {

        public Task<User> GetUser()
        {
            return Task.Run(() =>
            {
                var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test")
        .Options;
                using ApplicationDbContext db = new(contextOptions);
                User max = db.Users.First();
                foreach (User user in db.Users)
                {
                    if (user.Orders != null && user.Orders.Count > max.Orders.Count)
                        max = user;
                }
                return max;
            });
        }


        public Task<List<User>> GetUsers()
        {
            return Task.Run(() =>
            {
                var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test").Options;
                using ApplicationDbContext db = new(contextOptions);
                var users = from user in db.Users
                            where user.Status == UserStatus.Inactive
                            select user;

                return users.ToList<User>();
            });
        }
    }
}
