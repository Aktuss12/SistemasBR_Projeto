using System.Runtime.CompilerServices;

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

        public static void LimpezaDeImageBox(this DevExpress.XtraEditors.PictureEdit pictureBoxNull)
        {

        }

    }
}
