using System.ComponentModel;

namespace CadastroDeProdutosView.Features.Produto.Enums
{
    public class OrigemDaMercadoriaView
    {
        public enum OrigemDaMercadoria
        {
            [Description("0 - Nacional, exceto as indicadas nos códigos 3, 4, 5 e 8")]
            Nacional,
            [Description("1 - Estrangeira - Importação direta, exceto a indicada no código 6")]
            Estrangeira,
            [Description("2 - Estrangeira - Adquirida no mercado interno, exceto a indicada no código 7")]
            Estrangeira2,
            [Description("3 - Nacional, mercadoria ou bem com Conteúdo de Importação superior a 40% (quarenta por cento) e inferior ou igual a 70% (setenta por cento)\r\n")]
            Nacional3,
            [Description("4 - Nacional, cuja produção tenha sido feita em conformidade com os processos produtivos básicos de que tratam o Decreto-Lei no 288/67, e as Leis nos 8.248/91, 8.387/91, 10.176/01 e 11.484/07")]
            Nacional4,
            [Description("5 - Nacional, mercadoria ou bem com Conteúdo de Importação inferior ou igual a 40% (quarenta por cento)")]
            Nacional5,
            [Description("6 - Estrangeira - Importação direta, sem similar nacional, constante em lista de Resolução CAMEX e gás natural")]
            Estrangeira6,
            [Description("7 - Estrangeira - Adquirida no mercado interno, sem similar nacional, constante em lista de Resolução CAMEX e gás natural")]
            Estrangeira7,
            [Description("8 - Nacional, mercadoria ou bem com Conteúdo de Importação superior a 70% (setenta por cento)")]
            Nacional8
        }
    }
}