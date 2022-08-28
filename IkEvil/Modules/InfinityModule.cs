using Discord.Interactions;
using Evil.Application.Handlres.InfinityCommands.DragonQuery;
using Handlres.PingCommand;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evil.IKBot.Modules
{
    public class InfinityModule : InteractionModuleBase<SocketInteractionContext>
    {



        private readonly IMediator mediator;

        public InfinityModule(IMediator mediator)
        {

            this.mediator = mediator;
        }

        [SlashCommand("dragon-gold", "Get Dragon Gold.")]
        public async Task GetDragonGold(int dragonlevel)
        {

            var result = await mediator.Send(new GetDragonGoldQuery() { DragonLevel = dragonlevel });
            await RespondAsync(text: result.GoldCost.ToString(), ephemeral: true);
        }

    }
}
