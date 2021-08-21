using EntityFrameworkCore._01_SqlConfiguration.Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EntityFrameworkCore._01_SqlConfiguration
{
    public class RelationsSamples
    {
        public RelationsSamples()
        {
            using var context = new MovieContext();
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        [Fact]
        public async Task Add()
        {
            using (var context = new MovieContext())
            {
                await context.AddAsync(new Person { FirstName = "James", LastName = "Cameron" });
                await context.AddAsync(new Genre { Name = "Dramat" });
                await context.SaveChangesAsync();
            }

            using (var context = new MovieContext())
            {
                var person = await context.People.FindAsync(1);
                var genre1 = await context.Genres.FindAsync(1);
                var genre2 = new Genre { Name = "Akcja" };
                var movie1 = new Movie { Title = "Titanic", Director = person, Genre = genre1 };
                var movie2 = new Movie { Title = "Obcy 2", Director = person, Genre = genre2 };
                var movie3 = new Movie { Title = "Terminator", Director = person, Genre = genre2 };
                await context.AddAsync(movie1);
                await context.AddAsync(movie2);
                await context.AddAsync(movie3);
                await context.SaveChangesAsync();
            }

            //sprawdzamy czy gatunek "Akcja" został dodany
            using (var context = new MovieContext())
            {
                var genre = await context.Genres.FindAsync(2);

                genre.Should().NotBeNull();
                genre.Id.Should().Be(2);
                genre.Name.Should().Be("Akcja");
            }

            using (var context = new MovieContext())
            {
                var movie = await context.Movies.FindAsync(1);

                movie.Director.LastName.Should().NotBeNull();
                movie.Director.LastName.Should().Be("Cameron");
                movie.Genre.Name.Should().Be("Dramat");
            }

            using (var context = new MovieContext())
            {
                var person = await context.People.FindAsync(1);

                person.Movies.Should().NotBeNullOrEmpty();
                person.Movies.Should().HaveCount(3);
            }
        }

        [Fact]
        public async Task EagerLoading()
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
                var movie = await context.Movies
                    .Include(e => e.Genre)
                    .Include(e => e.Director)
                    .Where(e => e.Id == 1)
                    .SingleOrDefaultAsync();

                movie.Director.LastName.Should().Be("Cameron");
                movie.Genre.Name.Should().Be("Dramat");

            }

            using (var context = new MovieContext())
            {
                var person = await context.People
                    .Include(e => e.Movies)
                    .ThenInclude(e => e.Genre)
                    .Where(e => e.Id == 1)
                    .SingleOrDefaultAsync();

                person.Movies.Should().NotBeNullOrEmpty();
                person.Movies.Should().HaveCount(3);
            }
        }

        [Fact]
        public async Task LazyLoading()
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

                person.Movies.Should().NotBeNullOrEmpty();
                person.Movies.Should().HaveCount(3);
            }
        }
    }
}
