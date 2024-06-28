using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ToDo.Data;
using ToDo.Models;
using ToDo.Web.Models;

namespace ToDo.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //Tag tag = new Tag { Name = "Yazýlým" };             
            //_context.Tags.Add(tag);

            //List<Tag> tags = new List<Tag>();
            //tags.Add(new Tag { Name = "Rutin" });
            //tags.Add(new Tag { Name = "Ödev" });
            //tags.Add(new Tag { Name = "Ev Ýþi" });
            //_context.Tags.AddRange(tags);
            //_context.SaveChanges();


            

            return View();
        }

    }
}
