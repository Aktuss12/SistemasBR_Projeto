using System.ComponentModel;

namespace CadastroDeProdutosView.Features.Produto.Enums
{
    public class MarcaDoProdutoView
    {
        public enum MarcaDoProduto
        {
            [Description("Natural da Terra")] NaturalDaTerra,
            [Description("Saboraki")] Saboraki,
            [Description("Localiza")] Localiza,
            [Description("Granado")] Granado,
            [Description("Natura")] Natura,
            [Description("Ambev")] Ambev,
            [Description("Ypê")] Ypê
        }
    }
}