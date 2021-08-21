using EntityFrameworkCore.Introduction.Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace EntityFrameworkCore.Introduction.Exercises
{
    [Trait("Category", "InMemoryExercise1")]
    public class CRUDExercises
    {
        private async Task LoadDataToDb(MovieContext context)
        {
            //Tutaj coś trzeba dodać ;)
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task Exercise1()
        {
            //1. dodaj do bazy danych następujące gatunki filmowe (genre): "Akcja", "Komedia", "Dramat" (o Id odpowiednio 1,2,3) używając metody LoadDataToDb


            //check 1 - sprawdź, czy się dodały


            //2. zmodyfikuj nazwę gatunku "Akcja" na "Akcja/Sensacja"

            //check 2

            //3. usuń gatunek "Komedia"

            //check 3

            //4. pobierz wszystkie gatunki z bazy danych. upewnij się że zostały tylko 2
        }
    }
}