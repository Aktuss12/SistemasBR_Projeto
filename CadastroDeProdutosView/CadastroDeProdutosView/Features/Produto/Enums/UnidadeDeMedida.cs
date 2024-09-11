using System.ComponentModel;

namespace CadastroDeProdutosView.Features.Produto.Enums
{
    public partial class CadastroDeProdutosView
    {
        public enum UnidadeDeMedida
        {
            [Description("UN")]
            Unidade,
            [Description("KG")]
            Quilos,
            [Description("LT")]
            Litros,
            [Description("MT")]
            Metros
        }
    }
}
