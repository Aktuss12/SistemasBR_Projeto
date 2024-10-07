using System;
using System.Data;
using System.Windows.Forms;
using CadastroDeProdutosView.Features.Commons;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using FirebirdSql.Data.FirebirdClient;

namespace CadastroDeProdutosView.Features.Produto.Views
{
    public partial class PesquisaDeProdutosView : Form
    {
        private readonly string connectionString;
        private CadastroDeProdutosView? cadastroForm;
        private bool mostrarAtivos = true;

        public PesquisaDeProdutosView()
        {
            connectionString = ConfiguracaoDoBancoDeDados.ObterStringDeConexao();
            InitializeComponent();
            DesativarBotoes();
            CarregarBancoDeDados();
        }

        public int? SelecionadorIdProduto { get; private set; }

        private void CarregarBancoDeDados()
        {
            try
            {
                var carregarTabela = GetCombinedData();
                pesquisarGridControl.DataSource = carregarTabela;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Erro ao carregar dados: " + ex.Message, "Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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

        private void desativarProdutoButtomItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (pesquisarGridControl.MainView is not GridView gridView)
            {
                XtraMessageBox.Show("O grid não é um GridView.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var focusedRowHandle = gridView.FocusedRowHandle;

            if (focusedRowHandle < 0)
            {
                XtraMessageBox.Show("Selecione um produto para desativar.", "Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var colunaSelecionada = gridView.GetDataRow(focusedRowHandle);
            if (colunaSelecionada == null)
            {
                XtraMessageBox.Show("Erro ao obter o produto selecionado.", "Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var idProduto = Convert.ToInt32(colunaSelecionada["idProduto"]);

            using var messageBox = new MessageBoxCustomizado("Tem certeza que deseja desativar esse produto?");
            messageBox.ShowDialog();
            if (!messageBox.Resultado) return;

            DesativarEReativarProduto.DesativarProduto(connectionString, idProduto);

            XtraMessageBox.Show("Produto desativado com sucesso.", "Sucesso", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            CarregarBancoDeDados();
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

        private void reativarProdutoButtomItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (pesquisarGridControl.MainView is not GridView gridView)
            {
                XtraMessageBox.Show("O grid não é um GridView.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var focusedRowHandle = gridView.FocusedRowHandle;

            if (focusedRowHandle < 0)
            {
                XtraMessageBox.Show("Selecione um produto para reativar.", "Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var colunaSelecionada = gridView.GetDataRow(focusedRowHandle);
            if (colunaSelecionada == null)
            {
                XtraMessageBox.Show("Erro ao obter o produto selecionado.", "Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var idProduto = Convert.ToInt32(colunaSelecionada["idProduto"]);

            using var messageBox = new MessageBoxCustomizado("Tem certeza que deseja reativar esse produto?");
            messageBox.ShowDialog();
            if (!messageBox.Resultado) return;

            DesativarEReativarProduto.ReativarProduto(connectionString, idProduto);

            XtraMessageBox.Show("Produto reativado com sucesso.", "Sucesso", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            CarregarBancoDeDados();
        }

        private void alterarButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (pesquisarGridControl.MainView is not GridView gridView)
            {
                XtraMessageBox.Show("Por favor, selecione a aba de pesquisa.", "Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var focusedRowHandle = gridView.FocusedRowHandle;
            if (focusedRowHandle < 0)
            {
                XtraMessageBox.Show("Selecione um produto para alterar.", "Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var colunaSelecionada = gridView.GetDataRow(focusedRowHandle);
            if (colunaSelecionada == null)
            {
                XtraMessageBox.Show("Erro ao obter o produto selecionado.", "Erro", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            SelecionadorIdProduto = Convert.ToInt32(colunaSelecionada["idProduto"]);

            if (SelecionadorIdProduto == null) return;

            if (cadastroForm is { IsDisposed: false })
            {
                cadastroForm.Activate();
                cadastroForm.CarregarProduto(SelecionadorIdProduto.Value);
            }
            else
            {
                cadastroForm = new CadastroDeProdutosView(SelecionadorIdProduto.Value);
                cadastroForm.FormClosed += (_, _) =>
                {
                    cadastroForm = null;
                    CarregarBancoDeDados();
                    WindowState = FormWindowState.Normal;
                };
                cadastroForm.ShowDialog();
            }
        }
    }
}