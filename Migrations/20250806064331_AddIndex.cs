using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndexFragmentation.Test.Migrations
{
    /// <inheritdoc />
    public partial class AddIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FragmentationTest_Guid4",
                table: "FragmentationTest",
                column: "Guid4");

            migrationBuilder.CreateIndex(
                name: "IX_FragmentationTest_Guid7",
                table: "FragmentationTest",
                column: "Guid7");

            migrationBuilder.CreateIndex(
                name: "IX_FragmentationTest_Ulid",
                table: "FragmentationTest",
                column: "Ulid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FragmentationTest_Guid4",
                table: "FragmentationTest");

            migrationBuilder.DropIndex(
                name: "IX_FragmentationTest_Guid7",
                table: "FragmentationTest");

            migrationBuilder.DropIndex(
                name: "IX_FragmentationTest_Ulid",
                table: "FragmentationTest");
        }
    }
}
