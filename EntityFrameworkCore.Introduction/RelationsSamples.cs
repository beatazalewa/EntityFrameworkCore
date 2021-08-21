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
        }


    }
}
