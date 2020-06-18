using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Abstract
{
    public interface IPeople
    {
        People GetPeople(int id);
        IEnumerable<People> GetAll();
        People AddPeople(People people);
        People DeletePeople(int id);
        People UpdatePeople(People peopleChanges);
    }
}
