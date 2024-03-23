using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SellPhoneShop.Models;
using SellPhoneShop.Repository;

namespace SellPhoneShop.Controllers
{
	public class CategoryController : Controller
	{
		private readonly DataContext _dataContext;
		public CategoryController(DataContext context)
		{
			_dataContext = context;
		}
		public async Task<IActionResult> Index(string Slug="")
		{	
			CategoryModel category = _dataContext.Categories.Where(c=>c.Slug == Slug).FirstOrDefault();
			if (category == null)
			{	
				//trở về trang index
				return RedirectToAction("Index");
			}
			//so sánh categoryid
			var productsByCategory = _dataContext.Products.Where(c=>c.CategoryId==category.Id);
			return View(await productsByCategory.OrderByDescending(c=>c.Id).ToListAsync());
		}
	}
}
