using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CadastroDeProdutosView.Features.Commons;

namespace CadastroDeProdutosView.Features.Produto.Views
{
    public partial class PesquisaDeProdutosView : Form
    {
        public int? SelecionadorIdProduto { get; private set; }
        private readonly string _connectionString;
        private bool _mostrarAtivos = true;
        private CadastroDeProdutosView? _cadastroForm;


        public PesquisaDeProdutosView()
        {
            _connectionString = ConfiguracaoDoBancoDeDados.ObterStringDeConexao();
            InitializeComponent();
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
            using var conexao = new FbConnection(_connectionString);
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
            comando.Parameters.AddWithValue("@ativo", _mostrarAtivos ? 1 : 0);
            using var dataAdapter = new FbDataAdapter(comando);
            dataAdapter.Fill(tabelaDados);

            return tabelaDados;
        }


        private void ClicadoBotaoDeDesativarProduto(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

            DesativarEReativarProduto.DesativarProduto(_connectionString, idProduto);

            XtraMessageBox.Show("Produto desativado com sucesso");
            CarregarBancoDeDados();
        }

        private void DesativarBotoes()
        {
            desativarProdutoButtomItem.Enabled = !produtosDesativadosToggleSwitchh.IsOn;
            reativarProdutoButtomItem.Enabled = produtosDesativadosToggleSwitchh.IsOn;
        }

        public void AlteradoToggleDeProdutosDesativados(object sender, EventArgs e)
        { 
            _mostrarAtivos = !_mostrarAtivos;
            DesativarBotoes();
            CarregarBancoDeDados();
        }

        private void MudouValorPesquisaTextEdit(object sender, EventArgs e)
        {
            var nomeProduto = pesquisarTextEdit.Text.Trim();
            pesquisarGridView.ActiveFilterString =
               !string.IsNullOrEmpty(nomeProduto) ? $"[nome] LIKE '%{nomeProduto}%'" : string.Empty;
        }

        private void ClicadoBotaoDeReativarProduto(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pesquisarGridControl.MainView is not GridView gridView)
            {
                XtraMessageBox.Show("O grid não é um GridView.");
                return;
            }

            var focusedRowHandle = gridView.FocusedRowHandle;

            if (focusedRowHandle < 0)
            {
                XtraMessageBox.Show("Selecione um produto para reativar.");
                return;
            }

            var colunaSelecionada = gridView.GetDataRow(focusedRowHandle);
            if (colunaSelecionada == null)
            {
                XtraMessageBox.Show("Erro ao obter o produto selecionado.");
                return;
            }

            var idProduto = Convert.ToInt32(colunaSelecionada["idProduto"]);

            using var messageBox = new MessageBoxCustomizado("Tem certeza que deseja reativar esse produto?");
            messageBox.ShowDialog();
            if (!messageBox.Resultado) return;

            DesativarEReativarProduto.ReativarProduto(_connectionString, idProduto);

            XtraMessageBox.Show("Produto reativado com sucesso");
            CarregarBancoDeDados();
        }

        private static Form? GetOpenForm<T>() where T : Form
        {
            return Application.OpenForms.OfType<T>().FirstOrDefault();
        }


        private void ClicadoBotaoDeAlterar(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (pesquisarGridControl.MainView is not GridView gridview)
            {
                XtraMessageBox.Show("Por favor, selecione a aba de pesquisa");
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

            if (SelecionadorIdProduto == null) return;

            if (_cadastroForm is { IsDisposed: false })
            {
                _cadastroForm.Activate();
                _cadastroForm.CarregarProduto(SelecionadorIdProduto.Value);
            }
            else
            {
                _cadastroForm = new CadastroDeProdutosView(SelecionadorIdProduto.Value);
                _cadastroForm.FormClosed += (_, _) =>
                {
                    _cadastroForm = null;
                    CarregarBancoDeDados();
                    WindowState = FormWindowState.Normal;
                };
                _cadastroForm.ShowDialog();
            }
        }

        private void ClicadoBotaoDeCadastro(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var cadastroFormView = GetOpenForm<CadastroDeProdutosView>();
            if (cadastroFormView != null)
            {
                cadastroFormView.Activate();
                return;
            }
            var abrirCadastro = new CadastroDeProdutosView(0);
            abrirCadastro.ShowDialog();
        }
    }
}