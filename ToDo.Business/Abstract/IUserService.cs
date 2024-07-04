using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDo.Business.Shared.Abstract;
using ToDo.Models;
using ToDo.Models.DTOs;

namespace ToDo.Business.Abstract
{
    public interface IUserService:IService<AppUser>
    {
        IQueryable<AppUserDto> GetAllWithTodoCount();

        ClaimsPrincipal?  CheckLogin(AppUser user);
    }
}
