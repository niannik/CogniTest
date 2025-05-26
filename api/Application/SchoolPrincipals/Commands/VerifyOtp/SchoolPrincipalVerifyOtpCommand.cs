using Application.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.SchoolPrincipals.Commands.VerifyOtp;

public record SchoolPrincipalVerifyOtpCommand : IRequest<Result<SchoolPrincipalVerifyOtpResponse>>
{
    [RegularExpression(@"^09[0|1|2|3|9][0-9]{8}$", ErrorMessage = "شماره موبایل وارد شده معتبر نیست.")]
    public required string PhoneNumber { get; set; }
    public required string OtpCode { get; set; }
}
