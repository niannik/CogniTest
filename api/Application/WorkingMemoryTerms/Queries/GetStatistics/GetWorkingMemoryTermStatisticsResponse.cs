namespace Application.WorkingMemoryTerms.Queries.GetStatistics;

public record GetWorkingMemoryTermStatisticsResponse
{
    public required int Order { get; set; }
    public required int CorrectCount { get; set; }
    public required int IncorrectCount { get; set; }
    public required int UnansweredCount { get; set; }
}
