using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invest.Infrastructure.Data.Migrations.Business
{
    /// <inheritdoc />
    public partial class SaleHistoryProfit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ProfitInUSD",
                table: "SaleHistories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProfitPercentage",
                table: "SaleHistories",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfitInUSD",
                table: "SaleHistories");

            migrationBuilder.DropColumn(
                name: "ProfitPercentage",
                table: "SaleHistories");
        }
    }
}
