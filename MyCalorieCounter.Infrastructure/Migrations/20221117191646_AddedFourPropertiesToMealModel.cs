using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCalorieCounter.Infrastructure.Migrations
{
    public partial class AddedFourPropertiesToMealModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<double>(
                name: "Calories",
                table: "Meals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Carbs",
                table: "Meals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Fats",
                table: "Meals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Proteins",
                table: "Meals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calories",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "Carbs",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "Fats",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "Proteins",
                table: "Meals");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
