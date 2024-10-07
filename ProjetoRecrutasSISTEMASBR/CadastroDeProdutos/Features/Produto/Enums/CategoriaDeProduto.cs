using System.ComponentModel;

namespace CadastroDeProdutosView.Features.Produto.Enums
{
    public class CategoriaDoProdutoView
    {
        public enum CategoriaDeProdutos
        {
            [Description("Alimentos")] Alimentos,
            [Description("Higiene")] Higiene,
            [Description("Bebidas")] Bebidas,
            [Description("Hortifrúti")] Hortifruiti,
            [Description("Limpeza")] Limpeza,
            [Description("Papelaria")] Papelaria,
            [Description("Cosméticos")] Cosmeticos,
            [Description("Eletrônicos")] Eletronicos,
            [Description("Roupas")] Roupas,
            [Description("Serviço")] Servico
        }
    }
}