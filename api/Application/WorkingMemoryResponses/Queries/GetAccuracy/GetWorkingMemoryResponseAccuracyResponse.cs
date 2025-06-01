namespace Application.WorkingMemoryResponses.Queries.GetAccuracy;

public record GetWorkingMemoryResponseAccuracyResponse
{
    public required string UserFullName { get; set; }
    public required double UserAccuracyPercent { get; set; }
    public required double TotalAccuracyPercent { get; set; }
}