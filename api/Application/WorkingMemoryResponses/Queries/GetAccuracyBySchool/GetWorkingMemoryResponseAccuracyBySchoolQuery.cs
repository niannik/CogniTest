using Application.Common;
using MediatR;

namespace Application.WorkingMemoryResponses.Queries.GetAccuracyBySchool;

public record GetWorkingMemoryResponseAccuracyBySchoolQuery(int UserTestSessionId, int SchoolId) : IRequest<Result<GetWorkingMemoryResponseAccuracyBySchoolResponse>>;
