using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using PPDI.MS.Oraculo.Domain;

namespace PPDI.MS.Oraculo.Repository
{
    public interface INegociacaoRepostory
    {
        Task InserirNegociacaoAsync(Negociacao negociacao);
        Task AtualizarFechamentoAsync(Negociacao negociacao);
        Task<Negociacao> BuscarNegociacao(string Id);
    }

    public class NegociacaoRepository : INegociacaoRepostory
    {
        private readonly AmazonDynamoDBConfig _amazonDynamoDBConfig;
        private readonly AmazonDynamoDBClient _amazonDynamoDBClient;
        private readonly Table _tabela;

        public NegociacaoRepository(IConfiguration configuration)
        {
            _amazonDynamoDBConfig = new AmazonDynamoDBConfig();
            _amazonDynamoDBConfig.RegionEndpoint = RegionEndpoint.AFSouth1;
            _amazonDynamoDBConfig.ServiceURL = configuration.GetValue<string>("service_url");

            _amazonDynamoDBClient = new AmazonDynamoDBClient(_amazonDynamoDBConfig);

            _tabela = Table.LoadTable(_amazonDynamoDBClient, "NegociacaoTable");
        }

        public async Task AtualizarFechamentoAsync(Negociacao negociacao)
        {
            if (negociacao.FimNegociacao == null)
                throw new InvalidOperationException("Negociação não fechada");

            var item = await _tabela.GetItemAsync(negociacao.Id);
            item["FimNegociacao"] = negociacao.FimNegociacao;

            await _tabela.PutItemAsync(item);
        }

        public async Task<Negociacao> BuscarNegociacao(string Id)
        {
            var item = await _tabela.GetItemAsync(Guid.Parse(Id));

            return new Negociacao(
                id: Guid.Parse(item["Id"]),
                inicioNegociacao: DateTime.Parse(item["InicioNegociacao"]),
                fimNegociacao: DateTime.Parse(item["FimNegociacao"]),
                taxaPactuada: decimal.Parse(item["TaxaPactuada"]),
                valorPactuado: decimal.Parse(item["ValorPactuado"]),
                moedaPactuada: Convert.ToString(item["MoedaPactuada"])
            );
        }

        public async Task InserirNegociacaoAsync(Negociacao negociacao)
        {
            var negociacaoDocument = new Document();

            negociacaoDocument["Id"] = negociacao.Id;
            negociacaoDocument["InicioNegociacao"] = negociacao.InicioNegociacao;
            negociacaoDocument["ValorPactuado"] = negociacao.ValorPactuado;
            negociacaoDocument["TaxaPactuada"] = negociacao.TaxaPactuada;
            negociacaoDocument["MoedaPactuada"] = negociacao.MoedaPactuada;

            await _tabela.PutItemAsync(negociacaoDocument);
        }


    }
}
