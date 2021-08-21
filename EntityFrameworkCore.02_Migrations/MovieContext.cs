using EntityFrameworkCore._02_Migrations.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore._02_Migrations
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
                .UseSqlServer("Server=LAPTOP-ZALNET\\SQLEXPRESS;Database=EFCoreMigrations;Trusted_Connection=True;MultipleActiveResultSets=True;");
            //.UseInMemoryDatabase("EFCoreMigrations.db");
        }
    }
}
