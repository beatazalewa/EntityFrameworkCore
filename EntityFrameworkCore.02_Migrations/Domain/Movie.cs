using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore._02_Migrations.Domain
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual Genre Genre { get; set; } //one
        public virtual Person Director { get; set; } //one
    }
}
