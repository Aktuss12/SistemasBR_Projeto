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
/*            PosicaoInicialDoForm();*/
            CarregarConfiguracao();
        }

/*        private void PosicaoInicialDoForm()
        {
            StartPosition = FormStartPosition.CenterParent;
        }*/

        private void CarregarConfiguracao()
        {
            var config = ConfiguracaoDoBancoDeDados.CarregarBancoDeDados();
            bancoDeDadosTextEdit.Text = config.CaminhoBanco;
        }

        private void ClicadoBotaoDeAplicarMudança(object sender, EventArgs e)
        {
            var novoCaminho = bancoDeDadosTextEdit.Text.Trim();
            if (string.IsNullOrWhiteSpace(novoCaminho))
            {
                XtraMessageBox.Show("O caminho do banco de dados não pode estar vazio.");
                return;
            }

            var config = new ConfiguracaoBanco { CaminhoBanco = novoCaminho };
            ConfiguracaoDoBancoDeDados.SalvarConfiguracao(config);
            XtraMessageBox.Show("Configuração salva com sucesso. Reinicie a aplicação para aplicar as mudanças.");
        }
        
        private void ClicadoBotaoDeExplorarArquivos(object sender, EventArgs e)
        {
            var abrirExploradorDeArquivos = new OpenFileDialog();
            abrirExploradorDeArquivos.Filter = "Arquivos de Banco de Dados Firebird (*.FDB)|*.FDB|Todos os arquivos (*.*)|*.*";

            if (abrirExploradorDeArquivos.ShowDialog() != DialogResult.OK) return;
            try
            {
                var caminhoDoArquivo = abrirExploradorDeArquivos.FileName;
                bancoDeDadosTextEdit.Text = caminhoDoArquivo;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao selecionar o arquivo: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}