using Microsoft.AspNetCore.Routing;

namespace API.Api.Base.Middlewares;

internal interface IEndpointBuilder
{
    void MapEndpoints(IEndpointRouteBuilder endpoints);
}