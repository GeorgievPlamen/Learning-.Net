using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class StockMarketDbContext : DbContext
    {
        public StockMarketDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<BuyOrder> BuyOrders { get; set; }
        public DbSet<SellOrder> SellOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BuyOrder>().ToTable("BuyOders");   
            modelBuilder.Entity<SellOrder>().ToTable("SellOrders");

            modelBuilder.Entity<BuyOrder>().HasData(
                new BuyOrder()
                {
                    BuyOrderID = Guid.NewGuid(),
                    StockName = "microsoft",
                    StockSymbol = "MSFT",
                    DateAndTimeOfOrder = DateTime.Parse("2007-02-12"),
                    Price = 30,
                    Quantity = 10
                });

            modelBuilder.Entity<SellOrder>().HasData(
                new SellOrder()
                {
                    SellOrderID = Guid.NewGuid(),
                    StockName = "microsoft",
                    StockSymbol = "MSFT",
                    DateAndTimeOfOrder = DateTime.Parse("2007-02-12"),
                    Price = 30,
                    Quantity = 10
                });
        }
    }
}
