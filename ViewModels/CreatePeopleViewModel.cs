using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class CreatePeopleViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Dept? Department { get; set; }
        public List<IFormFile> Photos { get; set; }


    }
}
