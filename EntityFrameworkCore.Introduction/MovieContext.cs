using EntityFrameworkCore.Introduction.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Introduction
{
    public class MovieContext : DbContext
    {
        private readonly bool _useLazyLoading;
        public MovieContext()
        {

        }
        public MovieContext(bool useLazyLoading)
        {
            _useLazyLoading = useLazyLoading;
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (_useLazyLoading)
            {
                optionsBuilder.UseLazyLoadingProxies().UseInMemoryDatabase("MoviesDb");
            }
            else
            {
                optionsBuilder.UseInMemoryDatabase("MoviesDb");
            }
        }
    }
}
