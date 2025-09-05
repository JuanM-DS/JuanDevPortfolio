using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Authentication.Migrations
{
    /// <inheritdoc />
    public partial class setIpAddressUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshToken_IpAddress",
                schema: "Identity",
                table: "RefreshToken");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_IpAddress",
                schema: "Identity",
                table: "RefreshToken",
                column: "IpAddress",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshToken_IpAddress",
                schema: "Identity",
                table: "RefreshToken");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_IpAddress",
                schema: "Identity",
                table: "RefreshToken",
                column: "IpAddress");
        }
    }
}
