using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.UserTestSessions.Queries.GetAllBySchool;

public class GetAllUserTestSessionsBySchoolHandler : IRequestHandler<GetAllUserTestSessionsBySchoolQuery, Result<PaginatedList<GetAllUserTestSessionsBySchoolResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetAllUserTestSessionsBySchoolHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PaginatedList<GetAllUserTestSessionsBySchoolResponse>>> Handle(GetAllUserTestSessionsBySchoolQuery request, CancellationToken cancellationToken)
    {
        var response = await _dbContext.UserTestSessions
            .Where(x => x.CompletedAt.HasValue && x.WorkingMemoryTest!.Type == request.TestType && x.User!.SchoolId == request.SchoolId)
            .When(request.SearchTerm != null, x => x.User!.FirstName.Contains(request.SearchTerm!)
                                                   || x.User.LastName.Contains(request.SearchTerm!)
                                                   || x.User.PhoneNumber.Contains(request.SearchTerm!))
            .When(request.IsRightHanded != null, x => x.User!.IsRightHanded == request.IsRightHanded!.Value)
            .Select(x => new GetAllUserTestSessionsBySchoolResponse
            {
                UserTestSessionId = x.Id,
                FirstName = x.User!.FirstName,
                LastName = x.User!.LastName,
                Age = x.User!.Age,
                PhoneNumber = x.User.PhoneNumber,
                IsRightHanded = x.User!.IsRightHanded,
                TestType = x.WorkingMemoryTest!.Type
            }).ToPaginatedListAsync(request.Pagination, cancellationToken);

        return response;
    }
}
