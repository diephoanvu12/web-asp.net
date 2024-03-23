using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SellPhoneShop.Repository;

namespace SellPhoneShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class BrandController : Controller
	{
		private readonly DataContext _dataContext;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public BrandController(DataContext context, IWebHostEnvironment webHostEnvironment)
		{
			_dataContext = context;
			_webHostEnvironment = webHostEnvironment;
		}
		public async Task<IActionResult> Index()
		{
			return View(await _dataContext.Brands.OrderBy(p => p.Id).ToListAsync());
		}

        public IActionResult Add()
        {
            return View();
        }
    }
}
