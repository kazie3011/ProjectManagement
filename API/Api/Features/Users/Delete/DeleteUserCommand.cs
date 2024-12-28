using API.Shared.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.Features.Users.Delete;

internal sealed class DeleteUserCommand : ICommand<DeleteUserResponse>
{
    [FromRoute(Name = "userId")]
    public required Guid UserId { get; init; }
}