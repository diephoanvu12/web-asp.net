using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SellPhoneShop.Models;
using SellPhoneShop.Models.ViewModels;

namespace SellPhoneShop.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUserModel> _userManager;
        private SignInManager<AppUserModel> _signInManager;
            
        public AccountController(SignInManager<AppUserModel> signInManager, UserManager<AppUserModel> userManager) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl)
		{
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginVM)
        {   
            if(ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginVM.Username,loginVM.Password,false,false);
                if(result.Succeeded)
                {
                    return Redirect(loginVM.ReturnUrl ?? "/");
                }
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không hợp lệ!");
            }
			return View(loginVM);
		}
        
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> Register(UserModel user)
		{   
            if(ModelState.IsValid) 
            { 
                AppUserModel newUser = new AppUserModel { UserName = user.Username, Email = user.Email};
                IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
                {
                    TempData["success"] = "Tạo tài khoản thành công!";
                    return Redirect("/account/register");
                }
                foreach(IdentityError error in result.Errors) 
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
			return View(user);
		}

		public async Task<IActionResult> Logout(string returnUrl = "/")
		{
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
		}
	}
}
