using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkCore._02_Migrations.Migrations
{
    public partial class AddLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Movies",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "Movies");
        }
    }
}
