using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndexFragmentation.Test.Migrations
{
    /// <inheritdoc />
    public partial class AddSearchTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "SearchTime",
                table: "FragmentationTestResult",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchTime",
                table: "FragmentationTestResult");
        }
    }
}
