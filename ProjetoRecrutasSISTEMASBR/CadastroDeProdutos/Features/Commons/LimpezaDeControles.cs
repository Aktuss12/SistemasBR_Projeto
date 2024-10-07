using DevExpress.XtraEditors;

namespace CadastroDeProdutosView.Features.Commons
{
    public static class LimpezaDeControles
    {
        public static void LimpezaDeTextEdit(this TextEdit textEdit)
        {
            textEdit.Text = null;
        }

        public static void LimpezaDeLookUpEdit(this LookUpEdit lookUpEdit)
        {
            lookUpEdit.EditValue = null;
        }

        public static void LimpezaDeImageBox(this PictureEdit pictureEdit)
        {
            pictureEdit.Image = null;
        }
    }
}
