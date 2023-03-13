using Microsoft.AspNetCore.Mvc.Formatters;

namespace PPDI.MS.Oraculo.Domain
{
    public class Negociacao
    {
        public Guid Id { get; private set; }
        public DateTime InicioNegociacao { get; private set; }
        public DateTime? FimNegociacao { get; private set; }
        public decimal TaxaPactuada { get; private set; }
        public decimal ValorPactuado { get; private set; }
        public string MoedaPactuada { get; private set; }

        public Negociacao(decimal taxaPactuada, decimal valorPactuado, string moedaPactuada)
        {
            Id = Guid.NewGuid();
            InicioNegociacao = DateTime.UtcNow;
            TaxaPactuada = taxaPactuada;
            ValorPactuado = valorPactuado;
            MoedaPactuada = moedaPactuada;
        }

        public Negociacao(Guid id, DateTime inicioNegociacao, DateTime? fimNegociacao, decimal taxaPactuada, decimal valorPactuado, string moedaPactuada)
        {
            Id = id;
            InicioNegociacao = inicioNegociacao;
            FimNegociacao = fimNegociacao;
            TaxaPactuada = taxaPactuada;
            ValorPactuado = valorPactuado;
            MoedaPactuada = moedaPactuada;
        }

        public void FinalizarNegociacao()
        {
            if (FimNegociacao != null)
                throw new InvalidOperationException("Negociação finalizada anteriormente");

            FimNegociacao = DateTime.UtcNow;
        }
    }
}
