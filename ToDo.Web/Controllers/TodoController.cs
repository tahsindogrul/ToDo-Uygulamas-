using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Web.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            //return Json(new
            //{
            //    data = _context.Todos.Select(t => new
            //    {
            //        t.Id,
            //        t.Name,
            //        t.Description,
            //        StatusName = t.Status.Name,
            //        Tags = t.Tags.Select(tag => 
            //            tag.Name
            //        ).ToArray()


            //    })
            //});



            //var result2 = _context.Todos.Select(t => new Todo
            //{
            //    Id = t.Id,
            //    Name = t.Name,
            //    Status = t.Status,
            //    Description = t.Description,
            //    Tags = t.Tags.ToList()
            //});
            // return Json(new data)

            var userId= int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); 

            var result2 = _context.Todos.Where(t=>t.AppUserId==userId).Select(t => new Todo
            {
                Id = t.Id,
                Name = t.Name,
                Status = t.Status,
                Description = t.Description,
                Tags = t.Tags.ToList()
            });

            return Json(new { data = result2 });
        }

        public IActionResult RemoveTag(int todoId, int tagId)
        {
            Todo todo = _context.Todos.Include(t => t.Tags).FirstOrDefault(t => t.Id == todoId);
            Tag tag = _context.Tags.Find(tagId);

            todo.Tags.Remove(tag);
            _context.Todos.Update(todo);
            _context.SaveChanges();

            return Ok("işlem başarılı");

        }


        [HttpPost]
        public IActionResult Add(Todo todo, int[] tags )
        {

           

           todo.Tags= _context.Tags.Where(t => tags.Contains(t.Id)).ToList();
            todo.AppUserId= int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _context.Todos.Add(todo);
            _context.SaveChanges();




            return Ok(todo);
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
            _context.Todos.Remove(todo);
            _context.SaveChanges();
            return Ok();
        }


        public IActionResult UpdateTodo(int id)
        {
            var todo =_context.Todos.Where(t=>t.Id == id).Select(t => new Todo
            {
                Id = t.Id,
                Name = t.Name,
                Status = t.Status,
                Description = t.Description,
                Tags = t.Tags.ToList()

            }).FirstOrDefault();

            return Json(todo);




        }

        [HttpPost]
        public IActionResult UpdateTodo(Todo todo, int[] tagsId)
        {

            todo.AppUserId = 1;
            Todo asilTodo = _context.Todos.Include(t => t.Tags).FirstOrDefault(t => t.Id == todo.Id);
            asilTodo.Name = todo.Name;
            asilTodo.Description = todo.Description;
            asilTodo.StatusId = todo.StatusId;
            asilTodo.Tags.Clear();
            asilTodo.Tags = _context.Tags.Where(t => tagsId.Contains(t.Id)).ToList();

            _context.Todos.Update(asilTodo);
            _context.SaveChanges();
            return Ok();
        }












        //public IActionResult UpdateTodo(int id)
        //{
        //    var todo = _context.Todos.Where(i => i.Id == id).Select(todo => new
        //    {
        //        todo.Id,
        //        todo.Name,
        //        todo.Description,
        //        statusId = todo.Status.Id,
        //        tagsId = todo.Tags.Select(i => i.Id).ToList()


        //    }).FirstOrDefault();
        //    return Json(todo);
        //}
        //[HttpPost]
        //public IActionResult UpdateTodo(Todo todo, List<int> tagsId)
        //{
        //   //var asilTodo= _context.Todos.Where(i => i.Id == todo.Id).Select(t => new
        //   // {
        //   //     t.Id,
        //   //     t.Name,
        //   //     t.Description,
        //   //     statusId = t.Status.Id,
        //   //     tagsId = t.Tags.Select(i => i.Id).ToList()


        //   // }).FirstOrDefault();



        //    Todo asilTodo = _context.Todos.Include(k => k.Tags).FirstOrDefault(t => t.Id == todo.Id);


        //    asilTodo.Name= todo.Name;
        //    asilTodo.Description= todo.Description;
        //    asilTodo.StatusId= todo.StatusId;
        //    asilTodo.Tags.Clear();
        //    foreach(int item in tagsId)
        //    {
        //        asilTodo.Tags.Add(_context.Tags.FirstOrDefault(i=>i.Id== item));

        //    }
        //    _context.Todos.Update(asilTodo);
        //    _context.SaveChanges();

        //    return Ok();
        //}

    }


}


