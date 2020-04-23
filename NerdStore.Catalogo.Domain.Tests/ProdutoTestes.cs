using NerdStore.Core.DomainObjects;
using System;
using Xunit;
using NerdStore.Catalogo.Domain;

namespace NerdStore.Catalogo.Domain.Tests
{
    public class ProdutoTestes
    {
        [Fact]
        public void Produto_Validar_ValidacoesDevemRetornarExceptions()
        {
            var ex = Assert.Throws<DomainException>(() =>
            new Produto(string.Empty, "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, "imagem", new Dimensoes(1, 1, 1)));

            Assert.Equal("O campo Nome do produto n�o pode estar vazio", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
            new Produto("Nome", string.Empty, false, 100, Guid.NewGuid(), DateTime.Now, "imagem", new Dimensoes(1, 1, 1)));

            Assert.Equal("O campo Descricao do produto n�o pode estar vazio", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
            new Produto("Nome", "Descricao", false, 0, Guid.NewGuid(), DateTime.Now, "imagem", new Dimensoes(1, 1, 1)));

            Assert.Equal("O campo Valor do produto n�o pode ser menor que 1", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
            new Produto("Nome", "Descricao", false, 100, Guid.Empty, DateTime.Now, "imagem", new Dimensoes(1, 1, 1)));

            Assert.Equal("O campo CategoriaId do produto n�o pode ser vazio", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
            new Produto("Nome", "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, string.Empty, new Dimensoes(1, 1, 1)));

            Assert.Equal("O campo Imagem do produto n�o pode estar vazio", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
            new Produto("Nome", "Descricao", false, 100, Guid.NewGuid(), DateTime.Now, string.Empty, new Dimensoes(0, 1, 1)));

            Assert.Equal("O Campo Altura n�o pode ser menos ou igual a 0", ex.Message);
        }
    }
}
