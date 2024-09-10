using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace CadastroDeProdutosView
{
    public partial class CadastroDeProdutosView : Form
    {
        public CadastroDeProdutosView()
        {
            InitializeComponent();
        }

        public enum Produtos
        {
            Quilos,
            Litros,
            Metros,
            Unidades
        }

        private Dictionary<int, string> GetProdutosList()
        {
            return Enum.GetValues(typeof(Produtos)).Cast<Produtos>().ToDictionary(x => (int)x, x => x.ToString());
        }
        private void CadastroDeProdutosView_Load(object sender, EventArgs e)
        {
            undMedidaLookUpEdit.Properties.DataSource = GetProdutosList();
            undMedidaLookUpEdit.Properties.ValueMember = "Key";
            undMedidaLookUpEdit.Properties.DisplayMember = " Value";

        }

        private void labelControl_Click(object sender, EventArgs e)
        {

        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void produtosTabNavigationPage_Paint(object sender, PaintEventArgs e)
        {

        }

        private void undMedidaTextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {


        }

        private void precoLabelControl_Click(object sender, EventArgs e)
        {

        }

        public void lookUpEdit1_EditValueChanged_1(object sender, EventArgs e)
        {
        }

        private void nomeTextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void marcaTextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void categoriaTextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void situacaoTributariaTextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void origemMercadoriaTextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void naturezaOperacaoTextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void fornecedorTextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
