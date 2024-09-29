using CadastroDeProdutosView.Features.Commons;
using CadastroDeProdutosView.Features.Commons.Services;
using CadastroDeProdutosView.Features.Produto.Enums;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Windows.Forms;

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

            this.produtoId = produtoId;
            connectionString = ConfiguracaoDoBancoDeDados.ObterStringDeConexao();
            if (produtoId > 0)
            {
                CarregarProduto(this.produtoId.Value);
            }
        }

        // Chamando as enums para as LookUpEdit
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
        }

        private void salvarButtomItem_ItemClick(object sender, ItemClickEventArgs e)
        {
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
                using var conexao = new FbConnection(connectionString);
                conexao.Open();
                using var transacao = conexao.BeginTransaction();

                if (produtoId is > 0)
                {
                    AlterarProduto(produtoId.Value);
                }
                else
                {
                    // Conexão com a tabela Produtos do banco de dados
                    const string insertProdutoQuery = @"
                        INSERT INTO PRODUTO (
                            Nome,
                            Categoria,
                            Fornecedor,
                            CodigoDeBarras,
                            UnidadeDeMedida,
                            Estoque,
                            Marca,
                            Custo,
                            Markup,
                            PrecoDaVenda)
                            VALUES 
                            (@Nome,
                            @Categoria,
                            @Fornecedor,
                            @codigoDeBarras,
                            @unidadeDeMedida,
                            @Estoque,
                            @Marca,
                            @Custo,
                            @Markup,
                            @precoDaVenda)
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
                        command.Parameters.Add("@markup", FbDbType.Decimal).Value = markup;
                        var precoVenda = ConversorParaDecimal.ParseDecimal(precoVendaTextEdit.Text);
                        command.Parameters.Add("@PrecoDaVenda", FbDbType.Decimal).Value = precoVenda;

                        idProduto = (int)command.ExecuteScalar();
                    }

                    // Conexão com a tabela Informações Fiscais do banco de dados
                    const string insertInformacoesFiscaisQuery = @"
                        INSERT INTO INFORMACOESFISCAIS
                            (idProduto,
                            origemDaMercadoria,
                            situacaoTributaria,
                            naturezaDaOperacao,
                            Ncm,
                            aliquotaDeIcms,
                            reducaoDeCalculo)
                            VALUES
                            (@idProduto,
                            @origemDaMercadoria,
                            @situacaoTributaria,
                            @naturezaDaOperacao,
                            @Ncm,
                            @aliquotaDeIcms,
                            @reducaoDeCalculo)";

                    using (var informacoesCommand = new FbCommand(insertInformacoesFiscaisQuery, conexao, transacao))
                    {
                        informacoesCommand.Parameters.Add("@idProduto", FbDbType.Integer).Value = idProduto;
                        informacoesCommand.Parameters.Add("@origemDaMercadoria", FbDbType.VarChar).Value = origemDaMercadoriaLookUpEdit.EditValue ?? DBNull.Value;
                        informacoesCommand.Parameters.Add("@situacaoTributaria", FbDbType.VarChar).Value = situacaoTributariaLookUpEdit.EditValue ?? DBNull.Value;
                        informacoesCommand.Parameters.Add("@naturezaDaOperacao", FbDbType.VarChar).Value = naturezaDaOperacaoLookUpEdit.EditValue ?? DBNull.Value;
                        informacoesCommand.Parameters.Add("@ncm", FbDbType.VarChar).Value = ncmTextEdit.Text ?? (object)DBNull.Value;
                        var aliquotaDeIcms = ConversorParaDecimal.ParseDecimal(aliquotaDeIcmsTextEdit.Text);
                        informacoesCommand.Parameters.Add("@aliquotaDeIcms", FbDbType.Decimal).Value = aliquotaDeIcms;
                        var reducaoDeCalculo = ConversorParaDecimal.ParseDecimal(reducaoDeCalculoIcmsTextEdit.Text);
                        informacoesCommand.Parameters.Add("reducaoDeCalculo", FbDbType.Decimal).Value = reducaoDeCalculo;
                        informacoesCommand.ExecuteNonQuery();
                    }
                    XtraMessageBox.Show("Produto cadastrado com sucesso");
                }
                LimparLookUpEditsETextEdits();
                transacao.Commit();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Erro ao salvar o produto: {ex.Message}");
            }
        }

        private void CarregarProduto(int idProduto)
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
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Erro ao carregar o produto: {ex.Message}");
            }
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
                        PrecoDaVenda = @PrecoDaVenda
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
                    command.ExecuteNonQuery();
                }
                XtraMessageBox.Show("Produto atualizado com sucesso.");
                AtualizarInformacoesFiscais(idProduto, conexao, transacao);
                transacao.Commit();
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

        private void AtualizarInformacoesFiscais(int idProduto, FbConnection conexao, FbTransaction transacao)
        {
            const string updateInformacoesFiscaisQuery = @"
                UPDATE INFORMACOESFISCAIS
                SET origemDaMercadoria = @origemDaMercadoria,
                situacaoTributaria = @situacaoTributaria,
                naturezaDaOperacao = @naturezaDaOperacao,
                Ncm = @Ncm,
                aliquotaDeIcms = @aliquotaDeIcms,
                reducaoDeCalculo = @reducaoDeCalculo
                WHERE idProduto = @idProduto";

            using var command = new FbCommand(updateInformacoesFiscaisQuery, conexao, transacao);
            command.Parameters.Add("@idProduto", FbDbType.Integer).Value = idProduto;
            command.Parameters.Add("@origemDaMercadoria", FbDbType.VarChar).Value = origemDaMercadoriaLookUpEdit.EditValue ?? DBNull.Value;
            command.Parameters.Add("@situacaoTributaria", FbDbType.VarChar).Value = situacaoTributariaLookUpEdit.EditValue ?? DBNull.Value;
            command.Parameters.Add("@naturezaDaOperacao", FbDbType.VarChar).Value = naturezaDaOperacaoLookUpEdit.EditValue ?? DBNull.Value;
            command.Parameters.Add("@Ncm", FbDbType.VarChar).Value = (object)ncmTextEdit.Text ?? DBNull.Value;
            var aliquotaDeIcms = ConversorParaDecimal.ParseDecimal(aliquotaDeIcmsTextEdit.Text);
            command.Parameters.Add("@aliquotaDeIcms", FbDbType.Decimal).Value = aliquotaDeIcms;
            var reducaoDeCalculo = ConversorParaDecimal.ParseDecimal(reducaoDeCalculoIcmsTextEdit.Text);
            command.Parameters.Add("reducaoDeCalculo", FbDbType.Decimal).Value = reducaoDeCalculo;

            command.ExecuteNonQuery();
        }

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

        private void pesquisarProdutoButtomItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            Hide();
            var pesquisarProdutos = new PesquisaDeProdutosView();
            pesquisarProdutos.ShowDialog();
            Close();
        }

        private void fornecedorTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            fornecedorTextEdit.Properties.MaxLength = 50;
        }

        private void precoVendaTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            precoVendaTextEdit.Properties.MaxLength = 6;
        }
        private void ncmTextEdit_EditValueChanged_1(object sender, EventArgs e)
        {
            ncmTextEdit.Properties.MaxLength = 8;
        }

        private void estoqueTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            estoqueTextEdit.Properties.MaxLength = 9;
        }

        private void nomeTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            nomeTextEdit.Properties.MaxLength = 100;
        }

        private void marcaLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            marcaLookUpEdit.Properties.MaxLength = 50;
        }

        private void reducaoDeCalculoIcmsTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            reducaoDeCalculoIcmsTextEdit.Properties.MaxLength = 5;
        }

        private void aliquotaDeIcmsTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            aliquotaDeIcmsTextEdit.Properties.MaxLength = 5;
        }
    }
}