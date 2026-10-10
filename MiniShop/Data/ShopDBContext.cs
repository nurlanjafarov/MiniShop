using Microsoft.EntityFrameworkCore;
using MiniShop.Models;

namespace MiniShop.Data
{
    public class ShopDBContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("ShopDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>(e =>
            {
                e.Property(t => t.Name).HasMaxLength(100).IsRequired();
                e.Property(t => t.Price).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Order>(e =>
            {
                e.Property(o => o.CustomerName).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<OrderItem>(e =>
            {
                e.Property(oi => oi.Price).HasPrecision(18, 2);

                
                e.HasOne(oi => oi.Ticket)
                 .WithMany(t => t.OrderItems)
                 .HasForeignKey(oi => oi.TicketId)
                 .OnDelete(DeleteBehavior.Restrict);

                
                e.HasOne(oi => oi.Order)
                 .WithMany(o => o.Items)
                 .HasForeignKey(oi => oi.OrderId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

           
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket { Id = 1, Name = "VIP Ticket", Price = 150m, Stock = 10 },
                new Ticket { Id = 2, Name = "Standard Ticket", Price = 50m, Stock = 100 },
                new Ticket { Id = 3, Name = "Student Ticket", Price = 25m, Stock = 50 });
        }
    }
}
