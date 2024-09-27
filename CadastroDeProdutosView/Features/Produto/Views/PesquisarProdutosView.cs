using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Windows.Forms;
using CadastroDeProdutosView.Features.Commons;

namespace CadastroDeProdutosView.Features.Produto.Views
{
    public partial class PesquisarProdutosView : Form
    {
        private readonly string connectionString;
        private bool mostrarAtivos = true;
        public int? SelecionadorIdProduto { get; private set; }

        public PesquisarProdutosView()
        {
            InitializeComponent();
            DesativarBotoes();
            this.connectionString = ConfiguracaoManager.ObterStringConexao();
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
            var tabelaDados = new DataTable();
            using var conexao = new FbConnection(connectionString);
            conexao.Open();
            const string query = @"
                SELECT 
                    P.idProduto, 
                    P.nome, 
                    P.categoria, 
                    P.unidadeDeMedida, 
                    P.estoque, 
                    P.precoDaVenda
                FROM PRODUTO P
                WHERE P.ativo = @ativo";

            using var comando = new FbCommand(query, conexao);
            comando.Parameters.AddWithValue("@ativo", mostrarAtivos ? 1 : 0);
            using var dataAdapter = new FbDataAdapter(comando);
            dataAdapter.Fill(tabelaDados);

            return tabelaDados;
        }

        private void pesquisarGridControl_Click(object sender, EventArgs e)
        {
        }

        private void desativarProdutoButtomItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pesquisarGridControl.MainView is not GridView gridView)
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

            var colunaSelecionada = gridView.GetDataRow(focusedRowHandle);
            if (colunaSelecionada == null)
            {
                XtraMessageBox.Show("Erro ao obter o produto selecionado.");
                return;
            }

            var idProduto = Convert.ToInt32(colunaSelecionada["idProduto"]);

            using var messageBox = new MessageBoxCustomizado("Tem certeza que deseja excluir esse produto?");
            messageBox.ShowDialog();
            if (!messageBox.Resultado)return;
            
            ExcluirProduto(idProduto);
            XtraMessageBox.Show("Produto excluido com sucesso");
            CarregarBancoDeDados();
        }

        private void ExcluirProduto(int idProduto)
        {
            using var conexao = new FbConnection(connectionString);
            conexao.Open();
            const string updateProductQuery = "UPDATE PRODUTO SET ativo = 0 WHERE idProduto = @idProduto";
            using var command = new FbCommand(updateProductQuery, conexao);
            command.Parameters.AddWithValue("@idProduto", idProduto);
            command.ExecuteNonQuery();
        }

        private void ReativarProduto(int idProduto)
        {
            using var conexao = new FbConnection(connectionString);
            conexao.Open();
            const string updateProductQuery = "UPDATE PRODUTO SET ativo = 1 WHERE idProduto = @idProduto";
            using var command = new FbCommand(updateProductQuery, conexao);
            command.Parameters.AddWithValue("idProduto", idProduto);
            command.ExecuteNonQuery();
        }

        private void DesativarBotoes()
        {
            desativarProdutoButtomItem.Enabled = !produtosDesativadosToggleSwitchh.IsOn;
            reativarProdutoButtomItem.Enabled = produtosDesativadosToggleSwitchh.IsOn;
        }

        public void produtosDesativadosToggleSwitchh_Toggled_1(object sender, EventArgs e)
        {
            mostrarAtivos = !mostrarAtivos;
            DesativarBotoes();
            CarregarBancoDeDados();
        }

        private void pesquisarTextEdit_EditValueChanged(object sender, EventArgs e)
        {
            var nomeProduto = pesquisarTextEdit.Text.Trim();
            pesquisarGridView.ActiveFilterString =
                !string.IsNullOrEmpty(nomeProduto) ? $"[nome] LIKE '%{nomeProduto}%'" : string.Empty;
        }

        private void reativarProdutoButtomItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pesquisarGridControl.MainView is not GridView gridView)
            {
                XtraMessageBox.Show("O produto não foi selecionado");
                return;
            }

            var FocusedHowHandle = gridView.FocusedRowHandle;
            if (FocusedHowHandle < 0)
            {
                XtraMessageBox.Show("Selecione um produto para reativar");
                return;
            }

            var colunaSelecionada = gridView.GetDataRow(FocusedHowHandle);
            if (colunaSelecionada == null)
            {
                XtraMessageBox.Show("Ocorreu um erro ao reativar o produto selecionado");
                return;
            }

            var idProduto = Convert.ToInt32(colunaSelecionada["idProduto"]);
            using var messageBox = new MessageBoxCustomizado("Tem certeza que deseja reativar esse produto?");
            messageBox.ShowDialog();
            if (!messageBox.Resultado) return;

            ReativarProduto(idProduto);
            XtraMessageBox.Show("Produto reativado com sucesso");
            CarregarBancoDeDados();
        }

        private void alterarButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pesquisarGridControl.MainView is not GridView gridview)
            {
                XtraMessageBox.Show("Por favor, Selecione a aba de pesquisa");
                return;
            }

            var focusedRowHandle = gridview.FocusedRowHandle;
            if (focusedRowHandle < 0)
            {
                XtraMessageBox.Show("Selecione um produto para alterar");
                return;
            }

            var colunaSelecionada = gridview.GetDataRow(focusedRowHandle);
            if (colunaSelecionada == null)
            {
                XtraMessageBox.Show("Erro ao obter o produto selecionado");
                return; 
            }

            SelecionadorIdProduto = Convert.ToInt32(colunaSelecionada["idProduto"]);

            if (SelecionadorIdProduto != null)
            {
                var cadastroDeProdutos = new CadastroDeProdutosView(SelecionadorIdProduto.Value);
                cadastroDeProdutos.ShowDialog();
            }

            CarregarBancoDeDados();
        }

        private void cadastroButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Hide();
            var abrirCadastro = new CadastroDeProdutosView(0);
            abrirCadastro.ShowDialog();
            Close();
        }
    }
}