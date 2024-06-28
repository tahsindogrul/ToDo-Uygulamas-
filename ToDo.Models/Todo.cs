using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class Todo : BaseModel
    {
        public string Description { get; set; }

        public int StatusId {  get; set; }

        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual Status Status { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
