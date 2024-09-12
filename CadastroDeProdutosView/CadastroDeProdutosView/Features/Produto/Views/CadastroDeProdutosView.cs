using CadastroDeProdutosView.Features.Commons;
using CadastroDeProdutosView.Features.Produto.Enums;
using DevExpress.XtraEditors;
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
                .PreencherLookUpEditComOValorDoEnum<UnidadeDeMedidaView.UnidadeDeMedida>();

            categoriaDeProdutosLookUpEdit
                .PreencherLookUpEditComOValorDoEnum<CategoriaDoProdutoView.CategoriaDeProdutos>();

            marcaLookUpEdit
                .PreencherLookUpEditComOValorDoEnum<MarcaDoProdutoView.MarcaDoProduto>();

            origemDaMercadoriaLookUpEdit
                .PreencherLookUpEditComOValorDoEnum<OrigemDaMercadoriaView.OrigemDaMercadoria>();

            situacaoTributariaLookUpEdit
                .PreencherLookUpEditComOValorDoEnum<SituacaoTributariaView.SituacaoTributaria>();

            naturezaDaOperacaoLookUpEdit
                .PreencherLookUpEditComOValorDoEnum <NaturezaDaOperacaoView.NaturezaDaOperacao>();
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

        private void LimparLookUpEdits()
        {
            unidadeDeMedidaLookUpEdit.EditValue = null;
            categoriaDeProdutosLookUpEdit.EditValue = null;
            marcaLookUpEdit.EditValue = null;
            origemDaMercadoriaLookUpEdit.EditValue = null;
            situacaoTributariaLookUpEdit.EditValue = null;
            naturezaDaOperacaoLookUpEdit.EditValue = null;
        }
        private bool ValidarCamposObrigatorios()
        {
            var todosCamposPreenchidos =
                (!string.IsNullOrWhiteSpace(nomeTextEdit.Text) &&
                 !string.IsNullOrWhiteSpace(fornecedorTextEdit.Text) &&
                 !string.IsNullOrWhiteSpace(codigodebarrasTextEdit.Text) &&
                 !string.IsNullOrWhiteSpace(estoqueTextEdit.Text) &&
                 !string.IsNullOrWhiteSpace(precoVendaTextEdit.Text) &&
                 !string.IsNullOrWhiteSpace(custoTextEdit.Text) &&
                 !string.IsNullOrWhiteSpace(markupTextEdit.Text) &&
                 !string.IsNullOrWhiteSpace(ncmTextEdit.Text) &&
                 !string.IsNullOrWhiteSpace(aliquotaDeIcmsTextEdit.Text) &&
                 !string.IsNullOrWhiteSpace(reducaoIcmsTextEdit.Text));

            if (todosCamposPreenchidos)
            {
                return true;
            }
            else
            {
                XtraMessageBox.Show("Todos os campos obrigatórios devem ser preenchidos.");
                return false;
            }
        }

        public void nomeTextEdit_EditValueChanged(object sender, EventArgs e)
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

        private void LimparTextEdits()
        {
            nomeTextEdit.Text = "";
            fornecedorTextEdit.Text = "";
            codigodebarrasTextEdit.Text = "";
            estoqueTextEdit.Text = "";
            precoVendaTextEdit.Text = "";
            custoTextEdit.Text = "";
            markupTextEdit.Text = "";
            ncmTextEdit.Text = "";
            aliquotaDeIcmsTextEdit.Text = "";
            reducaoIcmsTextEdit.Text = "";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposObrigatorios())
            {
                XtraMessageBox.Show("Produto nao cadastrado");
            }

            else if(ValidarCamposObrigatorios())
            {
                XtraMessageBox.Show("Produto cadastrado com Sucesso!");
                 LimparTextEdits();
                 LimparLookUpEdits();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposObrigatorios())
            {
                XtraMessageBox.Show("Produto nao cadastrado");
            }

            else if (ValidarCamposObrigatorios())
            {
                XtraMessageBox.Show("Produto cadastrado com Sucesso!");
                LimparTextEdits();
                LimparLookUpEdits();
            }
        }
    }
}
