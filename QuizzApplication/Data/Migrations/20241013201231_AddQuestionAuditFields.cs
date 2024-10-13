using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Question",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Question",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Question",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Question",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Question");
        }
    }
}
