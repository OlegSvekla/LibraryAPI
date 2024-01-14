using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class VerificationTokenAndLastAccessedOnColumnToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastAccessedOn",
                table: "User",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastAccessedOn",
                table: "User");
        }
    }
}
