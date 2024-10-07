
using System.Windows.Forms;

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
            StartPosition = FormStartPosition.CenterParent;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurarCaminhoDoBancoDeDadosView));
            this.caminhoDoBancoDeDadosLabelControl = new System.Windows.Forms.Label();
            this.bancoDeDadosTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.aplicarButton = new DevExpress.XtraEditors.SimpleButton();
            this.exploradorBandoDeDadosButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.bancoDeDadosTextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // caminhoDoBancoDeDadosLabelControl
            // 
            this.caminhoDoBancoDeDadosLabelControl.AutoSize = true;
            this.caminhoDoBancoDeDadosLabelControl.Font = new System.Drawing.Font("Yu Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.caminhoDoBancoDeDadosLabelControl.Location = new System.Drawing.Point(44, 59);
            this.caminhoDoBancoDeDadosLabelControl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.caminhoDoBancoDeDadosLabelControl.Name = "caminhoDoBancoDeDadosLabelControl";
            this.caminhoDoBancoDeDadosLabelControl.Size = new System.Drawing.Size(341, 30);
            this.caminhoDoBancoDeDadosLabelControl.TabIndex = 0;
            this.caminhoDoBancoDeDadosLabelControl.Text = "Caminho do Banco de dados:";
            // 
            // bancoDeDadosTextEdit
            // 
            this.bancoDeDadosTextEdit.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.bancoDeDadosTextEdit.Location = new System.Drawing.Point(49, 94);
            this.bancoDeDadosTextEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bancoDeDadosTextEdit.Name = "bancoDeDadosTextEdit";
            this.bancoDeDadosTextEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.bancoDeDadosTextEdit.Size = new System.Drawing.Size(336, 28);
            this.bancoDeDadosTextEdit.TabIndex = 1;
            // 
            // aplicarButton
            // 
            this.aplicarButton.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.aplicarButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aplicarButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("aplicarButton.ImageOptions.Image")));
            this.aplicarButton.Location = new System.Drawing.Point(49, 132);
            this.aplicarButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.aplicarButton.Name = "aplicarButton";
            this.aplicarButton.Size = new System.Drawing.Size(57, 51);
            this.aplicarButton.TabIndex = 2;
            this.aplicarButton.Click += new System.EventHandler(this.aplicarButton_Click);
            // 
            // exploradorBandoDeDadosButton
            // 
            this.exploradorBandoDeDadosButton.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.exploradorBandoDeDadosButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exploradorBandoDeDadosButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("exploradorBandoDeDadosButton.ImageOptions.Image")));
            this.exploradorBandoDeDadosButton.Location = new System.Drawing.Point(328, 132);
            this.exploradorBandoDeDadosButton.Name = "exploradorBandoDeDadosButton";
            this.exploradorBandoDeDadosButton.Size = new System.Drawing.Size(57, 51);
            this.exploradorBandoDeDadosButton.TabIndex = 3;
            this.exploradorBandoDeDadosButton.Click += new System.EventHandler(this.exploradorBancoDeDadosButton_Click);
            // 
            // ConfigurarCaminhoDoBancoDeDadosView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 210);
            this.Controls.Add(this.exploradorBandoDeDadosButton);
            this.Controls.Add(this.aplicarButton);
            this.Controls.Add(this.bancoDeDadosTextEdit);
            this.Controls.Add(this.caminhoDoBancoDeDadosLabelControl);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
        private DevExpress.XtraEditors.SimpleButton exploradorBandoDeDadosButton;
    }
}