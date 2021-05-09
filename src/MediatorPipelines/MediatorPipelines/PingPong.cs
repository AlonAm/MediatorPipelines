using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorPipelines
{
    // Request
    public class Ping : IRequest<Pong>
    {
        public string Message { get; set; }
    }

    // Validator
    public class PingValidator : AbstractValidator<Ping>
    {
        public PingValidator()
        {
            RuleFor(v => v.Message)
                .MaximumLength(255)
                .NotEmpty();
        }
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
            Console.WriteLine("Executing request");

            var response = new Pong { Message = request.Message + " Pong" };

            return Task.FromResult(response);
        }
    }
}
