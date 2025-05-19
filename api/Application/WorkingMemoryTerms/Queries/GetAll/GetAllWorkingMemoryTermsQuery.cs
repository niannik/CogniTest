using Application.Common;
using MediatR;

namespace Application.WorkingMemoryTerms.Queries.GetAll;

public record GetAllWorkingMemoryTermsQuery(int TestId, int UserId) : IRequest<Result<List<GetAllWorkingMemoryTermsResponse>>>;
