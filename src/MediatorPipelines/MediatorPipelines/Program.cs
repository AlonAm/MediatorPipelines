using MediatR;
using MediatR.Pipeline;
using SimpleInjector;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorPipelines
{
    class Program
    {
        static async Task Main()
        {
            var container = new Container();

            var assemblies = new[]
            {
                typeof(IMediator).GetTypeInfo().Assembly,
                typeof(Program).GetTypeInfo().Assembly
            };

            container.RegisterSingleton<IMediator, Mediator>();

            container.Register(typeof(IRequestHandler<,>), assemblies);

            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);

            container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            {
                typeof(RequestPreProcessorBehavior<,>),
                typeof(RequestPostProcessorBehavior<,>),
            });

            container.Collection.Register(typeof(IRequestPreProcessor<>), new[] { typeof(GenericRequestPreProcessor<>) });

            container.Collection.Register(typeof(IRequestPostProcessor<,>), new[] {
                typeof(GenericRequestPostProcessor<,>),
                typeof(ConstrainedRequestPostProcessor<,>) });

            //container.Verify();

            var mediator = container.GetInstance<IMediator>();

            var ping = new Ping { Message = "Ping" };

            var pong = await mediator.Send(ping);

            Console.WriteLine(pong.Message);

            Console.ReadLine();
        }
    }

    public class Ping : IRequest<Pong>
    {
        public string Message { get; set; }
    }

    public class Pong
    {
        public string Message { get; set; }
    }

    public class PingHandler : IRequestHandler<Ping, Pong>
    {
        public Task<Pong> Handle(Ping request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Pong { Message = request.Message + " Pong" });
        }
    }
}
