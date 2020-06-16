using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Abstract;
using WebApp.ViewModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPeople _people;

        public HomeController(IPeople people)
        {
            _people = people;
        }

        public ViewResult Index(int? id)
        {
            
            var q = _people.GetPeople(id??5);
            ViewData["data"] = q;
            ViewBag.Header= "from viewbag";

            return View(q);


        }


        public ViewResult List()
        {
            PeopleViewModel peopleVM = new PeopleViewModel()
                        {
                            peopleList = _people.GetAll(),
                            header="Details"            
                        };          
            return View("List",peopleVM);
        }

        [HttpGet]
        public ViewResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Create(People people)
        //public RedirectToActionResult Create(People people)
        {
            if(ModelState.IsValid)
            { People newPeople = _people.AddPeople(people);
            return RedirectToAction("Index",new { id= newPeople.Id });
            }
            return View();

        }



        //public JsonResult Index()
        //{

        //    var q = _people.GetPeople(5);

        //    //return Json (new{ Id= q.Id, Name=q.Name });
        //    return Json(q);

        //}

        //public ObjectResult Index()
        //{
        //     var q = _people.GetPeople(5);

        //    return  new ObjectResult(q) ;


        //}
    }
}
