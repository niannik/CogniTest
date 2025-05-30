using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.SchoolPrincipals.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;

namespace Application.SchoolPrincipals.Commands.VerifyOtp;

public class SchoolPrincipalVerifyOtpHandler : IRequestHandler<SchoolPrincipalVerifyOtpCommand, Result<SchoolPrincipalVerifyOtpResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IMemoryCache _cache;
    private readonly ISignInSchoolPrincipalService _signInSchoolPrincipalService;

    public SchoolPrincipalVerifyOtpHandler(IApplicationDbContext dbContext, IHostEnvironment hostEnvironment, IMemoryCache cache, ISignInSchoolPrincipalService signInSchoolPrincipalService)
    {
        _dbContext = dbContext;
        _hostEnvironment = hostEnvironment;
        _cache = cache;
        _signInSchoolPrincipalService = signInSchoolPrincipalService;
    }

    public async Task<Result<SchoolPrincipalVerifyOtpResponse>> Handle(SchoolPrincipalVerifyOtpCommand request, CancellationToken cancellationToken)
    {
        if (_cache.TryGetValue(request.PhoneNumber, out string? otpCode))
        {
            if (request.OtpCode != otpCode)
            {
                var isValid = _hostEnvironment.IsProduction() == false && request.OtpCode == "12345";
                if (isValid == false)
                    return SchoolPrincipalErrors.OtpCodeIsNotValid;
            }
        }
        else
        {
            return SchoolPrincipalErrors.OtpCodeIsExpired;
        }

        var schoolPrincipal = await _dbContext.SchoolPrincipals
            .FirstOrDefaultAsync(x => x.PhoneNumber == $"{request.PhoneNumber}" || x.PhoneNumber == $"0+988{request.PhoneNumber}");

        if (schoolPrincipal is null)
            return SchoolPrincipalErrors.SchoolPrincipalNotFound;

        var token = await _signInSchoolPrincipalService.LoginAsync(schoolPrincipal);

        return new SchoolPrincipalVerifyOtpResponse()
        {
            Token = token,
            SchoolId = schoolPrincipal.SchoolId
        };
    }
}

