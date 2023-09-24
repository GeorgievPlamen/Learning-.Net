using Microsoft.AspNetCore.Mvc;
using IntroToModels.Models;

namespace IntroToModels.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Content("At Home page.");
        }

        [Route("/order")]
        public IActionResult Order(Order order)
        {
            if(!ModelState.IsValid) 
            {
                foreach(var model in ModelState.Values)
                {
                    foreach(var err in model.Errors)
                    {
                        if (err != null)
                        {
                            return Content(err.ErrorMessage);
                        }
                    }
                }
            }
            order.OrderNo = new Random().Next(1000);
            return Json(order.OrderNo);
        }
    }
}
