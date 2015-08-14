namespace Campus.ImportZizhu
{
    partial class SelectSource
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
            this.pUser = new System.Windows.Forms.Panel();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnViewRuleExcel = new DevComponents.DotNetBar.ButtonX();
            this.btnViewRule = new DevComponents.DotNetBar.ButtonX();
            this.lstSheetNames = new System.Windows.Forms.ListBox();
            this.txtSourceFile = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSelectFile = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.SelectSourceFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ctlerrors = new System.Windows.Forms.ErrorProvider(this.components);
            this.BackupDialog = new System.Windows.Forms.SaveFileDialog();
            this.pUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlerrors)).BeginInit();
            this.SuspendLayout();
            // 
            // pUser
            // 
            this.pUser.BackColor = System.Drawing.Color.Transparent;
            this.pUser.Controls.Add(this.labelX4);
            this.pUser.Controls.Add(this.labelX3);
            this.pUser.Controls.Add(this.labelX2);
            this.pUser.Controls.Add(this.btnViewRuleExcel);
            this.pUser.Controls.Add(this.btnViewRule);
            this.pUser.Controls.Add(this.lstSheetNames);
            this.pUser.Controls.Add(this.txtSourceFile);
            this.pUser.Controls.Add(this.btnSelectFile);
            this.pUser.Location = new System.Drawing.Point(21, 44);
            this.pUser.Name = "pUser";
            this.pUser.Size = new System.Drawing.Size(451, 272);
            this.pUser.TabIndex = 29;
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX4.Location = new System.Drawing.Point(23, 203);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(94, 26);
            this.labelX4.TabIndex = 43;
            this.labelX4.Text = "● 汇入格式";
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX3.Location = new System.Drawing.Point(24, 65);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(111, 26);
            this.labelX3.TabIndex = 42;
            this.labelX3.Text = "● 选择工作表";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX2.Location = new System.Drawing.Point(24, 8);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(204, 26);
            this.labelX2.TabIndex = 41;
            this.labelX2.Text = "● 选择来源档案(汇入来源)";
            // 
            // btnViewRuleExcel
            // 
            this.btnViewRuleExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnViewRuleExcel.BackColor = System.Drawing.Color.Transparent;
            this.btnViewRuleExcel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnViewRuleExcel.Location = new System.Drawing.Point(128, 235);
            this.btnViewRuleExcel.Name = "btnViewRuleExcel";
            this.btnViewRuleExcel.Size = new System.Drawing.Size(75, 23);
            this.btnViewRuleExcel.TabIndex = 40;
            this.btnViewRuleExcel.Text = "空白格式";
            // 
            // btnViewRule
            // 
            this.btnViewRule.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnViewRule.BackColor = System.Drawing.Color.Transparent;
            this.btnViewRule.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnViewRule.Location = new System.Drawing.Point(47, 235);
            this.btnViewRule.Name = "btnViewRule";
            this.btnViewRule.Size = new System.Drawing.Size(75, 23);
            this.btnViewRule.TabIndex = 39;
            this.btnViewRule.Text = "检视说明";
            // 
            // lstSheetNames
            // 
            this.lstSheetNames.FormattingEnabled = true;
            this.lstSheetNames.ItemHeight = 17;
            this.lstSheetNames.Location = new System.Drawing.Point(47, 93);
            this.lstSheetNames.Name = "lstSheetNames";
            this.lstSheetNames.Size = new System.Drawing.Size(345, 106);
            this.lstSheetNames.TabIndex = 37;
            // 
            // txtSourceFile
            // 
            this.txtSourceFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSourceFile.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            // 
            // 
            // 
            this.txtSourceFile.Border.Class = "TextBoxBorder";
            this.txtSourceFile.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSourceFile.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtSourceFile.Location = new System.Drawing.Point(47, 35);
            this.txtSourceFile.Name = "txtSourceFile";
            this.txtSourceFile.Size = new System.Drawing.Size(345, 23);
            this.txtSourceFile.TabIndex = 1;
            this.txtSourceFile.WatermarkText = "请选择或输入档案位置";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelectFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSelectFile.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSelectFile.Location = new System.Drawing.Point(398, 35);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(29, 22);
            this.btnSelectFile.TabIndex = 35;
            this.btnSelectFile.Text = "...";
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
            this.labelX1.Location = new System.Drawing.Point(22, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(127, 21);
            this.labelX1.TabIndex = 30;
            this.labelX1.Text = "选择档案与数据表。";
            // 
            // SelectSourceFileDialog
            // 
            this.SelectSourceFileDialog.Filter = "Excel 檔案 (*.xls)| *.xls";
            // 
            // ctlerrors
            // 
            this.ctlerrors.ContainerControl = this;
            // 
            // BackupDialog
            // 
            this.BackupDialog.Filter = "Excel 檔案 (*.xls)| *.xls";
            // 
            // SelectSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 366);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.pUser);
            this.DoubleBuffered = true;
            this.Name = "SelectSource";
            this.Text = "选择档案与汇入方式";
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnPrevious, 0);
            this.Controls.SetChildIndex(this.pUser, 0);
            this.Controls.SetChildIndex(this.labelX1, 0);
            this.pUser.ResumeLayout(false);
            this.pUser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctlerrors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pUser;
        public DevComponents.DotNetBar.Controls.TextBoxX txtSourceFile;
        public DevComponents.DotNetBar.ButtonX btnSelectFile;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.ListBox lstSheetNames;
        private DevComponents.DotNetBar.ButtonX btnViewRuleExcel;
        private DevComponents.DotNetBar.ButtonX btnViewRule;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private System.Windows.Forms.OpenFileDialog SelectSourceFileDialog;
        private System.Windows.Forms.ErrorProvider ctlerrors;
        private System.Windows.Forms.SaveFileDialog BackupDialog;
    }
}