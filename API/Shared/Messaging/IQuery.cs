using FluentResults;
using MediatR;

namespace API.Shared.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;