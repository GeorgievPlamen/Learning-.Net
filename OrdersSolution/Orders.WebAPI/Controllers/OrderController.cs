using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.WebAPI.AppDbContext;
using Orders.WebAPI.Enteties;
using Orders.WebAPI.Enteties.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace Orders.WebAPI.Controllers
{
    public class OrderController : CustomerBaseController
    {
        private readonly ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            var orders = await _db.Orders.ToListAsync();
            return orders;
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetOrder(Guid orderId)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(temp =>
            temp.OrderId == orderId);

            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder(Order order)
        {
            await _db.Orders.AddAsync(order);
            _db.SaveChanges();
            return order;
        }


        /*public Guid OrderId { get; set; }
        public string? OrderNumber { get; set; }
        public string? CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }*/
    }
}
