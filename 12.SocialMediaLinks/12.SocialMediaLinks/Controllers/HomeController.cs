using _12.SocialMediaLinks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace _12.SocialMediaLinks.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<SocialMediaLinksOptions> _options;

        public HomeController(IOptions<SocialMediaLinksOptions> options)
        {
            _options = options;
        }

        [Route("/")]
        public IActionResult Index()
        {
            ViewBag.Links = new List<string?>()
            {
                _options.Value.Facebook,
                _options.Value.Instagram,
                _options.Value.Twitter,
                _options.Value.Youtube
            };
            return View();
        }
    }
}
