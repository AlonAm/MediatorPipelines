using MediatR;
using MediatR.Pipeline;
using SimpleInjector;
using System;
using System.Reflection;
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

            // Mediator
            container.RegisterSingleton<IMediator, Mediator>();

            // Handlers
            container.Register(typeof(IRequestHandler<,>), assemblies);

            // Dependency Injection
            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);

            // Pipeline
            container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            {
                typeof(RequestPreProcessorBehavior<,>),
                typeof(RequestPostProcessorBehavior<,>),
            });

            // Processors 
            container.Collection.Register(typeof(IRequestPreProcessor<>), new[] { typeof(GenericRequestPreProcessor<>) });
            container.Collection.Register(typeof(IRequestPostProcessor<,>), new[] { typeof(GenericRequestPostProcessor<,>), typeof(ConstrainedRequestPostProcessor<,>) });

            // Run it!

            var mediator = container.GetInstance<IMediator>();

            var ping = new Ping { Message = "Ping" };

            var pong = await mediator.Send(ping);
            
            Console.WriteLine(pong.Message); // "Ping Pong"

            Console.ReadLine();
        }
    }
}
