namespace CadastroDeProdutosView.Features.Produto.Models
{
    public class Produtos
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string? Categoria { get; set; }

        public string? Fornecedor { get; set; }

        public string? CodigoDeBarras { get; set; }

        public string? UnidadeDeMedida { get; set; }

        public int Estoque { get; set; }

        public string? Marca { get; set; }

        public decimal Custo { get; set; }

        public decimal Markup { get; set; }

        public decimal PrecoDaVenda { get; set; }

        public bool Ativo { get; set; } = true; 

        public byte[]? Imagem { get; set; }

        public InformacoesFiscais InformacoesFiscais { get; set; } = new InformacoesFiscais();
    }
}
