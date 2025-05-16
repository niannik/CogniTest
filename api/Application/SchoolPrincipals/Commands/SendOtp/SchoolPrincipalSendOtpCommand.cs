using Application.Common;
using MediatR;

namespace Application.SchoolPrincipals.Commands.SendOtp;

public record SchoolPrincipalSendOtpCommand(string PhoneNumber) : IRequest<Result<SchoolPrincipalSendOtpResponse>>
{
}
