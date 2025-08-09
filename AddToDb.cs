using IndexFragmentation.Test.Db;
using IndexFragmentation.Test.Db.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UUIDNext;

namespace IndexFragmentation.Test
{
    internal static class AddToDb
    {
        public static async Task AddDatabaseRows(IServiceScope scope, int totalCount)
        {
            try
            {
                await using var db = scope.ServiceProvider.GetRequiredService<ProjectContext>();
                for (var i = 0; i < totalCount; i++)
                {
                    db.FragmentationTest.Add(new FragmentationTestEntity()
                    {
                        UuidNext = UUIDNext.Uuid.NewDatabaseFriendly(Database.SqlServer),
                        Guid4 = Guid.NewGuid(),
                        Guid7 = Guid.CreateVersion7(),
                        Ulid = Ulid.NewUlid()
                    });
                }

                await db.SaveChangesAsync();

                var item = db.IndexFragmentationViewTest.AsNoTracking().First();
                db.FragmentationTestResult.Add(new FragmentationTestResultEntity()
                {
                    UuidNext = item.UuidNext,
                    Guid4 = item.Guid4,
                    Guid7 = item.Guid7,
                    Ulid = item.Ulid,
                    TotalCount = item.TotalCount
                });

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}", ConsoleColor.Red);
            }
        }
    }
}
