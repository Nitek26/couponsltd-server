using CouponsLtd.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CouponsLtd.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options
           ) : base(options)
        {
        }

        public DbSet<CouponDAO> Coupons { get; set; }
        public DbSet<UserDAO> Users { get; set; }
    }
}
