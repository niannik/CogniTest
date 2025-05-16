namespace Application.Common.Interfaces;

public interface ISmsService
{
    int SendMessageWithTemplate(string phoneNumber, string template, string token1, string token2 = null, string token3 = null);
}

