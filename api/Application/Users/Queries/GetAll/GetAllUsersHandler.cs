using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetAll;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Result<PaginatedList<GetAllUsersResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetAllUsersHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PaginatedList<GetAllUsersResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var response = await _dbContext.UserTestSessions
            .Where(x => x.CompletedAt.HasValue)
            .When(request.SearchTerm != null, x => x.User.FirstName.Contains(request.SearchTerm!)
                                                   || x.User.LastName.Contains(request.SearchTerm!)
                                                   || x.User.PhoneNumber.Contains(request.SearchTerm!))
            .When(request.TestType != null, x => x.WorkingMemoryTest!.Type == request.TestType!.Value)
            .When(request.IsRightHanded != null, x => x.User!.IsRightHanded == request.IsRightHanded!.Value)
            .Select(x => new GetAllUsersResponse
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
