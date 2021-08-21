using EntityFrameworkCore.Introduction.Domain;
using Microsoft.EntityFrameworkCore;
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
                var movie3 = new Movie { Id = 3, Title = "Terminator", Director = person, Genre = genre2 };

                await context.AddAsync(movie1);
                await context.AddAsync(movie2);
                await context.AddAsync(movie3);
                await context.SaveChangesAsync();
            }

            using (var context = new MovieContext(true))
            {
                var moviesStartsWithT = await context.Movies.Where(e => e.Title.StartsWith("T")).ToArrayAsync();
            }

            using (var context = new MovieContext(true))
            {
                var moviesDirectors = await context.Movies
                    .Select(e => new { e.Title, e.Director.LastName })
                    .OrderBy(e => e.Title)
                    .ToArrayAsync();
            }

            using (var context = new MovieContext(true))
            {
                var moviesDirectors = context.Movies
                    .Select(e => new { e.Title, Director = e.Director.LastName })
                    .OrderBy(e => e.Title);

                foreach (var movieDirector in moviesDirectors)
                {
                    //todo: something
                }

                foreach (var movieDirector in moviesDirectors)
                {
                    //todo: something
                }
            }

            using (var context = new MovieContext(true))
            {
                var numberOfMovies = await context.Movies.CountAsync();
            }
        }
    }
}
