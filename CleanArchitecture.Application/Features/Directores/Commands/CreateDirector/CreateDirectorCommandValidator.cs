using FluentValidation;

namespace CleanArchitecture.Application.Features.Directores.Commands.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator() 
        {
            RuleFor(p => p.Nombres)
                .NotNull().WithMessage("{Nombres} no puede ser nulo");
            RuleFor(p => p.Apellidos)
                .NotNull().WithMessage("{Apellidos} no puede ser nulo");
        }
    }
}
