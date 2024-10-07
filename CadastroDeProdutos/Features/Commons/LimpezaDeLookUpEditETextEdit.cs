using DevExpress.XtraEditors;

namespace CadastroDeProdutosView.Features.Commons
{
    public static class LimparLookUpEditETextEdit
    {
        public static void LimpezaDeLookUpEdit(this LookUpEdit lookUpEditNull)
        {
            lookUpEditNull.EditValue = null;
        }

        public static void LimpezaDeTextEdit(this TextEdit textEditsNull)
        {
            textEditsNull.Text = null;
        }

        public static void LimpezaDeImageBox(this PictureEdit pictureBoxNull)
        {
            pictureBoxNull.Image = null;
        }
    }
}