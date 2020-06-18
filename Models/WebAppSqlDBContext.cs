using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class WebAppSqlDBContext: DbContext
    {
        public WebAppSqlDBContext(DbContextOptions<WebAppSqlDBContext> options)
            : base(options)
        {

        }


        public DbSet<People> Peoples { get; set; }
    }
}
