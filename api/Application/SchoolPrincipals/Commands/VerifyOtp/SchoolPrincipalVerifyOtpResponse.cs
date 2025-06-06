﻿using Application.SchoolPrincipals.Common.Models;

namespace Application.SchoolPrincipals.Commands.VerifyOtp;

public record SchoolPrincipalVerifyOtpResponse
{
    public required SchoolPrincipalTokenDto Token { get; set; }
    public int? SchoolId { get; set; }
}
