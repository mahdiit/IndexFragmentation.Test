using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndexFragmentation.Test.Migrations
{
    /// <inheritdoc />
    public partial class UuIdNext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UuidNext",
                table: "FragmentationTest",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FragmentationTest_UuidNext",
                table: "FragmentationTest",
                column: "UuidNext");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FragmentationTest_UuidNext",
                table: "FragmentationTest");

            migrationBuilder.DropColumn(
                name: "UuidNext",
                table: "FragmentationTest");
        }
    }
}
