using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.Data;
using ToDo.Models;
using ToDo.Models.ViewModels;

namespace ToDo.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
           

          var result= _context.Users.Select(u => new
            {
                u.Id,
                u.Name,
                u.IsAdmin,
                TotalTodo=u.Todos.Count    

            }).ToList();
            return Json(new {data=result});
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginWm user)
        {
            AppUser appUser=_context.Users.FirstOrDefault(u=>u.Password==user.Password && u.Name==user.Name);
            if (appUser==null)
            {
                TempData["error"] = "Kullanıcı Adı veya Şifreniz Yanlıştır";
                return View();
            }
            else
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.GivenName, appUser.Name));
                claims.Add(new Claim(ClaimTypes.Role, appUser.IsAdmin ? "Admin" : "User"));

                ClaimsIdentity identity = new ClaimsIdentity
                  (claims,CookieAuthenticationDefaults.AuthenticationScheme);

               await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent=user.IsPersistent});

                return RedirectToAction("Index", "Home");
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
            _context.Users.Remove(appUser);
            _context.SaveChanges();
            return Ok();
        }



        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(AppUser appUser)
        {
            _context.Users.Add(appUser);
            _context.SaveChanges();
            return Ok(appUser);
        }

        [Authorize(Roles = "Admin")]   
        public IActionResult GetById(int id)
        {
            return Json(_context.Users.FirstOrDefault(u=>u.Id == id));
            
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(AppUser appUser)
        {
            _context.Users.Update(appUser);
            _context.SaveChanges();
            return Ok();

        }

    }
}
