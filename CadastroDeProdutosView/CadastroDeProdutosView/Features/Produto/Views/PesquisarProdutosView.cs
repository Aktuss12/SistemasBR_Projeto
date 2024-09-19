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

        private static DataTable GetCombinedData()
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
                        LEFT JOIN INFORMACOESFISCAIS I ON P.idProduto = I.idProduto";

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