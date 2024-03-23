using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SellPhoneShop.Models;
using SellPhoneShop.Repository;

namespace SellPhoneShop.Controllers
{
	public class BrandController : Controller
	{
		private readonly DataContext _dataContext;
		public BrandController(DataContext context)
		{
			_dataContext = context;
		}
		public async Task<IActionResult> Index(string Slug = "")
		{
			BrandModel brand = _dataContext.Brands.Where(c => c.Slug == Slug).FirstOrDefault();
			if (brand == null)
			{
				//trở về trang index
				return RedirectToAction("Index");
			}
			//so sánh categoryid
			var productsByBrand = _dataContext.Products.Where(c => c.BrandId == brand.Id);
			return View(await productsByBrand.OrderByDescending(c => c.Id).ToListAsync());
		}
	}
}
