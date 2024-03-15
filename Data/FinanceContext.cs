using api.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class FinanceContext(DbContextOptions<FinanceContext> options)
        : IdentityDbContext<AppUser>(options)
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portifolio> Portifolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Portifolio>()
                .HasKey(Portifolio => new { Portifolio.UserId, Portifolio.StockId });
            builder
                .Entity<Portifolio>()
                .HasOne(Portifolio => Portifolio.user)
                .WithMany(user => user.Portifolios)
                .HasForeignKey(Portifolio => Portifolio.UserId);
            builder
                .Entity<Portifolio>()
                .HasOne(Portifolio => Portifolio.Stock)
                .WithMany(Stock => Stock.Portifolios)
                .HasForeignKey(Portifolio => Portifolio.StockId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "user", NormalizedName = "USER" }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
