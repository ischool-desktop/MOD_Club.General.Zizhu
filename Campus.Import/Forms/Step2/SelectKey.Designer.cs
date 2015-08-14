namespace Campus.ImportZizhu
{
    partial class SelectKey
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
            this.cboIdField = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.lblImportActionMessage = new DevComponents.DotNetBar.LabelX();
            this.lstActions = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // cboIdField
            // 
            this.cboIdField.DisplayMember = "Text";
            this.cboIdField.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboIdField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIdField.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboIdField.FormattingEnabled = true;
            this.cboIdField.ItemHeight = 18;
            this.cboIdField.Location = new System.Drawing.Point(65, 81);
            this.cboIdField.Name = "cboIdField";
            this.cboIdField.Size = new System.Drawing.Size(326, 24);
            this.cboIdField.TabIndex = 25;
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
            this.labelX1.Location = new System.Drawing.Point(24, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(368, 21);
            this.labelX1.TabIndex = 31;
            this.labelX1.Text = "识别栏是更新数据的依据，验证栏则是帮助检查资料合理性。";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(65, 108);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(326, 51);
            this.labelX4.TabIndex = 34;
            this.labelX4.Text = "此字段是系统用来识别数据用，在源数据中必须是唯一的，不可重复的。";
            this.labelX4.WordWrap = true;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX3.Location = new System.Drawing.Point(44, 54);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(78, 26);
            this.labelX3.TabIndex = 42;
            this.labelX3.Text = "● 标识列";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelX2.Location = new System.Drawing.Point(44, 171);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(94, 26);
            this.labelX2.TabIndex = 43;
            this.labelX2.Text = "● 汇入方式";
            // 
            // lblImportActionMessage
            // 
            this.lblImportActionMessage.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblImportActionMessage.BackgroundStyle.Class = "";
            this.lblImportActionMessage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblImportActionMessage.Location = new System.Drawing.Point(261, 199);
            this.lblImportActionMessage.Name = "lblImportActionMessage";
            this.lblImportActionMessage.Size = new System.Drawing.Size(205, 106);
            this.lblImportActionMessage.TabIndex = 44;
            this.lblImportActionMessage.WordWrap = true;
            // 
            // lstActions
            // 
            this.lstActions.FormattingEnabled = true;
            this.lstActions.ItemHeight = 17;
            this.lstActions.Location = new System.Drawing.Point(66, 199);
            this.lstActions.Name = "lstActions";
            this.lstActions.Size = new System.Drawing.Size(189, 106);
            this.lstActions.TabIndex = 46;
            // 
            // SelectKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 366);
            this.Controls.Add(this.lstActions);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cboIdField);
            this.Controls.Add(this.lblImportActionMessage);
            this.DoubleBuffered = true;
            this.Name = "SelectKey";
            this.Text = "";
            this.TitleText = "选择识别栏与验证栏";
            this.Controls.SetChildIndex(this.lblImportActionMessage, 0);
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnPrevious, 0);
            this.Controls.SetChildIndex(this.cboIdField, 0);
            this.Controls.SetChildIndex(this.labelX1, 0);
            this.Controls.SetChildIndex(this.labelX4, 0);
            this.Controls.SetChildIndex(this.labelX3, 0);
            this.Controls.SetChildIndex(this.labelX2, 0);
            this.Controls.SetChildIndex(this.lstActions, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevComponents.DotNetBar.Controls.ComboBoxEx cboIdField;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX lblImportActionMessage;
        private System.Windows.Forms.ListBox lstActions;
    }
}