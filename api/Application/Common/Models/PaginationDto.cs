namespace Application.Common.Models;

public record PaginationDto
{
    public PaginationDto()
    {
        PageIndex = 1;
        PageSize = 10;
    }
    public PaginationDto(int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
    public int PageIndex { get; init; }
    public int PageSize { get; init; }
};