using Application.Common;
using MediatR;

namespace Application.SchoolPrincipals.Commands.VerifyOtp;

public record SchoolPrincipalVerifyOtpCommand : IRequest<Result<SchoolPrincipalVerifyOtpResponse>>
{
    public required string PhoneNumber { get; set; }
    public required string OtpCode { get; set; }
}
