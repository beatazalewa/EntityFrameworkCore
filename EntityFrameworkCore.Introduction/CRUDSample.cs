using EntityFrameworkCore.Introduction.Domain;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EntityFrameworkCore.Introduction
{ 
    //IMPORTANT: Run each "test" separately
    [Trait("Category", "InMemory")]
    public class CRUDSample
    {
        [Fact]
        public async Task Add()
        {
            using (var context = new MovieContext(true))
            {
                var person1 = new Person { Id = 1, FirstName = "John", LastName = "McTiernan" };
                var person2 = new Person { Id = 2, FirstName = "James", LastName = "Cameron" };

                await context.People.AddAsync(person1);
                await context.People.AddAsync(person2);
                await context.SaveChangesAsync();
            }

            using (var context = new MovieContext(true))
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
            using (var context = new MovieContext(true))
            {
                var person = new Person { Id = 1, FirstName = "John", LastName = "McTiernan" };

                await context.People.AddAsync(person);
                await context.SaveChangesAsync();
            }

            using (var context = new MovieContext(true))
            {
                var person = await context.People.FindAsync(1);
                person.FirstName.Should().Be("Ridley");
                person.LastName.Should().Be("Scott");
                await context.SaveChangesAsync();
            }

            using (var context = new MovieContext(true))
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
