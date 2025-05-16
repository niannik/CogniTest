using Application.Common;
using MediatR;

namespace Application.SchoolPrincipals.Commands.UpdateProfile;

public record UpdateSchoolPrincipalProfileCommand : IRequest<Result>
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string NationalCode { get; set; }
    public required int SchoolId { get; set; }
}
