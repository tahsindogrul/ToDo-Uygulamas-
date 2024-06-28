using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Models.ViewModels
{
    public class TagListVM
    {
        public List<Tag> Tags { get; set; }

        public Tag Tag { get; set; }
    }
}
