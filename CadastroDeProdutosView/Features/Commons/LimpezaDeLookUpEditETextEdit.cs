namespace CadastroDeProdutosView.Features.Commons
{
    public static class LimparLookUpEditETextEdit
    {
        public static void LimpezaDeLookUpEdit(this DevExpress.XtraEditors.LookUpEdit lookUpEditNull)
        {
            lookUpEditNull.EditValue = null;
        }

        public static void LimpezaDeTextEdit(this DevExpress.XtraEditors.TextEdit textEditsNull)
        {
            textEditsNull.Text = null;
        }
    }
}
