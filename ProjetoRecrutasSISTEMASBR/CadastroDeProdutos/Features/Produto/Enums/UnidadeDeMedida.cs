using System.ComponentModel;

namespace CadastroDeProdutosView.Features.Produto.Enums
{
    public class UnidadeDeMedidaView
    {
        public enum UnidadeDeMedida
        {
            [Description("PALETE")]
            Palete,
            [Description("FARDO")]
            Fardo,
            [Description("LATA")]
            Lata,
            [Description("UN")]
            Unidade,
            [Description("CM")]
            Centimetro,
            [Description("KG")] 
            Quilos,
            [Description("CX")]
            Caixa,
            [Description("G")] 
            Grama,
            [Description("M²")] 
            MetroQuadrado,
            [Description("L")]
            Litros,
            [Description("M")]
            Metros,
            [Description("ML")] 
            Mililitros
        }
    }
}