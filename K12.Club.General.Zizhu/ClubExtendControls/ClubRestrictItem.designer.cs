namespace K12.Club.General.Zizhu
{
    partial class ClubRestrictItem
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tbBoyLimit3 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbBoyLimit2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbBoyLimit1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lbGrade3Limit = new DevComponents.DotNetBar.LabelX();
            this.Grade2Limit = new DevComponents.DotNetBar.LabelX();
            this.lbGrade1Limit = new DevComponents.DotNetBar.LabelX();
            this.tbBoyLimit4 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tbBoyLimit5 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.tbGirlLimit5 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbGirlLimit4 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbGirlLimit1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbGirlLimit3 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbGirlLimit2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.tbLimit5 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbLimit4 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbLimit1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbLimit3 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbLimit2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.SuspendLayout();
            // 
            // tbBoyLimit3
            // 
            // 
            // 
            // 
            this.tbBoyLimit3.Border.Class = "TextBoxBorder";
            this.tbBoyLimit3.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbBoyLimit3.Location = new System.Drawing.Point(278, 112);
            this.tbBoyLimit3.Name = "tbBoyLimit3";
            this.tbBoyLimit3.Size = new System.Drawing.Size(98, 25);
            this.tbBoyLimit3.TabIndex = 7;
            this.tbBoyLimit3.TextChanged += new System.EventHandler(this.tbGrade3Limit_TextChanged);
            // 
            // tbBoyLimit2
            // 
            // 
            // 
            // 
            this.tbBoyLimit2.Border.Class = "TextBoxBorder";
            this.tbBoyLimit2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbBoyLimit2.Location = new System.Drawing.Point(278, 77);
            this.tbBoyLimit2.Name = "tbBoyLimit2";
            this.tbBoyLimit2.Size = new System.Drawing.Size(98, 25);
            this.tbBoyLimit2.TabIndex = 5;
            this.tbBoyLimit2.TextChanged += new System.EventHandler(this.tbGrade2Limit_TextChanged);
            // 
            // tbBoyLimit1
            // 
            // 
            // 
            // 
            this.tbBoyLimit1.Border.Class = "TextBoxBorder";
            this.tbBoyLimit1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbBoyLimit1.Location = new System.Drawing.Point(278, 42);
            this.tbBoyLimit1.Name = "tbBoyLimit1";
            this.tbBoyLimit1.Size = new System.Drawing.Size(98, 25);
            this.tbBoyLimit1.TabIndex = 3;
            this.tbBoyLimit1.TextChanged += new System.EventHandler(this.tbGrade1Limit_TextChanged);
            // 
            // lbGrade3Limit
            // 
            this.lbGrade3Limit.AutoSize = true;
            // 
            // 
            // 
            this.lbGrade3Limit.BackgroundStyle.Class = "";
            this.lbGrade3Limit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbGrade3Limit.Location = new System.Drawing.Point(52, 116);
            this.lbGrade3Limit.Name = "lbGrade3Limit";
            this.lbGrade3Limit.Size = new System.Drawing.Size(54, 21);
            this.lbGrade3Limit.TabIndex = 6;
            this.lbGrade3Limit.Text = "三 年 级";
            // 
            // Grade2Limit
            // 
            this.Grade2Limit.AutoSize = true;
            // 
            // 
            // 
            this.Grade2Limit.BackgroundStyle.Class = "";
            this.Grade2Limit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Grade2Limit.Location = new System.Drawing.Point(52, 81);
            this.Grade2Limit.Name = "Grade2Limit";
            this.Grade2Limit.Size = new System.Drawing.Size(54, 21);
            this.Grade2Limit.TabIndex = 4;
            this.Grade2Limit.Text = "二 年 级";
            // 
            // lbGrade1Limit
            // 
            this.lbGrade1Limit.AutoSize = true;
            // 
            // 
            // 
            this.lbGrade1Limit.BackgroundStyle.Class = "";
            this.lbGrade1Limit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbGrade1Limit.Location = new System.Drawing.Point(52, 46);
            this.lbGrade1Limit.Name = "lbGrade1Limit";
            this.lbGrade1Limit.Size = new System.Drawing.Size(54, 21);
            this.lbGrade1Limit.TabIndex = 2;
            this.lbGrade1Limit.Text = "一 年 级";
            // 
            // tbBoyLimit4
            // 
            // 
            // 
            // 
            this.tbBoyLimit4.Border.Class = "TextBoxBorder";
            this.tbBoyLimit4.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbBoyLimit4.Location = new System.Drawing.Point(278, 147);
            this.tbBoyLimit4.Name = "tbBoyLimit4";
            this.tbBoyLimit4.Size = new System.Drawing.Size(98, 25);
            this.tbBoyLimit4.TabIndex = 9;
            this.tbBoyLimit4.TextChanged += new System.EventHandler(this.tbBoyLimit4_TextChanged);
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(52, 151);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(54, 21);
            this.labelX1.TabIndex = 8;
            this.labelX1.Text = "四 年 级";
            // 
            // tbBoyLimit5
            // 
            // 
            // 
            // 
            this.tbBoyLimit5.Border.Class = "TextBoxBorder";
            this.tbBoyLimit5.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbBoyLimit5.Location = new System.Drawing.Point(278, 182);
            this.tbBoyLimit5.Name = "tbBoyLimit5";
            this.tbBoyLimit5.Size = new System.Drawing.Size(98, 25);
            this.tbBoyLimit5.TabIndex = 11;
            this.tbBoyLimit5.TextChanged += new System.EventHandler(this.tbBoyLimit5_TextChanged);
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(52, 186);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(54, 21);
            this.labelX2.TabIndex = 10;
            this.labelX2.Text = "五 年 级";
            // 
            // tbGirlLimit5
            // 
            // 
            // 
            // 
            this.tbGirlLimit5.Border.Class = "TextBoxBorder";
            this.tbGirlLimit5.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbGirlLimit5.Location = new System.Drawing.Point(413, 182);
            this.tbGirlLimit5.Name = "tbGirlLimit5";
            this.tbGirlLimit5.Size = new System.Drawing.Size(98, 25);
            this.tbGirlLimit5.TabIndex = 16;
            this.tbGirlLimit5.TextChanged += new System.EventHandler(this.tbGirlLimit5_TextChanged);
            // 
            // tbGirlLimit4
            // 
            // 
            // 
            // 
            this.tbGirlLimit4.Border.Class = "TextBoxBorder";
            this.tbGirlLimit4.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbGirlLimit4.Location = new System.Drawing.Point(413, 147);
            this.tbGirlLimit4.Name = "tbGirlLimit4";
            this.tbGirlLimit4.Size = new System.Drawing.Size(98, 25);
            this.tbGirlLimit4.TabIndex = 15;
            this.tbGirlLimit4.TextChanged += new System.EventHandler(this.tbGirlLimit4_TextChanged);
            // 
            // tbGirlLimit1
            // 
            // 
            // 
            // 
            this.tbGirlLimit1.Border.Class = "TextBoxBorder";
            this.tbGirlLimit1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbGirlLimit1.Location = new System.Drawing.Point(413, 42);
            this.tbGirlLimit1.Name = "tbGirlLimit1";
            this.tbGirlLimit1.Size = new System.Drawing.Size(98, 25);
            this.tbGirlLimit1.TabIndex = 12;
            this.tbGirlLimit1.TextChanged += new System.EventHandler(this.tbGirlLimit1_TextChanged);
            // 
            // tbGirlLimit3
            // 
            // 
            // 
            // 
            this.tbGirlLimit3.Border.Class = "TextBoxBorder";
            this.tbGirlLimit3.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbGirlLimit3.Location = new System.Drawing.Point(413, 112);
            this.tbGirlLimit3.Name = "tbGirlLimit3";
            this.tbGirlLimit3.Size = new System.Drawing.Size(98, 25);
            this.tbGirlLimit3.TabIndex = 14;
            this.tbGirlLimit3.TextChanged += new System.EventHandler(this.tbGirlLimit3_TextChanged);
            // 
            // tbGirlLimit2
            // 
            // 
            // 
            // 
            this.tbGirlLimit2.Border.Class = "TextBoxBorder";
            this.tbGirlLimit2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbGirlLimit2.Location = new System.Drawing.Point(413, 77);
            this.tbGirlLimit2.Name = "tbGirlLimit2";
            this.tbGirlLimit2.Size = new System.Drawing.Size(98, 25);
            this.tbGirlLimit2.TabIndex = 13;
            this.tbGirlLimit2.TextChanged += new System.EventHandler(this.tbGirlLimit2_TextChanged);
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(317, 11);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(20, 21);
            this.labelX3.TabIndex = 17;
            this.labelX3.Text = "男";
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(452, 11);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(20, 21);
            this.labelX4.TabIndex = 18;
            this.labelX4.Text = "女";
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(169, 13);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(47, 21);
            this.labelX5.TabIndex = 24;
            this.labelX5.Text = "总人数";
            // 
            // tbLimit5
            // 
            // 
            // 
            // 
            this.tbLimit5.Border.Class = "TextBoxBorder";
            this.tbLimit5.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbLimit5.Location = new System.Drawing.Point(143, 182);
            this.tbLimit5.Name = "tbLimit5";
            this.tbLimit5.Size = new System.Drawing.Size(98, 25);
            this.tbLimit5.TabIndex = 23;
            this.tbLimit5.TextChanged += new System.EventHandler(this.tbLimit5_TextChanged);
            // 
            // tbLimit4
            // 
            // 
            // 
            // 
            this.tbLimit4.Border.Class = "TextBoxBorder";
            this.tbLimit4.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbLimit4.Location = new System.Drawing.Point(143, 147);
            this.tbLimit4.Name = "tbLimit4";
            this.tbLimit4.Size = new System.Drawing.Size(98, 25);
            this.tbLimit4.TabIndex = 22;
            this.tbLimit4.TextChanged += new System.EventHandler(this.tbLimit4_TextChanged);
            // 
            // tbLimit1
            // 
            // 
            // 
            // 
            this.tbLimit1.Border.Class = "TextBoxBorder";
            this.tbLimit1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbLimit1.Location = new System.Drawing.Point(143, 42);
            this.tbLimit1.Name = "tbLimit1";
            this.tbLimit1.Size = new System.Drawing.Size(98, 25);
            this.tbLimit1.TabIndex = 19;
            this.tbLimit1.TextChanged += new System.EventHandler(this.tbLimit1_TextChanged);
            // 
            // tbLimit3
            // 
            // 
            // 
            // 
            this.tbLimit3.Border.Class = "TextBoxBorder";
            this.tbLimit3.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbLimit3.Location = new System.Drawing.Point(143, 112);
            this.tbLimit3.Name = "tbLimit3";
            this.tbLimit3.Size = new System.Drawing.Size(98, 25);
            this.tbLimit3.TabIndex = 21;
            this.tbLimit3.TextChanged += new System.EventHandler(this.tbLimit3_TextChanged);
            // 
            // tbLimit2
            // 
            // 
            // 
            // 
            this.tbLimit2.Border.Class = "TextBoxBorder";
            this.tbLimit2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbLimit2.Location = new System.Drawing.Point(143, 77);
            this.tbLimit2.Name = "tbLimit2";
            this.tbLimit2.Size = new System.Drawing.Size(98, 25);
            this.tbLimit2.TabIndex = 20;
            this.tbLimit2.TextChanged += new System.EventHandler(this.tbLimit2_TextChanged);
            // 
            // ClubRestrictItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.tbLimit5);
            this.Controls.Add(this.tbLimit4);
            this.Controls.Add(this.tbLimit1);
            this.Controls.Add(this.tbLimit3);
            this.Controls.Add(this.tbLimit2);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.tbGirlLimit5);
            this.Controls.Add(this.tbGirlLimit4);
            this.Controls.Add(this.tbGirlLimit1);
            this.Controls.Add(this.tbGirlLimit3);
            this.Controls.Add(this.tbGirlLimit2);
            this.Controls.Add(this.tbBoyLimit5);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.tbBoyLimit4);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.tbBoyLimit1);
            this.Controls.Add(this.tbBoyLimit3);
            this.Controls.Add(this.tbBoyLimit2);
            this.Controls.Add(this.lbGrade1Limit);
            this.Controls.Add(this.Grade2Limit);
            this.Controls.Add(this.lbGrade3Limit);
            this.Name = "ClubRestrictItem";
            this.Size = new System.Drawing.Size(550, 230);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX tbBoyLimit3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbBoyLimit2;
        private DevComponents.DotNetBar.Controls.TextBoxX tbBoyLimit1;
        private DevComponents.DotNetBar.LabelX lbGrade3Limit;
        private DevComponents.DotNetBar.LabelX Grade2Limit;
        private DevComponents.DotNetBar.LabelX lbGrade1Limit;
        private DevComponents.DotNetBar.Controls.TextBoxX tbBoyLimit4;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbBoyLimit5;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX tbGirlLimit5;
        private DevComponents.DotNetBar.Controls.TextBoxX tbGirlLimit4;
        private DevComponents.DotNetBar.Controls.TextBoxX tbGirlLimit1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbGirlLimit3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbGirlLimit2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX tbLimit5;
        private DevComponents.DotNetBar.Controls.TextBoxX tbLimit4;
        private DevComponents.DotNetBar.Controls.TextBoxX tbLimit1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbLimit3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbLimit2;

    }
}
