using CadastroDeProdutosView.Features.Commons;
using CadastroDeProdutosView.Features.Produto.Enums;
using DevExpress.XtraEditors;
using System;
using System.Diagnostics.Eventing.Reader;
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
            var todosCamposPreenchidos = true;
            if (string.IsNullOrWhiteSpace(nomeTextEdit.Text))
            {
                nomeLabelControl.Text = "Nome: <color=red>*</color>";
                nomeLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else 
            {
                nomeLabelControl.Text = "Nome:";
                nomeLabelControl.AllowHtmlString = false;
            }

            if (string.IsNullOrWhiteSpace(fornecedorTextEdit.Text))
            {
                fornecedorLabelControl.Text = "Fornecedor: <color=red>*</color>";
                fornecedorLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else
            {
                fornecedorLabelControl.Text = "Fornecedor:";
                fornecedorLabelControl.AllowHtmlString = false;
            }

            if (string.IsNullOrWhiteSpace(codigodebarrasTextEdit.Text))
            {
                codigodebarrasLabelControl.Text = "Codigo de Barras: <color=red>*</color>";
                codigodebarrasLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else
            {
                codigodebarrasLabelControl.Text = "Codigo De Barras:";
                codigodebarrasLabelControl.AllowHtmlString = false;
            }

            if (string.IsNullOrWhiteSpace(estoqueTextEdit.Text))
            {
                estoqueLabelControl.Text = "Estoque: <color=red>*</color>";
                estoqueLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else
            {
                estoqueLabelControl.Text = "Estoque:";
                estoqueLabelControl.AllowHtmlString = false;
            }

            if (string.IsNullOrWhiteSpace(precoVendaTextEdit.Text))
            {
                precoDaVendaLabelControl.Text = "Preço da Venda: <color=red>*</color>";
                precoDaVendaLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else
            {
                precoDaVendaLabelControl.Text = "Preço da Venda:";
                precoDaVendaLabelControl.AllowHtmlString = false;
            }

            if (string.IsNullOrWhiteSpace(custoTextEdit.Text))
            {
                custoLabelControl.Text = "Custo: <color=red>*</color>";
                custoLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else
            {
                custoLabelControl.Text = "Custo:";
                custoLabelControl.AllowHtmlString = false;
            }

            if (string.IsNullOrWhiteSpace(markupTextEdit.Text))
            {
                markupLabelControl.Text = "Markup: <color=red>*</color>";
                markupLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else
            {
                markupLabelControl.Text = "Markup:";
                markupLabelControl.AllowHtmlString = false;
            }

            if (string.IsNullOrWhiteSpace(ncmTextEdit.Text))
            {
                ncmLabelControl.Text = "NCM: <color=red>*</color>";
                ncmLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else
            {
                ncmLabelControl.Text = "NCM:";
                ncmLabelControl.AllowHtmlString = false;
            }

            if (string.IsNullOrWhiteSpace(aliquotaDeIcmsTextEdit.Text))
            {
                aliquotaDeIcmsLabelControl.Text = "Alíquota de ICMS (%): <color=red>*</color>";
                aliquotaDeIcmsLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else
            {
                aliquotaDeIcmsLabelControl.Text = "Alíquota de ICMS (%):";
                aliquotaDeIcmsLabelControl.AllowHtmlString = false;
            }

            if (string.IsNullOrWhiteSpace(reducaoDeCalculoIcmsTextEdit.Text))
            {
                reducaoDeCalculoIcmsLabelControl.Text = "Redução de Cálculo do ICMS (%): <color=red>*</color>";
                reducaoDeCalculoIcmsLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else
            {
                reducaoDeCalculoIcmsLabelControl.Text = "Redução de Cálculo do ICMS (%):";
                reducaoDeCalculoIcmsLabelControl.AllowHtmlString = false;
            }

            if (unidadeDeMedidaLookUpEdit.EditValue == null)
            {
                unidadeDeMedidaLabelControl.Text = "Und. de Medida: <color=red>*</color>";
                unidadeDeMedidaLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else
            {
                unidadeDeMedidaLabelControl.Text = "Und. de Medida:";
                unidadeDeMedidaLabelControl.AllowHtmlString = false;
            }

            if (categoriaDeProdutosLookUpEdit.EditValue == null)
            {
                categoriaLabelControl.Text = "Categoria: <color=red>*</color>";
                categoriaLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else
            {
                categoriaLabelControl.Text = "Categoria:";
                categoriaLabelControl.AllowHtmlString = false;
            }

            if (marcaLookUpEdit.EditValue == null)
            {
                marcaLabelControl.Text = "Marca: <color=red>*</color>";
                marcaLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else
            {
                marcaLabelControl.Text = "Marca:";
                marcaLabelControl.AllowHtmlString = false;
            }

            if (origemDaMercadoriaLookUpEdit.EditValue == null)
            {
                origemDaMercadoriaLabelControl.Text = "Origem da Mercadoria: <color=red>*</color>";
                origemDaMercadoriaLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else
            {
                origemDaMercadoriaLabelControl.Text = "Origem da Mercadoria:";
                origemDaMercadoriaLabelControl.AllowHtmlString = false;
            }

            if (situacaoTributariaLookUpEdit.EditValue == null) 
            {
                situacaoTributariaLabelControl.Text = "Situação Tributaria: <color=red>*</color>";
                situacaoTributariaLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else
            {
                situacaoTributariaLabelControl.Text = "Situação Tributaria:";
                situacaoTributariaLabelControl.AllowHtmlString = false;
            }

            if (naturezaDaOperacaoLookUpEdit.EditValue == null)
            {
                naturezaOperacaoLabelControl.Text = "Natureza da Operação: <color=red>*</color>";
                naturezaOperacaoLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else
            {
                naturezaOperacaoLabelControl.Text = "Natureza da Operação: <color=red>*</color>";
                naturezaOperacaoLabelControl.AllowHtmlString = false;
            }

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
        }

        // Automação de limpeza das TextEdit após salvar o produto cadastrado
        private void LimparTextEdits()
        {
            nomeTextEdit.Text = null;
            fornecedorTextEdit.Text = null;
            codigodebarrasTextEdit.Text = null;
            estoqueTextEdit.Text = null;
            precoVendaTextEdit.Text = null;
            custoTextEdit.Text = null;
            markupTextEdit.Text = null;
            ncmTextEdit.Text = null;
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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void salvarButtomItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ValidarCamposObrigatorios())
            {
                XtraMessageBox.Show("Produto cadastrado com sucesso");
                LimparTextEdits();
                LimparLookUpEdits();
            }
            else
            {
                XtraMessageBox.Show("Todos os campos obrigatórios devem ser preenchidos!");
            }
        }

        private void categoriaDeProdutosLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void marcaLabelControl_Click(object sender, EventArgs e)
        {

        }
    }
}
