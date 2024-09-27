using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace CadastroDeProdutosView.Features.Commons
{
    public static class MessageBoxCustomizada
    {
        public static DialogResult Show(string message, string title)
        {
            return XtraMessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }
    }
}