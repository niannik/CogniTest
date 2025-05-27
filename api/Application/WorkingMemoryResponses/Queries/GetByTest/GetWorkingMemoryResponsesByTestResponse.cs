namespace Application.WorkingMemoryResponses.Queries.GetByTest;

public record GetWorkingMemoryResponsesByTestResponse
{
    public required int CorrectAnswers { get; set; }
    public required int IncorrectAnswers { get; set; }
    public required int UnansweredTerms { get; set; }
    public required int TotalTerms { get; set; }
}
