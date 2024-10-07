using CadastroDeProdutosView.Features.Commons;
using CadastroDeProdutosView.Features.Commons.Services;
using CadastroDeProdutosView.Features.Produto.Enums;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using CadastroDeProdutosView.Features.Produto.Models;


namespace CadastroDeProdutosView.Features.Produto.Views
{
    public partial class CadastroDeProdutosView : Form
    {
        private readonly ConexaoProdutosFirebird _conexao;
        private readonly int? _produtoId;
        private byte[]? _imagemDoProduto;

        public CadastroDeProdutosView(int produtoId = 0)
        {
            InitializeComponent();
            InitializeComponents();
            _produtoId = produtoId > 0 ? produtoId : null;
            _conexao = new ConexaoProdutosFirebird();

            if (_produtoId.HasValue) CarregarProduto(_produtoId.Value);
        }

        private void InitializeComponents()
        {
            InitializeLookUpEdit();
            LimitadorDeCaracteres();
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

        private void LimitadorDeCaracteres()
        {
            fornecedorTextEdit.Properties.MaxLength = 50;
            markupTextEdit.Properties.MaxLength = 6;
            custoTextEdit.Properties.MaxLength = 6;
            precoVendaTextEdit.Properties.MaxLength = 6;
            codigoDeBarrasTextEdit.Properties.MaxLength = 13;
            ncmTextEdit.Properties.MaxLength = 8;
            estoqueTextEdit.Properties.MaxLength = 9;
            nomeTextEdit.Properties.MaxLength = 50;
            marcaLookUpEdit.Properties.MaxLength = 50;
            reducaoDeCalculoIcmsTextEdit.Properties.MaxLength = 6;
            aliquotaDeIcmsTextEdit.Properties.MaxLength = 6;
        }

        private void LimpezaDeCampos()
        {
            nomeTextEdit.LimpezaDeTextEdit();
            fornecedorTextEdit.LimpezaDeTextEdit();
            codigoDeBarrasTextEdit.LimpezaDeTextEdit();
            estoqueTextEdit.LimpezaDeTextEdit();
            custoTextEdit.LimpezaDeTextEdit();
            markupTextEdit.LimpezaDeTextEdit();
            precoVendaTextEdit.LimpezaDeTextEdit();
            ncmTextEdit.LimpezaDeTextEdit();
            aliquotaDeIcmsTextEdit.LimpezaDeTextEdit();
            reducaoDeCalculoIcmsTextEdit.LimpezaDeTextEdit();
            categoriaDeProdutosLookUpEdit.LimpezaDeLookUpEdit();
            unidadeDeMedidaLookUpEdit.LimpezaDeLookUpEdit();
            marcaLookUpEdit.LimpezaDeLookUpEdit();
            origemDaMercadoriaLookUpEdit.LimpezaDeLookUpEdit();
            situacaoTributariaLookUpEdit.LimpezaDeLookUpEdit();
            naturezaDaOperacaoLookUpEdit.LimpezaDeLookUpEdit();
            imagemDoProdutoPictureEdit.LimpezaDeImageBox();
        }

        public static event Action? ProdutosAtualizados;

        private void salvarButtomItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                var produto = CriarProduto();
                if (_produtoId.HasValue)
                {
                    produto.Id = _produtoId.Value;
                    _conexao.AlterarProduto(produto);
                    ShowMessage("Produto alterado com sucesso");
                    Close();
                }
                else
                {
                    var novoProdutoId = _conexao.InserirProduto(produto);
                    ShowMessage("Produto cadastrado com sucesso");
                    produto.Id = novoProdutoId;
                }

                LimpezaDeCampos();
                ProdutosAtualizados?.Invoke();
            }
            catch (Exception ex)
            {
                ShowMessage($"Erro ao salvar o produto: {ex.Message}");
            }
        }

        private static void ShowMessage(string message)
        {
            XtraMessageBox.Show(message);
        }

        private bool ValidarCampos()
        {
            if (!string.IsNullOrWhiteSpace(codigoDeBarrasTextEdit.Text) &&
                !_conexao.ValidarCodigoDeBarras(codigoDeBarrasTextEdit.Text))
            {
                ShowMessage("O código de barras não é um tipo EAN-13");
                codigodebarrasLabelControl.Text = "Código de Barras: <color=red>*</color>";
                codigodebarrasLabelControl.AllowHtmlString = true;
                return false;
            }

            ResetCodigoDeBarrasLabel();

            if (ValidacaoDeCampos.ValidacaoParaCamposObrigatorios(
                    nomeTextEdit,
                    estoqueTextEdit,
                    precoVendaTextEdit,
                    unidadeDeMedidaLookUpEdit,
                    categoriaDeProdutosLookUpEdit,
                    nomeLabelControl,
                    estoqueLabelControl,
                    precoDaVendaLabelControl,
                    unidadeDeMedidaLabelControl,
                    categoriaLabelControl))
            {
                return true;
            }
            XtraMessageBox.Show("Todos os campos obrigatorios devem ser preenchidos");
            return false;
        }

        private void ResetCodigoDeBarrasLabel()
        {
            codigodebarrasLabelControl.Text = "Codigo de Barras:";
            codigodebarrasLabelControl.AllowHtmlString = false;
        }

        private Models.Produto CriarProduto()
        {
            return new Models.Produto
            {
                Nome = nomeTextEdit.Text,
                Categoria = categoriaDeProdutosLookUpEdit.EditValue?.ToString(),
                Fornecedor = fornecedorTextEdit.Text,
                CodigoDeBarras = codigoDeBarrasTextEdit.Text,
                UnidadeDeMedida = unidadeDeMedidaLookUpEdit.EditValue?.ToString(),
                Estoque = int.Parse(estoqueTextEdit.Text),
                Marca = marcaLookUpEdit.EditValue?.ToString(),
                Custo = ConversaoUtil.ConversaoParaDecimal(custoTextEdit.Text),
                Markup = ConversaoUtil.ConversaoParaDecimal(markupTextEdit.Text),
                PrecoDaVenda = ConversaoUtil.ConversaoParaDecimal(precoVendaTextEdit.Text),
                Imagem = _imagemDoProduto,

                InformacoesFiscais = new InformacoesFiscais
                {
                    OrigemDaMercadoria = origemDaMercadoriaLookUpEdit.EditValue?.ToString(),
                    SituacaoTributaria = situacaoTributariaLookUpEdit.EditValue?.ToString(),
                    NaturezaDaOperacao = naturezaDaOperacaoLookUpEdit.EditValue?.ToString(),
                    Ncm = ncmTextEdit.Text,
                    AliquotaDeIcms = ConversaoUtil.ConversaoParaDecimal(aliquotaDeIcmsTextEdit.Text),
                    ReducaoDeCalculo = ConversaoUtil.ConversaoParaDecimal(reducaoDeCalculoIcmsTextEdit.Text)
                }
            };
        }

        public void CarregarProduto(int idProduto)
        {
            try
            {
                var produto = _conexao.CarregarProduto(idProduto);
                PreencherFormularioComProduto(produto);
            }
            catch (Exception ex)
            {
                ShowMessage($"Erro ao carregar o produto: {ex.Message}");
            }
        }

        private void PreencherFormularioComProduto(Models.Produto produto)
        {
            nomeTextEdit.Text = produto.Nome;
            categoriaDeProdutosLookUpEdit.EditValue = produto.Categoria;
            fornecedorTextEdit.Text = produto.Fornecedor;
            codigoDeBarrasTextEdit.Text = produto.CodigoDeBarras;
            unidadeDeMedidaLookUpEdit.EditValue = produto.UnidadeDeMedida;
            estoqueTextEdit.Text = produto.Estoque.ToString();
            marcaLookUpEdit.EditValue = produto.Marca;
            custoTextEdit.Text = produto.Custo.ToString(CultureInfo.InvariantCulture);
            markupTextEdit.Text = produto.Markup.ToString(CultureInfo.InvariantCulture);
            precoVendaTextEdit.Text = produto.PrecoDaVenda.ToString(CultureInfo.InvariantCulture);
            origemDaMercadoriaLookUpEdit.EditValue = produto.InformacoesFiscais.OrigemDaMercadoria;
            situacaoTributariaLookUpEdit.EditValue = produto.InformacoesFiscais.SituacaoTributaria;
            naturezaDaOperacaoLookUpEdit.EditValue = produto.InformacoesFiscais.NaturezaDaOperacao;
            ncmTextEdit.Text = produto.InformacoesFiscais.Ncm;
            aliquotaDeIcmsTextEdit.Text = produto.InformacoesFiscais.AliquotaDeIcms.ToString(CultureInfo.InvariantCulture);
            reducaoDeCalculoIcmsTextEdit.Text = produto.InformacoesFiscais.ReducaoDeCalculo.ToString(CultureInfo.InvariantCulture); 
            _imagemDoProduto = produto.Imagem;
            if (produto.Imagem != null) imagemDoProdutoPictureEdit.Image = Image.FromStream(new MemoryStream(produto.Imagem));
        }

        private void adicionarImagemButton_Click(object sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            _imagemDoProduto = File.ReadAllBytes(openFileDialog.FileName);
            imagemDoProdutoPictureEdit.Image = Image.FromFile(openFileDialog.FileName);
        }

        private void excluirImagemButton_Click(object sender, EventArgs e)
        {
            _imagemDoProduto = null;
            imagemDoProdutoPictureEdit.Image = null;
        }

        private void codigoDeBarrasButton_Click(object sender, EventArgs e)
        {
            var gerarCodigoDeBarras = CalculadorDeCodigoDeBarras.GerarCodigoDeBarrasEAN13();
            if (!string.IsNullOrWhiteSpace(gerarCodigoDeBarras))
            {
                codigoDeBarrasTextEdit.EditValue = gerarCodigoDeBarras;
            }
        }

        private void AtualizarPrecoVenda()
        {
            if (decimal.TryParse(custoTextEdit.Text, out var custo) &&
                decimal.TryParse(markupTextEdit.Text, out var markup))
            {
                precoVendaTextEdit.Text = (custo + custo * (markup / 100)).ToString(CultureInfo.InvariantCulture);
            }
        }

        private void markupTextEdit_EditValueChanged_1(object sender, EventArgs e)
        {
            AtualizarPrecoVenda();
        }

        private void custoTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            AtualizarPrecoVenda();
        }

        private void pesquisarProdutoButtomItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            var pesquisaDeProdutosView = new PesquisaDeProdutosView();
            pesquisaDeProdutosView.ShowDialog();
        }

        private void alterarBancoDeDadosButton_Click(object sender, EventArgs e)
        {
            var alterarBancoDeDados = new ConfigurarCaminhoDoBancoDeDadosView();
            alterarBancoDeDados.ShowDialog();
        }
    }
}
