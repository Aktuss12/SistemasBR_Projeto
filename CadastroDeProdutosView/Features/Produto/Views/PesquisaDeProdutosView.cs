using CadastroDeProdutosView.Features.Commons;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace CadastroDeProdutosView.Features.Produto.Views
{
    public partial class PesquisaDeProdutosView : Form
    {
        private readonly string connectionString;
        private bool mostrarAtivos = true;
        private readonly CadastroDeProdutosView? cadastroForm;

        public PesquisaDeProdutosView(CadastroDeProdutosView cadastroForm = null!)
        {
            InitializeComponent();
            connectionString = ConfiguracaoDoBancoDeDados.ObterStringDeConexao();
            this.cadastroForm = cadastroForm;
            DesativarBotoes();
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
                XtraMessageBox.Show("Selecione um produto para desativar.");
                return;
            }

            var colunaSelecionada = gridView.GetDataRow(focusedRowHandle);
            if (colunaSelecionada == null)
            {
                XtraMessageBox.Show("Erro ao obter o produto selecionado.");
                return;
            }

            var idProduto = Convert.ToInt32(colunaSelecionada["idProduto"]);

            using var messageBox = new MessageBoxCustomizado("Tem certeza que deseja desativar esse produto?");
            messageBox.ShowDialog();
            if (!messageBox.Resultado) return;

            DesativarProduto(idProduto);
            XtraMessageBox.Show("Produto desativado com sucesso");
            CarregarBancoDeDados();
        }

        private void DesativarProduto(int idProduto)
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

            var idProduto = Convert.ToInt32(colunaSelecionada["idProduto"]);

            var novoCadastro = new CadastroDeProdutosView(idProduto);
            novoCadastro.Show();
            Close();
        }

        private void cadastroButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var novoCadastro = new CadastroDeProdutosView(0);
            novoCadastro.Show();
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (cadastroForm == null) return;
            cadastroForm.BringToFront();
            cadastroForm.ReativarBotaoPesquisa();
        }
    }
}