using FluentValidation;
using FluentValidation.Results;
using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace NerdStore.Vendas.Domain
{
    public class Voucher : Entity
    {
        public string Codigo { get; set; }
        public decimal? Percentual { get; set; }
        public decimal? ValorDesconto { get; set; }
        public int Quantidade { get; set; }
        public TipoDescontoVoucher TipoDescontoVoucher { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataUtilizacao { get; set; }
        public DateTime DataValidade { get; set; }
        public bool Ativo { get; set; }
        public bool Utilizado { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }

        internal ValidationResult ValidarSeAplicavel()
        {
            return new VoucherAplicavelValidation().Validate(this);
        }
    }
    public class VoucherAplicavelValidation : AbstractValidator<Voucher>
    {

        public VoucherAplicavelValidation()
        {
            RuleFor(c => c.DataValidade)
                .Must(DataVencimentoSuperiorAtual)
                .WithMessage("Este voucher está expirado.");

            RuleFor(c => c.Ativo)
                .Equal(true)
                .WithMessage("Este voucher não é mais válido.");

            RuleFor(c => c.Utilizado)
                .Equal(false)
                .WithMessage("Este voucher já foi utilizado.");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage("Este voucher não está mais disponível");
        }

        protected static bool DataVencimentoSuperiorAtual(DateTime dataValidade)
        {
            return dataValidade >= DateTime.Now;
        }
    }
}
