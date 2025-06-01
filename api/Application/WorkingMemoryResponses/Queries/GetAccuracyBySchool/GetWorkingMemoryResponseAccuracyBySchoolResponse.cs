namespace Application.WorkingMemoryResponses.Queries.GetAccuracyBySchool;

public record GetWorkingMemoryResponseAccuracyBySchoolResponse
{
    public required string UserFullName { get; set; }
    public required double UserAccuracyPercent { get; set; }
    public required double TotalSchoolAccuracyPercent { get; set; }
    public required double TotalAccuracyPercent { get; set; }
}
