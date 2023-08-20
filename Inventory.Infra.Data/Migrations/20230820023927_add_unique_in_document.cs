using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_unique_in_document : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Company_Document",
                table: "Company",
                column: "Document",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Company_Document",
                table: "Company");
        }
    }
}
