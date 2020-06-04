using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Bus;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.WebApp.MVC.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private readonly DomainNotificationHandler notifications;
        private readonly IMediatorHandler mediatorHandler;

        protected Guid ClienteId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

        public ControllerBase(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediatorHandler)
        {
            this.notifications = (DomainNotificationHandler)notifications;
            this.mediatorHandler = mediatorHandler;
        }

        protected bool OperacaoValida()
        {
            return !notifications.TemNotificacao();
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
        }

        protected IEnumerable<string> ObterMensagensErro()
        {
            return notifications.ObterNotificacoes().Select(c => c.Value).ToList();
        }
    }
}
