using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomrPhoneNumber",
                table: "Order",
                newName: "CustomerPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "CustomrFullName",
                table: "Order",
                newName: "CustomerFullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerPhoneNumber",
                table: "Order",
                newName: "CustomrPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "CustomerFullName",
                table: "Order",
                newName: "CustomrFullName");
        }
    }
}
