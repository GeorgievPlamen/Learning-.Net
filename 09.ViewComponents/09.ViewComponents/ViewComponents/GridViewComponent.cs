using Microsoft.AspNetCore.Mvc;
using _09.ViewComponents.Models;

namespace _09.ViewComponents.ViewComponents
{
    public class GridViewComponent : ViewComponent
    {
        public  async Task<IViewComponentResult> InvokeAsync(PersonGridModel grid)
        {
            return View(grid);
        }
    }
}
