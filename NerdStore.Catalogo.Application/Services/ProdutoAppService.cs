using AutoMapper;
using NerdStore.Catalogo.Application.ViewModels;
using NerdStore.Catalogo.Domain;
using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IMapper mapper;
        private readonly IEstoqueService estoqueService;

        public ProdutoAppService(IProdutoRepository produtoRepository, IMapper mapper,
            IEstoqueService estoqueService)
        {
            this.produtoRepository = produtoRepository;
            this.mapper = mapper;
            this.estoqueService = estoqueService;
        }

        public async Task AdicionarProduto(ProdutoViewModel produtoViewModel)
        {
            var produto = mapper.Map<Produto>(produtoViewModel);
            produtoRepository.Adicionar(produto);

            await produtoRepository.UnitOfWork.Commit();
        }

        public async Task AtualizarProduto(ProdutoViewModel produtoViewModel)
        {
            var produto = mapper.Map<Produto>(produtoViewModel);
            produtoRepository.Atualizar(produto);

            await produtoRepository.UnitOfWork.Commit();
        }

        public async Task<ProdutoViewModel> DebitarEstoque(Guid id, int quantidade)
        {
            if(!estoqueService.DebitarEstoque(id, quantidade).Result)
            {
                throw new DomainException("Falha ao debitar estoque");
            }

            return mapper.Map<ProdutoViewModel>(await produtoRepository.ObterPorId(id));
        }
        public async Task<ProdutoViewModel> ReporEstoque(Guid id, int quantidade)
        {
            if (!estoqueService.ReporEstoque(id, quantidade).Result)
            {
                throw new DomainException("Falha ao repor estoque");
            }

            return mapper.Map<ProdutoViewModel>(await produtoRepository.ObterPorId(id));
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterCategorias()
        {
            return mapper.Map<IEnumerable<CategoriaViewModel>>(await produtoRepository.ObterCategorias());
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo)
        {
            return mapper.Map<IEnumerable<ProdutoViewModel>>(await produtoRepository.ObterPorCategoria(codigo));
        }

        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {
            return mapper.Map<ProdutoViewModel>(await produtoRepository.ObterPorId(id));
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            return mapper.Map<IEnumerable<ProdutoViewModel>>(await produtoRepository.ObterTodos());
        }

        public void Dispose()
        {
            produtoRepository?.Dispose();
            estoqueService?.Dispose();
        }
    }
}
