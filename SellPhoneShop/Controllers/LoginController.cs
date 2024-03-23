using Microsoft.AspNetCore.Mvc;

namespace SellPhoneShop.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
