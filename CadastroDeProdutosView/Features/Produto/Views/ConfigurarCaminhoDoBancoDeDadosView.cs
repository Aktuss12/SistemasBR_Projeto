using CadastroDeProdutosView.Features.Commons;
using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace CadastroDeProdutosView.Features.Produto.Views
{
    public partial class ConfigurarCaminhoDoBancoDeDadosView : Form
    {
        public ConfigurarCaminhoDoBancoDeDadosView()
        {
            InitializeComponent();
            PosicaoInicial();
            CarregarConfiguracao();
        }

        private void PosicaoInicial()
        {
            StartPosition = FormStartPosition.CenterParent;
        }

        private void CarregarConfiguracao()
        {
            var config = ConfiguracaoManager.CarregarConfiguracao();
            bancoDeDadosTextEdit.Text = config.CaminhoBanco;
        }

        private void bancoDeDadosTextEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void aplicarButton_Click(object sender, EventArgs e)
        {
            var novoCaminho = bancoDeDadosTextEdit.Text.Trim();
            if (string.IsNullOrWhiteSpace(novoCaminho))
            {
                XtraMessageBox.Show("O caminho do banco de dados não pode estar vazio.");
                return;
            }

            var config = new ConfiguracaoBanco { CaminhoBanco = novoCaminho };
            ConfiguracaoManager.SalvarConfiguracao(config);
            XtraMessageBox.Show("Configuração salva com sucesso. Reinicie a aplicação para aplicar as mudanças.");
            Close();
        }

        private void ConfigurarCaminhoDoBancoDeDadosView_Load(object sender, EventArgs e)
        {

        }

        private void exploradorBandoDeDadosButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivos de Banco de Dados Firebird (*.FDB)|*.FDB|Todos os arquivos (*.*)|*.*";

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            try
            {
                var caminhoDoArquivo = openFileDialog.FileName;
                bancoDeDadosTextEdit.Text = caminhoDoArquivo;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao selecionar o arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}