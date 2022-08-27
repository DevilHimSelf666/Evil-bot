using Handlres;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evil.Application.Handlres
{
    public record PingCommand : IRequest<pingDto>
    {

        public int Latency { get; set; }

    }
    public class PingHandler : IRequestHandler<PingCommand, pingDto>
    {
        public Task<pingDto> Handle(Handlres.PingCommand request, CancellationToken cancellationToken)
        {
           return Task.FromResult(new pingDto() {Message = $":ping_pong: It took me {request.Latency}ms to respond to you!" }); 
        }
    }


}
