using MediatR;
using MultiTenantEMS.Application.Common;

namespace MultiTenantEMS.Application.Abstractions.Messaging
{
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : ICommand<TResponse>
    {
    }

    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
    {
    }
}
