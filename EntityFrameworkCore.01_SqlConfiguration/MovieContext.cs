using EntityFrameworkCore._01_SqlConfiguration.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore._01_SqlConfiguration
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies()
                .UseSqlServer("Server=LAPTOP-ZALNET\\SQLEXPRESS;Database=EFCore;TrustedConnection=True;MultipleActiveResultSets=True;");
            //.UseInMemoryDatabase("EFCore.db");
        }
    }
}
