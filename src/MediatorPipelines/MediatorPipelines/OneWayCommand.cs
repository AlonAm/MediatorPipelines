using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorPipelines
{
    public class OneWayCommand : IRequest
    {
        public string Message { get; set; }
    }

    public class OneWayCommandHandler : IRequestHandler<OneWayCommand>
    {
        public Task<Unit> Handle(OneWayCommand request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }
    }
}
