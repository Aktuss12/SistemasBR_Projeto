using System.ComponentModel;

namespace CadastroDeProdutosView.Features.Produto.Enums
{
    public class CategoriaDoProdutoView
    {
        public enum CategoriaDeProdutos
        {
            [Description("Produto")]
            Produto,
            [Description("Higiene")]
            Higiene,
            [Description("Bebidas")]
            Bebidas,
            [Description("Hortfrúiti")]
            Hortifruiti,
            [Description("Serviço")]
            Servico,
            [Description("Limpeza")]
            Limpeza
        }
    }
}