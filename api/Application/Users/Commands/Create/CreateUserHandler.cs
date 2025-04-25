using Application.Common;
using Application.Common.Interfaces;
using Application.Users.Common;
using Domain.Entities.UserAggregate;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands.Create;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public CreateUserHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isPhoneNumberExists = await _dbContext.Users
            .AnyAsync(x => x.PhoneNumber == request.PhoneNumber, cancellationToken);

        if (isPhoneNumberExists)
            return UserErrors.PhoneNumberAlreadyExists;

        var user = new User(request.PhoneNumber, request.FirstName, request.LastName, request.Age, request.Gender, request.IsRightHanded);
        _dbContext.Users.Add(user);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
