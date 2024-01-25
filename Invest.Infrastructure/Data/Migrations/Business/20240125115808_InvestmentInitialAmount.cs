using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invest.Infrastructure.Data.Migrations.Business
{
    /// <inheritdoc />
    public partial class InvestmentInitialAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InitialAmount",
                table: "Investments",
                type: "decimal(20,10)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialAmount",
                table: "Investments");
        }
    }
}
