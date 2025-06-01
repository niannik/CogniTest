using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.WorkingMemoryTerms.Queries.GetStatistics;

public record GetWorkingMemoryTermStatisticsQuery(WorkingMemoryTestType TestType): IRequest<Result<List<GetWorkingMemoryTermStatisticsResponse>>>;
