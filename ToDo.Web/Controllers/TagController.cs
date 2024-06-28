using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Data;
using ToDo.Models;
using ToDo.Models.ViewModels;

namespace ToDo.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class TagController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TagController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            //1.yol
            //TagListVM vm = new TagListVM();
            //vm.Tags = _context.Tags.ToList();

            //return View(vm);

            //2.yol
            return View(new TagListVM { Tags = _context.Tags.ToList() });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            Tag tag = _context.Tags.Find(id);
            if ((tag != null))
            {
                _context.Tags.Remove(tag);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteAjax(Tag tag)
        {
            _context.Tags.Remove(tag);
            _context.SaveChanges();
            return Ok("İşlem başarılı");

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return Ok(tag.Id);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id )
        { 

            return View(_context.Tags.Find(id));

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(Tag tag) {
            _context.Tags.Update(tag);
            _context.SaveChanges();
            return Ok(tag.Id);
        
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAll()
        {
            //data table için new diyerek isimsiz obje oluşturuyoruz
            return Json(new {data= _context.Tags.ToList()});
        }
    }

    
}
