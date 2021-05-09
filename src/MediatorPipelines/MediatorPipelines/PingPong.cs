using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorPipelines
{
    // Request
    public class Ping : IRequest<Pong>
    {
        public string Message { get; set; }
    }

    // Response
    public class Pong
    {
        public string Message { get; set; }
    }

    // Handler
    public class PingHandler : IRequestHandler<Ping, Pong>
    {
        public Task<Pong> Handle(Ping request, CancellationToken cancellationToken)
        {
            var response = new Pong { Message = request.Message + " Pong" };

            return Task.FromResult(response);
        }
    }
}
