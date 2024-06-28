using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Web.Controllers
{
    public class StatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatusController(ApplicationDbContext context)
        {
            _context = context;

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAll()
        {
            return Json(_context.Statuses.ToList());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(Status status)
        {
            _context.Statuses.Add(status);
            _context.SaveChanges();
            return Ok(status.Id);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(Status status)
        {
            _context.Statuses.Update(status);
            _context.SaveChanges();
            return Ok();                  
        }



        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(Status status)
        {
            _context.Statuses.Remove(status);
            _context.SaveChanges();
            return Ok();

        }

    }
}
