using MediatR;
using NerdStore.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Core.Bus
{
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediator mediator;

        public MediatrHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await mediator.Publish(evento);
        }
    }
}
