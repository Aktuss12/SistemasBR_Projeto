using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CadastroDeProdutosView.Features.Commons
{
    public sealed class MessageBoxCustomizado : XtraForm
    {
        private readonly SimpleButton botaoNao;
        private readonly SimpleButton botaoSim;

        public MessageBoxCustomizado(string mensagem)
        {
            var labelMensagem = new Label
            {
                Text = mensagem,
                AutoSize = true,
                Font = new Font("Segoe UI", 8.25f),
                TextAlign = ContentAlignment.MiddleCenter,
                Top = 30,
                Left = (ClientSize.Width - 250) / 2,
                Width = 250
            };

            botaoSim = new SimpleButton
            {
                Text = "Sim",
                DialogResult = DialogResult.Yes,
                Width = 75,
                Top = 70,
                Left = (ClientSize.Width - 160) / 2 - 10
            };

            botaoNao = new SimpleButton
            {
                Text = "Não",
                DialogResult = DialogResult.No,
                Width = 75,
                Top = 70,
                Left = botaoSim.Right + 10
            };

            botaoSim.Click += (_, _) =>
            {
                Resultado = true;
                Hide();
                Close();
            };
            botaoNao.Click += (_, _) =>
            {
                Resultado = false;
                Hide();
                Close();
            };

            Controls.Add(labelMensagem);
            Controls.Add(botaoSim);
            Controls.Add(botaoNao);

            Text = "Confirmação";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            ClientSize = new Size(270, 110);
            AcceptButton = botaoSim;
            CancelButton = botaoNao;
        }

        public bool Resultado { get; private set; }
    }
}