
namespace CadastroDeProdutosView.Features.Produto.Views
{
    partial class PesquisarProdutosView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PesquisarProdutosView));
            this.pesquisarLabelControl = new DevExpress.XtraEditors.LabelControl();
            this.pesquisarTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.pesquisarBehaviorManager = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.pesquisarBarManager = new DevExpress.XtraBars.BarManager(this.components);
            this.bar = new DevExpress.XtraBars.Bar();
            this.desativarProdutoButtomItem = new DevExpress.XtraBars.BarButtonItem();
            this.reativarProdutoButtomItem = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.salvarBarItem = new DevExpress.XtraBars.BarButtonItem();
            this.excluirButtomItem = new DevExpress.XtraBars.BarButtonItem();
            this.pesquisarButtomItem = new DevExpress.XtraBars.BarButtonItem();
            this.barHeaderItem1 = new DevExpress.XtraBars.BarHeaderItem();
            this.barCheckItem1 = new DevExpress.XtraBars.BarCheckItem();
            this.excluirProdutoButtomItem = new DevExpress.XtraBars.BarButtonItem();
            this.pesquisarProdutoButtomItem = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.pesquisarGridControl = new DevExpress.XtraGrid.GridControl();
            this.pesquisarGridView = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.produtosDesativadosToggleSwitchh = new DevExpress.XtraEditors.ToggleSwitch();
            this.searchImgEdit = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pesquisarTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pesquisarBehaviorManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pesquisarBarManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pesquisarGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pesquisarGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.produtosDesativadosToggleSwitchh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchImgEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pesquisarLabelControl
            // 
            this.pesquisarLabelControl.Appearance.Font = new System.Drawing.Font("Yu Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pesquisarLabelControl.Appearance.Options.UseFont = true;
            this.pesquisarLabelControl.Location = new System.Drawing.Point(12, 52);
            this.pesquisarLabelControl.Name = "pesquisarLabelControl";
            this.pesquisarLabelControl.Size = new System.Drawing.Size(101, 27);
            this.pesquisarLabelControl.TabIndex = 0;
            this.pesquisarLabelControl.Text = "Pesquisar";
            // 
            // pesquisarTextEdit
            // 
            this.pesquisarTextEdit.EditValue = "";
            this.pesquisarTextEdit.Location = new System.Drawing.Point(12, 85);
            this.pesquisarTextEdit.Name = "pesquisarTextEdit";
            this.pesquisarTextEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.pesquisarTextEdit.Size = new System.Drawing.Size(337, 22);
            this.pesquisarTextEdit.TabIndex = 1;
            this.pesquisarTextEdit.EditValueChanged += new System.EventHandler(this.pesquisarTextEdit_EditValueChanged);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Appearance.BackColor = System.Drawing.Color.White;
            this.barDockControlTop.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.barDockControlTop.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barDockControlTop.Appearance.Options.UseBackColor = true;
            this.barDockControlTop.Appearance.Options.UseFont = true;
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 35);
            this.barDockControlTop.Manager = null;
            this.barDockControlTop.Size = new System.Drawing.Size(800, 0);
            // 
            // pesquisarBarManager
            // 
            this.pesquisarBarManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar});
            this.pesquisarBarManager.DockControls.Add(this.barDockControl1);
            this.pesquisarBarManager.DockControls.Add(this.barDockControlBottom);
            this.pesquisarBarManager.DockControls.Add(this.barDockControlLeft);
            this.pesquisarBarManager.DockControls.Add(this.barDockControlRight);
            this.pesquisarBarManager.Form = this;
            this.pesquisarBarManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.salvarBarItem,
            this.desativarProdutoButtomItem,
            this.excluirButtomItem,
            this.pesquisarButtomItem,
            this.barHeaderItem1,
            this.barCheckItem1,
            this.excluirProdutoButtomItem,
            this.pesquisarProdutoButtomItem,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.reativarProdutoButtomItem});
            this.pesquisarBarManager.MaxItemId = 14;
            // 
            // bar
            // 
            this.bar.BarName = "Barra de Tarefas";
            this.bar.DockCol = 0;
            this.bar.DockRow = 0;
            this.bar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.desativarProdutoButtomItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.reativarProdutoButtomItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3)});
            this.bar.OptionsBar.AllowQuickCustomization = false;
            this.bar.OptionsBar.AutoPopupMode = DevExpress.XtraBars.BarAutoPopupMode.OnlyMenu;
            this.bar.OptionsBar.DrawBorder = false;
            this.bar.Text = "Tools";
            // 
            // desativarProdutoButtomItem
            // 
            this.desativarProdutoButtomItem.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.desativarProdutoButtomItem.Caption = "DESATIVAR";
            this.desativarProdutoButtomItem.Id = 1;
            this.desativarProdutoButtomItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("salvarProdutoButtomItem.ImageOptions.SvgImage")));
            this.desativarProdutoButtomItem.Name = "desativarProdutoButtomItem";
            this.desativarProdutoButtomItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.desativarProdutoButtomItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.desativarProdutoButtomItem_ItemClick);
            // 
            // reativarProdutoButtomItem
            // 
            this.reativarProdutoButtomItem.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.reativarProdutoButtomItem.Caption = "REATIVAR";
            this.reativarProdutoButtomItem.Id = 13;
            this.reativarProdutoButtomItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("reativarProdutoButtomItem.ImageOptions.SvgImage")));
            this.reativarProdutoButtomItem.Name = "reativarProdutoButtomItem";
            this.reativarProdutoButtomItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.reativarProdutoButtomItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.reativarProdutoButtomItem_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.barButtonItem3.Caption = "ALTERAR";
            this.barButtonItem3.Id = 12;
            this.barButtonItem3.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.ImageOptions.LargeImage")));
            this.barButtonItem3.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem3.ImageOptions.SvgImage")));
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barDockControl1
            // 
            this.barDockControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.barDockControl1.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.barDockControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barDockControl1.Appearance.Options.UseBackColor = true;
            this.barDockControl1.Appearance.Options.UseFont = true;
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Manager = this.pesquisarBarManager;
            this.barDockControl1.Size = new System.Drawing.Size(800, 35);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 450);
            this.barDockControlBottom.Manager = this.pesquisarBarManager;
            this.barDockControlBottom.Size = new System.Drawing.Size(800, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 35);
            this.barDockControlLeft.Manager = this.pesquisarBarManager;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 415);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(800, 35);
            this.barDockControlRight.Manager = this.pesquisarBarManager;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 415);
            // 
            // salvarBarItem
            // 
            this.salvarBarItem.Caption = "Salvar";
            this.salvarBarItem.Id = 0;
            this.salvarBarItem.Name = "salvarBarItem";
            // 
            // excluirButtomItem
            // 
            this.excluirButtomItem.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.excluirButtomItem.Caption = "EXCLUIR";
            this.excluirButtomItem.Id = 2;
            this.excluirButtomItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("excluirButtomItem.ImageOptions.Image")));
            this.excluirButtomItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("excluirButtomItem.ImageOptions.LargeImage")));
            this.excluirButtomItem.Name = "excluirButtomItem";
            this.excluirButtomItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu;
            // 
            // pesquisarButtomItem
            // 
            this.pesquisarButtomItem.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.pesquisarButtomItem.Caption = "PESQUISAR";
            this.pesquisarButtomItem.Id = 3;
            this.pesquisarButtomItem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("pesquisarButtomItem.ImageOptions.Image")));
            this.pesquisarButtomItem.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("pesquisarButtomItem.ImageOptions.LargeImage")));
            this.pesquisarButtomItem.Name = "pesquisarButtomItem";
            // 
            // barHeaderItem1
            // 
            this.barHeaderItem1.Caption = "barHeaderItem1";
            this.barHeaderItem1.Id = 4;
            this.barHeaderItem1.Name = "barHeaderItem1";
            // 
            // barCheckItem1
            // 
            this.barCheckItem1.Caption = "barCheckItem1";
            this.barCheckItem1.Id = 5;
            this.barCheckItem1.Name = "barCheckItem1";
            // 
            // excluirProdutoButtomItem
            // 
            this.excluirProdutoButtomItem.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.excluirProdutoButtomItem.Caption = "EXCLUIR";
            this.excluirProdutoButtomItem.Id = 7;
            this.excluirProdutoButtomItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("excluirProdutoButtomItem.ImageOptions.SvgImage")));
            this.excluirProdutoButtomItem.Name = "excluirProdutoButtomItem";
            this.excluirProdutoButtomItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // pesquisarProdutoButtomItem
            // 
            this.pesquisarProdutoButtomItem.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.pesquisarProdutoButtomItem.Caption = "PESQUISAR";
            this.pesquisarProdutoButtomItem.Id = 8;
            this.pesquisarProdutoButtomItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("pesquisarProdutoButtomItem.ImageOptions.SvgImage")));
            this.pesquisarProdutoButtomItem.Name = "pesquisarProdutoButtomItem";
            this.pesquisarProdutoButtomItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.barButtonItem1.Caption = "ALTERAR";
            this.barButtonItem1.Id = 9;
            this.barButtonItem1.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.ImageOptions.LargeImage")));
            this.barButtonItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem1.ImageOptions.SvgImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Border = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.barButtonItem2.Caption = "ALTERAR";
            this.barButtonItem2.Id = 11;
            this.barButtonItem2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.ImageOptions.Image")));
            this.barButtonItem2.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.ImageOptions.LargeImage")));
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // pesquisarGridControl
            // 
            this.pesquisarGridControl.Location = new System.Drawing.Point(0, 179);
            this.pesquisarGridControl.MainView = this.pesquisarGridView;
            this.pesquisarGridControl.Name = "pesquisarGridControl";
            this.pesquisarGridControl.Size = new System.Drawing.Size(800, 274);
            this.pesquisarGridControl.TabIndex = 8;
            this.pesquisarGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.pesquisarGridView});
            this.pesquisarGridControl.Click += new System.EventHandler(this.pesquisarGridControl_Click);
            // 
            // pesquisarGridView
            // 
            this.pesquisarGridView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1});
            this.pesquisarGridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.pesquisarGridView.GridControl = this.pesquisarGridControl;
            this.pesquisarGridView.Name = "pesquisarGridView";
            this.pesquisarGridView.OptionsBehavior.Editable = false;
            this.pesquisarGridView.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.pesquisarGridView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.pesquisarGridView.OptionsView.ShowGroupPanel = false;
            this.pesquisarGridView.PaintStyleName = "Office2003";
            this.pesquisarGridView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridBand1.AppearanceHeader.Options.UseFont = true;
            this.gridBand1.Caption = "Produtos";
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            // 
            // produtosDesativadosToggleSwitchh
            // 
            this.produtosDesativadosToggleSwitchh.Location = new System.Drawing.Point(10, 149);
            this.produtosDesativadosToggleSwitchh.MenuManager = this.pesquisarBarManager;
            this.produtosDesativadosToggleSwitchh.Name = "produtosDesativadosToggleSwitchh";
            this.produtosDesativadosToggleSwitchh.Properties.FullFocusRect = true;
            this.produtosDesativadosToggleSwitchh.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.produtosDesativadosToggleSwitchh.Properties.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.produtosDesativadosToggleSwitchh.Properties.OffText = "Inativos";
            this.produtosDesativadosToggleSwitchh.Properties.OnText = "Ativos";
            this.produtosDesativadosToggleSwitchh.Size = new System.Drawing.Size(72, 24);
            this.produtosDesativadosToggleSwitchh.TabIndex = 14;
            this.produtosDesativadosToggleSwitchh.Toggled += new System.EventHandler(this.produtosDesativadosToggleSwitchh_Toggled);
            // 
            // searchImgEdit
            // 
            this.searchImgEdit.EditValue = ((object)(resources.GetObject("searchImgEdit.EditValue")));
            this.searchImgEdit.Location = new System.Drawing.Point(119, 53);
            this.searchImgEdit.MenuManager = this.pesquisarBarManager;
            this.searchImgEdit.Name = "searchImgEdit";
            this.searchImgEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.searchImgEdit.Properties.Appearance.ForeColor = System.Drawing.Color.Transparent;
            this.searchImgEdit.Properties.Appearance.Options.UseBackColor = true;
            this.searchImgEdit.Properties.Appearance.Options.UseForeColor = true;
            this.searchImgEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.searchImgEdit.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.searchImgEdit.Size = new System.Drawing.Size(18, 26);
            this.searchImgEdit.TabIndex = 20;
            // 
            // PesquisarProdutosView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.searchImgEdit);
            this.Controls.Add(this.produtosDesativadosToggleSwitchh);
            this.Controls.Add(this.pesquisarGridControl);
            this.Controls.Add(this.pesquisarTextEdit);
            this.Controls.Add(this.pesquisarLabelControl);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControl1);
            this.Name = "PesquisarProdutosView";
            this.Text = "PesquisarProdutosView";
            ((System.ComponentModel.ISupportInitialize)(this.pesquisarTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pesquisarBehaviorManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pesquisarBarManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pesquisarGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pesquisarGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.produtosDesativadosToggleSwitchh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchImgEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl pesquisarLabelControl;
        private DevExpress.XtraEditors.TextEdit pesquisarTextEdit;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager pesquisarBarManager;
        private DevExpress.XtraBars.Bar bar;
        private DevExpress.XtraBars.BarButtonItem desativarProdutoButtomItem;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem salvarBarItem;
        private DevExpress.XtraBars.BarButtonItem excluirButtomItem;
        private DevExpress.XtraBars.BarButtonItem pesquisarButtomItem;
        private DevExpress.XtraBars.BarHeaderItem barHeaderItem1;
        private DevExpress.XtraBars.BarCheckItem barCheckItem1;
        private DevExpress.XtraBars.BarButtonItem excluirProdutoButtomItem;
        private DevExpress.XtraBars.BarButtonItem pesquisarProdutoButtomItem;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.Utils.Behaviors.BehaviorManager pesquisarBehaviorManager;
        private DevExpress.XtraGrid.GridControl pesquisarGridControl;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView pesquisarGridView;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraBars.BarButtonItem reativarProdutoButtomItem;
        private DevExpress.XtraEditors.ToggleSwitch produtosDesativadosToggleSwitchh;
        private DevExpress.XtraEditors.PictureEdit searchImgEdit;
    }
}