using Microsoft.EntityFrameworkCore.Migrations;

namespace Volunteer.DAL.Migrations
{
    public partial class telephone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "telephone",
                table: "users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "telephone",
                table: "users");
        }
    }
}
