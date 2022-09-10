using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evil.Application.Handlres.InfinityCommands.ImortalStats
{
    public record ImortalStatRequest : IRequest<GetImortalStatDto>
    {
        
    }
    public class GetImortalStatHandler : IRequestHandler<ImortalStatRequest, GetImortalStatDto>
    {
        public Task<GetImortalStatDto> Handle(ImortalStatRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
