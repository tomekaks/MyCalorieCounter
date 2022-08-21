using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCalorieCounter.Infrastructure.Migrations
{
    public partial class AddedCaloriesBurnedPropertyToDailySum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Calories",
                table: "MyActivities",
                newName: "CaloriesBurned");

            migrationBuilder.AddColumn<double>(
                name: "CaloriesBurned",
                table: "DailySums",
                type: "float",
                nullable: true,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaloriesBurned",
                table: "DailySums");

            migrationBuilder.RenameColumn(
                name: "CaloriesBurned",
                table: "MyActivities",
                newName: "Calories");
        }
    }
}
