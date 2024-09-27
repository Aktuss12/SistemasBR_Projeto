using CadastroDeProdutosView.Features.Commons;
using CadastroDeProdutosView.Features.Produto.Enums;
using DevExpress.XtraEditors;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace CadastroDeProdutosView.Features.Produto.Views
{
    public partial class CadastroDeProdutosView : Form
    {
        private readonly int? produtoId;
        private readonly string connectionString;
        

        public CadastroDeProdutosView(int produtoId)
        {
            InitializeComponent();
            InitializeLookUpEdit();
            codigodebarrasTextEdit_EditValueChanged(null, null);

            this.produtoId = produtoId;
            connectionString = ConfiguracaoManager.ObterStringConexao();

            if (produtoId > 0)
            {
                CarregarProduto(this.produtoId.Value);
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

        // Metodo para limpar as LookUpEdits e TextEdits
        private void LimparLookUpEditsETextEdits()
        {
            nomeTextEdit.LimparTextEdit();
            fornecedorTextEdit.LimparTextEdit();
            codigodebarrasTextEdit.LimparTextEdit();
            estoqueTextEdit.LimparTextEdit();
            custoTextEdit.LimparTextEdit();
            markupTextEdit.LimparTextEdit();
            precoVendaTextEdit.LimparTextEdit();
            ncmTextEdit.LimparTextEdit();
            aliquotaDeIcmsTextEdit.LimparTextEdit();
            reducaoDeCalculoIcmsTextEdit.LimparTextEdit();
            categoriaDeProdutosLookUpEdit.LimparLookUpEdit();
            unidadeDeMedidaLookUpEdit.LimparLookUpEdit();
            marcaLookUpEdit.LimparLookUpEdit();
            origemDaMercadoriaLookUpEdit.LimparLookUpEdit();
            situacaoTributariaLookUpEdit.LimparLookUpEdit();
            naturezaDaOperacaoLookUpEdit.LimparLookUpEdit();
        }

        public void AtualizarProduto(int idProduto)
        {
            try
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
                         PrecoDaVenda = @PrecoDaVenda
                        WHERE idProduto = @idProduto";

                    using (var command = new FbCommand(updateProdutoQuery, conexao, transacao))
                    {
                        command.Parameters.Add("@Nome", FbDbType.VarChar).Value = nomeTextEdit.Text ?? (object)DBNull.Value;
                        command.Parameters.Add("@Categoria", FbDbType.VarChar).Value = categoriaDeProdutosLookUpEdit.EditValue ?? DBNull.Value;
                        command.Parameters.Add("@Fornecedor", FbDbType.VarChar).Value = fornecedorTextEdit.Text ?? (object)DBNull.Value;
                        command.Parameters.Add("@CodigoDeBarras", FbDbType.VarChar).Value = codigodebarrasTextEdit.Text ?? (object)DBNull.Value;
                        command.Parameters.Add("@UnidadeDeMedida", FbDbType.VarChar).Value = unidadeDeMedidaLookUpEdit.EditValue ?? DBNull.Value;
                        command.Parameters.Add("@Estoque", FbDbType.Integer).Value = int.TryParse(estoqueTextEdit.EditValue.ToString(), out var estoque) ? estoque : DBNull.Value;
                        command.Parameters.Add("@Marca", FbDbType.VarChar).Value = marcaLookUpEdit.EditValue ?? DBNull.Value;

                        if (decimal.TryParse(custoTextEdit.Text, out var custo))
                            command.Parameters.Add("@Custo", FbDbType.Decimal).Value = custo;
                        else
                            command.Parameters.Add("@Custo", FbDbType.Decimal).Value = DBNull.Value;

                        if (decimal.TryParse(markupTextEdit.Text, out var markup))
                            command.Parameters.Add("@Markup", FbDbType.Decimal).Value = markup;
                        else
                            command.Parameters.Add("@Markup", FbDbType.Decimal).Value = DBNull.Value;

                        if (decimal.TryParse(precoVendaTextEdit.Text, out var precoVenda))
                            command.Parameters.Add("@PrecoDaVenda", FbDbType.Decimal).Value = precoVenda;
                        else
                            command.Parameters.Add("@PrecoDaVenda", FbDbType.Decimal).Value = DBNull.Value;

                        command.Parameters.Add("@idProduto", FbDbType.Integer).Value = idProduto;

                        command.ExecuteNonQuery();
                    }

                    AtualizarInformacoesFiscais(idProduto, conexao, transacao);

                    transacao.Commit();
                    XtraMessageBox.Show("Produto atualizado com sucesso.");
                    LimparLookUpEditsETextEdits();
                                        Hide();
                    Close();
                }
                catch (Exception ex)
                {
                    transacao.Rollback();
                    throw new Exception("Erro ao atualizar o produto no banco de dados", ex);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Erro ao conectar ao banco de dados: {ex.Message}");
            }
        }

        private void salvarButtomItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!ValidarCamposObrigatorios.ValidacaoDeCamposObrigatorios(
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

            AtualizarEstiloLabelCodigoBarras(true);

            try
            {
                using var connection = new FbConnection(connectionString);
                connection.Open();

                using var transaction = connection.BeginTransaction();

                if (produtoId is > 0)
                {
                    AtualizarProduto(produtoId.Value);
                }
                else
                {
                    const string insertProdutoQuery = @"
                INSERT INTO PRODUTO (Nome, Categoria, Fornecedor, CodigoDeBarras, UnidadeDeMedida, Estoque, Marca, Custo, Markup, PrecoDaVenda)
                VALUES (@nome, @categoria, @fornecedor, @codigoDeBarras, @unidadeDeMedida, @estoque, @marca, @custo, @markup, @precoDaVenda)
                RETURNING idProduto";

                    int idProduto;

                    using (var command = new FbCommand(insertProdutoQuery, connection, transaction))
                    {
                        command.Parameters.Add("@Nome", FbDbType.VarChar).Value = nomeTextEdit.Text ?? (object)DBNull.Value;
                        command.Parameters.Add("@Categoria", FbDbType.VarChar).Value = categoriaDeProdutosLookUpEdit.EditValue ?? DBNull.Value;
                        command.Parameters.Add("@Fornecedor", FbDbType.VarChar).Value = fornecedorTextEdit.Text ?? (object)DBNull.Value;
                        command.Parameters.Add("@CodigoDeBarras", FbDbType.VarChar).Value = codigodebarrasTextEdit.Text ?? (object)DBNull.Value;
                        command.Parameters.Add("@UnidadeDeMedida", FbDbType.VarChar).Value = unidadeDeMedidaLookUpEdit.EditValue ?? DBNull.Value;
                        command.Parameters.Add("@Estoque", FbDbType.Integer).Value = int.TryParse(estoqueTextEdit.EditValue.ToString(), out var estoque) ? estoque : DBNull.Value;
                        command.Parameters.Add("@Marca", FbDbType.VarChar).Value = marcaLookUpEdit.EditValue ?? DBNull.Value;

                        if (decimal.TryParse(custoTextEdit.Text, out var custo))
                            command.Parameters.Add("@Custo", FbDbType.Decimal).Value = custo;
                        else
                            command.Parameters.Add("@Custo", FbDbType.Decimal).Value = DBNull.Value;

                        if (decimal.TryParse(markupTextEdit.Text, out var markup))
                            command.Parameters.Add("@Markup", FbDbType.Decimal).Value = markup;
                        else
                            command.Parameters.Add("@Markup", FbDbType.Decimal).Value = DBNull.Value;

                        if (decimal.TryParse(precoVendaTextEdit.Text, out var precoVenda))
                            command.Parameters.Add("@PrecoDaVenda", FbDbType.Decimal).Value = precoVenda;
                        else
                            command.Parameters.Add("@PrecoDaVenda", FbDbType.Decimal).Value = DBNull.Value;

                        idProduto = (int)command.ExecuteScalar();
                    }

                    const string insertInformacoesFiscaisQuery = @"
                INSERT INTO INFORMACOESFISCAIS (idProduto, origemDaMercadoria, situacaoTributaria, naturezaDaOperacao, ncm, aliquotaDeIcms, reducaoDeCalculo)
                VALUES (@idProduto, @origemDaMercadoria, @situacaoTributaria, @naturezaDaOperacao, @ncm, @aliquotaDeIcms, @reducaoDeCalculo)";

                    using (var informacoesCommand = new FbCommand(insertInformacoesFiscaisQuery, connection, transaction))
                    {
                        informacoesCommand.Parameters.Add("@idProduto", FbDbType.Integer).Value = idProduto;
                        informacoesCommand.Parameters.Add("@origemDaMercadoria", FbDbType.VarChar).Value = origemDaMercadoriaLookUpEdit.EditValue ?? DBNull.Value;
                        informacoesCommand.Parameters.Add("@situacaoTributaria", FbDbType.VarChar).Value = situacaoTributariaLookUpEdit.EditValue ?? DBNull.Value;
                        informacoesCommand.Parameters.Add("@naturezaDaOperacao", FbDbType.VarChar).Value = naturezaDaOperacaoLookUpEdit.EditValue ?? DBNull.Value;
                        informacoesCommand.Parameters.Add("@ncm", FbDbType.VarChar).Value = ncmTextEdit.Text ?? (object)DBNull.Value;

                        if (decimal.TryParse(aliquotaDeIcmsTextEdit.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var aliquotaIcms))
                            informacoesCommand.Parameters.Add("@aliquotaDeIcms", FbDbType.Decimal).Value = aliquotaIcms;
                        else
                            informacoesCommand.Parameters.Add("@aliquotaDeIcms", FbDbType.Decimal).Value = DBNull.Value;

                        if (decimal.TryParse(reducaoDeCalculoIcmsTextEdit.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var reducaoCalculo))
                            informacoesCommand.Parameters.Add("@reducaoDeCalculo", FbDbType.Decimal).Value = reducaoCalculo;
                        else
                            informacoesCommand.Parameters.Add("@reducaoDeCalculo", FbDbType.Decimal).Value = DBNull.Value;

                        informacoesCommand.ExecuteNonQuery();
                    }

                    XtraMessageBox.Show("Produto cadastrado com sucesso");
                }

                LimparLookUpEditsETextEdits();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Erro ao salvar o produto: {ex.Message}");
            }
        }

        private void AtualizarInformacoesFiscais(int idProduto, FbConnection conexao, FbTransaction transacao)
        {
            const string updateInformacoesFiscaisQuery = @"
                UPDATE INFORMACOESFISCAIS
                SET origemDaMercadoria = @origemDaMercadoria,
                situacaoTributaria = @situacaoTributaria,
                naturezaDaOperacao = @naturezaDaOperacao,
                ncm = @ncm,
                aliquotaDeIcms = @aliquotaDeIcms,
                reducaoDeCalculo = @reducaoDeCalculo
                WHERE idProduto = @idProduto";

            using var command = new FbCommand(updateInformacoesFiscaisQuery, conexao, transacao);
            command.Parameters.Add("@idProduto", FbDbType.Integer).Value = idProduto;
            command.Parameters.Add("@origemDaMercadoria", FbDbType.VarChar).Value = origemDaMercadoriaLookUpEdit.EditValue ?? DBNull.Value;
            command.Parameters.Add("@situacaoTributaria", FbDbType.VarChar).Value = situacaoTributariaLookUpEdit.EditValue ?? DBNull.Value;
            command.Parameters.Add("@naturezaDaOperacao", FbDbType.VarChar).Value = naturezaDaOperacaoLookUpEdit.EditValue ?? DBNull.Value;
            command.Parameters.Add("@ncm", FbDbType.VarChar).Value = ncmTextEdit.Text ?? (object)DBNull.Value;

            if (decimal.TryParse(aliquotaDeIcmsTextEdit.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var aliquotaIcms))
                command.Parameters.Add("@aliquotaDeIcms", FbDbType.Decimal).Value = aliquotaIcms;
            else
                command.Parameters.Add("@aliquotaDeIcms", FbDbType.Decimal).Value = DBNull.Value;

            if (decimal.TryParse(reducaoDeCalculoIcmsTextEdit.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var reducaoCalculo))
                command.Parameters.Add("@reducaoDeCalculo", FbDbType.Decimal).Value = reducaoCalculo;
            else
                command.Parameters.Add("@reducaoDeCalculo", FbDbType.Decimal).Value = DBNull.Value;

            command.ExecuteNonQuery();
        }


        private void CarregarProduto(int idProduto)
        {
            try
            {
                using var connection = new FbConnection(connectionString);
                connection.Open();
                const string query = @"
                    SELECT P.*, I.*
                    FROM PRODUTO P
                    LEFT JOIN INFORMACOESFISCAIS I ON P.idProduto = I.idProduto
                    WHERE P.idProduto = @idProduto";

                using var command = new FbCommand(query, connection);
                command.Parameters.AddWithValue("@idProduto", idProduto);

                using var reader = command.ExecuteReader();
                if (!reader.Read()) return;
                nomeTextEdit.Text = reader["Nome"].ToString();
                categoriaDeProdutosLookUpEdit.EditValue = reader["Categoria"];
                fornecedorTextEdit.Text = reader["Fornecedor"].ToString();
                codigodebarrasTextEdit.Text = reader["CodigoDeBarras"].ToString();
                unidadeDeMedidaLookUpEdit.EditValue = reader["UnidadeDeMedida"];
                estoqueTextEdit.Text = reader["Estoque"].ToString();
                marcaLookUpEdit.EditValue = reader["Marca"];
                custoTextEdit.Text = reader["Custo"].ToString();
                markupTextEdit.Text = reader["Markup"].ToString();
                precoVendaTextEdit.Text = reader["PrecoDaVenda"].ToString();

                origemDaMercadoriaLookUpEdit.EditValue = reader["origemDaMercadoria"];
                situacaoTributariaLookUpEdit.EditValue = reader["situacaoTributaria"];
                naturezaDaOperacaoLookUpEdit.EditValue = reader["naturezaDaOperacao"];
                ncmTextEdit.Text = reader["ncm"].ToString();
                aliquotaDeIcmsTextEdit.Text = reader["aliquotaDeIcms"].ToString();
                reducaoDeCalculoIcmsTextEdit.Text = reader["reducaoDeCalculo"].ToString();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Erro ao carregar o produto: {ex.Message}");
            }
        }

        private void codigoDeBarrasButton_Click(object sender, EventArgs e)
        {
            var verificarCodigoDeBarras = CalculadorDeCodigoDeBarras.GerarCodigoDeBarrasEAN13();
            if (CalculadorDeCodigoDeBarras.ValidarCodigoDeBarrasEAN13(verificarCodigoDeBarras))
            {
               var codigoDeBarras = CalculadorDeCodigoDeBarras.GerarCodigoDeBarrasEAN13();
                codigodebarrasTextEdit.EditValue = codigoDeBarras;
            }
            else
            {
                XtraMessageBox.Show("O código de barras EAN-13 não é válido!");
            }
        }

        private void AtualizarEstiloLabelCodigoBarras(bool valido)
        {
            codigodebarrasLabelControl.Text = valido
                ? "Codigo de Barras: "
                : "Codigo de Barras: <color=red>*</color>";

            codigodebarrasLabelControl.AllowHtmlString = !valido;
        }

        private void codigodebarrasTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            codigodebarrasTextEdit.Enabled = false;
        }

        private void custoTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(custoTextEdit.Text, out var custo) &&
                decimal.TryParse(markupTextEdit.Text, out var markup))
            {
                CalculoDeCustoEMarkupParaPrecoVenda.CalcularPrecoVenda(custo, markup, precoVendaTextEdit);
            }
        }

        private void markupTextEdit_EditValueChanged_1(object sender, EventArgs e)
        {
            if (decimal.TryParse(custoTextEdit.Text, out var custo) &&
                decimal.TryParse(markupTextEdit.Text, out var markup))
            {
                CalculoDeCustoEMarkupParaPrecoVenda.CalcularPrecoVenda(custo, markup, precoVendaTextEdit);
            }
        }

        // Abertura do form de pesquisar os produtos cadastrados, com validação de não abrir outro cadastro caso ja tenha aberto pelo alterar
        private void pesquisarProdutoButtomItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            Hide();
            var pesquisarProdutos = new PesquisarProdutosView();
            pesquisarProdutos.ShowDialog();
            Close();
        }

        private void fornecedorTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            fornecedorTextEdit.Properties.MaxLength = 100;
        }

        private void estoqueTextEdit_EditValueChanged_1(object sender, EventArgs e)
        {
            estoqueTextEdit.Properties.MaxLength = 12;
        }

        private void precoVendaTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            precoVendaTextEdit.Properties.MaxLength = 16;
        }
        private void ncmTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            ncmTextEdit.Properties.MaxLength = 8;
        }

        private void alterarBancoDeDadosButton_Click(object sender, EventArgs e)
        {
            var alterarBancoDeDados = new ConfigurarCaminhoDoBancoDeDadosView();
            alterarBancoDeDados.ShowDialog();
        }
    }
}