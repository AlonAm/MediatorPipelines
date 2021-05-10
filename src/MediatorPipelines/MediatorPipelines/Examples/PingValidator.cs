using FluentValidation;

namespace MediatorPipelines
{
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
}
