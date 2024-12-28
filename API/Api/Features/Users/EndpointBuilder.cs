using API.Api.Base.Extensions;
using API.Api.Base.Middlewares;
using API.Api.Features.Users.Create;
using API.Api.Features.Users.Delete;
using API.Api.Features.Users.Get;
using API.Api.Features.Users.GetList;
using API.Api.Features.Users.Update;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace API.Api.Features.Users;

public class EndpointBuilder :IEndpointBuilder
{
    public void MapEndpoints(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("users", async (
            IMediator mediator,
            [AsParameters] GetUserListQuery query,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        });
        
        routeBuilder.MapGet("users/{userId}", async (
            IMediator mediator,
            [AsParameters] GetUserQuery query,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(query, cancellationToken);
            return result.ToHttpResult();
        });
        
        routeBuilder.MapPut("users/{userId}", async (
            IMediator mediator,
            [AsParameters] UpdateUserCommand command,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(command, cancellationToken);
            return result.ToHttpResult();
        });
        
        routeBuilder.MapPost("users", async (
            IMediator mediator,
            [AsParameters] CreateUserCommand command,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(command, cancellationToken);
            return result.ToHttpResult();
        });
        
        routeBuilder.MapDelete("users/{userId}", async (
            IMediator mediator,
            [AsParameters] DeleteUserCommand command,
            CancellationToken cancellationToken
        ) =>
        {
            var result = await mediator.Send(command, cancellationToken);
            return result.ToHttpResult();
        });
    }
}