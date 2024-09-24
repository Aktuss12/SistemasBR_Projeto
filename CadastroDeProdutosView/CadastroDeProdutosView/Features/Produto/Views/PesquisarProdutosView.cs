using DevExpress.XtraEditors;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Windows.Forms;

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
            using (var conexao = new FbConnection(connectionString: connectionString))
            {
                try
                {
                    conexao.Open();
                    const string query =
                        @"SELECT idProduto, Nome, Categoria, CodigoDeBarras, UnidadeDeMedida, Estoque, PrecoDaVenda
                  FROM PRODUTO
                  WHERE UPPER(Nome) LIKE UPPER(@nomeProduto) AND ativo = 1";
                    using (var cmd = new FbCommand(cmdText: query, connection: conexao))
                    {
                        cmd.Parameters.AddWithValue(parameterName: "@nomeProduto", value: $"%{nomeProduto}%");
                        using (var da = new FbDataAdapter(selectCommand: cmd))
                        {
                            da.Fill(dataTable: tabelaData);
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(text: $"Erro: {ex.Message}");
                }
            }
            return tabelaData;
        }

        private void DesativarProduto(int idProduto)
        {
            const string query = "UPDATE PRODUTO SET ativo = 0 WHERE idProduto = @idProduto";

            using (var conexao = new FbConnection(connectionString))
            {
                var command = new FbCommand(query, conexao);
                command.Parameters.AddWithValue("@idProduto", idProduto);

                try
                {
                    conexao.Open();
                    var rowsAffected = command.ExecuteNonQuery();
                    XtraMessageBox.Show(rowsAffected > 0
                        ? "Produto desativado com sucesso."
                        : "Nenhum produto foi encontrado com esse ID.");
                }
                catch (FbException ex)
                {
                    XtraMessageBox.Show($"Erro ao desativar produto: {ex.Message}");
                }
                finally
                {
                    conexao.Close(); 
                }
            }

            AtualizarGridView();
        }

        private void AtualizarGridView()
        {
            const string query = "SELECT * FROM PRODUTO WHERE ativo = 1";
            var dtProduto = new DataTable();

            using (var conexao = new FbConnection(connectionString))
            {
                var adapter = new FbDataAdapter(query, conexao);
                conexao.Open();
                adapter.Fill(dtProduto);
                conexao.Close();
            }

            pesquisarGridControl.DataSource = dtProduto;
            pesquisarGridView.RefreshData();
        }


        private void pesquisarGridControl_Click(object sender, EventArgs e)
        {
        }

        private void salvarProdutoButtomItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pesquisarGridView.FocusedRowHandle < 0)
            {
                XtraMessageBox.Show("Selecione um produto para desativar");
                return;
            }

            var idProduto = pesquisarGridView.GetFocusedRowCellValue("idProduto");

            if (idProduto == null || idProduto == DBNull.Value)
            {
                XtraMessageBox.Show("Produto não encontrado");
                return;
            }

            DesativarProduto(Convert.ToInt32(idProduto)); 
        }



        private void produtosDesativadosToggleSwitchh_Toggled(object sender, EventArgs e)
        {
        }

        private void pesquisarTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            var pesquisa = pesquisarTextEdit.Text;
            var produtos = PesquisarProdutos(nomeProduto: pesquisa);
            pesquisarGridControl.DataSource = produtos;
            pesquisarGridView.BestFitColumns();
        }

        private void CarregarDados()
        {
            var produtos = PesquisarProdutos(nomeProduto: nomeProduto);
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

        }
    }
}