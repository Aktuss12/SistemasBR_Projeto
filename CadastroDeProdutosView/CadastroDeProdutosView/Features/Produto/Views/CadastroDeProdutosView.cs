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

            tipoDeEstoqueLookUpEdit
                .PreencherLookUpEditComOValorDoEnum<UnidadeDeMedidaView.UnidadeDeMedida>();

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
            codigodebarrasTextEdit.Properties.MaxLength = 13;
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

        public void nomeTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            nomeTextEdit.Properties.MaxLength = 100;
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

        // Validação para os campos obrigatorios do cadastro
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
                 !string.IsNullOrWhiteSpace(reducaoDeCalculoIcmsTextEdit.Text));

            return todosCamposPreenchidos;
        }

        // Automação de limpeza nas LookUpEdit após salvar o produto cadastrado
        private void LimparLookUpEdits()
        {
            unidadeDeMedidaLookUpEdit.EditValue = null;
            categoriaDeProdutosLookUpEdit.EditValue = null;
            marcaLookUpEdit.EditValue = null;
            origemDaMercadoriaLookUpEdit.EditValue = null;
            situacaoTributariaLookUpEdit.EditValue = null;
            naturezaDaOperacaoLookUpEdit.EditValue = null;
            tipoDeEstoqueLookUpEdit.EditValue = null;
        }

        // Automação de limpeza das TextEdit após salvar o produto cadastrado
        private void LimparTextEdits()
        {
            nomeTextEdit.Text = "";
            fornecedorTextEdit.Text = "";
            codigodebarrasTextEdit.Text = "";
            estoqueTextEdit.Text = null;
            precoVendaTextEdit.Text = null;
            custoTextEdit.Text = null;
            markupTextEdit.Text = null;
            ncmTextEdit.Text = "";
            aliquotaDeIcmsTextEdit.Text = null;
            reducaoDeCalculoIcmsTextEdit.Text = null;
        }



        

        private void fornecedorTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            fornecedorTextEdit.Properties.MaxLength = 100;
        }

        private void estoqueTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            estoqueTextEdit.Properties.MaxLength = 12;
        }

        private void precoVendaTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            precoVendaTextEdit.Properties.MaxLength = 16;
        }

        private void custoTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            custoTextEdit.Properties.MaxLength = 8;
        }

        private void markupTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            markupTextEdit.Properties.MaxLength = 8;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            {
                if (ValidarCamposObrigatorios())
                {
                    XtraMessageBox.Show("Produto cadastrado com sucesso");
                    LimparTextEdits();
                    LimparLookUpEdits();
                }
                else
                    XtraMessageBox.Show("Todos os campos obrigatórios devem ser preenchidos!");
            }
        }

        private void tipoDeEstoqueLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void ncmTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            ncmTextEdit.Properties.MaxLength = 8;
        }

        private void aliquotaDeIcmsTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            aliquotaDeIcmsTextEdit.Properties.MaxLength = 8;
        }

        private void reducaoDeCalculoIcmsTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            reducaoDeCalculoIcmsTextEdit.Properties.MaxLength = 8;
        }
    }
}
