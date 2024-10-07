using DevExpress.XtraEditors;

namespace CadastroDeProdutosView.Features.Commons
{
    public static class ValidacaoDeCamposObrigatorios
    {
        public static bool ValidacaoParaCamposObrigatorios(
            TextEdit nomeTextEdit,
            TextEdit estoqueTextEdit,
            TextEdit precoVendaTextEdit,
            LookUpEdit unidadeDeMedidaLookUpEdit,
            LookUpEdit categoriaDeProdutosLookUpEdit,
            LabelControl nomeLabelControl,
            LabelControl estoqueLabelControl,
            LabelControl precoDaVendaLabelControl,
            LabelControl unidadeDeMedidaLabelControl,
            LabelControl categoriaLabelControl)
        {
            var todosCamposPreenchidos = true;
            todosCamposPreenchidos &= !string.IsNullOrWhiteSpace(nomeTextEdit.Text);
            todosCamposPreenchidos &= !string.IsNullOrWhiteSpace(estoqueTextEdit.Text);
            todosCamposPreenchidos &= !string.IsNullOrWhiteSpace(precoVendaTextEdit.Text);
            todosCamposPreenchidos &= unidadeDeMedidaLookUpEdit.EditValue != null;
            todosCamposPreenchidos &= categoriaDeProdutosLookUpEdit.EditValue != null;

            nomeLabelControl.Text = string.IsNullOrWhiteSpace(nomeTextEdit.Text)
                ? "Nome: <color=red>*</color>"
                : "Nome: *";
            nomeLabelControl.AllowHtmlString = string.IsNullOrWhiteSpace(nomeTextEdit.Text);

            estoqueLabelControl.Text = string.IsNullOrWhiteSpace(estoqueTextEdit.Text)
                ? "Estoque: <color=red>*</color>"
                : "Estoque: *";
            estoqueLabelControl.AllowHtmlString = string.IsNullOrWhiteSpace(estoqueTextEdit.Text);

            precoDaVendaLabelControl.Text = string.IsNullOrWhiteSpace(precoVendaTextEdit.Text)
                ? "Preço da Venda: <color=red>*</color>"
                : "Preço da Venda: *";
            precoDaVendaLabelControl.AllowHtmlString = string.IsNullOrWhiteSpace(precoVendaTextEdit.Text);

            unidadeDeMedidaLabelControl.Text = unidadeDeMedidaLookUpEdit.EditValue == null
                ? "Und. de Medida: <color=red>*</color>"
                : "Und. de Medida: *";
            unidadeDeMedidaLabelControl.AllowHtmlString = unidadeDeMedidaLookUpEdit.EditValue == null;

            categoriaLabelControl.Text = categoriaDeProdutosLookUpEdit.EditValue == null
                ? "Categoria: <color=red>*</color>"
                : "Categoria: *";
            categoriaLabelControl.AllowHtmlString = categoriaDeProdutosLookUpEdit.EditValue == null;

            return todosCamposPreenchidos;
        }
    }
}