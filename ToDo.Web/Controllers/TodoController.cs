using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDo.Business.Abstract;
using ToDo.Data;
using ToDo.Data.Repository.Absract;
using ToDo.Data.Repository.Shared.Absract;
using ToDo.Models;

namespace ToDo.Web.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {         

            return Json(new { data = _todoService.GetAll(int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))) });
        }

        public IActionResult RemoveTag(int todoId, int tagId)
        {
           

            return Ok(_todoService.RemoveTag(todoId,tagId));


        }


        [HttpPost]
        public IActionResult Add(Todo todo, int[] tags )
        {

           

            return Ok(_todoService.Add(todo, tags, int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))));
        }

        //[HttpPost]
        //public IActionResult Delete(int todoId)
        //{
        //    _context.Todos.Remove(_context.Todos.Find(todoId));
        //    _context.SaveChanges();
        //    return Ok();
        //}

        [HttpPost]
        public IActionResult Delete(Todo todo)
        {
           
            return Ok(_todoService.Delete(todo.Id) is object);
        }


      













    }


}


