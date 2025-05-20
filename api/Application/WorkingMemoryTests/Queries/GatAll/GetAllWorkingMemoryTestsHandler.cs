using Application.Common;
using Application.Common.Interfaces;
using Application.Users.Common;
using Application.WorkingMemoryTests.Common.Enums;
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
        var userRsponse = await _dbContext.Users
            .Where(x => x.Id == request.Id)
            .Select(x => new
            {
                OneBackCount = x.WorkingMemoryResponses!.Count(x => x.WorkingMemoryTerm!.WorkingMemoryTest!.Type == WorkingMemoryTestType.OneBack),
                TwoBackCount = x.WorkingMemoryResponses!.Count(x => x.WorkingMemoryTerm!.WorkingMemoryTest!.Type == WorkingMemoryTestType.TwoBack),
                ThreeBackCount = x.WorkingMemoryResponses!.Count(x => x.WorkingMemoryTerm!.WorkingMemoryTest!.Type == WorkingMemoryTestType.ThreeBack)
            }).FirstOrDefaultAsync(cancellationToken);

        if (userRsponse is null)
            return UserErrors.UserNotFound;

        var workingMemoryTests = await _dbContext.WorkingMemoryTests
            .Select(x => new
            {
                x.Id,
                x.Type,
                x.Order,
                x.Description,
                AudioPath = x.Audio!.FilePath,
                TermsCount = x.WorkingMemoryTerms!.Count
            }).ToListAsync(cancellationToken);

        var response = new List<GetAllWorkingMemoryTestsResponse>();

        var order = 1;
        foreach (var workingMemoryTest in workingMemoryTests)
        {
            var status = WorkingMemoryTestStatus.Unavailable;
            if (workingMemoryTest.Type == WorkingMemoryTestType.OneBack && workingMemoryTest.TermsCount != 0)
            {
                if (workingMemoryTest.TermsCount == userRsponse.OneBackCount)
                {
                    status = WorkingMemoryTestStatus.Completed;
                    order = workingMemoryTest.Order + 1;
                }
                else if (userRsponse.OneBackCount == 0)
                {
                    status = WorkingMemoryTestStatus.Available;
                }
                else if (userRsponse.OneBackCount < workingMemoryTest.TermsCount)
                {
                    status = WorkingMemoryTestStatus.InProgress;
                }
            }
            else if (workingMemoryTest.Type == WorkingMemoryTestType.TwoBack && workingMemoryTest.TermsCount != 0)
            {
                if (userRsponse.TwoBackCount == workingMemoryTest.TermsCount)
                {
                    status = WorkingMemoryTestStatus.Completed;
                    order = workingMemoryTest.Order + 1;
                }
                else if (userRsponse.TwoBackCount == 0)
                {
                    if (order != workingMemoryTest.Order)
                        status = WorkingMemoryTestStatus.Unavailable;
                    else
                        status = WorkingMemoryTestStatus.Available;
                }
                else if (userRsponse.TwoBackCount < workingMemoryTest.TermsCount)
                {
                    status = WorkingMemoryTestStatus.InProgress;
                }
            }
            else if (workingMemoryTest.Type == WorkingMemoryTestType.ThreeBack && workingMemoryTest.TermsCount != 0)
            {
                if (userRsponse.ThreeBackCount == workingMemoryTest.TermsCount)
                    status = WorkingMemoryTestStatus.Completed;
                else if (userRsponse.ThreeBackCount == 0)
                {
                    if (order != workingMemoryTest.Order)
                        status = WorkingMemoryTestStatus.Unavailable;
                    else
                        status = WorkingMemoryTestStatus.Available;
                }
                else if (userRsponse.ThreeBackCount < workingMemoryTest.TermsCount)
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
