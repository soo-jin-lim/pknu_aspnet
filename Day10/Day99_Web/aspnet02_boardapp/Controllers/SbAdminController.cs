using Microsoft.AspNetCore.Mvc;

namespace aspnet02_boardapp.Controllers
{
    public class SbAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
