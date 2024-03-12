using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
