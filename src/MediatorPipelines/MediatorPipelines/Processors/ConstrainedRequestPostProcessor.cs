using MediatR.Pipeline;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorPipelines
{
    internal class ConstrainedRequestPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
        where TRequest : Ping
    {
        public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            Console.WriteLine("..Constrained Post Processor");

            return Task.CompletedTask;
        }
    }
}