namespace Application.WorkingMemoryResponses.Queries.GetByTestId;

public record GetWorkingMemoryResponsesByTestIdResponse
{
    public required int CorrectAnswers { get; set; }
    public required int IncorrectAnswers { get; set; }
    public required int UnansweredTerms { get; set; }
    public required int TotalTerms { get; set; }
}
