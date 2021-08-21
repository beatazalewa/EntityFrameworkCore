using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkCore._02_Migrations.Migrations
{
    public partial class AddYear : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearOfProduction",
                table: "Movies",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearOfProduction",
                table: "Movies");
        }
    }
}
