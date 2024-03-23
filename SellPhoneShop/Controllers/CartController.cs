using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using SellPhoneShop.Models;
using SellPhoneShop.Models.ViewModels;
using SellPhoneShop.Repository;
using System.Collections.Generic;

namespace SellPhoneShop.Controllers
{
	public class CartController : Controller
	{
		private readonly DataContext _dataContext;
		public CartController(DataContext _context)
		{
			_dataContext = _context;
		}

		public IActionResult Index()
		{
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemViewModel cartVM = new()
			{
				CartItems = cartItems,
				GrandTotal = cartItems.Sum(x => x.Quantity * x.Price)
			};
 			return View(cartVM);
		}

		public ActionResult Checkout()
		{
			return View("~/Views/Checkout/Index.cshtml");
		}

		public async Task<IActionResult> Add(int Id)
		{
			ProductModel product = await _dataContext.Products.FindAsync(Id);
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemModel cartItems = cart.Where(c => c.ProductId == Id).FirstOrDefault();

			if (cartItems == null)
			{
				cart.Add(new CartItemModel(product));
			}
			else
			{
				cartItems.Quantity += 1;
			}
			//lưu trữ dữ liệu cart vào session
			HttpContext.Session.SetJson("Cart", cart);

			//thông báo 
			TempData["success"] = "Thêm vào giỏ hàng thành công!";

			//trả về trang hiện tại
			return Redirect(Request.Headers["Referer"].ToString());
		}

		public async Task<IActionResult> Increase(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemModel cartItems = cart.Where(c => c.ProductId == Id).FirstOrDefault();

			if (cartItems.Quantity >= 1)
			{
				++cartItems.Quantity;
			}
			else
			{
				cart.RemoveAll(p => p.ProductId == Id);
			}

			if (cart.Count == 0)
			{
				//xóa session Cart
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				//lưu trữ dữ liệu cart vào session
				HttpContext.Session.SetJson("Cart", cart);
			}
			//trả về trang index
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Decrease(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemModel cartItems = cart.Where(c=>c.ProductId == Id).FirstOrDefault();

			if(cartItems.Quantity >1) 
			{
				--cartItems.Quantity;
			}
			else 
			{
				cart.RemoveAll(p => p.ProductId == Id);
			}

			if(cart.Count == 0) 
			{
				//xóa session Cart
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				//lưu trữ dữ liệu cart vào session
				HttpContext.Session.SetJson("Cart", cart);
			}
			//trả về trang index
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Remove(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			
			cart.RemoveAll(p=>p.ProductId == Id);

			if(cart.Count == 0)
			{
				//xóa session Cart
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				//lưu trữ dữ liệu cart vào session
				HttpContext.Session.SetJson("Cart", cart);
			}
            //thông báo 
            TempData["success"] = "Xóa sản phẩm thành công!";
            //trả về trang index
            return RedirectToAction("Index");
		}

		public async Task<IActionResult> Clear()
		{
			//xóa session Cart
			HttpContext.Session.Remove("Cart");
            //thông báo 
            TempData["success"] = "Xóa giỏ hàng thành công!";
            //trả về trang index
            return RedirectToAction("Index");
		}
	}
}
