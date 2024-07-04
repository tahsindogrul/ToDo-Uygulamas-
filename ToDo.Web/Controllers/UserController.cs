using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;
using ToDo.Business.Abstract;
using ToDo.Data;
using ToDo.Data.Repository.Absract;
using ToDo.Models;
using ToDo.Models.DTOs;
using ToDo.Models.ViewModels;

namespace ToDo.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
          
            return Json(new {data= _userService.GetAllWithTodoCount()});
        }


        [HttpPost]
        public async Task<IActionResult> Login(AppUser user)
        {
            ClaimsPrincipal? principal= _userService.CheckLogin(user);
            if (principal==null)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home");
            }
            else
            {

              
            }

        }

        //public IActionResult RecoveryPassword()
        //{
        //    return View();  
        //}

        //[HttpPost]
        //public IActionResult ShowPassword(string name)
        //{
        //    AppUser user =_context.Users.FirstOrDefault(u=>u.Name==name);
        //    TempData["error"] = $"Kullanıcı Adı{user.Name} veya Şifreniz {user.Password}";
        //    return RedirectToAction("Login", "User");
        //}


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }


        //1.yöntem

        //public IActionResult Delete(int userId)
        //{
        //     _context.Users.Remove(_context.Users.Find(userId));
        //    _context.SaveChanges();
        //    return View();
        //}


        //2.yöntem

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(AppUser appUser)
        {
            
            return Ok(_userService.Delete(appUser.Id));
        }



        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(AppUser appUser)
        {
         
            return Ok(_userService.Add(appUser));
        }

        [Authorize(Roles = "Admin")]   
        public IActionResult GetById(int id)
        {
            return Json(_userService.GetById(id));
            
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(AppUser appUser)
        {
          
            return Ok(_userService.Update(appUser));

        }

    }
}
