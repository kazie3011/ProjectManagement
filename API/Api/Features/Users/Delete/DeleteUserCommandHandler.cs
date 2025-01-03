﻿using API.Persistence.Contexts;
using API.Shared.Messaging;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace API.Api.Features.Users.Delete;

internal sealed class DeleteUserCommandHandler(DataContext context) : ICommandHandler<DeleteUserCommand, DeleteUserResponse>
{
    public async Task<Result<DeleteUserResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstAsync(u => u.Id == request.UserId, cancellationToken);
        context.Users.Remove(user);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok(new DeleteUserResponse
        {
            UserId = request.UserId
        });
    }
}