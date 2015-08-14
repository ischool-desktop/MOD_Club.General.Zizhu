namespace Campus.ImportZizhu
{
    partial class SelectFields
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
            this.chkSelectAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lvSourceFieldList = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkSelectAll.BackgroundStyle.Class = "";
            this.chkSelectAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkSelectAll.Checked = true;
            this.chkSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelectAll.CheckValue = "Y";
            this.chkSelectAll.Location = new System.Drawing.Point(34, 317);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(54, 21);
            this.chkSelectAll.TabIndex = 3;
            this.chkSelectAll.Text = "全选";
            // 
            // lvSourceFieldList
            // 
            // 
            // 
            // 
            this.lvSourceFieldList.Border.Class = "ListViewBorder";
            this.lvSourceFieldList.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lvSourceFieldList.CheckBoxes = true;
            this.lvSourceFieldList.FullRowSelect = true;
            this.lvSourceFieldList.Location = new System.Drawing.Point(34, 53);
            this.lvSourceFieldList.Name = "lvSourceFieldList";
            this.lvSourceFieldList.ShowItemToolTips = true;
            this.lvSourceFieldList.Size = new System.Drawing.Size(424, 258);
            this.lvSourceFieldList.TabIndex = 2;
            this.lvSourceFieldList.UseCompatibleStateImageBehavior = false;
            this.lvSourceFieldList.View = System.Windows.Forms.View.List;
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
            this.labelX1.Location = new System.Drawing.Point(23, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(395, 21);
            this.labelX1.TabIndex = 31;
            this.labelX1.Text = "请选择您要汇入的字段，若下方没有字段，请直接进行数据验证。";
            // 
            // SelectFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 366);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.lvSourceFieldList);
            this.DoubleBuffered = true;
            this.Name = "SelectFields";
            this.Text = "";
            this.TitleText = "选择要汇入的字段";
            this.Controls.SetChildIndex(this.btnNext, 0);
            this.Controls.SetChildIndex(this.btnPrevious, 0);
            this.Controls.SetChildIndex(this.lvSourceFieldList, 0);
            this.Controls.SetChildIndex(this.chkSelectAll, 0);
            this.Controls.SetChildIndex(this.labelX1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.CheckBoxX chkSelectAll;
        private DevComponents.DotNetBar.Controls.ListViewEx lvSourceFieldList;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}