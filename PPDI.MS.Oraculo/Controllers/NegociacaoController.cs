using Microsoft.AspNetCore.Mvc;
using PPDI.MS.Oraculo.Domain;
using PPDI.MS.Oraculo.DTO;
using PPDI.MS.Oraculo.Repository;

namespace PPDI.MS.Oraculo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NegociacaoController : ControllerBase
    {
        private readonly INegociacaoRepostory _negociacaoRepostory;

        public NegociacaoController(INegociacaoRepostory negociacaoRepostory) =>        
            _negociacaoRepostory = negociacaoRepostory;

        [HttpPost]
        public async Task<IActionResult> CriarNovaNegociacao(NovaNegociacaoDTO novaNegociacaoDTO)
        {
            Negociacao negociacao = new Negociacao(
                novaNegociacaoDTO.TaxaPactuada,
                novaNegociacaoDTO.ValorPactuado,
                novaNegociacaoDTO.MoedaPactuada
            );

            await _negociacaoRepostory.InserirNegociacaoAsync(negociacao);

            return Ok(negociacao.Id);
        }

        [HttpGet]
        public async Task<IActionResult> CriarNovaNegociacao(string Id) 
        {
            var negociacao = await _negociacaoRepostory.BuscarNegociacao(Id);
            var negociacaoDTO = new NegociacaoDTO
            {
                Id = negociacao.Id.ToString(),
                EstaFinalizada = negociacao.FimNegociacao != null,
                MoedaPactuada = negociacao.MoedaPactuada,
                TaxaPactuada = negociacao.TaxaPactuada,
                ValorPactuado = negociacao.ValorPactuado
            };

            return Ok(negociacaoDTO);
        }
    }
}
