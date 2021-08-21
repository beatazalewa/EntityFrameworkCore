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
    [Trait("Category", "InMemoryRelation")]
    public class RelationsSamples
    {
        [Fact]
        public async Task Add()
        {
            using (var context = new MovieContext(true))
            {
                await context.AddAsync(new Person { Id = 1, FirstName = "James", LastName = "Cameron" });
                await context.AddAsync(new Genre { Id = 1, Name = "Dramat"});
                await context.SaveChangesAsync();
            }

            using (var context = new MovieContext(true))
            {
                var person = await context.People.FindAsync(1);
                var genre1 = await context.Genres.FindAsync(1);
                var genre2 = new Genre { Id = 2, Name = "Akcja" };
                var movie1 = new Movie { Id = 1, Title = "Titanic", Director = person, Genre = genre1 };
                var movie2 = new Movie { Id = 2, Title = "Obcy 2", Director = person, Genre = genre2 };
                var movie3 = new Movie { Id = 3, Title = "Terminator", Director = person, Genre = genre2 };
                await context.AddAsync(movie1);
                await context.AddAsync(movie2);
                await context.AddAsync(movie3);
                await context.SaveChangesAsync();
            }
        }
    }
}
