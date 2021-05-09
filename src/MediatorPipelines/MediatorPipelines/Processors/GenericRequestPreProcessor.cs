using MediatR.Pipeline;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorPipelines
{
    public class GenericRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Pre Processor");

            return Task.CompletedTask;
        }
    }
}