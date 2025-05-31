namespace Application.WorkingMemoryResponses.Queries.GetBySession;

public record GetWorkingMemoryResponsesByTestSessionResponse
{
    public required int Oder { get; set; }
    public bool? IsAnswerCorrect { get; set; }
    public long ResponseTime { get; set; }
}
