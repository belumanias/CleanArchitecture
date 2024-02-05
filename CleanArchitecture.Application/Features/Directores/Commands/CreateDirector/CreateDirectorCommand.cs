using MediatR;

namespace CleanArchitecture.Application.Features.Directores.Commands.CreateDirector
{
    public class CreateDirectorCommand :IRequest<int>
    {
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public int VideoId { get; set; }
    }
}
