using CadastroDeProdutosView.Features.Commons;
using System;
using System.Windows.Forms;

namespace CadastroDeProdutosView.Features.Produto.Views
{
    public partial class CadastroDeProdutosView : Form
    {
        public CadastroDeProdutosView()
        {
            InitializeComponent();
            InitializeLookUpEdit();
        }

        private void InitializeLookUpEdit()
        {
            // Implementação das Enums para o LookUpEdit
            unidadeDeMedidaLookUpEdit
                .PreencherLookUpEditComOValorDoEnum<Enums.UnidadeDeMedidaView.UnidadeDeMedida>();

            categoriaDeProdutosLookUpEdit
                .PreencherLookUpEditComOValorDoEnum<Enums.CategoriaDoProdutoView.CategoriaDeProdutos>();

            marcaLookUpEdit
                .PreencherLookUpEditComOValorDoEnum<Enums.MarcaDoProdutoView.MarcaDoProduto>();

            origemDaMercadoriaLookUpEdit
                .PreencherLookUpEditComOValorDoEnum<Enums.OrigemDaMercadoriaView.OrigemDaMercadoria>();

            situacaoTributariaLookUpEdit
                .PreencherLookUpEditComOValorDoEnum<Enums.SituacaoTributariaView.SituacaoTributaria>();

            naturezaDaOperacaoLookUpEdit
                .PreencherLookUpEditComOValorDoEnum <Enums.NaturezaDaOperacaoView.NaturezaDaOperacao>();
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

        private void precoLabelControl_Click(object sender, EventArgs e)
        {

        }

        public void lookUpEdit1_EditValueChanged_1(object sender, EventArgs e)
        {

        }

        private void nomeTextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void fornecedorLabelControl_Click(object sender, EventArgs e)
        {

        }

        private void marcaLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void categoriaLabelControl_Click(object sender, EventArgs e)
        {

        }

        private void naturezaDaOperacaoLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void origemDaMercadoriaLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
