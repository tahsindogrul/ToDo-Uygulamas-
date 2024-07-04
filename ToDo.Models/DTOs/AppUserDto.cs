using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Models.DTOs
{
    public class AppUserDto:AppUser
    {
        public int TotalTodo { get; set; }
    }
}
