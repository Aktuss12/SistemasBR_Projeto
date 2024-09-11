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
            InitializeLookUpEdit();
        }



        public enum UnidadeDeMedida
        {
            Quilos = 1,
            Litros = 2,
            Metros = 3,
            Unidade = 4
        }

        private void InitializeLookUpEdit()
        {
            Dictionary<int, string> unidadeDeMedida = Enum.GetValues(typeof(UnidadeDeMedida))
                .Cast<UnidadeDeMedida>()
                .ToDictionary(x => (int)x, x => x.ToString());


            undMedidaLookUpEdit.Properties.DataSource = unidadeDeMedida;
            undMedidaLookUpEdit.Properties.ValueMember = "Id";
            undMedidaLookUpEdit.Properties.DisplayMember = "Nome";
        }


        private void CadastroDeProdutosView_Load(object sender, EventArgs e)
        {

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
