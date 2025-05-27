using Application.Common;
using MediatR;

namespace Application.WorkingMemoryResponses.Queries.GetBySchool;

public record GetWorkingMemoryResponsesBySchoolQuery(int SchoolId, int TestId) : IRequest<Result<List<GetWorkingMemoryResponsesBySchoolVm>>>;
