namespace Campus.ImportZizhu
{
    partial class SelectValidate
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
            this.lblCorrect = new DevComponents.DotNetBar.LabelX();
            this.lblWarning = new DevComponents.DotNetBar.LabelX();
            this.lblError = new DevComponents.DotNetBar.LabelX();
            this.btnViewResult = new DevComponents.DotNetBar.ButtonX();
            this.lnkCancelValid = new System.Windows.Forms.LinkLabel();
            this.pgValidProgress = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.lblCorrectCount = new DevComponents.DotNetBar.LabelX();
            this.lblWarningCount = new DevComponents.DotNetBar.LabelX();
            this.lblErrorCount = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblProgress = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCorrect
            // 
            this.lblCorrect.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblCorrect.BackgroundStyle.Class = "";
            this.lblCorrect.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblCorrect.Location = new System.Drawing.Point(19, 158);
            this.lblCorrect.Name = "lblCorrect";
            this.lblCorrect.Size = new System.Drawing.Size(78, 23);
            this.lblCorrect.TabIndex = 24;
            this.lblCorrect.Text = "自动修正：";
            // 
            // lblWarning
            // 
            this.lblWarning.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblWarning.BackgroundStyle.Class = "";
            this.lblWarning.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblWarning.Location = new System.Drawing.Point(19, 135);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(78, 23);
            this.lblWarning.TabIndex = 23;
            this.lblWarning.Text = "提示数量：";
            // 
            // lblError
            // 
            this.lblError.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblError.BackgroundStyle.Class = "";
            this.lblError.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblError.Location = new System.Drawing.Point(19, 112);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(78, 23);
            this.lblError.TabIndex = 22;
            this.lblError.Text = "错误数量：";
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
            this.btnViewResult.TabIndex = 21;
            this.btnViewResult.Text = "检视验证结果";
            // 
            // lnkCancelValid
            // 
            this.lnkCancelValid.AutoSize = true;
            this.lnkCancelValid.BackColor = System.Drawing.Color.Transparent;
            this.lnkCancelValid.Location = new System.Drawing.Point(319, 20);
            this.lnkCancelValid.Name = "lnkCancelValid";
            this.lnkCancelValid.Size = new System.Drawing.Size(60, 17);
            this.lnkCancelValid.TabIndex = 20;
            this.lnkCancelValid.TabStop = true;
            this.lnkCancelValid.Text = "取消验证";
            this.lnkCancelValid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lnkCancelValid.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // pgValidProgress
            // 
            this.pgValidProgress.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.pgValidProgress.BackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.DockSiteBackColor;
            this.pgValidProgress.BackgroundStyle.Class = "";
            this.pgValidProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.pgValidProgress.Location = new System.Drawing.Point(19, 42);
            this.pgValidProgress.Name = "pgValidProgress";
            this.pgValidProgress.Size = new System.Drawing.Size(360, 23);
            this.pgValidProgress.TabIndex = 19;
            this.pgValidProgress.Text = "v";
            // 
            // lblCorrectCount
            // 
            this.lblCorrectCount.AutoSize = true;
            this.lblCorrectCount.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblCorrectCount.BackgroundStyle.Class = "";
            this.lblCorrectCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblCorrectCount.Location = new System.Drawing.Point(97, 159);
            this.lblCorrectCount.Name = "lblCorrectCount";
            this.lblCorrectCount.Size = new System.Drawing.Size(15, 21);
            this.lblCorrectCount.TabIndex = 27;
            this.lblCorrectCount.Text = "0";
            // 
            // lblWarningCount
            // 
            this.lblWarningCount.AutoSize = true;
            this.lblWarningCount.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblWarningCount.BackgroundStyle.Class = "";
            this.lblWarningCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblWarningCount.Location = new System.Drawing.Point(97, 136);
            this.lblWarningCount.Name = "lblWarningCount";
            this.lblWarningCount.Size = new System.Drawing.Size(15, 21);
            this.lblWarningCount.TabIndex = 26;
            this.lblWarningCount.Text = "0";
            // 
            // lblErrorCount
            // 
            this.lblErrorCount.AutoSize = true;
            this.lblErrorCount.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblErrorCount.BackgroundStyle.Class = "";
            this.lblErrorCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblErrorCount.Location = new System.Drawing.Point(97, 113);
            this.lblErrorCount.Name = "lblErrorCount";
            this.lblErrorCount.Size = new System.Drawing.Size(15, 21);
            this.lblErrorCount.TabIndex = 25;
            this.lblErrorCount.Text = "0";
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
            this.labelX1.Location = new System.Drawing.Point(26, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(364, 21);
            this.labelX1.TabIndex = 31;
            this.labelX1.Text = "您选择的\"来源档案\"会被另存新文件并加上\"验证讯息\"字段。";
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.pictureBox1);
            this.groupPanel1.Controls.Add(this.lblProgress);
            this.groupPanel1.Controls.Add(this.lblCorrectCount);
            this.groupPanel1.Controls.Add(this.pgValidProgress);
            this.groupPanel1.Controls.Add(this.lblWarningCount);
            this.groupPanel1.Controls.Add(this.lnkCancelValid);
            this.groupPanel1.Controls.Add(this.lblErrorCount);
            this.groupPanel1.Controls.Add(this.lblCorrect);
            this.groupPanel1.Controls.Add(this.lblError);
            this.groupPanel1.Controls.Add(this.lblWarning);
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
            this.groupPanel1.TabIndex = 35;
            this.groupPanel1.Text = "验证进度";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(315, 108);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 40;
            this.pictureBox1.TabStop = false;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblProgress.BackgroundStyle.Class = "";
            this.lblProgress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblProgress.Location = new System.Drawing.Point(19, 71);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 0);
            this.lblProgress.TabIndex = 23;
            // 
            // SelectValidate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 366);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnViewResult);
            this.DoubleBuffered = true;
            this.Name = "SelectValidate";
            this.Text = "";
            this.TitleText = "检查数据正确性";
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnPrevious, 0);
            this.Controls.SetChildIndex(this.btnViewResult, 0);
            this.Controls.SetChildIndex(this.labelX1, 0);
            this.Controls.SetChildIndex(this.groupPanel1, 0);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblCorrect;
        private DevComponents.DotNetBar.LabelX lblWarning;
        private DevComponents.DotNetBar.LabelX lblError;
        private DevComponents.DotNetBar.ButtonX btnViewResult;
        private System.Windows.Forms.LinkLabel lnkCancelValid;
        private DevComponents.DotNetBar.Controls.ProgressBarX pgValidProgress;
        private DevComponents.DotNetBar.LabelX lblCorrectCount;
        private DevComponents.DotNetBar.LabelX lblWarningCount;
        private DevComponents.DotNetBar.LabelX lblErrorCount;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.LabelX lblProgress;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}