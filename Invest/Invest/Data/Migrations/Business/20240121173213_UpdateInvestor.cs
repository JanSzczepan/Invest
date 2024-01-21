using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invest.Data.Migrations.Business
{
    /// <inheritdoc />
    public partial class UpdateInvestor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(name: "Name", table: "Investors", newName: "FirstName");
            migrationBuilder.RenameColumn(name: "Surname", table: "Investors", newName: "LastName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(name: "FirstName", table: "Investors", newName: "Name");
            migrationBuilder.RenameColumn(name: "LastName", table: "Investors", newName: "Surname");
        }
    }
}
