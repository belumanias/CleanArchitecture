using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandHandler : IRequestHandler<UpdateStreamerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateStreamerCommandHandler> _logger;

        public UpdateStreamerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateStreamerCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToUpdate = await _unitOfWork.StreamerRepository.GetByIdAsync(request.Id);
            if (streamerToUpdate == null)
            {
                _logger.LogError($"No se encontró el streamer con id {request.Id}");
                throw new NotFoundException (nameof(Streamer), request.Id);
            }
            _mapper.Map(request, streamerToUpdate, typeof(UpdateStreamerCommand), typeof(Streamer));
            _unitOfWork.StreamerRepository.UpdateEntity(streamerToUpdate);
            var result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                _logger.LogError($"No se pudo actualizar el record de streamer {streamerToUpdate.Id}");
                throw new Exception($"No se pudo actualizar el record de streamer {streamerToUpdate.Id}");
            }

            _logger.LogInformation($"Se actualizó correctamente el streamer con Id {streamerToUpdate.Id}");
            return Unit.Value;
        }
    }
}
