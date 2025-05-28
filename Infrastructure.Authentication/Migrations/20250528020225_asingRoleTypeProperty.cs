using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class asingRoleTypeProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                schema: "Identity",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                schema: "Identity",
                table: "AspNetRoles");
        }
    }
}
