using API.Shared.Messaging;

namespace API.Api.Features.Users.GetList;

internal sealed class GetUserListQuery : IQuery<GetUserListResponse>
{
    public int Page { get; init; }
    public int PageSize { get; init; }
}