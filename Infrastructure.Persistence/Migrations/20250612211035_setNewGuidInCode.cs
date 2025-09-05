using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class setNewGuidInCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "WorkExperience_Details",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NewId()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "WorkExperience",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NewId()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Technology_Items",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NewId()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Skills",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NewId()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NewId()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Project_Images",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NewId()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Profiles",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NewId()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Comment_Reference",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NewId()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "WorkExperience_Details",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NewId()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "WorkExperience",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NewId()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Technology_Items",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NewId()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Skills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NewId()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NewId()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Project_Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NewId()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Profiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NewId()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Comment_Reference",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NewId()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
