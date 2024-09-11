using System.ComponentModel;

namespace CadastroDeProdutosView.Features.Produto.Enums
{
    public class MarcaDoProdutoView
    {
        public enum MarcaDoProduto
        {
            [Description("Natura")] Natura,
            [Description("Granado")] Granado,
            [Description("Ambev")] Ambev,
            [Description("Natural da Terra")] NaturalDaTerra,
            [Description("Localiza")] Localiza,
            [Description("Ypê")] Ypê
        }

    }
}