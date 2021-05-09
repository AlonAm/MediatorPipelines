using MediatR.Pipeline;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorPipelines
{
    public class GenericRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}