using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.SchoolPrincipals.Common;
using Domain.Entities.SchoolAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;

namespace Application.SchoolPrincipals.Commands.SendOtp;

public class SchoolPrincipalSendOtpHandler : IRequestHandler<SchoolPrincipalSendOtpCommand, Result<SchoolPrincipalSendOtpResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ISmsService _smsService;
    private readonly IMemoryCache _cache;
    private readonly SmsSettings _smsSettings;
    private readonly IHostEnvironment _hostEnvironment;
    public SchoolPrincipalSendOtpHandler(IApplicationDbContext dbContext, ISmsService smsService, IMemoryCache cache, SmsSettings smsSettings, IHostEnvironment hostEnvironment)
    {
        _dbContext = dbContext;
        _smsService = smsService;
        _cache = cache;
        _smsSettings = smsSettings;
        _hostEnvironment = hostEnvironment;
    }

    public async Task<Result<SchoolPrincipalSendOtpResponse>> Handle(SchoolPrincipalSendOtpCommand request, CancellationToken cancellationToken)
    {
        var schoolPrincipal = await _dbContext.SchoolPrincipals
            .Where(x => x.PhoneNumber == $"{request.PhoneNumber}" || x.PhoneNumber == $"0+988{request.PhoneNumber}")
            .FirstOrDefaultAsync(cancellationToken);

        if (schoolPrincipal is null)
        {
            schoolPrincipal = new SchoolPrincipal(request.PhoneNumber);
            _dbContext.SchoolPrincipals.Add(schoolPrincipal);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        var otpCode = Random.Shared.Next(11111, 99999).ToString();

        if (_cache.TryGetValue(request.PhoneNumber, out _))
        {
            return SchoolPrincipalErrors.TooManyAttempts;
        }

        _cache.Set(request.PhoneNumber, otpCode, DateTimeOffset.Now.AddSeconds(_smsSettings.OtpCodeExpirationSeconds));

        if (_hostEnvironment.IsProduction())
            _smsService.SendMessageWithTemplate($"0{request.PhoneNumber}", "otp-code", otpCode);

        var response = new SchoolPrincipalSendOtpResponse(_smsSettings.OtpCodeExpirationSeconds);

        return response;
    }
}
