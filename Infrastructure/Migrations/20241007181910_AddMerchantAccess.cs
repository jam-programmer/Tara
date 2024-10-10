using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMerchantAccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MerchantAccess",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    accessCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    merchantCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    terminalCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    terminalTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantAccess", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MerchantAccess_merchantCode",
                table: "MerchantAccess",
                column: "merchantCode");

            migrationBuilder.CreateIndex(
                name: "IX_MerchantAccess_terminalCode",
                table: "MerchantAccess",
                column: "terminalCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MerchantAccess");
        }
    }
}
