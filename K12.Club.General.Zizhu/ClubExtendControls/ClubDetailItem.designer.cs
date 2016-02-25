namespace K12.Club.General.Zizhu
{
    partial class ClubDetailItem
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
            this.txtAbout = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cbLocation = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbTeacher1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lbTeacher1 = new DevComponents.DotNetBar.LabelX();
            this.lbLocation = new DevComponents.DotNetBar.LabelX();
            this.lbAbout = new DevComponents.DotNetBar.LabelX();
            this.lbSchoolYear = new DevComponents.DotNetBar.LabelX();
            this.lbCategory = new DevComponents.DotNetBar.LabelX();
            this.lbCLUBNumber = new DevComponents.DotNetBar.LabelX();
            this.tbCLUBNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cbCategory = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lbTeacher2 = new DevComponents.DotNetBar.LabelX();
            this.cbTeacher2 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lbTeacher3 = new DevComponents.DotNetBar.LabelX();
            this.cbTeacher3 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tbTotalNumberHours = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lbTotalNumberHours = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cbFullPhase = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.lbClubName = new DevComponents.DotNetBar.LabelX();
            this.txtClubName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtDomain = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtType = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtFormal = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.SuspendLayout();
            // 
            // txtAbout
            // 
            // 
            // 
            // 
            this.txtAbout.Border.Class = "TextBoxBorder";
            this.txtAbout.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAbout.Location = new System.Drawing.Point(87, 266);
            this.txtAbout.Margin = new System.Windows.Forms.Padding(4);
            this.txtAbout.Multiline = true;
            this.txtAbout.Name = "txtAbout";
            this.txtAbout.Size = new System.Drawing.Size(416, 67);
            this.txtAbout.TabIndex = 12;
            // 
            // cbLocation
            // 
            this.cbLocation.DisplayMember = "Text";
            this.cbLocation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLocation.FormattingEnabled = true;
            this.cbLocation.ItemHeight = 19;
            this.cbLocation.Location = new System.Drawing.Point(88, 162);
            this.cbLocation.Name = "cbLocation";
            this.cbLocation.Size = new System.Drawing.Size(178, 25);
            this.cbLocation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbLocation.TabIndex = 6;
            // 
            // cbTeacher1
            // 
            this.cbTeacher1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTeacher1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTeacher1.DisplayMember = "Text";
            this.cbTeacher1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbTeacher1.FormattingEnabled = true;
            this.cbTeacher1.ItemHeight = 19;
            this.cbTeacher1.Location = new System.Drawing.Point(325, 162);
            this.cbTeacher1.Name = "cbTeacher1";
            this.cbTeacher1.Size = new System.Drawing.Size(178, 25);
            this.cbTeacher1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbTeacher1.TabIndex = 7;
            this.cbTeacher1.Leave += new System.EventHandler(this.cbTeacher1_Leave);
            // 
            // lbTeacher1
            // 
            this.lbTeacher1.AutoSize = true;
            // 
            // 
            // 
            this.lbTeacher1.BackgroundStyle.Class = "";
            this.lbTeacher1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbTeacher1.Location = new System.Drawing.Point(277, 164);
            this.lbTeacher1.Name = "lbTeacher1";
            this.lbTeacher1.Size = new System.Drawing.Size(41, 21);
            this.lbTeacher1.TabIndex = 9;
            this.lbTeacher1.Text = "老师1";
            // 
            // lbLocation
            // 
            this.lbLocation.AutoSize = true;
            // 
            // 
            // 
            this.lbLocation.BackgroundStyle.Class = "";
            this.lbLocation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbLocation.Location = new System.Drawing.Point(21, 164);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(60, 21);
            this.lbLocation.TabIndex = 3;
            this.lbLocation.Text = "场　　地";
            // 
            // lbAbout
            // 
            this.lbAbout.AutoSize = true;
            // 
            // 
            // 
            this.lbAbout.BackgroundStyle.Class = "";
            this.lbAbout.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbAbout.Location = new System.Drawing.Point(21, 266);
            this.lbAbout.Name = "lbAbout";
            this.lbAbout.Size = new System.Drawing.Size(60, 21);
            this.lbAbout.TabIndex = 15;
            this.lbAbout.Text = "简　　介";
            // 
            // lbSchoolYear
            // 
            this.lbSchoolYear.AutoSize = true;
            // 
            // 
            // 
            this.lbSchoolYear.BackgroundStyle.Class = "";
            this.lbSchoolYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbSchoolYear.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbSchoolYear.Location = new System.Drawing.Point(87, 15);
            this.lbSchoolYear.Name = "lbSchoolYear";
            this.lbSchoolYear.Size = new System.Drawing.Size(125, 32);
            this.lbSchoolYear.TabIndex = 0;
            this.lbSchoolYear.Text = "学年度 / 学期";
            // 
            // lbCategory
            // 
            this.lbCategory.AutoSize = true;
            // 
            // 
            // 
            this.lbCategory.BackgroundStyle.Class = "";
            this.lbCategory.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbCategory.Location = new System.Drawing.Point(21, 198);
            this.lbCategory.Name = "lbCategory";
            this.lbCategory.Size = new System.Drawing.Size(60, 21);
            this.lbCategory.TabIndex = 5;
            this.lbCategory.Text = "类　　型";
            // 
            // lbCLUBNumber
            // 
            this.lbCLUBNumber.AutoSize = true;
            // 
            // 
            // 
            this.lbCLUBNumber.BackgroundStyle.Class = "";
            this.lbCLUBNumber.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbCLUBNumber.Location = new System.Drawing.Point(277, 62);
            this.lbCLUBNumber.Name = "lbCLUBNumber";
            this.lbCLUBNumber.Size = new System.Drawing.Size(44, 21);
            this.lbCLUBNumber.TabIndex = 7;
            this.lbCLUBNumber.Text = "代   码";
            // 
            // tbCLUBNumber
            // 
            // 
            // 
            // 
            this.tbCLUBNumber.Border.Class = "TextBoxBorder";
            this.tbCLUBNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbCLUBNumber.Location = new System.Drawing.Point(325, 60);
            this.tbCLUBNumber.Margin = new System.Windows.Forms.Padding(4);
            this.tbCLUBNumber.Name = "tbCLUBNumber";
            this.tbCLUBNumber.Size = new System.Drawing.Size(178, 25);
            this.tbCLUBNumber.TabIndex = 1;
            // 
            // cbCategory
            // 
            this.cbCategory.DisplayMember = "Text";
            this.cbCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.ItemHeight = 19;
            this.cbCategory.Location = new System.Drawing.Point(88, 196);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(178, 25);
            this.cbCategory.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbCategory.TabIndex = 8;
            // 
            // lbTeacher2
            // 
            this.lbTeacher2.AutoSize = true;
            // 
            // 
            // 
            this.lbTeacher2.BackgroundStyle.Class = "";
            this.lbTeacher2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbTeacher2.Location = new System.Drawing.Point(277, 198);
            this.lbTeacher2.Name = "lbTeacher2";
            this.lbTeacher2.Size = new System.Drawing.Size(41, 21);
            this.lbTeacher2.TabIndex = 11;
            this.lbTeacher2.Text = "老师2";
            // 
            // cbTeacher2
            // 
            this.cbTeacher2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTeacher2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTeacher2.DisplayMember = "Text";
            this.cbTeacher2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbTeacher2.FormattingEnabled = true;
            this.cbTeacher2.ItemHeight = 19;
            this.cbTeacher2.Location = new System.Drawing.Point(325, 196);
            this.cbTeacher2.Name = "cbTeacher2";
            this.cbTeacher2.Size = new System.Drawing.Size(178, 25);
            this.cbTeacher2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbTeacher2.TabIndex = 9;
            this.cbTeacher2.Leave += new System.EventHandler(this.cbTeacher2_Leave);
            // 
            // lbTeacher3
            // 
            this.lbTeacher3.AutoSize = true;
            // 
            // 
            // 
            this.lbTeacher3.BackgroundStyle.Class = "";
            this.lbTeacher3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbTeacher3.Location = new System.Drawing.Point(277, 232);
            this.lbTeacher3.Name = "lbTeacher3";
            this.lbTeacher3.Size = new System.Drawing.Size(41, 21);
            this.lbTeacher3.TabIndex = 13;
            this.lbTeacher3.Text = "老师3";
            // 
            // cbTeacher3
            // 
            this.cbTeacher3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTeacher3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTeacher3.DisplayMember = "Text";
            this.cbTeacher3.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbTeacher3.FormattingEnabled = true;
            this.cbTeacher3.ItemHeight = 19;
            this.cbTeacher3.Location = new System.Drawing.Point(325, 230);
            this.cbTeacher3.Name = "cbTeacher3";
            this.cbTeacher3.Size = new System.Drawing.Size(178, 25);
            this.cbTeacher3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbTeacher3.TabIndex = 11;
            this.cbTeacher3.Leave += new System.EventHandler(this.cbTeacher3_Leave);
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(507, 164);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(42, 21);
            this.labelX1.TabIndex = 17;
            this.labelX1.Text = "(评分)";
            // 
            // tbTotalNumberHours
            // 
            // 
            // 
            // 
            this.tbTotalNumberHours.Border.Class = "TextBoxBorder";
            this.tbTotalNumberHours.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbTotalNumberHours.Location = new System.Drawing.Point(87, 60);
            this.tbTotalNumberHours.Margin = new System.Windows.Forms.Padding(4);
            this.tbTotalNumberHours.Name = "tbTotalNumberHours";
            this.tbTotalNumberHours.Size = new System.Drawing.Size(178, 25);
            this.tbTotalNumberHours.TabIndex = 0;
            // 
            // lbTotalNumberHours
            // 
            this.lbTotalNumberHours.AutoSize = true;
            // 
            // 
            // 
            this.lbTotalNumberHours.BackgroundStyle.Class = "";
            this.lbTotalNumberHours.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbTotalNumberHours.Location = new System.Drawing.Point(21, 62);
            this.lbTotalNumberHours.Name = "lbTotalNumberHours";
            this.lbTotalNumberHours.Size = new System.Drawing.Size(60, 21);
            this.lbTotalNumberHours.TabIndex = 18;
            this.lbTotalNumberHours.Text = "总课时数";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(21, 232);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(60, 21);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "长短课程";
            // 
            // cbFullPhase
            // 
            this.cbFullPhase.DisplayMember = "Text";
            this.cbFullPhase.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFullPhase.FormattingEnabled = true;
            this.cbFullPhase.ItemHeight = 19;
            this.cbFullPhase.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cbFullPhase.Location = new System.Drawing.Point(88, 230);
            this.cbFullPhase.Name = "cbFullPhase";
            this.cbFullPhase.Size = new System.Drawing.Size(178, 25);
            this.cbFullPhase.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbFullPhase.TabIndex = 10;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "短课程";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "长课程";
            // 
            // lbClubName
            // 
            this.lbClubName.AutoSize = true;
            // 
            // 
            // 
            this.lbClubName.BackgroundStyle.Class = "";
            this.lbClubName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbClubName.Location = new System.Drawing.Point(21, 96);
            this.lbClubName.Name = "lbClubName";
            this.lbClubName.Size = new System.Drawing.Size(60, 21);
            this.lbClubName.TabIndex = 1;
            this.lbClubName.Text = "名　　称";
            // 
            // txtClubName
            // 
            // 
            // 
            // 
            this.txtClubName.Border.Class = "TextBoxBorder";
            this.txtClubName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtClubName.Location = new System.Drawing.Point(88, 94);
            this.txtClubName.Margin = new System.Windows.Forms.Padding(4);
            this.txtClubName.Name = "txtClubName";
            this.txtClubName.Size = new System.Drawing.Size(178, 25);
            this.txtClubName.TabIndex = 2;
            // 
            // txtDomain
            // 
            // 
            // 
            // 
            this.txtDomain.Border.Class = "TextBoxBorder";
            this.txtDomain.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDomain.Location = new System.Drawing.Point(325, 94);
            this.txtDomain.Margin = new System.Windows.Forms.Padding(4);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(178, 25);
            this.txtDomain.TabIndex = 3;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(277, 96);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(44, 21);
            this.labelX3.TabIndex = 20;
            this.labelX3.Text = "领   域";
            // 
            // txtType
            // 
            // 
            // 
            // 
            this.txtType.Border.Class = "TextBoxBorder";
            this.txtType.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtType.Location = new System.Drawing.Point(325, 128);
            this.txtType.Margin = new System.Windows.Forms.Padding(4);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(178, 25);
            this.txtType.TabIndex = 5;
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(277, 130);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(44, 21);
            this.labelX4.TabIndex = 22;
            this.labelX4.Text = "属   性";
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(21, 130);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(60, 21);
            this.labelX5.TabIndex = 1;
            this.labelX5.Text = "上课形式";
            // 
            // txtFormal
            // 
            // 
            // 
            // 
            this.txtFormal.Border.Class = "TextBoxBorder";
            this.txtFormal.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFormal.Location = new System.Drawing.Point(87, 128);
            this.txtFormal.Margin = new System.Windows.Forms.Padding(4);
            this.txtFormal.Name = "txtFormal";
            this.txtFormal.Size = new System.Drawing.Size(178, 25);
            this.txtFormal.TabIndex = 4;
            // 
            // ClubDetailItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.txtDomain);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.tbTotalNumberHours);
            this.Controls.Add(this.lbTotalNumberHours);
            this.Controls.Add(this.cbFullPhase);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.txtAbout);
            this.Controls.Add(this.txtFormal);
            this.Controls.Add(this.txtClubName);
            this.Controls.Add(this.cbLocation);
            this.Controls.Add(this.cbTeacher3);
            this.Controls.Add(this.cbTeacher2);
            this.Controls.Add(this.tbCLUBNumber);
            this.Controls.Add(this.cbTeacher1);
            this.Controls.Add(this.lbTeacher3);
            this.Controls.Add(this.lbTeacher2);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.lbCLUBNumber);
            this.Controls.Add(this.lbCategory);
            this.Controls.Add(this.lbSchoolYear);
            this.Controls.Add(this.lbAbout);
            this.Controls.Add(this.lbLocation);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.lbTeacher1);
            this.Controls.Add(this.lbClubName);
            this.Controls.Add(this.labelX1);
            this.Name = "ClubDetailItem";
            this.Size = new System.Drawing.Size(550, 350);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal DevComponents.DotNetBar.Controls.TextBoxX txtAbout;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbLocation;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbTeacher1;
        private DevComponents.DotNetBar.LabelX lbTeacher1;
        private DevComponents.DotNetBar.LabelX lbLocation;
        private DevComponents.DotNetBar.LabelX lbAbout;
        private DevComponents.DotNetBar.LabelX lbSchoolYear;
        private DevComponents.DotNetBar.LabelX lbCategory;
        private DevComponents.DotNetBar.LabelX lbCLUBNumber;
        internal DevComponents.DotNetBar.Controls.TextBoxX tbCLUBNumber;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cbCategory;
        private DevComponents.DotNetBar.LabelX lbTeacher2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbTeacher2;
        private DevComponents.DotNetBar.LabelX lbTeacher3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbTeacher3;
        private DevComponents.DotNetBar.LabelX labelX1;
        internal DevComponents.DotNetBar.Controls.TextBoxX tbTotalNumberHours;
        private DevComponents.DotNetBar.LabelX lbTotalNumberHours;
        private DevComponents.DotNetBar.LabelX labelX2;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cbFullPhase;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.LabelX lbClubName;
        internal DevComponents.DotNetBar.Controls.TextBoxX txtClubName;
        internal DevComponents.DotNetBar.Controls.TextBoxX txtDomain;
        private DevComponents.DotNetBar.LabelX labelX3;
        internal DevComponents.DotNetBar.Controls.TextBoxX txtType;
        private DevComponents.DotNetBar.LabelX labelX4;
        internal DevComponents.DotNetBar.Controls.TextBoxX txtFormal;
        private DevComponents.DotNetBar.LabelX labelX5;
    }
}
