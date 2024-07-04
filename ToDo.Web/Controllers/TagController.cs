using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Business.Abstract;
using ToDo.Data;
using ToDo.Data.Repository.Shared.Absract;
using ToDo.Models;
using ToDo.Models.ViewModels;

namespace ToDo.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

     



        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            //1.yol
            //TagListVM vm = new TagListVM();
            //vm.Tags = _context.Tags.ToList();

            //return View(vm);

            //2.yol
            return View(new TagListVM { Tags = _tagService.GetAll().ToList()});
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
           _tagService.Delete(id);

            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteAjax(Tag tag)
        {
          
            return Ok(_tagService.Delete(tag.Id));

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(Tag tag)
        {
           
            return Ok(_tagService.Add(tag).Id) ;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id )
        { 

            return View(_tagService.GetById(id));

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(Tag tag) {
           
            return Ok(_tagService.Update(tag));
        
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAll()
        {
            //data table için new diyerek isimsiz obje oluşturuyoruz
            return Json(new {data= _tagService.GetAll()});
        }
    }

    
}
