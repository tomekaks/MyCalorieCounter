using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCalorieCounter.Infrastructure.Migrations
{
    public partial class AddDatePropertyToMealModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Meals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Meals");
        }
    }
}
