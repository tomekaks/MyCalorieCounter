using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCalorieCounter.Infrastructure.Migrations
{
    public partial class AddDailyGoalTableAndApplicationUserRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Meals",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DailySums",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DailyGoals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Calories = table.Column<double>(type: "float", nullable: false),
                    Proteins = table.Column<double>(type: "float", nullable: false),
                    Carbs = table.Column<double>(type: "float", nullable: false),
                    Fats = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyGoals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyGoals_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meals_UserId",
                table: "Meals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DailySums_UserId",
                table: "DailySums",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyGoals_UserId",
                table: "DailyGoals",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DailySums_AspNetUsers_UserId",
                table: "DailySums",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_AspNetUsers_UserId",
                table: "Meals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailySums_AspNetUsers_UserId",
                table: "DailySums");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_AspNetUsers_UserId",
                table: "Meals");

            migrationBuilder.DropTable(
                name: "DailyGoals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_UserId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_DailySums_UserId",
                table: "DailySums");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DailySums");
        }
    }
}
