using Microsoft.EntityFrameworkCore.Migrations;

namespace EFRepository.Migrations
{
    public partial class cashback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CashbackPercentage",
                table: "Sales",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "CashbackValue",
                table: "Sales",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashbackPercentage",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CashbackValue",
                table: "Sales");
        }
    }
}
