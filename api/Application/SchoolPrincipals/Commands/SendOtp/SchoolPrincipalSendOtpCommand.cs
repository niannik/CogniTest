using Application.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.SchoolPrincipals.Commands.SendOtp;

public record SchoolPrincipalSendOtpCommand(
    [RegularExpression(@"^09[0|1|2|3|9][0-9]{8}$", ErrorMessage = "شماره موبایل وارد شده معتبر نیست.")]
    string PhoneNumber
) : IRequest<Result<SchoolPrincipalSendOtpResponse>>;