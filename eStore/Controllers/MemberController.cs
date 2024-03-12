using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
