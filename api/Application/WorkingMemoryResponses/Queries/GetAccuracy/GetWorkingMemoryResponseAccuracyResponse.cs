namespace Application.WorkingMemoryResponses.Queries.GetAccuracy;

public record GetWorkingMemoryResponseAccuracyResponse
{
    public required double UserAccuracyPercent { get; set; }
    public required double TotalAccuracyPercent { get; set; }
}