using CadastroDeProdutosView.Features.Commons;
using CadastroDeProdutosView.Features.Commons.Services;
using CadastroDeProdutosView.Features.Produto.Enums;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CadastroDeProdutosView.Features.Produto.Views
{
    public partial class CadastroDeProdutosView : Form
    {
        private readonly int? produtoId;
        private byte[] imagemDoProduto = null!;
        private readonly string connectionString;

        public CadastroDeProdutosView(int produtoId)
        {
            InitializeComponent();
            InitializeLookUpEdit();
            LimitadorDeCaracteres();
            

            this.produtoId = produtoId;
            connectionString = ConfiguracaoDoBancoDeDados.ObterStringDeConexao();
            if (produtoId > 0)
            {
                CarregarProduto(this.produtoId.Value);
            }
        }
        
        // Adicionando as enums services para as LookUpEdit
        private void InitializeLookUpEdit()
        {
            unidadeDeMedidaLookUpEdit.PreencherLookUpEditComOValorDoEnum<UnidadeDeMedidaView.UnidadeDeMedida>();
            categoriaDeProdutosLookUpEdit.PreencherLookUpEditComOValorDoEnum<CategoriaDoProdutoView.CategoriaDeProdutos>();
            marcaLookUpEdit.PreencherLookUpEditComOValorDoEnum<MarcaDoProdutoView.MarcaDoProduto>();
            origemDaMercadoriaLookUpEdit.PreencherLookUpEditComOValorDoEnum<OrigemDaMercadoriaView.OrigemDaMercadoria>();
            situacaoTributariaLookUpEdit.PreencherLookUpEditComOValorDoEnum<SituacaoTributariaView.SituacaoTributaria>();
            naturezaDaOperacaoLookUpEdit.PreencherLookUpEditComOValorDoEnum<NaturezaDaOperacaoView.NaturezaDaOperacao>();
        }

        // Limpando as LookUpEdits, TextEdit e ImageEdit
        private void LimparLookUpEditsETextEdits()
        {
            nomeTextEdit.LimpezaDeTextEdit();
            fornecedorTextEdit.LimpezaDeTextEdit();
            codigodebarrasTextEdit.LimpezaDeTextEdit();
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

        // Limitando a quantidade de caracteres dos campos para que seja responsivo com o Banco de Dados
        private void LimitadorDeCaracteres()
        {
            fornecedorTextEdit.Properties.MaxLength = 50;
            precoVendaTextEdit.Properties.MaxLength = 6;
            codigodebarrasTextEdit.Properties.MaxLength = 13;
            ncmTextEdit.Properties.MaxLength = 8;
            estoqueTextEdit.Properties.MaxLength = 9;
            nomeTextEdit.Properties.MaxLength = 100;
            marcaLookUpEdit.Properties.MaxLength = 50;
            reducaoDeCalculoIcmsTextEdit.Properties.MaxLength = 6;
            aliquotaDeIcmsTextEdit.Properties.MaxLength = 6;
        }


        private void salvarButtomItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            codigodebarrasTextEdit.Properties.MaxLength = 13;

            if (!string.IsNullOrWhiteSpace(codigodebarrasTextEdit.Text))
            {
                var codigoDeBarrasValido = CalculadorDeCodigoDeBarras.ValidarCodigoDeBarrasEAN13(codigodebarrasTextEdit.Text);

                if (!codigoDeBarrasValido)
                {
                    XtraMessageBox.Show("O código de barras não é um tipo EAN-13");
                    codigodebarrasLabelControl.Text = "Código de Barras: <color=red>*</color>";
                    codigodebarrasLabelControl.AllowHtmlString = true;
                    return;
                }
            }

            codigodebarrasLabelControl.Text = "Codigo de Barras:";
            codigodebarrasLabelControl.AllowHtmlString = false;

            if (!ValidacaoDeCamposObrigatorios.ValidacaoParaCamposObrigatorios(
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
                XtraMessageBox.Show("Todos os campos obrigatórios devem ser preenchidos!");
                return;
            }

            try
            { 
                // Conexão com a tabela Produtos do banco de dados
                using var conexao = new FbConnection(connectionString);
                conexao.Open();
                using var transacao = conexao.BeginTransaction();

                if (produtoId is > 0) 
                {
                    AlterarProduto(produtoId.Value);
                }
                else
                {
                    const string insertProdutoQuery = @"
                        INSERT INTO PRODUTO
                        (nome, categoria, Fornecedor, codigoDeBarras, unidadeDeMedida, estoque, marca, custo, markup, precoDaVenda, imagem)
                        VALUES (@Nome, @Categoria, @Fornecedor, @codigoDeBarras, @unidadeDeMedida, @Estoque, @Marca, @Custo, @Markup, @precoDaVenda, @Imagem)
                        RETURNING idProduto";

                    int idProduto;

                    using (var command = new FbCommand(insertProdutoQuery, conexao, transacao))
                    {
                        command.Parameters.Add("@Nome", FbDbType.VarChar).Value = nomeTextEdit.Text ?? (object)DBNull.Value;
                        command.Parameters.Add("@Categoria", FbDbType.VarChar).Value = categoriaDeProdutosLookUpEdit.EditValue ?? DBNull.Value;
                        command.Parameters.Add("@Fornecedor", FbDbType.VarChar).Value = fornecedorTextEdit.Text ?? (object)DBNull.Value;
                        command.Parameters.Add("@CodigoDeBarras", FbDbType.VarChar).Value = codigodebarrasTextEdit.Text ?? (object)DBNull.Value;
                        command.Parameters.Add("@UnidadeDeMedida", FbDbType.VarChar).Value = unidadeDeMedidaLookUpEdit.EditValue ?? DBNull.Value;
                        command.Parameters.Add("@Marca", FbDbType.VarChar).Value = marcaLookUpEdit.EditValue ?? DBNull.Value;
                        command.Parameters.Add("@Estoque", FbDbType.Integer).Value = estoqueTextEdit.Text ?? (object)DBNull.Value;
                        var custo = ConversorParaDecimal.ParseDecimal(custoTextEdit.Text);
                        command.Parameters.Add("@Custo", FbDbType.Decimal).Value = custo;
                        var markup = ConversorParaDecimal.ParseDecimal(markupTextEdit.Text);
                        command.Parameters.Add("@Markup", FbDbType.Decimal).Value = markup;
                        var precoVenda = ConversorParaDecimal.ParseDecimal(precoVendaTextEdit.Text);
                        command.Parameters.Add("@PrecoDaVenda", FbDbType.Decimal).Value = precoVenda;
                        command.Parameters.Add("@Imagem", FbDbType.Binary).Value = imagemDoProduto;

                        idProduto = (int)command.ExecuteScalar();
                        AtualizarGridDeProdutos();
                    }

                    // Conexão com a tabela Informações Fiscais do banco de dados
                    const string insertInformacoesFiscaisQuery = @"
                        INSERT INTO INFORMACOESFISCAIS
                        (idProduto, origemDaMercadoria, situacaoTributaria, naturezaDaOperacao, ncm, aliquotaDeIcms, reducaoDeCalculo)
                        VALUES (@idProduto, @origemDaMercadoria, @situacaoTributaria, @naturezaDaOperacao, @Ncm, @aliquotaDeIcms, @reducaoDeCalculo)";

                    using (var informacoesCommand = new FbCommand(insertInformacoesFiscaisQuery, conexao, transacao))
                    {
                        informacoesCommand.Parameters.Add("@idProduto", FbDbType.Integer).Value = idProduto;
                        informacoesCommand.Parameters.Add("@origemDaMercadoria", FbDbType.VarChar).Value = origemDaMercadoriaLookUpEdit.EditValue ?? DBNull.Value;
                        informacoesCommand.Parameters.Add("@situacaoTributaria", FbDbType.VarChar).Value = situacaoTributariaLookUpEdit.EditValue ?? DBNull.Value;
                        informacoesCommand.Parameters.Add("@naturezaDaOperacao", FbDbType.VarChar).Value = naturezaDaOperacaoLookUpEdit.EditValue ?? DBNull.Value;
                        informacoesCommand.Parameters.Add("@Ncm", FbDbType.VarChar).Value = ncmTextEdit.Text ?? (object)DBNull.Value;
                        var aliquotaDeIcms = ConversorParaDecimal.ParseDecimal(aliquotaDeIcmsTextEdit.Text);
                        informacoesCommand.Parameters.Add("@aliquotaDeIcms", FbDbType.Decimal).Value = aliquotaDeIcms;
                        var reducaoDeCalculo = ConversorParaDecimal.ParseDecimal(reducaoDeCalculoIcmsTextEdit.Text);
                        informacoesCommand.Parameters.Add("reducaoDeCalculo", FbDbType.Decimal).Value = reducaoDeCalculo;
                        informacoesCommand.ExecuteNonQuery();
                    }
                    XtraMessageBox.Show("Produto cadastrado com sucesso");
                    LimparLookUpEditsETextEdits();
                }
                transacao.Commit();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Erro ao salvar o produto: {ex.Message}");
            }
        }

        public void CarregarProduto(int idProduto)
        {
            try
            {
                // Conexão com o banco de dados para preencher o form de acordo com os produtos ja cadastrados
                using var conexao = new FbConnection(connectionString);
                conexao.Open();
                const string preenchimentoDeTabelasQuery = @"
                    SELECT P.*, I.*
                    FROM PRODUTO P
                    LEFT JOIN INFORMACOESFISCAIS I ON P.idProduto = I.idProduto
                    WHERE P.idProduto = @idProduto";

                using var command = new FbCommand(preenchimentoDeTabelasQuery, conexao);
                command.Parameters.AddWithValue("@idProduto", idProduto);
                using var leituraDeDados = command.ExecuteReader();

                if (!leituraDeDados.Read()) return;
                nomeTextEdit.Text = leituraDeDados["Nome"].ToString();
                categoriaDeProdutosLookUpEdit.EditValue = leituraDeDados["Categoria"];
                fornecedorTextEdit.Text = leituraDeDados["Fornecedor"].ToString();
                codigodebarrasTextEdit.Text = leituraDeDados["CodigoDeBarras"].ToString();
                unidadeDeMedidaLookUpEdit.EditValue = leituraDeDados["UnidadeDeMedida"];
                estoqueTextEdit.Text = leituraDeDados["Estoque"].ToString();
                marcaLookUpEdit.EditValue = leituraDeDados["Marca"];
                custoTextEdit.Text = leituraDeDados["Custo"].ToString();
                markupTextEdit.Text = leituraDeDados["Markup"].ToString();
                precoVendaTextEdit.Text = leituraDeDados["PrecoDaVenda"].ToString();
                origemDaMercadoriaLookUpEdit.EditValue = leituraDeDados["origemDaMercadoria"];
                situacaoTributariaLookUpEdit.EditValue = leituraDeDados["situacaoTributaria"];
                naturezaDaOperacaoLookUpEdit.EditValue = leituraDeDados["naturezaDaOperacao"];
                ncmTextEdit.Text = leituraDeDados["ncm"].ToString();
                aliquotaDeIcmsTextEdit.Text = leituraDeDados["aliquotaDeIcms"].ToString();
                reducaoDeCalculoIcmsTextEdit.Text = leituraDeDados["reducaoDeCalculo"].ToString();
                Conversor(leituraDeDados);

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Erro ao carregar o produto: {ex.Message}");
            }
        }

        private void AtualizarGridDeProdutos()
        {
            const string selectProdutosQuery = "SELECT * FROM PRODUTO ORDER BY Nome";
            using var conexao = new FbConnection(connectionString);
            conexao.Open();
            using var command = new FbCommand(selectProdutosQuery, conexao);
            using var reader = command.ExecuteReader();
        }

        public void AlterarProduto(int idProduto)
        {
            using var conexao = new FbConnection(connectionString);
            conexao.Open();

            using var transacao = conexao.BeginTransaction();
            try
            {
                const string updateProdutoQuery = @"
                        UPDATE PRODUTO
                        SET Nome = @Nome, 
                        Categoria = @Categoria, 
                        Fornecedor = @Fornecedor, 
                        CodigoDeBarras = @CodigoDeBarras, 
                        UnidadeDeMedida = @UnidadeDeMedida, 
                        Estoque = @Estoque, 
                        Marca = @Marca, 
                        Custo = @Custo, 
                        Markup = @Markup, 
                        PrecoDaVenda = @PrecoDaVenda,
                        imagem = @Imagem
                        WHERE idProduto = @idProduto";

                using (var command = new FbCommand(updateProdutoQuery, conexao, transacao))
                {
                    command.Parameters.Add("@idProduto", FbDbType.Integer).Value = idProduto;
                    command.Parameters.Add("@Nome", FbDbType.VarChar).Value = nomeTextEdit.Text ?? (object)DBNull.Value;
                    command.Parameters.Add("@Categoria", FbDbType.VarChar).Value = categoriaDeProdutosLookUpEdit.EditValue ?? DBNull.Value;
                    command.Parameters.Add("@Fornecedor", FbDbType.VarChar).Value = fornecedorTextEdit.Text ?? (object)DBNull.Value;
                    command.Parameters.Add("@CodigoDeBarras", FbDbType.VarChar).Value = codigodebarrasTextEdit.Text ?? (object)DBNull.Value;
                    command.Parameters.Add("@UnidadeDeMedida", FbDbType.VarChar).Value = unidadeDeMedidaLookUpEdit.EditValue ?? DBNull.Value;
                    command.Parameters.Add("@Estoque", FbDbType.VarChar).Value = estoqueTextEdit.Text ?? (object)DBNull.Value;
                    command.Parameters.Add("@Marca", FbDbType.VarChar).Value = marcaLookUpEdit.EditValue ?? DBNull.Value;
                    var custo = ConversorParaDecimal.ParseDecimal(custoTextEdit.Text);
                    command.Parameters.Add("@Custo", FbDbType.Decimal).Value = custo;
                    var markup = ConversorParaDecimal.ParseDecimal(markupTextEdit.Text);
                    command.Parameters.Add("@Markup", FbDbType.Decimal).Value = markup;
                    var precoVenda = ConversorParaDecimal.ParseDecimal(precoVendaTextEdit.Text);
                    command.Parameters.Add("@PrecoDaVenda", FbDbType.Decimal).Value = precoVenda;
                    command.Parameters.Add("@Imagem", FbDbType.Binary).Value = imagemDoProduto;
                    command.ExecuteNonQuery();
                }

                const string updateInformacoesFiscaisQuery = @"
                UPDATE INFORMACOESFISCAIS
                SET origemDaMercadoria = @origemDaMercadoria,
                situacaoTributaria = @situacaoTributaria,
                naturezaDaOperacao = @naturezaDaOperacao,
                Ncm = @Ncm,
                aliquotaDeIcms = @aliquotaDeIcms,
                reducaoDeCalculo = @reducaoDeCalculo
                WHERE idProduto = @idProduto";
                using (var commandInformacoesFiscais = new FbCommand(updateInformacoesFiscaisQuery, conexao, transacao))
                {
                    commandInformacoesFiscais.Parameters.Add("@idProduto", FbDbType.Integer).Value = idProduto;
                    commandInformacoesFiscais.Parameters.Add("@origemDaMercadoria", FbDbType.VarChar).Value = origemDaMercadoriaLookUpEdit.EditValue ?? DBNull.Value;
                    commandInformacoesFiscais.Parameters.Add("@situacaoTributaria", FbDbType.VarChar).Value = situacaoTributariaLookUpEdit.EditValue ?? DBNull.Value;
                    commandInformacoesFiscais.Parameters.Add("@naturezaDaOperacao", FbDbType.VarChar).Value = naturezaDaOperacaoLookUpEdit.EditValue ?? DBNull.Value;
                    commandInformacoesFiscais.Parameters.Add("@Ncm", FbDbType.VarChar).Value = (object)ncmTextEdit.Text ?? DBNull.Value;
                    var aliquotaDeIcms = ConversorParaDecimal.ParseDecimal(aliquotaDeIcmsTextEdit.Text);
                    commandInformacoesFiscais.Parameters.Add("@aliquotaDeIcms", FbDbType.Decimal).Value = aliquotaDeIcms;
                    var reducaoDeCalculo = ConversorParaDecimal.ParseDecimal(reducaoDeCalculoIcmsTextEdit.Text);
                    commandInformacoesFiscais.Parameters.Add("reducaoDeCalculo", FbDbType.Decimal).Value = reducaoDeCalculo;
                    commandInformacoesFiscais.ExecuteNonQuery();
                }
                var messageBox = new MessageBoxCustomizado("Tem certeza que deseja alterar esse produto?");
                messageBox.ShowDialog();
                if (!messageBox.Resultado) return;
                XtraMessageBox.Show("Produto alterado com sucesso");
                LimparLookUpEditsETextEdits(); 
                transacao.Commit();

                Close();
            }
            catch (Exception ex)
            {
                transacao.Rollback();
                throw new Exception("Produto não foi alterado no banco de dados!", ex);
            }
        }
        private void adicionarImagemButton_Click(object sender, EventArgs e)
        {
            using var abrirExploradoDeArquivo = new OpenFileDialog();
            abrirExploradoDeArquivo.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tiff;*.tif;*.webp;*.heic;*.svg;*.cr2;*.nef;*.arw";

            if (abrirExploradoDeArquivo.ShowDialog() != DialogResult.OK) return;

            var caminhoDaImagem = abrirExploradoDeArquivo.FileName;
            imagemDoProduto = ConversorDeImagemParaByte.ConversorDeValoresDeImagemParaByte(caminhoDaImagem, 190, 241);

            using var imagemOriginal = Image.FromFile(caminhoDaImagem);
            imagemDoProdutoPictureEdit.Image = new Bitmap(imagemOriginal, new Size(190, 241));
        }

        // Converte o array de bytes da imagem do banco de dados em uma imagem para exibir no picturebox
        private void Conversor(FbDataReader leituraDeDados)
        {
            if (leituraDeDados["Imagem"] != DBNull.Value)
            {
                var imagemBytes = (byte[])leituraDeDados["Imagem"];
                using var ms = new MemoryStream(imagemBytes);
                imagemDoProdutoPictureEdit.Image = Image.FromStream(ms);
            }
            else imagemDoProdutoPictureEdit.Image = null;
        }
        
        // Edita o valor da TextEdit do codigo de barras para o tipo EAN13 caso seja clicado no botão
        private void codigoDeBarrasButton_Click(object sender, EventArgs e)
        {
            var geradorDeCodigoDeBarras = CalculadorDeCodigoDeBarras.GerarCodigoDeBarrasEAN13();
            codigodebarrasTextEdit.EditValue = geradorDeCodigoDeBarras;
        }

        private void custoTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            custoTextEdit.Properties.MaxLength = 6;
            if (decimal.TryParse(custoTextEdit.Text, out var custo) &&
                decimal.TryParse(markupTextEdit.Text, out var markup))
            {
                CalculoDeCustoEMarkupParaPrecoVenda.CalcularPrecoVenda(custo, markup, precoVendaTextEdit);
            }
        }

        private void markupTextEdit_EditValueChanged_1(object sender, EventArgs e)
        {
            markupTextEdit.Properties.MaxLength = 6;
            if (decimal.TryParse(custoTextEdit.Text, out var custo) &&
                decimal.TryParse(markupTextEdit.Text, out var markup))
            {
                CalculoDeCustoEMarkupParaPrecoVenda.CalcularPrecoVenda(custo, markup, precoVendaTextEdit);
            }
        }

        private static Form? GetOpenForm<T>() where T : Form
        {
            return Application.OpenForms.OfType<T>().FirstOrDefault();
        }

        private void pesquisarProdutoButtomItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            var pesquisaForm = GetOpenForm<PesquisaDeProdutosView>();
            if (pesquisaForm != null)
            {
                pesquisaForm.Activate();
                return; 
            }
            var pesquisarProdutos = new PesquisaDeProdutosView();
            pesquisarProdutos.Show();
        }

        private void alterarBancoDeDadosButton_Click_1(object sender, EventArgs e)
        {
            var alterarBancoDeDados = new ConfigurarCaminhoDoBancoDeDadosView();
            alterarBancoDeDados.ShowDialog();
        }

        private void excluirImagemButton_Click(object sender, EventArgs e)
        {
            imagemDoProdutoPictureEdit.Image = null;
        }
    }
}