﻿using API.Domain.Entities.Users;
using API.Persistence.Contexts;
using API.Shared.Messaging;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace API.Api.Features.Users.Create;

internal sealed class CreateUserCommandHandler(DataContext context) : ICommandHandler<CreateUserCommand, CreateUserResponse>
{
    public async Task<Result<CreateUserResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var existedUser = await context.Users.FirstOrDefaultAsync(u => u.Email == command.Request.Email, cancellationToken: cancellationToken);
        if (existedUser is not null)
            return Result.Fail("User with this email already exists.");
        
        var user = User.Create(command.Request.FirstName, command.Request.LastName, command.Request.Email, command.Request.Phone, command.Request.IsActive);
        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok(new CreateUserResponse()
        {
            UserId = user.Id
        });
    }
}