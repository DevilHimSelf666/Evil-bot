using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evil.Application.Handlres.InfinityCommands.DragonQuery
{
    public record GetDragonGoldQuery : IRequest<DragonDto>
    {
        public GetDragonGoldQuery(int dragonLevel)
        {
            DragonLevel = dragonLevel;
        }

        public int DragonLevel { get; set; }
    }
    public class GetDragonGoldQueryHandler : IRequestHandler<GetDragonGoldQuery, DragonDto>
    {
        public GetDragonGoldQueryHandler(SqliteDbContext db)
        {
            Db = db;
        }

        public SqliteDbContext Db { get; }

        public async Task<DragonDto> Handle(GetDragonGoldQuery request, CancellationToken cancellationToken)
        {
            var result = await Db.Dragons.Where(x => x.Level == request.DragonLevel).FirstOrDefaultAsync();
            var goldCost = result is null ? 0 : result.GoldCost;
            return new DragonDto() { GoldCost = goldCost };
        }
    }
}
