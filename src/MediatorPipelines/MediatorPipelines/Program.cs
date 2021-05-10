using FluentValidation;
using MediatorPipelines.Behaviors;
using MediatR;
using MediatR.Pipeline;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace MediatorPipelines
{
    class Program
    {
        static async Task Main()
        {
            // Dependency Injection
            var container = new Container();
            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);

            // Assemblies to look for Handlers
            var assemblies = new[]
            {
                typeof(IMediator).GetTypeInfo().Assembly,
                typeof(Program).GetTypeInfo().Assembly
            };

            // Mediator
            container.RegisterSingleton<IMediator, Mediator>();

            // Handlers
            container.Register(typeof(IRequestHandler<,>), assemblies);

            // Pipeline
            container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            {
                typeof(LoggingBehavior<,>),
                typeof(ValidationBehavior<,>),
                typeof(RequestPreProcessorBehavior<,>),
                typeof(RequestPostProcessorBehavior<,>),
            });

            // Validators
            RegisterCollection(container, typeof(IValidator<>), assemblies);

            // Processors 
            RegisterCollection(container, typeof(IRequestPreProcessor<>), assemblies);
            RegisterCollection(container, typeof(IRequestPostProcessor<,>), assemblies);

            // Run it!

            var mediator = container.GetInstance<IMediator>();

            // One Way Example

            var oneWayCommand = new OneWayCommand();

            await mediator.Send(oneWayCommand);

            // Request-Response Example

            var ping = new Ping { Message = "Ping" };

            var pong = await mediator.Send(ping);

            Console.WriteLine("Result: {0}", pong.Message); // "Ping Pong"

            Console.ReadLine();
        }

        private static void RegisterCollection(Container container, Type collectionType, IReadOnlyCollection<Assembly> assemblies)
        {
            // we have to do this because by default, generic type definitions (such as the Constrained Notification Handler) won't be registered
            var handlerTypes = container.GetTypesToRegister(collectionType, assemblies, new TypesToRegisterOptions
            {
                IncludeGenericTypeDefinitions = true,
                IncludeComposites = false,
            });

            container.Collection.Register(collectionType, handlerTypes);
        }
    }
}
