using MediatR;

namespace Handlres.PingCommand
{
    public record PingCommand : IRequest<pingDto>
    {

        public int Latency { get; set; }

    }
    public class PingHandler : IRequestHandler<PingCommand, pingDto>
    {
        public Task<pingDto> Handle(PingCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new pingDto() { Message = $":ping_pong: It took me {request.Latency}ms to respond to you!" });
        }
    }


}
