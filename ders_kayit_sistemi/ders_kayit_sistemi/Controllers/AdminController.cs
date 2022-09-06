using Microsoft.AspNetCore.Mvc;

namespace ders_kayit_sistemi.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
