using DevExpress.XtraEditors;

namespace CadastroDeProdutosView.Features.Commons
{
    public static class CalculoDeCustoEMarkupParaPrecoVenda
    {
        public static void CalcularPrecoVenda(decimal custo, decimal markup, TextEdit precoVendaTextEdit)
        {
            if (custo >= 0 && markup >= 0)
            {
                var precoVenda = custo *(1 + (markup / 100));
                precoVendaTextEdit.Text = precoVenda.ToString("F2");
            }
            else
            {
                precoVendaTextEdit.Text = string.Empty;
            }
        }
    }
}
