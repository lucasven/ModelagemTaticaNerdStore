using MediatR;
using NerdStore.Core.Data.EventSource;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Core.Bus
{
    public class MediatrHandler : IMediatorHandler
    {
        private readonly IMediator mediator;
        private readonly IEventSourcingRepository eventSourcingRepository;

        public MediatrHandler(IMediator mediator, IEventSourcingRepository eventSourcingRepository)
        {
            this.mediator = mediator;
            this.eventSourcingRepository = eventSourcingRepository;
        }

        public MediatrHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<bool> EnviarComando<T>(T comando) where T : Command
        {
            return await mediator.Send(comando);
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await mediator.Publish(evento);

            if (!evento.GetType().BaseType.Name.Equals("DomainEvent"))
                await eventSourcingRepository.SalvarEvento(evento);
        }

        public async Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification
        {
            await mediator.Publish(notificacao);
        }
    }
}
