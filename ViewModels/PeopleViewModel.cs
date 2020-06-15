using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class PeopleViewModel
    {
        public IEnumerable<People> peopleList { get; set; }
        public string  header { get; set; }
    }
}
