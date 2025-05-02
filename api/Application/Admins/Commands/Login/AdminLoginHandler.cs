using Application.Admins.Common;
using Application.Admins.Common.Models;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.Common.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Admins.Commands.Login;

public class AdminLoginHandler : IRequestHandler<AdminLoginCommand, Result<AdminTokenDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ISignInAdminService _signInAdminService;
    public AdminLoginHandler(IApplicationDbContext dbContext, ISignInAdminService signInAdminService)
    {
        _dbContext = dbContext;
        _signInAdminService = signInAdminService;
    }

    public async Task<Result<AdminTokenDto>> Handle(AdminLoginCommand request, CancellationToken cancellationToken)
    {
        var admin = await _dbContext.Admins
            .FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);
        if (admin is null)
            return AdminErrors.UserNameIsWrong;

        var isPasswordMatch = PasswordHash.VerifyHashedPassword(admin.PasswordHash, request.Password);

        if (!isPasswordMatch)
            return AdminErrors.PasswordIsWrong;

        return await _signInAdminService.LoginAsync(admin, cancellationToken);
    }
}
