using IndexFragmentation.Test.Db.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IndexFragmentation.Test.Db;

public class ProjectContext(DbContextOptions<ProjectContext> option) : DbContext(option)
{
    public DbSet<Model.FragmentationTestEntity> FragmentationTest { get; set; }

    public DbSet<IndexFragmentationInfo> IndexFragmentationView { get; set; }

    public DbSet<FragmentationTestResultEntity> FragmentationTestResult { get; set; }

    public DbSet<IndexFragmentationInfoTest> IndexFragmentationViewTest { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FragmentationTestEntity>(ef =>
        {
            ef.ToTable("FragmentationTest");

            ef.HasKey(k => k.Id);
            ef.HasIndex(p => p.Guid4);
            ef.HasIndex(p => p.Guid7);
            ef.HasIndex(p => p.Ulid);
            ef.HasIndex(p => p.UuidNext);

            ef.Property(k => k.Ulid).HasConversion<UlidToBytesConverter>();
        });

        modelBuilder.Entity<FragmentationTestResultEntity>(ef =>
        {
            ef.ToTable("FragmentationTestResult");
            ef.HasKey(k => k.Id);
        });

        modelBuilder.Entity<IndexFragmentationInfo>()
            .HasNoKey()
            .ToView("IndexFragmentationView");

        modelBuilder.Entity<IndexFragmentationInfoTest>()
            .HasNoKey()
            .ToView("IndexFragmentationViewStat");

        base.OnModelCreating(modelBuilder);
    }

    public class UlidToBytesConverter : ValueConverter<Ulid, byte[]>
    {
        private static readonly ConverterMappingHints DefaultHints = new ConverterMappingHints(size: 16);

        public UlidToBytesConverter() : this(null)
        {
        }

        public UlidToBytesConverter(ConverterMappingHints? mappingHints)
            : base(
                convertToProviderExpression: x => x.ToByteArray(),
                convertFromProviderExpression: x => new Ulid(x),
                mappingHints: DefaultHints.With(mappingHints))
        {
        }
    }
}