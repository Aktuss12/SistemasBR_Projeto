namespace CadastroDeProdutosView.Features.Commons
{
    public static class ValidacaoDeCampos
    {
        public static bool ValidacaoParaCamposObrigatorios(
            DevExpress.XtraEditors.TextEdit nomeTextEdit,
            DevExpress.XtraEditors.TextEdit estoqueTextEdit,
            DevExpress.XtraEditors.TextEdit precoDaVendaTextEdit,
            DevExpress.XtraEditors.LookUpEdit unidadeDeMedidaLookUpEdit,
            DevExpress.XtraEditors.LookUpEdit categoriaDeProdutosLookUpEdit,
            DevExpress.XtraEditors.LabelControl nomeLabelControl,
            DevExpress.XtraEditors.LabelControl estoqueLabelControl,
            DevExpress.XtraEditors.LabelControl precoDaVendaLabelControl,
            DevExpress.XtraEditors.LabelControl unidadeDeMedidaLabelControl,
            DevExpress.XtraEditors.LabelControl categoriaLabelControl)
        {
            var camposPreenchidos = true;
            camposPreenchidos &= !string.IsNullOrWhiteSpace(nomeTextEdit.Text);
            camposPreenchidos &= !string.IsNullOrWhiteSpace(estoqueTextEdit.Text);
            camposPreenchidos &= !string.IsNullOrWhiteSpace(precoDaVendaTextEdit.Text);
            camposPreenchidos &= unidadeDeMedidaLookUpEdit.EditValue != null;
            camposPreenchidos &= categoriaDeProdutosLookUpEdit.EditValue != null;

            nomeLabelControl.Text = string.IsNullOrWhiteSpace(nomeTextEdit.Text)
                ? "Nome: <color=red>*</color>"
                : "Nome: *";
            nomeLabelControl.AllowHtmlString = string.IsNullOrWhiteSpace(nomeTextEdit.Text);

            estoqueLabelControl.Text = string.IsNullOrWhiteSpace(estoqueTextEdit.Text)
                ? "Estoque: <color=red>*</color>"
                : "Estoque: *";
            estoqueLabelControl.AllowHtmlString = string.IsNullOrWhiteSpace(estoqueTextEdit.Text);

            precoDaVendaLabelControl.Text = string.IsNullOrWhiteSpace(precoDaVendaTextEdit.Text)
                ? "Preço da Venda: <color=red>*</color>"
                : "Preço da Venda: *";
            precoDaVendaLabelControl.AllowHtmlString = string.IsNullOrWhiteSpace(precoDaVendaTextEdit.Text);

            unidadeDeMedidaLabelControl.Text = unidadeDeMedidaLookUpEdit.EditValue == null
                ? "Und. de Medida: <color=red>*</color>"
                : "Und. de Medida: *";
            unidadeDeMedidaLabelControl.AllowHtmlString = unidadeDeMedidaLookUpEdit.EditValue == null;

            categoriaLabelControl.Text = categoriaDeProdutosLookUpEdit.EditValue == null
                ? "Categoria: <color=red>*</color>"
                : "Categoria: *";
            categoriaLabelControl.AllowHtmlString = categoriaDeProdutosLookUpEdit.EditValue == null;

            return camposPreenchidos;
        }
    }
}
