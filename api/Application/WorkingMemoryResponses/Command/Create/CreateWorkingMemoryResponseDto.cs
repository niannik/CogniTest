namespace Application.WorkingMemoryResponses.Command.Create;

public record CreateWorkingMemoryResponseDto
{
    public int TermId { get; set; }
    public bool? IsTarget { get; set; }
    public long ResponseTime { get; set; }
}
