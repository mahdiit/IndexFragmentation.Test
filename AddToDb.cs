using System.Diagnostics;
using IndexFragmentation.Test.Db;
using IndexFragmentation.Test.Db.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UUIDNext;

namespace IndexFragmentation.Test
{
    internal static class AddToDb
    {
        public static async Task AddDatabaseRows(IServiceProvider provider, int totalCount)
        {
            using var scope = provider.CreateScope();
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

            var takeIndex = item.TotalCount / 2;
            var itemToSearch = db.FragmentationTest
                .OrderBy(x => x.Id)
                .Skip(takeIndex).Take(1)
                .AsNoTracking()
                .First();

            var totalWork = Stopwatch.StartNew();

            var search = new FragmentationTestResultEntity()
            {
                Guid4 = await SearchGuid4(provider, itemToSearch.Guid4),
                Guid7 = await SearchGuid7(provider, itemToSearch.Guid7),
                Ulid = await SearchUlid(provider, itemToSearch.Ulid),
                UuidNext = await SearchUuidNext(provider, itemToSearch.UuidNext)
            };

            totalWork.Stop();

            search.SearchTime = totalWork.Elapsed;
            search.TotalCount = item.TotalCount;

            db.FragmentationTestResult.Add(search);

            await db.SaveChangesAsync();
        }

        private static async ValueTask<long> SearchUuidNext(IServiceProvider provider, Guid id)
        {
            using var scope = provider.CreateScope();
            await using var db = scope.ServiceProvider.GetRequiredService<ProjectContext>();
            Stopwatch stopWatch = new();

            stopWatch.Start();
            var uuid = db.FragmentationTest.AsNoTracking()
                .FirstOrDefault(x => x.UuidNext == id);
            stopWatch.Stop();
            return stopWatch.ElapsedMilliseconds;
        }

        private static async ValueTask<long> SearchGuid4(IServiceProvider provider, Guid id)
        {
            using var scope = provider.CreateScope();
            await using var db = scope.ServiceProvider.GetRequiredService<ProjectContext>();
            Stopwatch stopWatch = new();

            stopWatch.Start();
            var uuid = db.FragmentationTest.AsNoTracking()
                .FirstOrDefault(x => x.Guid4 == id);
            stopWatch.Stop();
            return stopWatch.ElapsedMilliseconds;
        }

        private static async ValueTask<long> SearchGuid7(IServiceProvider provider, Guid id)
        {
            using var scope = provider.CreateScope();
            await using var db = scope.ServiceProvider.GetRequiredService<ProjectContext>();
            Stopwatch stopWatch = new();

            stopWatch.Start();
            var uuid = db.FragmentationTest.AsNoTracking()
                .FirstOrDefault(x => x.Guid7 == id);
            stopWatch.Stop();
            return stopWatch.ElapsedMilliseconds;
        }

        private static async ValueTask<long> SearchUlid(IServiceProvider provider, Ulid id)
        {
            using var scope = provider.CreateScope();
            await using var db = scope.ServiceProvider.GetRequiredService<ProjectContext>();
            Stopwatch stopWatch = new();

            stopWatch.Start();
            var uuid = db.FragmentationTest.AsNoTracking()
                .FirstOrDefault(x => x.Ulid == id);
            stopWatch.Stop();
            return stopWatch.ElapsedMilliseconds;
        }
    }
}
