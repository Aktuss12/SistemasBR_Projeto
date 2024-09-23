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
        private readonly string codigoDeBarras;
        private readonly string nomeProduto;
        private readonly string categoria;

        public PesquisarProdutosView(string nomeProduto, string categoria, string codigoDeBarras)
        {
            InitializeComponent();
            this.nomeProduto = nomeProduto;
            this.categoria = categoria;
            this.codigoDeBarras = codigoDeBarras;
        }

        public DataTable PesquisarProdutos(string nomeProduto, string categoria, string codigoDeBarras)
        {
            var tabelaData = new DataTable();
            using (var conexao = new FbConnection(connectionString))
            {
                try
                {
                    conexao.Open();
                    const string query =
                        @"SELECT idProduto, nome, categoria, fornecedor, codigoDeBarras, unidadeDeMedida,
                        estoque, marca, custo, markup, precoDaVenda, ativo
                        FROM PRODUTO
                        WHERE (nome LIKE @nomeProduto OR @nomeProduto IS NULL)
                        AND (categoria LIKE @categoria OR @categoria IS NULL)
                        AND (codigoDeBarras = @codigoDeBarras OR @codigoDeBarras IS NULL)";
                    using (var cmd = new FbCommand(query, conexao))
                    {
                        cmd.Parameters.AddWithValue("@nomeProduto",
                            string.IsNullOrEmpty(nomeProduto) ? (object)DBNull.Value : $"%{nomeProduto}%");
                        cmd.Parameters.AddWithValue("@categoria",
                            string.IsNullOrEmpty(categoria) ? (object)DBNull.Value : $"%{categoria}%");
                        cmd.Parameters.AddWithValue("@codigoDeBarras",
                            string.IsNullOrEmpty(codigoDeBarras) ? (object)DBNull.Value : codigoDeBarras);
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
        }

        private void CarregarDados()
        {
            var produtos = PesquisarProdutos(nomeProduto, categoria, codigoDeBarras);
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
    }
}