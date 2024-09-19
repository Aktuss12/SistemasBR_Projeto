using System;
using System.Data;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace CadastroDeProdutosView.Features.Produto.Views
{
    public partial class PesquisarProdutosView : Form
    {
        private const string connectionString = @"User ID=SYSDBA;Password=masterkey;Database=C:\Users\admin\Documents\BANCODEDADOSPRODUTOS.FDB;DataSource=localhost;Port=3050;Dialect=3;Charset=NONE;";

        public PesquisarProdutosView()
        {
            InitializeComponent();
            LoadProdutoData();
            LoadInformacoesFiscaisData();
        }

        private void LoadProdutoData()
        {
            try
            {
                var produtoTable = GetProdutoData();
                pesquisarGridControl.DataSource = produtoTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados de produtos: " + ex.Message);
            }
        }

        private static DataTable GetProdutoData()
        {
            var dataTable = new DataTable();
            using (var connection = new FbConnection(connectionString))
            {
                const string query = "SELECT * FROM PRODUTO";
                var dataAdapter = new FbDataAdapter(query, connection);
                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        private void LoadInformacoesFiscaisData()
        {
            try
            {
                var informacoesFiscaisTable = GetInformacoesFiscaisData();
                pesquisarGridControl.DataSource = informacoesFiscaisTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static DataTable GetInformacoesFiscaisData()
        {
            var dataTable = new DataTable();
            using (var connection = new FbConnection(connectionString))
            {
                const string query = "SELECT * FROM INFORMACOESFISCAIS";
                var dataAdapter = new FbDataAdapter(query, connection);
                dataAdapter.Fill(dataTable);
            }
            return dataTable;
        }

        private void pesquisarGridControl_Click(object sender, EventArgs e)
        {

        }
    }
}