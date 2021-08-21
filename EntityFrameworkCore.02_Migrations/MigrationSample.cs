using EntityFrameworkCore._02_Migrations.Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EntityFrameworkCore._02_Migrations
{
    public class MigrationSample
    {
        public MigrationSample()
        {
            using var context = new MovieContext();
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        [Fact]
        public async Task Sample1()
        {
            using (var context = new MovieContext())
            {
                var person = new Person { FirstName = "James", LastName = "Cameron" };
                var genre1 = new Genre { Name = "Dramat" };
                var genre2 = new Genre { Name = "Akcja" };
                var movie1 = new Movie { Title = "Titanic", Director = person, Genre = genre1 };
                var movie2 = new Movie { Title = "Obcy 2", Director = person, Genre = genre2 };
                var movie3 = new Movie { Title = "Terminator", Director = person, Genre = genre2 };
                await context.AddAsync(movie1);
                await context.AddAsync(movie2);
                await context.AddAsync(movie3);
                await context.SaveChangesAsync();
            }

            using (var context = new MovieContext())
            {
                var movie = await context.Movies.FindAsync(1);
                movie.Director.LastName.Should().Be("Cameron");
                movie.Genre.Name.Should().Be("Dramat");
            }

            using (var context = new MovieContext())
            {
                var person = await context.People.FindAsync(1);

                person.AsDirector.Should().NotBeNullOrEmpty();
                person.AsDirector.Should().HaveCount(3);
            }
        }
    }
}
