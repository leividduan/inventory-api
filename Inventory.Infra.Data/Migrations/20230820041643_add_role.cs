using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "CompanyUser",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "CompanyUser");
        }
    }
}
