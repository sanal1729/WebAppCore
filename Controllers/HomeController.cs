using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
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
      
        private readonly IWebHostEnvironment hostingEnvironment;

        
        public HomeController(IPeople people, IWebHostEnvironment hostingEnvironment)
        {
            _people = people;
            this.hostingEnvironment = hostingEnvironment;
        }

        public ViewResult Index(int? id)
        {
            //throw new Exception("Error in Details View");

            People people = _people.GetPeople(id.Value);

            if (people == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            var q = people;
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
        public IActionResult Create(CreatePeopleViewModel model)
        //public RedirectToActionResult Create(People people)
        {
            if(ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);

                People newPeople = new People
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    // Store the file name in PhotoPath property of the employee object
                    // which gets saved to the Employees database table
                    PhotoPath = uniqueFileName
                };




                _people.AddPeople(newPeople);
            return RedirectToAction("Index",new { id= newPeople.Id });
            }
            return View();

        }


        [HttpGet]
        public ViewResult Edit(int id)
        {
            People people = _people.GetPeople(id);
            EditPeopleViewModel editPeopleViewModel = new EditPeopleViewModel
            {
                Id = people.Id,
                Name = people.Name,
                Email = people.Email,
                Department = people.Department,
                ExistingPhotoPath = people.PhotoPath
            };
            return View(editPeopleViewModel);
        }


        [HttpPost]
        public IActionResult Edit(EditPeopleViewModel model)
        //public RedirectToActionResult Create(People people)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the employee being edited from the database
                People people = _people.GetPeople(model.Id);
                // Update the employee object with the data in the model object
                people.Name = model.Name;
                people.Email = model.Email;
                people.Department = model.Department;

                if (model.Photos != null && model.Photos.Count > 0)
                {
                    // If a new photo is uploaded, the existing photo must be
                    // deleted. So check if there is an existing photo and delete
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    // Save the new photo in wwwroot/images folder and update
                    // PhotoPath property of the employee object which will be
                    // eventually saved in the database
                    
                    people.PhotoPath = ProcessUploadedFile(model);
                }
               




                _people.UpdatePeople(people);
                return RedirectToAction("Index", new { id = people.Id });
            }
            return View();

        }

        private string ProcessUploadedFile(CreatePeopleViewModel model)
        {
            string uniqueFileName = null;

            // If the Photo property on the incoming model object is not null, then the user
            // has selected an image to upload.
            if (model.Photos != null && model.Photos.Count > 0)
            {

                // Loop thru each selected file
                foreach (IFormFile photo in model.Photos)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                   
                }
            }

            return uniqueFileName;
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
