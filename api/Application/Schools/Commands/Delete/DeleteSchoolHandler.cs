using Application.Common;
using Application.Common.Interfaces;
using Application.Schools.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Schools.Commands.Delete;

public class DeleteSchoolHandler : IRequestHandler<DeleteSchoolCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public DeleteSchoolHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(DeleteSchoolCommand request, CancellationToken cancellationToken)
    {
        var school = await _dbContext.Schools
            .Include(x => x.Students)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (school is null)
            return SchoolErrors.SchoolNotFound;

        else if (school.Students!.Any())
            return SchoolErrors.CannotRemoveSchoolThatHasStudents;

        _dbContext.Schools.Remove(school);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
