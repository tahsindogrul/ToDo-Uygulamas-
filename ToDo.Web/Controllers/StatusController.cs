using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Business.Abstract;
using ToDo.Data;
using ToDo.Data.Repository.Shared.Absract;
using ToDo.Models;

namespace ToDo.Web.Controllers
{
    public class StatusController : Controller
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult GetAll()
        {
            return Json(_statusService.GetAll());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(Status status)
        {

            return Ok(_statusService.Add(status)); ;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(Status status)
        {
           
            return Ok(_statusService.Update(status));                  
        }



        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(Status status)
        {
           
            return Ok(_statusService.Delete(status.Id) );

        }

    }
}
