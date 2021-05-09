using MediatR.Pipeline;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorPipelines
{
    internal class GenericRequestPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}