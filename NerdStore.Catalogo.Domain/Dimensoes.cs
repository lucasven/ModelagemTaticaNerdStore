using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace NerdStore.Catalogo.Domain
{
    public class Dimensoes : Entity
    {
        public decimal Altura { get; private set; }
        public decimal Largura { get; private set; }
        public decimal Profundidade { get; private set; }

        public Dimensoes(decimal altura, decimal largura, decimal profundidade)
        {
            Validacoes.ValidarSeMenorQue(altura, 1, "O Campo Altura não pode ser menos ou igual a 0");
            Validacoes.ValidarSeMenorQue(largura, 1, "O Campo Largura não pode ser menos ou igual a 0");
            Validacoes.ValidarSeMenorQue(profundidade, 1, "O Campo Profundidade não pode ser menos ou igual a 0");

            Altura = altura;
            Largura = largura;
            Profundidade = profundidade;
        }

        public string DescricaoFormatada()
        {
            return $"LxAxP {Largura} x {Altura} x {Profundidade}";
        }
    }
}
