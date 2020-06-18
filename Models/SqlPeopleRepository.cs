using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models.Abstract;

namespace WebApp.Models
{
    public class SqlPeopleRepository : IPeople
    {
        private readonly WebAppSqlDBContext context;
        public SqlPeopleRepository(WebAppSqlDBContext context)
        {
            this.context = context;
        }

        public People AddPeople(People people)
        {
            context.Peoples.Add(people);
            context.SaveChanges();
            return people;    
        }

        public People DeletePeople(int id)
        {
            People people = context.Peoples.Find(id);
            if (people != null)
            {
                context.Peoples.Remove(people);
                context.SaveChanges();
            }
            return people;
        }

        public IEnumerable<People> GetAll()
        {
            return context.Peoples;
        }

        public People GetPeople(int id)
        {
            return context.Peoples.Find(id);
        }

        public People UpdatePeople(People peopleChanges)
        {
            var people = context.Peoples.Attach(peopleChanges);
            people.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return peopleChanges;
        }
    }
}
