namespace Campus.ImportZizhu
{
    partial class SelectImport
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
            this.lnkCancelImport = new System.Windows.Forms.LinkLabel();
            this.pgImport = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.picComplete = new System.Windows.Forms.PictureBox();
            this.txtResult = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnViewResult = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picComplete)).BeginInit();
            this.SuspendLayout();
            // 
            // lnkCancelImport
            // 
            this.lnkCancelImport.AutoSize = true;
            this.lnkCancelImport.BackColor = System.Drawing.Color.Transparent;
            this.lnkCancelImport.Location = new System.Drawing.Point(319, 20);
            this.lnkCancelImport.Name = "lnkCancelImport";
            this.lnkCancelImport.Size = new System.Drawing.Size(60, 17);
            this.lnkCancelImport.TabIndex = 19;
            this.lnkCancelImport.TabStop = true;
            this.lnkCancelImport.Text = "取消汇入";
            this.lnkCancelImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lnkCancelImport.Visible = false;
            this.lnkCancelImport.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // pgImport
            // 
            this.pgImport.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.pgImport.BackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.DockSiteBackColor;
            this.pgImport.BackgroundStyle.Class = "";
            this.pgImport.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.pgImport.Location = new System.Drawing.Point(19, 42);
            this.pgImport.Name = "pgImport";
            this.pgImport.Size = new System.Drawing.Size(360, 23);
            this.pgImport.TabIndex = 17;
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.groupPanel1.Controls.Add(this.pgImport);
            this.groupPanel1.Controls.Add(this.lnkCancelImport);
            this.groupPanel1.Controls.Add(this.picComplete);
            this.groupPanel1.Controls.Add(this.txtResult);
            this.groupPanel1.Location = new System.Drawing.Point(44, 48);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(404, 236);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 34;
            this.groupPanel1.Text = "汇入进度";
            // 
            // picComplete
            // 
            this.picComplete.BackColor = System.Drawing.Color.Transparent;
            this.picComplete.Image = global::Campus.ImportZizhu.Properties.Resources.ok_64;
            this.picComplete.InitialImage = null;
            this.picComplete.Location = new System.Drawing.Point(315, 108);
            this.picComplete.Name = "picComplete";
            this.picComplete.Size = new System.Drawing.Size(64, 64);
            this.picComplete.TabIndex = 39;
            this.picComplete.TabStop = false;
            this.picComplete.Visible = false;
            // 
            // txtResult
            // 
            this.txtResult.AutoSize = true;
            this.txtResult.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.txtResult.BackgroundStyle.Class = "";
            this.txtResult.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtResult.Location = new System.Drawing.Point(19, 99);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(60, 21);
            this.txtResult.TabIndex = 38;
            this.txtResult.Text = "汇入讯息";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(34, 14);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(141, 21);
            this.labelX1.TabIndex = 32;
            this.labelX1.Text = "将数据汇入到数据库中";
            // 
            // btnViewResult
            // 
            this.btnViewResult.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnViewResult.AutoSize = true;
            this.btnViewResult.BackColor = System.Drawing.Color.Transparent;
            this.btnViewResult.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnViewResult.Location = new System.Drawing.Point(44, 329);
            this.btnViewResult.Name = "btnViewResult";
            this.btnViewResult.Size = new System.Drawing.Size(91, 25);
            this.btnViewResult.TabIndex = 37;
            this.btnViewResult.Text = "检视汇入结果";
            this.btnViewResult.Visible = false;
            // 
            // SelectImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 366);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnViewResult);
            this.Controls.Add(this.groupPanel1);
            this.DoubleBuffered = true;
            this.Name = "SelectImport";
            this.Text = "";
            this.TitleText = "汇入数据";
            this.Shown += new System.EventHandler(this.SelectImport_Shown);
            this.Controls.SetChildIndex(this.groupPanel1, 0);
            this.Controls.SetChildIndex(this.btnViewResult, 0);
            this.Controls.SetChildIndex(this.labelX1, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnPrevious, 0);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picComplete)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lnkCancelImport;
        private DevComponents.DotNetBar.Controls.ProgressBarX pgImport;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnViewResult;
        private DevComponents.DotNetBar.LabelX txtResult;
        private System.Windows.Forms.PictureBox picComplete;
    }
}