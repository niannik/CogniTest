using Application.Common;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkingMemoryResponses.Queries.GetBySchool;

public class GetWorkingMemoryResponsesBySchoolHandler : IRequestHandler<GetWorkingMemoryResponsesBySchoolQuery, Result<List<GetWorkingMemoryResponsesBySchoolVm>>>
{
    private readonly IApplicationDbContext _dbContext;
    public GetWorkingMemoryResponsesBySchoolHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetWorkingMemoryResponsesBySchoolVm>>> Handle(GetWorkingMemoryResponsesBySchoolQuery request, CancellationToken cancellationToken)
    {
        var terms = await _dbContext.WorkingMemoryTerms
            .Where(x => x.WorkingMemoryTestId == request.TestId && x.Order != 1)
            .Select(x => new
            {
                x.Id,
                x.IsTarget
            })
            .ToListAsync(cancellationToken);

        var termsCount = terms.Count();

        var userResponses = await _dbContext.WorkingMemoryResponses
            .Where(x => x.Student!.SchoolId == request.SchoolId
                && x.WorkingMemoryTerm!.WorkingMemoryTestId == request.TestId
                && x.WorkingMemoryTerm.Order != 1)
            .Include(x => x.WorkingMemoryTerm)
            .ToListAsync(cancellationToken);

        var groupedResponses = userResponses
            .GroupBy(x => x.StudentId)
            .Where(g => g.Count() == termsCount)
            .ToList();

        var termStats = terms.ToDictionary(
            x => x.Id,
            x => new GetWorkingMemoryResponsesBySchoolVm
            {
                TermId = x.Id,
                CorrectAnswers = 0,
                IncorrectAnswers = 0,
                UnansweredTerms = 0
            });

        foreach (var group in groupedResponses)
        {
            var answeredTermIds = new HashSet<int>(group.Select(r => r.WorkingMemoryTermId));

            foreach (var response in group)
            {
                if (response.IsTarget == response.WorkingMemoryTerm!.IsTarget)
                    termStats[response.WorkingMemoryTermId].CorrectAnswers++;
                else if (response.IsTarget != response.WorkingMemoryTerm!.IsTarget)
                    termStats[response.WorkingMemoryTermId].IncorrectAnswers++;
                else
                    termStats[response.WorkingMemoryTermId].UnansweredTerms++;
            }
        }

        return termStats.Values.ToList();
    }
}
