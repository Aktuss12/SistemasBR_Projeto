
namespace CadastroDeProdutosView.Features.Produto.Views
{
    partial class ConfigurarCaminhoDoBancoDeDadosView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.caminhoDoBancoDeDadosLabelControl = new System.Windows.Forms.Label();
            this.bancoDeDadosTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.aplicarButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.bancoDeDadosTextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // caminhoDoBancoDeDadosLabelControl
            // 
            this.caminhoDoBancoDeDadosLabelControl.AutoSize = true;
            this.caminhoDoBancoDeDadosLabelControl.Font = new System.Drawing.Font("Yu Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.caminhoDoBancoDeDadosLabelControl.Location = new System.Drawing.Point(8, 5);
            this.caminhoDoBancoDeDadosLabelControl.Name = "caminhoDoBancoDeDadosLabelControl";
            this.caminhoDoBancoDeDadosLabelControl.Size = new System.Drawing.Size(221, 19);
            this.caminhoDoBancoDeDadosLabelControl.TabIndex = 0;
            this.caminhoDoBancoDeDadosLabelControl.Text = "Caminho do Banco de dados:";
            // 
            // bancoDeDadosTextEdit
            // 
            this.bancoDeDadosTextEdit.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.bancoDeDadosTextEdit.Location = new System.Drawing.Point(12, 29);
            this.bancoDeDadosTextEdit.Name = "bancoDeDadosTextEdit";
            this.bancoDeDadosTextEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.bancoDeDadosTextEdit.Size = new System.Drawing.Size(296, 20);
            this.bancoDeDadosTextEdit.TabIndex = 1;
            // 
            // aplicarButton
            // 
            this.aplicarButton.Location = new System.Drawing.Point(314, 27);
            this.aplicarButton.Name = "aplicarButton";
            this.aplicarButton.Size = new System.Drawing.Size(75, 22);
            this.aplicarButton.TabIndex = 2;
            this.aplicarButton.Text = "Aplicar";
            // 
            // ConfigurarCaminhoDoBancoDeDadosView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 63);
            this.Controls.Add(this.aplicarButton);
            this.Controls.Add(this.bancoDeDadosTextEdit);
            this.Controls.Add(this.caminhoDoBancoDeDadosLabelControl);
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Name = "ConfigurarCaminhoDoBancoDeDadosView";
            this.Text = "ConfigurarCaminhoDoBancoDeDadosView";
            ((System.ComponentModel.ISupportInitialize)(this.bancoDeDadosTextEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label caminhoDoBancoDeDadosLabelControl;
        private DevExpress.XtraEditors.TextEdit bancoDeDadosTextEdit;
        private DevExpress.XtraEditors.SimpleButton aplicarButton;
    }
}