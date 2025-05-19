namespace Application.WorkingMemoryTerms.Queries.GetAll;

public record GetAllWorkingMemoryTermsResponse
{
    public required int Id { get; set; }
    public required int Order { get; set; }
    public required string PicturePath { get; set; }
    public UserWorkingMemoryTermsResponseDetail? UserResponseDetails { get; set; }
}

public record UserWorkingMemoryTermsResponseDetail
{
    public int? Id { get; set; }
    public bool? IsTarget { get; set; }
    public long? ResponseTime { get; set; }
}
