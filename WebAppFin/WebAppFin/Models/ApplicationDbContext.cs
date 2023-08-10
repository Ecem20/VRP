using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAppFin.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingLists> ShoppingLists { get; set; }
        public DbSet<SelectedProducts> SelectedProducts { get; set; }
        //public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship
            modelBuilder.Entity<ShoppingLists>()
                .HasOne(sl => sl.ApplicationUser)
                .WithMany()
                .HasForeignKey(sl => sl.UserName)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
 