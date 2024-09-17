using CadastroDeProdutosView.Features.Commons;
using CadastroDeProdutosView.Features.Produto.Enums;
using DevExpress.XtraEditors;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CadastroDeProdutosView.Features.Produto.Views
{
    public partial class CadastroDeProdutosView : Form
    {
        private bool isValidating;

        public CadastroDeProdutosView()
        {
            InitializeComponent();
            InitializeLookUpEdit();
        }

        private void InitializeLookUpEdit()
        {
            unidadeDeMedidaLookUpEdit.PreencherLookUpEditComOValorDoEnum<UnidadeDeMedidaView.UnidadeDeMedida>();
            categoriaDeProdutosLookUpEdit.PreencherLookUpEditComOValorDoEnum<CategoriaDoProdutoView.CategoriaDeProdutos>();
            marcaLookUpEdit.PreencherLookUpEditComOValorDoEnum<MarcaDoProdutoView.MarcaDoProduto>();
            origemDaMercadoriaLookUpEdit.PreencherLookUpEditComOValorDoEnum<OrigemDaMercadoriaView.OrigemDaMercadoria>();
            situacaoTributariaLookUpEdit.PreencherLookUpEditComOValorDoEnum<SituacaoTributariaView.SituacaoTributaria>();
            naturezaDaOperacaoLookUpEdit.PreencherLookUpEditComOValorDoEnum<NaturezaDaOperacaoView.NaturezaDaOperacao>();
        }

        private void CadastroDeProdutosView_Load(object sender, EventArgs e) { }

        private void labelControl_Click(object sender, EventArgs e) { }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            if (isValidating) return;

            isValidating = true;
            codigodebarrasTextEdit.Properties.MaxLength = 13;
            var texto = codigodebarrasTextEdit.Text;

            if (texto.Length == 13)
            {
                var valido = ValidarCodigoDeBarrasEAN13(texto);
                AtualizarEstiloLabelCodigoBarras(valido);
                if (!valido)
                {
                    XtraMessageBox.Show("Código de Barras EAN-13 inválido. Verifique os dados inseridos e tente novamente.");
                    codigodebarrasTextEdit.Text = string.Empty;
                }
            }
            else
            {
                AtualizarEstiloLabelCodigoBarras(true);
            }

            isValidating = false;
        }

        private void CalcularPrecoVenda()
        {
            if (decimal.TryParse(custoTextEdit.Text, out var custo) &&
                decimal.TryParse(markupTextEdit.Text, out var markup))
            {
                var precoVenda = custo * (1 + (markup / 100));
                precoVendaTextEdit.Text = precoVenda.ToString("F2");
            }
            else
            {
                precoVendaTextEdit.Text = string.Empty;
            }
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

        private void LimparLookUpEdits()
        {
            unidadeDeMedidaLookUpEdit.EditValue = null;
            categoriaDeProdutosLookUpEdit.EditValue = null;
            marcaLookUpEdit.EditValue = null;
            origemDaMercadoriaLookUpEdit.EditValue = null;
            situacaoTributariaLookUpEdit.EditValue = null;
            naturezaDaOperacaoLookUpEdit.EditValue = null;
        }

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
            if (isValidating) return;

            isValidating = true;
            CalcularPrecoVenda();
            isValidating = false;
        }

        private void markupTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (isValidating) return;

            isValidating = true;
            CalcularPrecoVenda();
            isValidating = false;
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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) { }

        private void salvarButtomItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!ValidarCamposObrigatorios())
            {
                XtraMessageBox.Show("Todos os campos obrigatórios devem ser preenchidos!");
                return;
            }

            if (!string.IsNullOrWhiteSpace(codigodebarrasTextEdit.Text) &&
                !ValidarCodigoDeBarrasEAN13(codigodebarrasTextEdit.Text))
            {
                XtraMessageBox.Show("O campo código de barras não é um EAN-13 válido. Por favor, verifique e tente novamente.");
                AtualizarEstiloLabelCodigoBarras(false);
                return;
            }

            AtualizarEstiloLabelCodigoBarras(true);

            try
            {
                using (var connection = new SqlConnection(@"Server=localhost;Database=C:\\Users\\admin\\Documents\\BancoDeDados\\BancoDeDadosCadastroDeProdutos.fdb;User=sysdba;Password=masterkey;Charset=NONE;"))
                {
                    connection.Open();

                    const string insertProdutoQuery = @"
                        INSERT INTO Produto (Nome, Fornecedor, CodigoDeBarras, Estoque, PrecoVenda, Custo, Markup, NCM, AliquotaICMS, ReducaoICMS, UnidadeDeMedida, Categoria)
                        VALUES (@Nome, @Fornecedor, @CodigoDeBarras, @Estoque, @PrecoVenda, @Custo, @Markup, @NCM, @AliquotaICMS, @ReducaoICMS, @UnidadeDeMedida, @Categoria)";

                    using (var command = new SqlCommand(insertProdutoQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", nomeTextEdit.Text);
                        command.Parameters.AddWithValue("@Fornecedor", fornecedorTextEdit.Text);
                        command.Parameters.AddWithValue("@CodigoDeBarras", codigodebarrasTextEdit.Text);
                        command.Parameters.AddWithValue("@Estoque", estoqueTextEdit.Text);
                        command.Parameters.AddWithValue("@PrecoVenda", precoVendaTextEdit.Text);
                        command.Parameters.AddWithValue("@Custo", custoTextEdit.Text);
                        command.Parameters.AddWithValue("@Markup", markupTextEdit.Text);
                        command.Parameters.AddWithValue("@NCM", ncmTextEdit.Text);
                        command.Parameters.AddWithValue("@AliquotaICMS", aliquotaDeIcmsTextEdit.Text);
                        command.Parameters.AddWithValue("@ReducaoICMS", reducaoDeCalculoIcmsTextEdit.Text);
                        command.Parameters.AddWithValue("@UnidadeDeMedida", unidadeDeMedidaLookUpEdit.EditValue);
                        command.Parameters.AddWithValue("@Categoria", categoriaDeProdutosLookUpEdit.EditValue);

                        command.ExecuteNonQuery();
                    }

                    XtraMessageBox.Show("Produto cadastrado com sucesso");
                    LimparTextEdits();
                    LimparLookUpEdits();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Erro ao salvar o produto: {ex.Message}");
            }
        }

        private void AtualizarEstiloLabelCodigoBarras(bool valido)
        {
            codigodebarrasLabelControl.Text = valido
                ? "Codigo de Barras: "
                : "Codigo de Barras: <color=red>*</color>";

            codigodebarrasLabelControl.AllowHtmlString = !valido;
        }

        // Verifica se os campos obrigatórios estão preenchidos
        private bool ValidarCamposObrigatorios()
        {
            var todosCamposPreenchidos = true;

            todosCamposPreenchidos &= !string.IsNullOrWhiteSpace(nomeTextEdit.Text);
            todosCamposPreenchidos &= !string.IsNullOrWhiteSpace(estoqueTextEdit.Text);
            todosCamposPreenchidos &= !string.IsNullOrWhiteSpace(precoVendaTextEdit.Text);
            todosCamposPreenchidos &= unidadeDeMedidaLookUpEdit.EditValue != null;
            todosCamposPreenchidos &= categoriaDeProdutosLookUpEdit.EditValue != null;

            nomeLabelControl.Text = string.IsNullOrWhiteSpace(nomeTextEdit.Text) ? "Nome: <color=red>*</color>" : "Nome: *";
            nomeLabelControl.AllowHtmlString = string.IsNullOrWhiteSpace(nomeTextEdit.Text);

            estoqueLabelControl.Text = string.IsNullOrWhiteSpace(estoqueTextEdit.Text) ? "Estoque: <color=red>*</color>" : "Estoque: *";
            estoqueLabelControl.AllowHtmlString = string.IsNullOrWhiteSpace(estoqueTextEdit.Text);

            precoDaVendaLabelControl.Text = string.IsNullOrWhiteSpace(precoVendaTextEdit.Text) ? "Preço da Venda: <color=red>*</color>" : "Preço da Venda: *";
            precoDaVendaLabelControl.AllowHtmlString = string.IsNullOrWhiteSpace(precoVendaTextEdit.Text);

            unidadeDeMedidaLabelControl.Text = unidadeDeMedidaLookUpEdit.EditValue == null ? "Und. de Medida: <color=red>*</color>" : "Und. de Medida: *";
            unidadeDeMedidaLabelControl.AllowHtmlString = unidadeDeMedidaLookUpEdit.EditValue == null;

            categoriaLabelControl.Text = categoriaDeProdutosLookUpEdit.EditValue == null ? "Categoria: <color=red>*</color>" : "Categoria: *";
            categoriaLabelControl.AllowHtmlString = categoriaDeProdutosLookUpEdit.EditValue == null;

            return todosCamposPreenchidos;
        }
    }
}