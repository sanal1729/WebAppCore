using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<People>().HasData(
                    new People
                    {
                        Id = 1,
                        Name = "Zia",
                        Department = Dept.IT,
                        Email = "zia@zia.com"
                    },
                    new People
                    {
                        Id = 2,
                        Name = "Tom",
                        Department = Dept.Finance,
                        Email = "tom@tom.com"
                    }
                    );
        }
    }
}
