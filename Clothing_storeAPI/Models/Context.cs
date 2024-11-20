using BTL_ASP.NetCore.Areas.Admin.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Clothing_storeAPI.Models
{
    public class Context : DbContext
    {
        public Context() { }
        public Context(DbContextOptions<Context> options)
            : base(options)
        {

        }
        //khai báo các thuộc tính map với bảng
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

      
       
    }

}
