using Microsoft.EntityFrameworkCore;
using Orders.WebAPI.Enteties;

namespace Orders.WebAPI.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {

        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems");

            modelBuilder.Entity<Order>().HasData(
             new Order { OrderID = Guid.Parse("F4816224-70D6-4491-AC52-34F298ACE16F"), OrderNumber = "ORD001", CustomerName = "John Doe", OrderDate = DateTime.Now, TotalAmount = 66.5 },
             new Order { OrderID = Guid.Parse("735886C0-FAF3-49CA-9776-8A20B756F1CB"), OrderNumber = "ORD002", CustomerName = "Jane Smith", OrderDate = DateTime.Now, TotalAmount = 225.8 }
             );

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { OrderItemId = Guid.Parse("D20882DF-7FCA-4EE8-88BB-37D2FC75E63F"), OrderId = Guid.Parse("F4816224-70D6-4491-AC52-34F298ACE16F"), ProductName = "Product A", Quantity = 2, UnitPrice = 10.00, TotalPrice = 20.00 },
                new OrderItem { OrderItemId = Guid.Parse("2E27B6A4-469D-4D7F-8B8B-54AF129675FD"), OrderId = Guid.Parse("F4816224-70D6-4491-AC52-34F298ACE16F"), ProductName = "Product B", Quantity = 3, UnitPrice = 15.50, TotalPrice = 46.50 },
                new OrderItem { OrderItemId = Guid.Parse("24D71AC2-0A9C-4914-9FD3-13BC25D42694"), OrderId = Guid.Parse("735886C0-FAF3-49CA-9776-8A20B756F1CB"), ProductName = "Product C", Quantity = 7, UnitPrice = 25.40, TotalPrice = 25.00 },
                new OrderItem { OrderItemId = Guid.Parse("AC90B8BC-349D-43FD-87A6-6A7ED8057697"), OrderId = Guid.Parse("735886C0-FAF3-49CA-9776-8A20B756F1CB"), ProductName = "Product D", Quantity = 4, UnitPrice = 12.00, TotalPrice = 25.00 }
            );
        }
    }
}
