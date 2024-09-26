namespace CadastroDeProdutosView.Features.Commons
{
    public static class LimparLookUpEdits
    {
        public static void LimparLookUpEdit(
            DevExpress.XtraEditors.LookUpEdit unidadeDeMedidaLookUpEdit,
            DevExpress.XtraEditors.LookUpEdit categoriaDeProdutosLookUpEdit,
            DevExpress.XtraEditors.LookUpEdit marcaLookUpEdit,
            DevExpress.XtraEditors.LookUpEdit origemDaMercadoriaLookUpEdit,
            DevExpress.XtraEditors.LookUpEdit situacaoTributariaLookUpEdit,
            DevExpress.XtraEditors.LookUpEdit naturezaDaOperacaoLookUpEdit)
        {
            unidadeDeMedidaLookUpEdit.EditValue = null;
            categoriaDeProdutosLookUpEdit.EditValue = null;
            marcaLookUpEdit.EditValue = null;
            origemDaMercadoriaLookUpEdit.EditValue = null;
            situacaoTributariaLookUpEdit.EditValue = null;
            naturezaDaOperacaoLookUpEdit.EditValue = null;
        }
    }
}
