using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer
{
    public class DeleteStreamerCommandHandler : IRequestHandler<DeleteStreamerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteStreamerCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteStreamerCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteStreamerCommandHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToDelete = await _unitOfWork.StreamerRepository.GetByIdAsync(request.Id);
            if (streamerToDelete == null)
            {
                _logger.LogError($"No se encontró el streamer con id {request.Id}");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }

            _unitOfWork.StreamerRepository.DeleteEntity(streamerToDelete);
            var result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                _logger.LogError($"No se pudo actualizar el record de streamer {streamerToDelete.Id}");
                throw new Exception($"No se pudo actualizar el record de streamer {streamerToDelete.Id}");
            }
            _logger.LogInformation($"El registro {streamerToDelete.Id} fue eliminado con éxito");
            return Unit.Value;
        }
    }
}
