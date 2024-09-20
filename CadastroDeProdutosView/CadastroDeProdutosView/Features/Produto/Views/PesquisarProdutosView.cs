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
            LoadCombinedData();
        }

        private void LoadCombinedData()
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

                var command = new FbCommand(query, connection);
                command.Parameters.Add("@ativo", FbDbType.Boolean).Value = mostrarAtivos;

                var dataAdapter = new FbDataAdapter(command);
                dataAdapter.Fill(dataTable);
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

            if (!(gridView.GetRow(focusedRowHandle) is DataRowView selectedRow))
            {
                XtraMessageBox.Show("Erro ao obter o produto selecionado.");
                return;
            }

            var idProduto = (int)selectedRow["idProduto"];

            var confirmResult = XtraMessageBox.Show("Tem certeza que deseja excluir este produto?", "Confirmação", MessageBoxButtons.YesNo);
            if (confirmResult != DialogResult.Yes) return;

            ExcluirProduto(idProduto);
            LoadCombinedData();
            XtraMessageBox.Show("Produto excluído com sucesso.");
        }

        private static void ExcluirProduto(int idProduto)
        {
            using (var connection = new FbConnection(connectionString))
            {
                connection.Open();

                const string deleteInfoQuery = "DELETE FROM INFORMACOESFISCAIS WHERE idProduto = @idProduto";
                using (var command = new FbCommand(deleteInfoQuery, connection))
                {
                    command.Parameters.Add("@idProduto", FbDbType.Integer).Value = idProduto;
                    command.ExecuteNonQuery();
                }

                const string deleteProductQuery = "DELETE FROM PRODUTO WHERE idProduto = @idProduto";
                using (var command = new FbCommand(deleteProductQuery, connection))
                {
                    command.Parameters.Add("@idProduto", FbDbType.Integer).Value = idProduto;
                    command.ExecuteNonQuery();
                }
            }
        }

        private void produtosDesativadosToggleSwitchh_Toggled(object sender, EventArgs e)
        {
            mostrarAtivos = !mostrarAtivos;
            LoadCombinedData();
        }
    }
}
