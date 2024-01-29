using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invest.Infrastructure.Data.Migrations.Business
{
    /// <inheritdoc />
    public partial class InvestorTotalProfit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalProfitInUSD",
                table: "Investors",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalProfitPercentage",
                table: "Investors",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalProfitInUSD",
                table: "Investors");

            migrationBuilder.DropColumn(
                name: "TotalProfitPercentage",
                table: "Investors");
        }
    }
}
