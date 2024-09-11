using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
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
            [Description("UN")]
            Unidade,
            [Description("KG")]
            Quilos,
            [Description("LT")]
            Litros,
            [Description("MT")]
            Metros
        }

        private void InitializeLookUpEdit()=>
            unidadeDeMedidaLookUpEdit.PreencherLookUpEditComOValorDoEnum<UnidadeDeMedida>();

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
