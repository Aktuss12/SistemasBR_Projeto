using System.ComponentModel;

namespace CadastroDeProdutosView.Features.Produto.Enums
{
    public class NaturezaDaOperacaoView
    {
        public enum NaturezaDaOperacao
        {
            [Description("Venda")]
            Venda,
            [Description("Devolução")]
            Devolucao,
            [Description("Complementar")]
            Complementar,
            [Description("Retorno")]
            Retorno,
            [Description("Venda Consignada")]
            VendaConsignada,
            [Description("Remessa")]
            EntregaFutura,
        }
    }
}
