using EntityFrameworkCore._01_SqlConfiguration.Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EntityFrameworkCore._01_SqlConfiguration
{
    public class CRUDSample
    {
        public CRUDSample()
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
                var person1 = new Person { FirstName = "John", LastName = "McTiernan" };
                var person2 = new Person { FirstName = "James", LastName = "Cameron" };

                await context.People.AddAsync(person1);
                await context.People.AddAsync(person2);
                await context.SaveChangesAsync();
            }

            using (var context = new MovieContext())
            {
                var person1 = await context.People.FindAsync(1);
                var person2 = await context.People.FindAsync(2);

                person1.Should().NotBeNull();
                person1.Id.Should().Be(1);
                person1.FirstName.Should().Be("John");
                person1.LastName.Should().Be("McTiernan");

                person2.Should().NotBeNull();
                person2.Id.Should().Be(2);
                person2.FirstName.Should().Be("James");
                person2.LastName.Should().Be("Cameron");
            }
        }

        [Fact]
        public async Task Update()
        {
            using (var context = new MovieContext())
            {
                var person = new Person {FirstName = "John", LastName = "McTiernan" };

               await context.People.AddAsync(person);
                await context.SaveChangesAsync();
            }

            using (var context = new MovieContext())
            {
                var person = await context.People.FindAsync(1);
                person.FirstName = "Ridley";
                person.LastName = "Scott";
                await context.SaveChangesAsync();
            }

            using (var context = new MovieContext())
            {
                var person = await context.People.FindAsync(1);
                person.Should().NotBeNull();
                person.Id.Should().Be(1);
                person.FirstName.Should().Be("Ridley");
                person.LastName.Should().Be("Scott");
            }
        }
    }
}
