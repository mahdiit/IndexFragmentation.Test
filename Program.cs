// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Threading.Channels;
using IndexFragmentation.Test;
using IndexFragmentation.Test.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var channel = Channel.CreateUnbounded<WriteModel>();
var cancellationToken = new CancellationTokenSource();

Task.Run(async () =>
{
    Console.WriteLine("Start");
    while (!cancellationToken.IsCancellationRequested)
    {
        await foreach (var item in channel.Reader.ReadAllAsync(cancellationToken.Token))
        {
            Console.ForegroundColor = item.Color;
            Console.WriteLine(item.Message);
        }
    }
});

var services = new ServiceCollection();
services.AddSingleton(ContextFactory.Configuration);
services.AddDbContext<ProjectContext>(option =>
{
    option.UseSqlServer(ContextFactory.Configuration.GetConnectionString("Db"));
});

var app = services.BuildServiceProvider();

app.GetRequiredService<ProjectContext>().Database.ExecuteSqlRaw("TRUNCATE TABLE FragmentationTestResult");

const int totalRows = 1_000_000;
const int batchSize = 1_000;
const int repeatCount = totalRows / batchSize;
var total = 0;
await Parallel.ForAsync(0, repeatCount, async (i, token) =>
{
    var sw = Stopwatch.StartNew();
    await W($"Start Items {i}", ConsoleColor.DarkYellow);
    try
    {
        await AddToDb.AddDatabaseRows(app, batchSize);
    }
    catch (Exception ex)
    {
        await W($"Error in {i}, {ex.Message}", ConsoleColor.Red);
    }

    sw.Stop();

    await W($"Done Items {i} in {sw.Elapsed.ToString()}", ConsoleColor.Green);
    Interlocked.Add(ref total, batchSize);
    await W($"TotalDone Items {total}", ConsoleColor.Green);
});

cancellationToken.Cancel();
Console.ResetColor();
channel.Writer.Complete();

Console.WriteLine("Done");
Console.ReadKey();
return;

async Task W(string txt, ConsoleColor color = ConsoleColor.White)
{
    await channel.Writer.WriteAsync(new WriteModel(txt, color));
}

record WriteModel(string Message, ConsoleColor Color);


