using API.Shared.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.Features.Users.Get;

internal sealed class GetUserQuery : IQuery<GetUserResponse>
{
    [FromRoute(Name = "userId")]
    public required Guid UserId { get; init; }
}