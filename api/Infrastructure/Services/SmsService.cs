using Application.Common.Interfaces;
using Application.Common.Settings;

namespace Infrastructure.Services;

public class SmsService : ISmsService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly SmsSettings _smsSettings;
    public SmsService(IApplicationDbContext dbContext, SmsSettings smsSettings)
    {
        _dbContext = dbContext;
        _smsSettings = smsSettings;
    }

    public int SendMessageWithTemplate(string phoneNumber, string template, string token1, string? token2 = null, string? token3 = null)
    {

        var api = new Kavenegar.KavenegarApi(_smsSettings.SmsApiKey);

        if (token2 == null)
            return api.VerifyLookup(phoneNumber, token1, template).Status;

        if (token3 == null)
            return api.VerifyLookup(phoneNumber, token1, token2, null, template).Status;

        return api.VerifyLookup(phoneNumber, token1, token2, token2, template).Status;

    }
}

