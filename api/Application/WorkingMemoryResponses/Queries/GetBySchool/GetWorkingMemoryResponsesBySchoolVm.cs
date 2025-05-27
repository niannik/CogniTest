namespace Application.WorkingMemoryResponses.Queries.GetBySchool;

public record GetWorkingMemoryResponsesBySchoolVm
{
    public required int TermId { get; set; }
    public required int CorrectAnswers { get; set; }
    public required int UnansweredTerms { get; set; }
    public required int IncorrectAnswers { get; set; }
}
