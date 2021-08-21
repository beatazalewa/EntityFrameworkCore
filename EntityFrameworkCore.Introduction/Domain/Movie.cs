using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Introduction.Domain
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual Genre Genre { get; set; } //one
        public virtual Person Director { get; set; } //one
    }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual IList<Movie> Movies { get; set; } //many
    }
}
