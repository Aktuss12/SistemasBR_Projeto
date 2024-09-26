namespace CadastroDeProdutosView.Features.Commons
{
    public static class LimparLookUpEditETextEdit
    {
        public static void LimparLookUpEdit(this DevExpress.XtraEditors.LookUpEdit lookUpEditNull)
        {
            lookUpEditNull.EditValue = null;
        }

        public static void LimparTextEdit(this DevExpress.XtraEditors.TextEdit textEditsNull)
        {
            textEditsNull.Text = null;
        }
    }
}
