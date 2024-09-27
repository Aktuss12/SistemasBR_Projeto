using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CadastroDeProdutosView.Features.Commons
{
    public sealed class MessageBoxCustomizado : XtraForm
    {
        private readonly SimpleButton buttonSim;
        private readonly SimpleButton buttonNao;

        public bool Resultado { get; private set; }

        public MessageBoxCustomizado(string mensagem)
        {
            var labelMensagem = new Label
            {
                Text = mensagem,
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 8.25f), 
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Top = 30,
                Left = (ClientSize.Width - 250) / 2,
                Width = 250 
            };

            buttonSim = new SimpleButton
            {
                Text = "Sim",
                DialogResult = DialogResult.Yes,
                Width = 75,
                Top = 70,
                Left = (ClientSize.Width - 160) / 2 - 10
            };

            buttonNao = new SimpleButton
            {
                Text = "Não",
                DialogResult = DialogResult.No,
                Width = 75,
                Top = 70,
                Left = buttonSim.Right + 10
            };

            buttonSim.Click += (_, _) => { Resultado = true; Close(); };
            buttonNao.Click += (_, _) => { Resultado = false; Close(); };

            Controls.Add(labelMensagem);
            Controls.Add(buttonSim);
            Controls.Add(buttonNao);

            Text = "Confirmação";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            ClientSize = new System.Drawing.Size(270, 110);
            AcceptButton = buttonSim; 
            CancelButton = buttonNao; 
        }
    }
}
