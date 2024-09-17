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
                .PreencherLookUpEditComOValorDoEnum<NaturezaDaOperacaoView.NaturezaDaOperacao>();
        }

        private void CadastroDeProdutosView_Load(object sender, EventArgs e)
        {

        }

        private void labelControl_Click(object sender, EventArgs e)
        {

        }

        private bool isValidating;

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (isValidating) return;

            isValidating = true;
            codigodebarrasTextEdit.Properties.MaxLength = 13;
            var texto = codigodebarrasTextEdit.Text;

            if (texto.Length == 13)
            {
                if (!ValidarCodigoDeBarrasEAN13(texto))
                {
                    XtraMessageBox.Show("Código de Barras EAN-13 inválido. Verifique os dados inseridos e tente novamente.");
                    codigodebarrasTextEdit.Text = string.Empty;
                    AtualizarEstiloLabelCodigoBarras(false);
                }
                else
                {
                    AtualizarEstiloLabelCodigoBarras(true);
                }
            }
            else
            {
                AtualizarEstiloLabelCodigoBarras(true);
            }

            isValidating = false;
        }

        private bool ValidarCodigoDeBarrasEAN13(string codigoDeBarras)
        {
            if (codigoDeBarras.Length != 13 || !long.TryParse(codigoDeBarras, out _))
                return false;

            var numeros = new int[12];
            for (var i = 0; i < 12; i++)
            {
                numeros[i] = int.Parse(codigoDeBarras[i].ToString());
            }

            var somaPar = 0;
            var somaImpar = 0;
            for (var i = 0; i < 12; i++)
            {
                if (i % 2 == 0)
                    somaImpar += numeros[i];
                else
                    somaPar += numeros[i];
            }

            var somaTotal = somaImpar + (somaPar * 3);
            var digitoVerificadorCalculado = (10 - (somaTotal % 10)) % 10;
            var digitoVerificadorInformado = int.Parse(codigoDeBarras[12].ToString());

            return digitoVerificadorCalculado == digitoVerificadorInformado;
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

            if (string.IsNullOrWhiteSpace(codigodebarrasTextEdit.Text))
            {
                AtualizarEstiloLabelCodigoBarras(false);
                todosCamposPreenchidos = false;
            }
            else
            {
                AtualizarEstiloLabelCodigoBarras(true);
            }

            if (string.IsNullOrWhiteSpace(nomeTextEdit.Text))
            {
                nomeLabelControl.Text = "Nome: <color=red>*</color>";
                nomeLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else 
            {
                nomeLabelControl.Text = "Nome: *";
                nomeLabelControl.AllowHtmlString = false;
            }

            if (string.IsNullOrWhiteSpace(codigodebarrasTextEdit.Text))
            {
                codigodebarrasLabelControl.Text = "Codigo de Barras: <color=red>*</color>";
                codigodebarrasLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else
            {
                codigodebarrasLabelControl.Text = "Codigo de Barras: *";
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
                estoqueLabelControl.Text = "Estoque: *";
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
                precoDaVendaLabelControl.Text = "Preço da Venda: *";
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
                custoLabelControl.Text = "Custo: *";
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
                markupLabelControl.Text = "Markup: *";
                markupLabelControl.AllowHtmlString = false;
            }

            if (unidadeDeMedidaLookUpEdit.EditValue == null)
            {
                unidadeDeMedidaLabelControl.Text = "Und. de Medida: <color=red>*</color>";
                unidadeDeMedidaLabelControl.AllowHtmlString = true;
                todosCamposPreenchidos = false;
            }
            else
            {
                unidadeDeMedidaLabelControl.Text = "Und. de Medida: *";
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
                categoriaLabelControl.Text = "Categoria: *";
                categoriaLabelControl.AllowHtmlString = false;
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
            {
                var camposPreenchidos = ValidarCamposObrigatorios();

                if (!camposPreenchidos)
                {
                    XtraMessageBox.Show("Todos os campos obrigatórios devem ser preenchidos!");
                    return;
                }

                if (!ValidarCodigoDeBarrasEAN13(codigodebarrasTextEdit.Text))
                {
                    XtraMessageBox.Show("O campo código de barras não é um EAN-13 válido. Por favor, verifique e tente novamente.");
                    AtualizarEstiloLabelCodigoBarras(false);
                    return;
                }

                AtualizarEstiloLabelCodigoBarras(true);

                XtraMessageBox.Show("Produto cadastrado com sucesso");
                LimparTextEdits();
                LimparLookUpEdits();
            }
        }

        private void AtualizarEstiloLabelCodigoBarras(bool valido)
        {
            if (valido)
            {
                codigodebarrasLabelControl.Text = "Codigo de Barras: *";
                codigodebarrasLabelControl.AllowHtmlString = false;
            }
            else
            {
                codigodebarrasLabelControl.Text = "Codigo de Barras: <color=red>*</color>";
                codigodebarrasLabelControl.AllowHtmlString = true;
            }
        }


        private void categoriaDeProdutosLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void marcaLabelControl_Click(object sender, EventArgs e)
        {

        }

        private void aliquotaDeIcmsLabelControl_Click(object sender, EventArgs e)
        {

        }
    }
}
