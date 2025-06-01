using Application.Common;
using MediatR;

namespace Application.WorkingMemoryResponses.Queries.GetBySchool;

public record GetWorkingMemoryResponsesBySchoolQuery(int UserTestSessionId, int schoolId) : IRequest<Result<List<GetWorkingMemoryResponsesBySchoolResponse>>>;
