using El_Aref.DAL.Model;
using El_Aref.PL.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Threading.Tasks;

namespace El_Aref.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region SignUp

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        //  Ahned@1234
        // Mohamedbanhawy538@gmail.com
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDTO model)
        {
            if(ModelState.IsValid)
            { 

                var user =await _userManager.FindByNameAsync(model.UserName);
                if(user == null)
                {
                    user =await _userManager.FindByEmailAsync(model.Email);
                    if(user == null)
                    {
                        user = new AppUser()
                        {
                            UserName = model.UserName,
                            FristName = model.FristName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgree

                        };

                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {

                            return RedirectToAction("SignIn");
                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                ModelState.AddModelError("", "Invalid SignUp !!");



                
            }
            return View(model);
        }




        #endregion


        #region SignIn

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var flag =await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result =await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                        if(result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index),"Home");

                        }
                    }
                }
                ModelState.AddModelError("", "Username or Email is already taken.");
            }
            return View(model);
        }






        #endregion


        #region SignOut

        #endregion
    }
}
