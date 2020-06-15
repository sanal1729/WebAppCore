using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Abstract;

namespace WebApp.Models
{
    public class NonSqlPeopleRepository : IPeople
    {
        private List<People> _peopleList;

        public NonSqlPeopleRepository()
        {
            _peopleList = new List<People>()
            {
            new People() { Id = 1,Name = "Sam"},
            new People() { Id = 2,Name = "Ram"},
            new People() { Id = 3,Name = "Raj"},
            new People() { Id = 4,Name = "Tom"},
            new People() { Id = 5,Name = "Zia"}

            };
        }


        

        public People GetPeople(int id)
        {
            return _peopleList.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<People> GetAll()
        {
            return _peopleList;
        }

        public People AddPeople(People people)
        {
            people.Id = _peopleList.Max(x => x.Id) + 1;
            _peopleList.Add(people);
            return people;
        }


    }
}
