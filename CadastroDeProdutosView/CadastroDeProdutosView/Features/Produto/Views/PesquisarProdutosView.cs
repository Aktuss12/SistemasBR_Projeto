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
                        @"SELECT idProduto, nome, categoria, fornecedor, codigoDeBarras, unidadeDeMedida,
                        estoque, marca, custo, markup, precoDaVenda, ativo
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
    }
}