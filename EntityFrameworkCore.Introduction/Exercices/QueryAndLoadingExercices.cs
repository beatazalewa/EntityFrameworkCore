using EntityFrameworkCore.Introduction.Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EntityFrameworkCore.Introduction.Exercises
{
    [Trait("Category", "InMemory")]
    public class QueryAndLoadingExercises
    {
        private async Task LoadDataToDb(MovieContext context)
        {
            var person1 = new Person { Id = 1, FirstName = "James", LastName = "Cameron" };
            var person2 = new Person { Id = 2, FirstName = "Ridley", LastName = "Scott" };
            var person3 = new Person { Id = 3, FirstName = "John", LastName = "McTiernan" };
            var person4 = new Person { Id = 4, FirstName = "Luc", LastName = "Besson" };
            var genre1 = new Genre { Id = 1, Name = "Dramat" };
            var genre2 = new Genre { Id = 2, Name = "Akcja" };
            var genre3 = new Genre { Id = 3, Name = "Sci-fi" };

            await context.AddAsync(new Movie { Id = 1, Title = "Titanic", Director = person1, Genre = genre1 });
            await context.AddAsync(new Movie { Id = 2, Title = "Obcy 2", Director = person1, Genre = genre2 });
            await context.AddAsync(new Movie { Id = 3, Title = "Terminator", Director = person1, Genre = genre2 });
            await context.AddAsync(new Movie { Id = 4, Title = "Blade Runner", Director = person2, Genre = genre3 });
            await context.AddAsync(new Movie { Id = 5, Title = "Piąty Element", Director = person4, Genre = genre3 });
            await context.AddAsync(new Movie { Id = 6, Title = "Predator", Director = person3, Genre = genre2 });
            await context.AddAsync(new Genre { Id = 4, Name = "Komedia" });
            await context.SaveChangesAsync();
        }
        [Fact]
        public async Task Exercise1()
        {
            using (var context = new MovieContext())
            {
                await LoadDataToDb(context);
            }

            //1. (EagerLoading) Pobierz z bazy danych filmy z gatunku "Sci-fi" wraz z reżyserem. Filmy powinny być posortowane alfabetycznie po tytule
            using (var context = new MovieContext())
            {
                //select Movies.*, Genres.*, Persons.* from Movies join Genres on Genre.Id = Movies.GenreId join Persons on Persons.Id = Movies.DirectorId where Genres.Name = 'Sci-fi'
                var movies = await context.Movies
                    .Include(e => e.Genre)
                    .Include(e => e.Director)
                    .Where(e => e.Genre.Name == "Sci-fi")
                    .OrderBy(e => e.Title).ToListAsync(); //tu wstaw prawidłowe query

                movies.Should().NotBeNullOrEmpty();
                movies.Should().HaveCount(2);
                movies.Should().SatisfyRespectively(
                    m =>
                    {
                        m.Title.Should().Be("Blade Runner");
                        m.Director.Should().NotBeNull();
                        m.Director.LastName.Should().Be("Scott");
                    },
                    m =>
                    {
                        m.Title.Should().Be("Piąty Element");
                        m.Director.Should().NotBeNull();
                        m.Director.LastName.Should().Be("Besson");
                    }
                );
            }
        }

        [Fact]
        public async Task Exercise2()
        {
            using (var context = new MovieContext(true))
            {
                await LoadDataToDb(context);
            }

            //1. (LazyLoading) Pobierz z bazy danych filmy z gatunku "Dramat" lub "Sci-fi". Posortuj filmy wg. nazwiska reżysera. Załaduj rezyserów i gatunki dla tych filmów
            using (var context = new MovieContext(true))
            {
                var movies = await context.Movies
                    .Where(m => m.Genre.Name == "Dramat" || m.Genre.Name == "Sci-fi")
                    .OrderBy(e => e.Director.LastName)
                    .ToArrayAsync(); //tu wstaw prawidłowe query

                movies.Should().NotBeNullOrEmpty();
                movies.Should().HaveCount(3);
                movies.Should().SatisfyRespectively(
                    d =>
                    {
                        d.Title.Should().Be("Piąty Element");
                        d.Director.LastName.Should().Be("Besson");
                        d.Genre.Name.Should().Be("Sci-fi");
                    },
                    d =>
                    {
                        d.Title.Should().Be("Titanic");
                        d.Director.LastName.Should().Be("Cameron");
                        d.Genre.Name.Should().Be("Dramat");
                    },
                    d =>
                    {
                        d.Title.Should().Be("Blade Runner");
                        d.Director.LastName.Should().Be("Scott");
                        d.Genre.Name.Should().Be("Sci-fi");
                    }
                );
            }
        }

        [Fact]
        public async Task Exercise3()
        {
            using (var context = new MovieContext())
            {
                await LoadDataToDb(context);
            }

            //1. (EagerLoading) Pobierz z bazy danych reżyserów którzy nakręcili film z gatunku "Sci-fi". załaduj wszystkie filmy dla tych reżyserów.
            using (var context = new MovieContext())
            {
                ////Sposób 1:
                //var directors = await context.People                            //osoby
                //    .Where(p => p.Movies.Any(m => m.Genre.Name == "Sci-fi"))    //dla których przynajmniej jeden film ma gatunek "Sci-fi"
                //    .OrderBy(e => e.LastName)                                   //posortuj po nazwisku (nie było w wymaganiach - dodałem żeby kolejność zwróconych filmów była taka sama jak w sposobie 1)
                //    .Include(e => e.Movies)                                     //dołącz do wyników filmy które były reżyserowane przez wybrane osoby
                //    .ToArrayAsync();                                            //wykonaj zapytanie (zmaterializuj kolekcję) i zwróć jako tablicę

                //Sposób 2:
                var directors = await context.Movies        //filmy
                    .Where(m => m.Genre.Name == "Sci-fi")   //z gatunku "Sci-fi"
                    .Select(m => m.Director)                //z wybranych filmów wybierz tylko reżyserów
                    .Distinct()                             //tylko unikatowe wartości
                    .OrderBy(e => e.LastName)               //posortuj po nazwisku (nie było w wymaganiach - dodałem żeby kolejność zwróconych filmów była taka sama jak w sposobie 1)
                    .Include(e => e.Movies)                 //dołącz do wyników filmy które były reżyserowane przez wybrane osoby
                    .ToArrayAsync();                        //wykonaj zapytanie (zmaterializuj kolekcję) i zwróć jako tablicę

                directors.Should().NotBeNullOrEmpty();
                directors.Should().HaveCount(2);
                directors.Should().SatisfyRespectively(
                    d =>
                    {
                        d.LastName.Should().Be("Besson");
                        d.Movies.Should().NotBeNullOrEmpty();
                        d.Movies.Should().HaveCount(1);
                    },
                    d =>
                    {
                        d.LastName.Should().Be("Scott");
                        d.Movies.Should().NotBeNullOrEmpty();
                        d.Movies.Should().HaveCount(1);
                    }
                );
            }
        }
    }
}
