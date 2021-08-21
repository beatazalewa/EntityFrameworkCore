using EntityFrameworkCore.Introduction.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EntityFrameworkCore.Introduction
{
    //IMPORTANT: Run each "test" separately
    [Trait("Category", "InMemoryQuery")]
    public class QuerySamples
    {
        [Fact]
        public async Task Query()
        {
            using (var context = new MovieContext())
            {
                var person = new Person { Id = 1, FirstName = "James", LastName = "Cameron" };
                var genre1 = new Genre { Id = 1, Name = "Dramat" };
                var genre2 = new Genre { Id = 2, Name = "Akcja" };
                var movie1 = new Movie { Id = 1, Title = "Titanic", Director = person, Genre = genre1 };
                var movie2 = new Movie { Id = 2, Title = "Obcy 2", Director = person, Genre = genre2 };
                var movie3 = new Movie { Id = 3, Title = "Terminator", Director = person, Genre = genre1 };
            }
        }
    }
}
