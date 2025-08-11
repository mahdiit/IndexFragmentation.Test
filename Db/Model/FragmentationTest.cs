namespace IndexFragmentation.Test.Db.Model;

public class FragmentationTestEntity
{
    public long Id { get; set; }
    public Guid Guid4 { get; set; }
    public Guid Guid7 { get; set; }
    public Guid UuidNext { get; set; }
    public Ulid Ulid { get; set; }
}

public class FragmentationTestResultEntity
{
    public long Id { get; set; }
    public double Guid4 { get; set; }
    public double Guid7 { get; set; }
    public double UuidNext { get; set; }
    public double Ulid { get; set; }
    public long TotalCount { get; set; }
    public TimeSpan? SearchTime { get; set; }
    public DateTime InsertDate { get; set; } = DateTime.Now;
}

public class IndexFragmentationInfo
{
    public string TableName { get; set; }
    public string IndexName { get; set; }
    public double AvgFragmentationInPercent { get; set; }
    public int PageCount { get; set; }
}

public class IndexFragmentationInfoTest
{
    public double Guid4 { get; set; }
    public double Guid7 { get; set; }
    public double UuidNext { get; set; }
    public double Ulid { get; set; }
    public int TotalCount { get; set; }
}