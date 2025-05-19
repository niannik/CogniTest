namespace Application.WorkingMemoryTests.Queries.GetById;

public record GetWorkingMemoryTestByIdResponse
{
    public required int Id { get; set; }
    public required string Description { get; set; }
    public string? AudioPath { get; set; }
}
