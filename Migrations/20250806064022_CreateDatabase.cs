using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndexFragmentation.Test.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FragmentationTest",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid4 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Guid7 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ulid = table.Column<byte[]>(type: "varbinary(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FragmentationTest", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FragmentationTest");
        }
    }
}
