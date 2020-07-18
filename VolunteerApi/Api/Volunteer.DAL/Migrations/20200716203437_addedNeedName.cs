using Microsoft.EntityFrameworkCore.Migrations;

namespace Volunteer.DAL.Migrations
{
    public partial class addedNeedName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "needs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "needs");
        }
    }
}
