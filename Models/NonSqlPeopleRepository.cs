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
            new People() { Id = 1,Name = "Sam",Email = "sam@gmail.com" , Department = Dept.IT},
            new People() { Id = 2,Name = "Ram",Email = "ram@gmail.com" , Department = Dept.HR},
            new People() { Id = 3,Name = "Raj",Email = "raj@gmail.com" , Department = Dept.Finance},
            new People() { Id = 4,Name = "Tom",Email = "tom@gmail.com" , Department = Dept.Finance},
            new People() { Id = 5,Name = "Zia",Email = "zia@gmail.com" , Department = Dept.IT}

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

        public People DeletePeople(int id)
        {
            People people = _peopleList.Where(p => p.Id == id).FirstOrDefault();
            if (people!=null)
            {
                _peopleList.Remove(people);
            }
            return people;
        }

        public People UpdatePeople(People peopleChanges)
        {
            People people = _peopleList.Where(p => p.Id == peopleChanges.Id).FirstOrDefault();
            if (people != null)
            {
                people.Name = peopleChanges.Name;
                people.Department = peopleChanges.Department;
                people.Email = peopleChanges.Email;
                
            }
            return people;
        }
    }
}
