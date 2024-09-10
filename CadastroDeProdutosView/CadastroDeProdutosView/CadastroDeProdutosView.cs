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

        public class Produto
        {
            public int id { get; set; }
            public string nome { get; set; }
        }

        private void CadastroDeProdutosView_Load(object sender, EventArgs e)
        {
           
            List<Produto> produtos = new List<Produto>
            {
                new Produto { id = 1, nome = "Grama" },
                new Produto { id = 2, nome = "Litro" },
                new Produto { id = 3, nome = "Unidade" },
                new Produto { id = 4, nome = "Metro" },
            };

            undMedidaLookUpEdit.Properties.DataSource = produtos;
            undMedidaLookUpEdit.Properties.DisplayMember = "Nome";
            undMedidaLookUpEdit.Properties.ValueMember = "Id";
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
