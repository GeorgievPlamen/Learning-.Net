using IntroToControllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntroToControllers.Controllers
{
    public class BankController : Controller
    {
        BankAccount bankAccount = new BankAccount
        {
            accountNumber = 1001,
            accountHolderName = "Example Name",
            currentBalance = 5000
        };

        [Route("/")]
        public IActionResult Index()
        {
            if (HttpContext.Request.Method != "GET")
            {
                return BadRequest("Only accepts GET requests");
            }
            
            return Content("<h1>Welcome To The Best Bank</h1>","text/html");
        }

        [Route("/account-details")]
        public IActionResult AccDetails()
        {
            if (HttpContext.Request.Method != "GET")
            {
                return BadRequest("Only accepts GET requests");
            }
            
            return Json(bankAccount);
        }

        [Route("/account-statement")]
        public IActionResult AccStatement()
        {
            if (HttpContext.Request.Method != "GET")
            {
                return BadRequest("Only accepts GET requests");
            }
            return File("/dummy.pdf", "application/pdf");
        }

        [Route("/get-current-balance/{accountNumber:int?}")]
        public IActionResult GetBalanceOfAcc(int? accountNumber)
        {
            if(HttpContext.Request.Method != "GET")
            {
                return BadRequest("Only accepts GET requests");
            }

            if (accountNumber.HasValue == false)
            {
                return NotFound("Account number should be supplied");
            }

          /*  int accNumber = Convert.ToInt32(HttpContext.Request.RouteValues["accountNumber"]);*/

            if (accountNumber != 1001)
            {
                return StatusCode(400, "Account number should be 1001");
            }

            return Content($"{bankAccount.currentBalance}");
        }
    }
}
