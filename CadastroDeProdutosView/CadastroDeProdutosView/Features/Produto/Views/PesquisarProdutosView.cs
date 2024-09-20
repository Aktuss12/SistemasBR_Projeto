using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using FirebirdSql.Data.FirebirdClient;

namespace CadastroDeProdutosView.Features.Produto.Views
{
    public partial class PesquisarProdutosView : Form
    {
        private const string connectionString = @"User ID=SYSDBA;Password=masterkey;Database=C:\Users\admin\Documents\BANCODEDADOSPRODUTOS.FDB;DataSource=localhost;Port=3050;Dialect=3;Charset=NONE;";

        private bool mostrarAtivos = true;

        public PesquisarProdutosView()
        {
            InitializeComponent();
            CarregarBancoDeDados();
        }

        private void CarregarBancoDeDados()
        {
            try
            {
                var combinedTable = GetCombinedData();
                pesquisarGridControl.DataSource = combinedTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        private DataTable GetCombinedData()
        {
            var dataTable = new DataTable();
            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();
                const string query = @"
                    SELECT 
                        P.idProduto, 
                        P.nome, 
                        P.categoria, 
                        P.fornecedor, 
                        P.codigoDeBarras, 
                        P.unidadeDeMedida, 
                        P.estoque, 
                        P.marca, 
                        P.custo, 
                        P.markup, 
                        P.precoDaVenda,
                        I.origemDaMercadoria,
                        I.situacaoTributaria,
                        I.naturezaDaOperacao,
                        I.ncm,
                        I.aliquotaDeIcms,
                        I.reducaoDeCalculo
                    FROM PRODUTO P
                    LEFT JOIN INFORMACOESFISCAIS I ON P.idProduto = I.idProduto
                    WHERE P.ativo = @ativo";

                using (var command = new FbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ativo", mostrarAtivos ? 1 : 0);
                    using (var dataAdapter = new FbDataAdapter(command))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                }
            }
            return dataTable;
        }

        private void pesquisarGridControl_Click(object sender, EventArgs e)
        {
        }

        private void salvarProdutoButtomItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!(pesquisarGridControl.MainView is GridView gridView))
            {
                XtraMessageBox.Show("O grid não é um GridView.");
                return;
            }

            var focusedRowHandle = gridView.FocusedRowHandle;

            if (focusedRowHandle < 0)
            {
                XtraMessageBox.Show("Selecione um produto para excluir.");
                return;
            }

            var selectedRow = gridView.GetDataRow(focusedRowHandle);
            if (selectedRow == null)
            {
                XtraMessageBox.Show("Erro ao obter o produto selecionado.");
                return;
            }

            var idProduto = Convert.ToInt32(selectedRow["idProduto"]);

            var confirmarResultado = XtraMessageBox.Show("Tem certeza que deseja excluir este produto?", "Confirmação", MessageBoxButtons.YesNo);
            if (confirmarResultado != DialogResult.Yes) return;

            ExcluirProduto(idProduto);
            XtraMessageBox.Show("Produto excluído com sucesso.");
            CarregarBancoDeDados();
        }

        private static void ExcluirProduto(int idProduto)
        {
            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();
                const string updateProductQuery = "UPDATE PRODUTO SET ativo = 0 WHERE idProduto = @idProduto";
                using (var command = new FbCommand(updateProductQuery, connection))
                {
                    command.Parameters.AddWithValue("@idProduto", idProduto);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void produtosDesativadosToggleSwitchh_Toggled(object sender, EventArgs e)
        {
            mostrarAtivos = !mostrarAtivos;
            CarregarBancoDeDados();
        }
    }
}