using System;
using System.Windows.Forms;
using CadastroDeProdutosView.Features.Commons;
using DevExpress.XtraEditors;

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
    }
}