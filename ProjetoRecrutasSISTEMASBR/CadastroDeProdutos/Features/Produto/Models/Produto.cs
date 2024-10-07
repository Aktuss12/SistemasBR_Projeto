namespace CadastroDeProdutosView.Features.Produto.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Categoria { get; set; }
        public string? Fornecedor { get; set; }
        public string? CodigoDeBarras { get; set; }
        public string? UnidadeDeMedida { get; set; }
        public int Estoque { get; set; }
        public string? Marca { get; set; }
        public decimal Custo { get; set; }
        public decimal Markup { get; set; }
        public decimal PrecoDaVenda { get; set; }
        public byte[]? Imagem { get; set; }
        public InformacoesFiscais InformacoesFiscais { get; set; } = null!;
    }
}
