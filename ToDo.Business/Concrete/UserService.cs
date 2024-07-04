using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDo.Business.Abstract;
using ToDo.Business.Shared.Concrete;
using ToDo.Data.Repository.Shared.Absract;
using ToDo.Models;
using ToDo.Models.DTOs;

namespace ToDo.Business.Concrete
{
    public class UserService:Service<AppUser>,IUserService
    {
        public UserService(IRepository<AppUser> repo) :base(repo)
        {
            
        }

        public ClaimsPrincipal? CheckLogin(AppUser user)
        {
            AppUser appUser = GetFirstOrDefault(u => u.Password == user.Password && u.Name == user.Name);

            if (appUser != null)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.GivenName, appUser.Name));
                claims.Add(new Claim(ClaimTypes.Role, appUser.IsAdmin ? "Admin" : "User"));

                ClaimsIdentity identity = new ClaimsIdentity
                  (claims, CookieAuthenticationDefaults.AuthenticationScheme);

            }
        }

            public IQueryable<AppUserDto> GetAllWithTodoCount()
        {
            return GetAll().Select(u => new AppUserDto
            {
                Id = u.Id,
                Name = u.Name,
                IsAdmin = u.IsAdmin,
                TotalTodo = u.Todos.Count()

            });
        }
    }
}
