using System.ComponentModel;

namespace CadastroDeProdutosView.Features.Produto.Enums
{
    public partial class CadastroDeProdutosView
    {
        public enum UnidadeDeMedida
        {
            [Description("UN")]
            Unidade,
            [Description("LTA")]
            Lata,
            [Description("KG")]
            Quilos,
            [Description("G")]
            Grama,
            [Description("M²")]
            MetroCubico,
            [Description("CM")]
            Centimetro,
            [Description("PALETE")]
            Palete,
            [Description("CX")]
            Caixa,
            [Description("FARDO")]
            Fardo,
            [Description("LT")]
            Litros,
            [Description("M")]
            Metros,
        }
    }
}
