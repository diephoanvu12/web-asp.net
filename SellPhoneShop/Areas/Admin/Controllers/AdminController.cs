using Microsoft.AspNetCore.Mvc;

namespace SellPhoneShop.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
