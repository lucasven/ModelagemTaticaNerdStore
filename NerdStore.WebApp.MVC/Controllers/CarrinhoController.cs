using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services;
using NerdStore.Core.Bus;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Application.Queries;
using NerdStore.Vendas.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class CarrinhoController : ControllerBase
    {
        private readonly IProdutoAppService produtoAppService;
        private readonly IMediatorHandler mediatorHandler;
        private readonly IPedidoQueries pedidoQueries;

        public CarrinhoController(INotificationHandler<DomainNotification> notifications, 
            IProdutoAppService produtoAppService, 
            IMediatorHandler mediatorHandler, 
            IPedidoQueries pedidoQueries) 
            : base(notifications, mediatorHandler)
        {
            this.produtoAppService = produtoAppService;
            this.mediatorHandler = mediatorHandler;
            this.pedidoQueries = pedidoQueries;
        }

        [Route("meu-carrinho")]
        public async Task<IActionResult> Index()
        {
            return View(await pedidoQueries.ObterCarrinhoCliente(ClienteId));
        }

        [HttpPost]
        [Route("meu-carrinho")]
        public async Task<IActionResult> AdicionarItem(Guid id, int quantidade)
        {
            var produto = await produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            if(produto.QuantidadeEstoque < quantidade)
            {
                TempData["Erro"] = "Produto com estoque insuficiente";
                return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id }) ;
            }

            var command = new AdicionarItemPedidoCommand(ClienteId, produto.Id, produto.Nome, quantidade, produto.Valor);
            await mediatorHandler.EnviarComando(command);

            if(OperacaoValida())
            {
                return RedirectToAction("Index");
            }

            TempData["Erros"] = ObterMensagensErro();
            return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
        }

        [HttpPost]
        [Route("remover-item")]
        public async Task<IActionResult> RemoverItem(Guid id)
        {
            var produto = await produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            var command = new RemoverItemPedidoCommand(ClienteId, id);
            await mediatorHandler.EnviarComando(command);

            if (OperacaoValida())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await pedidoQueries.ObterCarrinhoCliente(ClienteId));
        }

        [HttpPost]
        [Route("atualizar-item")]
        public async Task<IActionResult> AtualizarItem(Guid id, int quantidade)
        {
            var produto = await produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            var command = new AtualizarItemPedidoCommand(ClienteId, id, quantidade);
            await mediatorHandler.EnviarComando(command);

            if (OperacaoValida())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await pedidoQueries.ObterCarrinhoCliente(ClienteId));
        }

        [HttpPost]
        [Route("aplicar-voucher")]
        public async Task<IActionResult> AplicarVoucher(string voucherCodigo)
        {
            var command = new AplicarVoucherPedidoCommand(ClienteId, voucherCodigo);
            await mediatorHandler.EnviarComando(command);

            if (OperacaoValida())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await pedidoQueries.ObterCarrinhoCliente(ClienteId));
        }

        [Route("resumo-da-compra")]
        public async Task<IActionResult> ResumoDaCompra()
        {
            return View(await pedidoQueries.ObterCarrinhoCliente(ClienteId));
        }

        //[HttpPost]
        //[Route("iniciar-pedido")]
        //public async Task<IActionResult> IniciarPedido(CarrinhoViewModel carrinhoViewModel)
        //{
        //    var carrinho = await pedidoQueries.ObterCarrinhoCliente(ClienteId);

        //    var command = new IniciarPedidoCommand(carrinho.PedidoId, ClienteId, carrinho.ValorTotal, carrinhoViewModel.Pagamento.NomeCartao,
        //        carrinhoViewModel.Pagamento.NumeroCartao, carrinhoViewModel.Pagamento.ExpiracaoCartao, carrinhoViewModel.Pagamento.CvvCartao);

        //    await mediatorHandler.EnviarComando(command);

        //    if (OperacaoValida())
        //    {
        //        return RedirectToAction("Index", "Pedido");
        //    }

        //    return View("ResumoDaCompra", await pedidoQueries.ObterCarrinhoCliente(ClienteId));
        //}
    }
}
