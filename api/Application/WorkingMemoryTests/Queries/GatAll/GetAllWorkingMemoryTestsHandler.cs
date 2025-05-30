using Application.Common;
using Application.Common.Interfaces;
using Application.Users.Common;
using Application.WorkingMemoryTests.Common.Enums;
using Domain.Entities.UserAggregate;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkingMemoryTests.Queries.GatAll;

public class GetAllWorkingMemoryTestsHandler : IRequestHandler<GetAllWorkingMemoryTestsQuery, Result<List<GetAllWorkingMemoryTestsResponse>>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetAllWorkingMemoryTestsHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<List<GetAllWorkingMemoryTestsResponse>>> Handle(GetAllWorkingMemoryTestsQuery request, CancellationToken cancellationToken)
    {
        var workingMemoryTests = await _dbContext.WorkingMemoryTests
            .Select(x => new
            {
                x.Id,
                x.Type,
                x.Order,
                x.Description,
                AudioPath = x.Audio!.FilePath,
                UserTestSession = x.UserTestSessions!.FirstOrDefault(x => x.UserId == request.UserId)
            })
            .OrderBy(x => x.Order)
            .ToListAsync(cancellationToken);

        var response = new List<GetAllWorkingMemoryTestsResponse>();

        var order = 1;
        foreach (var workingMemoryTest in workingMemoryTests)
        {
            var status = WorkingMemoryTestStatus.Unavailable;
            if (workingMemoryTest.UserTestSession is null)
            {
                if (order != workingMemoryTest.Order)
                    status = WorkingMemoryTestStatus.Unavailable;
                else
                    status = WorkingMemoryTestStatus.Available;
            }
            else if (workingMemoryTest.UserTestSession.Status == TestSessionStatus.Block1Completed || workingMemoryTest.UserTestSession.Status == TestSessionStatus.Block2Completed)
            {
                status = WorkingMemoryTestStatus.Completed;
                order = workingMemoryTest.Order + 1;
            }
            else if (workingMemoryTest.UserTestSession.Status == TestSessionStatus.Block1InProgress || workingMemoryTest.UserTestSession.Status == TestSessionStatus.Block2InProgress)
            {
                status = WorkingMemoryTestStatus.InProgress;
            }
            var userWorkingMemoryTest = new GetAllWorkingMemoryTestsResponse()
            {
                Id = workingMemoryTest.Id,
                Order = workingMemoryTest.Order,
                Type = workingMemoryTest.Type,
                Status = status
            };

            response.Add(userWorkingMemoryTest);

        }

        return response;
    }
}
