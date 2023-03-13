namespace PPDI.MS.Oraculo.DTO
{
    public class NegociacaoDTO
    {
        public string Id { get; set; }
        public decimal TaxaPactuada { get; set; }
        public decimal ValorPactuado { get; set; }
        public string? MoedaPactuada { get; set; }
        public bool EstaFinalizada { get; set; }
    }
}
