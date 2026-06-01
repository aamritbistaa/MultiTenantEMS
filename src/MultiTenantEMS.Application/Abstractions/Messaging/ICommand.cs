using MediatR;
using MultiTenantEMS.Application.Common;

namespace MultiTenantEMS.Application.Abstractions.Messaging
{
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
    public interface ICommand : IRequest<Result>
    {
    }

}
