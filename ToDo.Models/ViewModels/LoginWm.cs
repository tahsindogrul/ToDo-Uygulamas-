﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Models.ViewModels
{
    public class LoginWm
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public bool IsPersistent { get; set; }
        

    }
}
