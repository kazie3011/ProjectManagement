﻿using API.Shared.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace API.Api.Features.Users.Create;

internal sealed class CreateUserCommand : ICommand<CreateUserResponse>
{
    [FromBody]
    public CreateUserRequest Request { get; init; }
}

internal sealed class CreateUserRequest
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public string? Phone { get; init; }
    public required bool IsActive { get; init; }
}