using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IndexFragmentation.Test.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIndexFragmentationView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        CREATE OR ALTER VIEW dbo.IndexFragmentationView AS
        SELECT 
            t.name AS TableName,
            i.name AS IndexName,
            ips.avg_fragmentation_in_percent,
            ips.page_count
        FROM 
            sys.dm_db_index_physical_stats (DB_ID(), OBJECT_ID('dbo.FragmentationTest'), NULL, NULL, 'LIMITED') AS ips
        JOIN 
            sys.indexes AS i ON ips.object_id = i.object_id AND ips.index_id = i.index_id
        JOIN 
            sys.tables AS t ON i.object_id = t.object_id
        WHERE 
            i.name IN (
                'PK_FragmentationTest',
                'IX_FragmentationTest_Guid4', 
                'IX_FragmentationTest_Guid7', 
                'IX_FragmentationTest_Ulid',
                'IX_FragmentationTest_UuidNext'
            );
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS dbo.IndexFragmentationView;");
        }
    }
}
