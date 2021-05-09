using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorPipelines.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            Console.WriteLine("Request started");

            var result = await next();

            Console.WriteLine("Request completed\n");

            return result;
        }
    }
}
