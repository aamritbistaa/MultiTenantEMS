using MediatR;
using MultiTenantEMS.Application.Common;

namespace MultiTenantEMS.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }

}
