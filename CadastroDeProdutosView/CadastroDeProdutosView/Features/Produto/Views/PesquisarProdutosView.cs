using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CadastroDeProdutosView.Features.Produto.Views
{
    public partial class PesquisarProdutosView : Form
    {
        private const string connectionString =
            @"User ID=SYSDBA;Password=masterkey;Database=C:\Users\admin\Documents\BANCODEDADOSPRODUTOS.FDB;DataSource=localhost;Port=3050;Dialect=3;Charset=NONE;";

        private readonly string nomeProduto;

        public PesquisarProdutosView(string nomeProduto)
        {
            InitializeComponent();
            this.nomeProduto = nomeProduto;
        }

        public DataTable PesquisarProdutos(string nomeProduto)
        {
            var tabelaData = new DataTable();
            using (var conexao = new FbConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    const string query =
                        @"SELECT idProduto, nome, categoria, codigoDeBarras, unidadeDeMedida,
                        estoque, precoDaVenda
                        FROM PRODUTO
                        WHERE UPPER(nome) LIKE UPPER(@nomeProduto)";
                    using (var cmd = new FbCommand(query, conexao))
                    {
                        cmd.Parameters.AddWithValue("@nomeProduto", $"%{nomeProduto}%");
                        using (var da = new FbDataAdapter(cmd))
                        {
                            da.Fill(tabelaData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Erro: {ex.Message}");
                }
            }
            return tabelaData;
        }

        private void pesquisarGridControl_Click(object sender, EventArgs e)
        {
        }

        private void salvarProdutoButtomItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void produtosDesativadosToggleSwitchh_Toggled(object sender, EventArgs e)
        {
        }

        private void pesquisarTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            var pesquisa = pesquisarTextEdit.Text;
            var produtos = PesquisarProdutos(pesquisa);
            pesquisarGridControl.DataSource = produtos;
            pesquisarGridView.BestFitColumns();
        }

        private void CarregarDados()
        {
            var produtos = PesquisarProdutos(nomeProduto);
            pesquisarGridControl.DataSource = produtos;
            pesquisarGridView.BestFitColumns();
        }

        private void PesquisarProdutosView_Load(object sender, EventArgs e)
        {
            CarregarDados();
        }

        private void produtosDesativadosLabelControl_Click(object sender, EventArgs e)
        {
        }

        private void AlterarButtomItem(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (pesquisarGridView.FocusedRowHandle < 0)
            {
                XtraMessageBox.Show("Selecione um produto antes de alterar.");
                return;
            }

            var idProdutoSelecionado = pesquisarGridView.GetFocusedRowCellValue("idProduto");

            if (idProdutoSelecionado == null)
            {
                XtraMessageBox.Show("Não foi possível obter o ID do produto.");
                return;
            }

            var cadastroDeProdutos = new CadastroDeProdutosView((int)idProdutoSelecionado);
            cadastroDeProdutos.ShowDialog();

            CarregarDados();

            const string novoNome = "Novo Nome";
            const string novaCategoria = "Nova Categoria";
            const string novoCodigoDeBarras = "Novo Código";
            const string novaUnidadeDeMedida = "Nova Unidade";
            const int novoEstoque = 0;
            const decimal novoPrecoDaVenda = 0;

            using (var conexao = new FbConnection(connectionString))
            {
                try
                {
                    const string query =
                        @"UPDATE PRODUTO
                        SET nome = @nome, categoria = @categoria, codigoDeBarras = @codigoDeBarras, 
                        unidadeDeMedida = @unidadeDeMedida, estoque = @estoque, precoDaVenda = @precoDaVenda
                        WHERE idProduto = @idProduto";
                    using (var cmd = new FbCommand(query, conexao))
                    {
                        cmd.Parameters.AddWithValue("@idProduto", idProdutoSelecionado);
                        cmd.Parameters.AddWithValue("@nome", novoNome);
                        cmd.Parameters.AddWithValue("@categoria", novaCategoria);
                        cmd.Parameters.AddWithValue("@codigoDeBarras", novoCodigoDeBarras);
                        cmd.Parameters.AddWithValue("@unidadeDeMedida", novaUnidadeDeMedida);
                        cmd.Parameters.AddWithValue("@estoque", novoEstoque);
                        cmd.Parameters.AddWithValue("@precoDaVenda", novoPrecoDaVenda);

                        conexao.Open();
                        cmd.ExecuteNonQuery();
                    }

                    XtraMessageBox.Show("Produto atualizado com sucesso!");
                }
                catch (FbException ex)
                {
                    XtraMessageBox.Show($"Erro ao atualizar produto: {ex.Message}");
                }
            }
        }

    }
}