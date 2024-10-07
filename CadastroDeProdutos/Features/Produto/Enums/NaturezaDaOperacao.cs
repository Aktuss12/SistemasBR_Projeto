using System.ComponentModel;

namespace CadastroDeProdutosView.Features.Produto.Enums
{
    public class NaturezaDaOperacaoView
    {
        public enum NaturezaDaOperacao
        {
            [Description("5101 - Venda de produção do estabelecimento")]
            VendaDeProducaoDoEstabelecimento,
            [Description("5102 - Venda de mercadoria adquirida ou recebida de terceiros")]
            VendaDeMercadoriaAdiquiridaOuRecebidaDeTerceiros,
            [Description("5103 - Venda de produção do estabelecimento com venda para o exterior")]
            VendaDeProducaoDoEstabelecimentoComVendaParaOExterior,
            [Description("6101 - Compra para industrialização")]
            CompraParaIndustrializacao,
            [Description("6102 - Compra para comercialização")]
            CompraParaComercializacao,
            [Description("5201 - Remessa de mercadoria para industrialização por encomenda")]
            RemessaDeMercadoriaParaIndustrializacaoPorEncomenda,
            [Description("5202 - Remessa de mercadoria para industrialização por conta e ordem de terceiro")]
            RemessaDeMercadoriaParaIndustrializacaoPorContaEOrdemDeTerceiro,
            [Description("5203 - Remessa de mercadoria para venda fora do estabelecimento")]
            RemessaDeMercadoriaParaVendaForaDoEstabelecimento,
            [Description("5401 - Devolução de venda de produção do estabelecimento")]
            DevolucaoDeVendaDeProducaoDoEstabelecimento,
            [Description("5402 - Devolução de venda de mercadoria adquirida ou recebida de terceiros")]
            DevolucaoDeVendaDeMercadoriaAdiquiridaOuRecebidaDeTerceiros,
            [Description("6201 - Importação de mercadorias para o ativo imobilizado")]
            ImportacaoDeMercadoriasParaOAtivoimobilizado,
            [Description("6202 - Importação de mercadorias para revenda")]
            ImportacaoDeMercadoriasParaRevenda,
            [Description("6301 - Prestação de serviço de transporte")]
            PrestacaoDeServicoDeTransporte,
            [Description("6302 - Prestação de serviço de comunicação")]
            PrestacaoDeServicoDeComunicacao,
            [Description("9901 - Ajuste de ICMS por substituição tributária")]
            AjusteDeIcmsPorSubstituicaoTributaria,
            [Description("9902 - Ajuste de ICMS por diferencial de alíquota")]
            AjusteDeIcmsPorDiferencialDeAliquota
        }
    }
}
