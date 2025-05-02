using Application.Common;
using Application.Common.Interfaces;
using Application.Users.Common.Models;
using Domain.Entities.UserAggregate;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands.Create;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<UserTokenDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ISignInUserService _signInUserService;
    public CreateUserHandler(IApplicationDbContext dbContext, ISignInUserService signInUserService)
    {
        _dbContext = dbContext;
        _signInUserService = signInUserService;
    }
    public async Task<Result<UserTokenDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber, cancellationToken);

        if (user is null)
        {
            user = new User(request.PhoneNumber, request.FirstName, request.LastName, request.Age, request.Gender, request.IsRightHanded, request.SchoolId);
            _dbContext.Users.Add(user);
        }
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return await _signInUserService.LoginAsync(user, cancellationToken);
    }
}
