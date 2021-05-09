using MediatR.Pipeline;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorPipelines
{
    internal class ConstrainedRequestPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
        where TRequest : Ping
    {
        public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}