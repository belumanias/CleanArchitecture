using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer
{
    public class DeleteStreamerCommandValidator : AbstractValidator<DeleteStreamerCommand>
    {
        public DeleteStreamerCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotNull().WithMessage("El Id no puede ser nulo")
                .NotEmpty().WithMessage("El Id no puede estar vacío");
        }
    }
}
