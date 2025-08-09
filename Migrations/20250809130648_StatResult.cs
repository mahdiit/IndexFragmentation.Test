using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndexFragmentation.Test.Migrations
{
    /// <inheritdoc />
    public partial class StatResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FragmentationTestResult",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid4 = table.Column<double>(type: "float", nullable: false),
                    Guid7 = table.Column<double>(type: "float", nullable: false),
                    UuidNext = table.Column<double>(type: "float", nullable: false),
                    Ulid = table.Column<double>(type: "float", nullable: false),
                    TotalCount = table.Column<long>(type: "bigint", nullable: false),
                    InsertDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FragmentationTestResult", x => x.Id);
                });

            migrationBuilder.Sql(@"
CREATE OR ALTER VIEW IndexFragmentationViewStat AS 
SELECT 
(SELECT Count(ft.Id) FROM FragmentationTest ft WITH (NOLOCK)) AS TotalCount,
(SELECT ROUND(ifv.avg_fragmentation_in_percent ,2)
FROM IndexFragmentationView ifv WITH (NOLOCK) WHERE ifv.IndexName = 'IX_FragmentationTest_Guid4') AS Guid4,
(SELECT ROUND(ifv.avg_fragmentation_in_percent ,2)
FROM IndexFragmentationView ifv WITH (NOLOCK) WHERE ifv.IndexName = 'IX_FragmentationTest_Guid7') AS Guid7,
(SELECT ROUND(ifv.avg_fragmentation_in_percent ,2)
FROM IndexFragmentationView ifv WITH (NOLOCK) WHERE ifv.IndexName = 'IX_FragmentationTest_Ulid') AS Ulid,
(SELECT ROUND(ifv.avg_fragmentation_in_percent ,2)
FROM IndexFragmentationView ifv WITH (NOLOCK) WHERE ifv.IndexName = 'IX_FragmentationTest_UuidNext') AS UuidNext
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FragmentationTestResult");

            migrationBuilder.Sql("DROP VIEW IF EXISTS dbo.IndexFragmentationViewStat;");
        }
    }
}
