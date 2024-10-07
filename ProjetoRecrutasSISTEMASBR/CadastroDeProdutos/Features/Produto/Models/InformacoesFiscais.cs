namespace CadastroDeProdutosView.Features.Produto.Models
{
    public class InformacoesFiscais
    {
        public string? OrigemDaMercadoria { get; set; }

        public string? Ncm { get; set; }

        public string? SituacaoTributaria { get; set; }

        public string? NaturezaDaOperacao { get; set; }

        public decimal AliquotaDeIcms { get; set; }

        public decimal ReducaoDeCalculo { get; set; }
    }
}
