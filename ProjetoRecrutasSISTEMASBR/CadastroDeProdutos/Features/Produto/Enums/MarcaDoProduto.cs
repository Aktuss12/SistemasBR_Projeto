using System.ComponentModel;

namespace CadastroDeProdutosView.Features.Produto.Enums
{
    public class MarcaDoProdutoView
    {
        public enum MarcaDoProduto
        {
            [Description("Nestle")] 
            Nestle,
            [Description("Saboraki")] 
            Saboraki,
            [Description("OMO")]
            OMO,
            [Description("Colgate")] 
            Colgate,
            [Description("Nivea")]
            Nivea,
            [Description(" Pão de Açúcar")]
            PaoDeAcucar,
            [Description("Redbull")]
            Redbull,
            [Description("Ypê")]
            Ype,
            [Description("Bombrill")]
            Bombrill,
            [Description("Dove")] 
            Dove
        }
    }
}