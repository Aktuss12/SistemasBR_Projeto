using CadastroDeProdutosView.Features.Commons;
using CadastroDeProdutosView.Features.Commons.Services;
using CadastroDeProdutosView.Features.Produto.Enums;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace CadastroDeProdutosView.Features.Produto.Views
{
    public partial class CadastroDeProdutosView : Form
    {

        private readonly int? _produtoId;
        private byte[] _imagemDoProduto = null!;
        private readonly ConexaoProdutosFirebird _conexao;

        public CadastroDeProdutosView(int produtoId = 0)
        {
            InitializeComponent();
            InitializeLookUpEdit();
            LimitadorDeCaracteres();

            _produtoId = produtoId > 0 ? produtoId : null;
            _conexao = new ConexaoProdutosFirebird();
            if (_produtoId.HasValue)
            {
                CarregarProduto(_produtoId.Value);
            }
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

        private void LimparLookUpEditsETextEdits()
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
                var produto = CriarProdutoDoFormulario();

                if (_produtoId.HasValue)
                {
                    produto.Id = _produtoId.Value;
                    _conexao.AlterarProduto(produto);
                    XtraMessageBox.Show("Produto alterado com sucesso");
                }
                else
                {
                    var novoProdutoid = _conexao.InserirProduto(produto);
                    XtraMessageBox.Show("Produto cadastrado com sucesso");
                    produto.Id = novoProdutoid;
                }

                LimparLookUpEditsETextEdits();

                ProdutosAtualizados?.Invoke();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Erro ao salvar o produto: {ex.Message}");
            }
        }

        private bool ValidarCampos()
        {
            if (!string.IsNullOrWhiteSpace(codigoDeBarrasTextEdit.Text))
            {
                if (!_conexao.ValidarCodigoDeBarras(codigoDeBarrasTextEdit.Text))
                {
                    XtraMessageBox.Show("O código de barras não é um tipo EAN-13");
                    codigodebarrasLabelControl.Text = "Código de Barras: <color=red>*</color>";
                    codigodebarrasLabelControl.AllowHtmlString = true;
                    return false;
                }
            }

            codigodebarrasLabelControl.Text = "Codigo de Barras:";
            codigodebarrasLabelControl.AllowHtmlString = false;

            if (ValidacaoDeCamposObrigatorios.ValidacaoParaCamposObrigatorios(
                    nomeTextEdit,
                    estoqueTextEdit,
                    precoVendaTextEdit,
                    unidadeDeMedidaLookUpEdit,
                    categoriaDeProdutosLookUpEdit,
                    nomeLabelControl,
                    estoqueLabelControl,
                    precoDaVendaLabelControl,
                    unidadeDeMedidaLabelControl,
                    categoriaLabelControl)) return true;
            XtraMessageBox.Show("Todos os campos obrigatórios devem ser preenchidos!");
            return false;
        }

        private Commons.Produto CriarProdutoDoFormulario()
        {
            return new Commons.Produto
            {
                Nome = nomeTextEdit.Text,
                Categoria = categoriaDeProdutosLookUpEdit.EditValue?.ToString(),
                Fornecedor = fornecedorTextEdit.Text,
                CodigoDeBarras = codigoDeBarrasTextEdit.Text,
                UnidadeDeMedida = unidadeDeMedidaLookUpEdit.EditValue?.ToString(),
                Estoque = int.Parse(estoqueTextEdit.Text),
                Marca = marcaLookUpEdit.EditValue?.ToString(),
                Custo = ConversorParaDecimal.ParseDecimal(custoTextEdit.Text),
                Markup = ConversorParaDecimal.ParseDecimal(markupTextEdit.Text),
                PrecoDaVenda = ConversorParaDecimal.ParseDecimal(precoVendaTextEdit.Text),
                Imagem = _imagemDoProduto,
                InformacoesFiscais = new InformacoesFiscais
                {
                    OrigemDaMercadoria = origemDaMercadoriaLookUpEdit.EditValue?.ToString(),
                    SituacaoTributaria = situacaoTributariaLookUpEdit.EditValue?.ToString(),
                    NaturezaDaOperacao = naturezaDaOperacaoLookUpEdit.EditValue?.ToString(),
                    Ncm = ncmTextEdit.Text,
                    AliquotaDeIcms = ConversorParaDecimal.ParseDecimal(aliquotaDeIcmsTextEdit.Text),
                    ReducaoDeCalculo = ConversorParaDecimal.ParseDecimal(reducaoDeCalculoIcmsTextEdit.Text)
                }
            };
        }

        public void CarregarProduto(int idProduto)
        {
            try
            {
                var produto = _conexao.CarregarProduto(idProduto);
                if (produto == null!) return;

                PreencherFormularioComProduto(produto);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Erro ao carregar o produto: {ex.Message}");
            }
        }

        private void PreencherFormularioComProduto(Commons.Produto produto)
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

            if (produto.Imagem == null) return;
            imagemDoProdutoPictureEdit.Image = _conexao.ConverterBytesParaImagem(produto.Imagem);
            _imagemDoProduto = produto.Imagem;
        }

        private void adicionarImagemButton_Click(object sender, EventArgs e)
        {
            using var abrirExploradoDeArquivo = new OpenFileDialog();
            abrirExploradoDeArquivo.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tiff;*.tif;*.webp;*.heic;*.svg;*.cr2;*.nef;*.arw";

            if (abrirExploradoDeArquivo.ShowDialog() != DialogResult.OK) return;

            var caminhoDaImagem = abrirExploradoDeArquivo.FileName;
            _imagemDoProduto = _conexao.ConverterImagemParaBytes(caminhoDaImagem, 190, 241);

            using var imagemOriginal = Image.FromFile(caminhoDaImagem);
            imagemDoProdutoPictureEdit.Image = new Bitmap(imagemOriginal, new Size(190, 241));
        }

        private void codigoDeBarrasButton_Click(object sender, EventArgs e)
        {
            var geradorDeCodigoDeBarras = _conexao.GerarCodigoDeBarras();
            codigoDeBarrasTextEdit.EditValue = geradorDeCodigoDeBarras;
        }

        private void custoTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            AtualizarPrecoVenda();
        }

        private void markupTextEdit_EditValueChanged_1(object sender, EventArgs e)
        {
            AtualizarPrecoVenda();
        }

        private void AtualizarPrecoVenda()
        {
            if (!decimal.TryParse(custoTextEdit.Text, out var custo) ||
                !decimal.TryParse(markupTextEdit.Text, out var markup)) return;
            var precoVenda = _conexao.CalcularPrecoVenda(custo, markup);
            precoVendaTextEdit.Text = precoVenda.ToString("N2");
        }

        private void pesquisarProdutoButtomItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            var pesquisaForm = Application.OpenForms.OfType<PesquisaDeProdutosView>().FirstOrDefault();
            if (pesquisaForm != null)
            {
                pesquisaForm.Activate();
                return;
            }

            var pesquisarProdutos = new PesquisaDeProdutosView();
            pesquisarProdutos.ShowDialog();
        }

        private void alterarBancoDeDadosButton_Click_1(object sender, EventArgs e)
        {
            var alterarBancoDeDados = new ConfigurarCaminhoDoBancoDeDadosView();
            alterarBancoDeDados.ShowDialog();
        }

        private void excluirImagemButton_Click(object sender, EventArgs e)
        {
            imagemDoProdutoPictureEdit.Image = null;
            _imagemDoProduto = null!;
        }

        private void codigoDeBarrasTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(codigoDeBarrasTextEdit.Text) || codigoDeBarrasTextEdit.Text != "0") return;
            codigoDeBarrasTextEdit.EditValue = null;
            codigoDeBarrasTextEdit.Refresh();
        }
    }
}