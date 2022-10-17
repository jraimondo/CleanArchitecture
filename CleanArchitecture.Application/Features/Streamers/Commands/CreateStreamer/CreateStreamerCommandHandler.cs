﻿using AutoMapper;
using CleanArchitecture.Application.Contracts.Infraestructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {
        private readonly IStreamerRepository _streamerRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateStreamerCommandHandler> _logger;


        public CreateStreamerCommandHandler(IStreamerRepository streamerRepository, IMapper mapper,
            IEmailService emailService, ILogger<CreateStreamerCommandHandler> logger)
        {

            _streamerRepository = streamerRepository;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;

        }

        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
        {

            var streamerEntity = _mapper.Map<Streamer>(request);

            var newStreamer = await _streamerRepository.AddAsync(streamerEntity);

            _logger.LogInformation($"Streamer {newStreamer.Id} fue creado exitosamente.");

            await SendEmail(newStreamer);

            return newStreamer.Id;
        }

        private async Task SendEmail(Streamer streamer)
        {
            var email = new Email
            {
                To = "correo@empresa.com",
                Body = "La compania stramer se creo correctamente.",
                Subject = "Mensaje Alerta"
            };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errores enviando el correo {streamer.Id}");
            }
        }


    }
}
