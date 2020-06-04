using NerdStore.Core.Messages;
using System;

namespace NerdStore.Vendas.Application.Events
{
    public class PedidoAtualizadoEvent : Event
    {
        public Guid ClienteId { get; set; }
        public Guid PedidoId { get; set; }
        public decimal ValorTotal { get; set; }

        public PedidoAtualizadoEvent(Guid clienteId, Guid pedidoId, decimal valorTotal)
        {
            AggregateId = pedidoId;
            ClienteId = clienteId;
            PedidoId = pedidoId;
            ValorTotal = valorTotal;
        }
    }
}
