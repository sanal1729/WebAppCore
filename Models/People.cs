using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApp.Models
{
    public class People 
    {       

        public int Id { get; set; }
        public string Name { get; set; }

        public Dept Department { get; set; }
    }
}
