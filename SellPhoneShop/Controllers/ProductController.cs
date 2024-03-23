using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SellPhoneShop.Repository;

namespace SellPhoneShop.Controllers
{
    public class ProductController : Controller
    {
		private readonly DataContext _dataContext;
        public ProductController(DataContext context) {
            _dataContext = context;
        }
		public IActionResult Index()
        {
			var products = _dataContext.Products.Include("Category").Include("Brand").ToList();
			return View(products);
		}

		public async Task<IActionResult> Details(int Id)
        {  
            if (Id == null) return RedirectToAction("Index"); 
			//so sánh categoryid
			var productsById = _dataContext.Products.Where(p => p.Id == Id).FirstOrDefault();
			return View(productsById); 
        }

	}
}
