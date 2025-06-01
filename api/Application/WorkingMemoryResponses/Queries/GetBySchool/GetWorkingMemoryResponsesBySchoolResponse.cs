namespace Application.WorkingMemoryResponses.Queries.GetBySchool;

public record GetWorkingMemoryResponsesBySchoolResponse
{
    public required int Oder { get; set; }
    public bool? IsAnswerCorrect { get; set; }
    public long ResponseTime { get; set; }
}
