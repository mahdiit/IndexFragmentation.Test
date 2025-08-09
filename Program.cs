// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using IndexFragmentation.Test;
using IndexFragmentation.Test.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Start");

var services = new ServiceCollection();
services.AddSingleton(ContextFactory.Configuration);
services.AddDbContext<ProjectContext>(option =>
{
    option.UseSqlServer(ContextFactory.Configuration.GetConnectionString("Db"));
});

var app = services.BuildServiceProvider();

const int totalRows = 3_000_000;
const int batchSize = 1_000;
const int repeatCount = totalRows / batchSize;
var total = 0;
await Parallel.ForAsync(0, repeatCount, async (i, token) =>
{
    var sw = Stopwatch.StartNew();
    W($"Start Items {i}", ConsoleColor.DarkYellow);
    await AddToDb.AddDatabaseRows(app.CreateScope(), batchSize);
    sw.Stop();

    W($"Done Items {i} in {sw.Elapsed.ToString()}", ConsoleColor.Green);
    Interlocked.Add(ref total, batchSize);
    W($"TotalDone Items {total}", ConsoleColor.Green);
});

Console.WriteLine("Done");
Console.ReadKey();
return;

void W(string txt, ConsoleColor color = ConsoleColor.White)
{
    Console.ForegroundColor = color;
    Console.WriteLine(txt);
    Console.ResetColor();
}


