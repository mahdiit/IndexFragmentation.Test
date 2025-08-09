using IndexFragmentation.Test.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IndexFragmentation.Test;

public class ContextFactory : IDesignTimeDbContextFactory<ProjectContext>
{
    public static readonly IConfiguration Configuration = new ConfigurationBuilder()
        .AddInMemoryCollection(new Dictionary<string, string?>
        {
            {"ConnectionStrings:Db", "Data Source=.;Database=TestDb;Application Name=FragmentationTestApp;Integrated Security=false;User ID=sa;Password=123@Admin;TrustServerCertificate=true;"}
        })
        .Build();

    public ProjectContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProjectContext>();
        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Db"));

        return new ProjectContext(optionsBuilder.Options);
    }
}