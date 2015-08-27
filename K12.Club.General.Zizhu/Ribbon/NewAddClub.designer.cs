namespace K12.Club.General.Zizhu
{
    partial class NewAddClub
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
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.intSchoolYear = new DevComponents.Editors.IntegerInput();
            this.intSemester = new DevComponents.Editors.IntegerInput();
            this.txtClubName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lbSchoolYear = new DevComponents.DotNetBar.LabelX();
            this.lbSemester = new DevComponents.DotNetBar.LabelX();
            this.lbClubName = new DevComponents.DotNetBar.LabelX();
            this.tbAboutClub = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lbAboutClub = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.tbLimit5 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.tbLimit4 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbGirlLimit5 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbLimit3 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbGirlLimit4 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbLimit2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbGirlLimit3 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbLimit1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbGirlLimit2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbGirlLimit1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbBoyLimit5 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lbGrade5Limit = new DevComponents.DotNetBar.LabelX();
            this.tbBoyLimit4 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lbGrade4Limit = new DevComponents.DotNetBar.LabelX();
            this.tbBoyLimit3 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbBoyLimit2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tbBoyLimit1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lbGrade3Limit = new DevComponents.DotNetBar.LabelX();
            this.Grade2Limit = new DevComponents.DotNetBar.LabelX();
            this.lbGrade1Limit = new DevComponents.DotNetBar.LabelX();
            this.cbTeacher = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lbTeacher = new DevComponents.DotNetBar.LabelX();
            this.lbLocation = new DevComponents.DotNetBar.LabelX();
            this.cbLocation = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lbCategory = new DevComponents.DotNetBar.LabelX();
            this.cbCategory = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.tbClubNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lbClubNumber = new DevComponents.DotNetBar.LabelX();
            this.cbTeacher2 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lbTeacher2 = new DevComponents.DotNetBar.LabelX();
            this.cbTeacher3 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lbTeacher3 = new DevComponents.DotNetBar.LabelX();
            this.tbTotalNumberHours = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.intSchoolYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intSemester)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.AutoSize = true;
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(413, 416);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "储存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.AutoSize = true;
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(494, 416);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 19;
            this.btnExit.Text = "离开";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // intSchoolYear
            // 
            this.intSchoolYear.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.intSchoolYear.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intSchoolYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intSchoolYear.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intSchoolYear.Location = new System.Drawing.Point(68, 10);
            this.intSchoolYear.MaxValue = 9999;
            this.intSchoolYear.MinValue = 90;
            this.intSchoolYear.Name = "intSchoolYear";
            this.intSchoolYear.ShowUpDown = true;
            this.intSchoolYear.Size = new System.Drawing.Size(80, 25);
            this.intSchoolYear.TabIndex = 21;
            this.intSchoolYear.Value = 90;
            this.intSchoolYear.ValueChanged += new System.EventHandler(this.intSchoolYear_ValueChanged);
            // 
            // intSemester
            // 
            this.intSemester.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.intSemester.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intSemester.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intSemester.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intSemester.Location = new System.Drawing.Point(220, 10);
            this.intSemester.MaxValue = 2;
            this.intSemester.MinValue = 1;
            this.intSemester.Name = "intSemester";
            this.intSemester.ShowUpDown = true;
            this.intSemester.Size = new System.Drawing.Size(55, 25);
            this.intSemester.TabIndex = 23;
            this.intSemester.Value = 1;
            this.intSemester.ValueChanged += new System.EventHandler(this.intSemester_ValueChanged);
            // 
            // txtClubName
            // 
            // 
            // 
            // 
            this.txtClubName.Border.Class = "TextBoxBorder";
            this.txtClubName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtClubName.Location = new System.Drawing.Point(68, 41);
            this.txtClubName.Name = "txtClubName";
            this.txtClubName.Size = new System.Drawing.Size(207, 25);
            this.txtClubName.TabIndex = 1;
            // 
            // lbSchoolYear
            // 
            this.lbSchoolYear.AutoSize = true;
            this.lbSchoolYear.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbSchoolYear.BackgroundStyle.Class = "";
            this.lbSchoolYear.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbSchoolYear.Location = new System.Drawing.Point(11, 12);
            this.lbSchoolYear.Name = "lbSchoolYear";
            this.lbSchoolYear.Size = new System.Drawing.Size(47, 21);
            this.lbSchoolYear.TabIndex = 20;
            this.lbSchoolYear.Text = "学年度";
            // 
            // lbSemester
            // 
            this.lbSemester.AutoSize = true;
            this.lbSemester.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbSemester.BackgroundStyle.Class = "";
            this.lbSemester.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbSemester.Location = new System.Drawing.Point(171, 12);
            this.lbSemester.Name = "lbSemester";
            this.lbSemester.Size = new System.Drawing.Size(34, 21);
            this.lbSemester.TabIndex = 22;
            this.lbSemester.Text = "学期";
            // 
            // lbClubName
            // 
            this.lbClubName.AutoSize = true;
            this.lbClubName.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbClubName.BackgroundStyle.Class = "";
            this.lbClubName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbClubName.Location = new System.Drawing.Point(11, 43);
            this.lbClubName.Name = "lbClubName";
            this.lbClubName.Size = new System.Drawing.Size(47, 21);
            this.lbClubName.TabIndex = 0;
            this.lbClubName.Text = "名　称";
            // 
            // tbAboutClub
            // 
            // 
            // 
            // 
            this.tbAboutClub.Border.Class = "TextBoxBorder";
            this.tbAboutClub.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbAboutClub.Location = new System.Drawing.Point(68, 295);
            this.tbAboutClub.Multiline = true;
            this.tbAboutClub.Name = "tbAboutClub";
            this.tbAboutClub.Size = new System.Drawing.Size(501, 110);
            this.tbAboutClub.TabIndex = 15;
            // 
            // lbAboutClub
            // 
            this.lbAboutClub.AutoSize = true;
            this.lbAboutClub.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbAboutClub.BackgroundStyle.Class = "";
            this.lbAboutClub.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbAboutClub.Location = new System.Drawing.Point(11, 295);
            this.lbAboutClub.Name = "lbAboutClub";
            this.lbAboutClub.Size = new System.Drawing.Size(47, 21);
            this.lbAboutClub.TabIndex = 14;
            this.lbAboutClub.Text = "简　介";
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.tbLimit5);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.tbLimit4);
            this.groupPanel1.Controls.Add(this.tbGirlLimit5);
            this.groupPanel1.Controls.Add(this.tbLimit3);
            this.groupPanel1.Controls.Add(this.tbGirlLimit4);
            this.groupPanel1.Controls.Add(this.tbLimit2);
            this.groupPanel1.Controls.Add(this.tbGirlLimit3);
            this.groupPanel1.Controls.Add(this.tbLimit1);
            this.groupPanel1.Controls.Add(this.tbGirlLimit2);
            this.groupPanel1.Controls.Add(this.tbGirlLimit1);
            this.groupPanel1.Controls.Add(this.tbBoyLimit5);
            this.groupPanel1.Controls.Add(this.lbGrade5Limit);
            this.groupPanel1.Controls.Add(this.tbBoyLimit4);
            this.groupPanel1.Controls.Add(this.lbGrade4Limit);
            this.groupPanel1.Controls.Add(this.tbBoyLimit3);
            this.groupPanel1.Controls.Add(this.tbBoyLimit2);
            this.groupPanel1.Controls.Add(this.tbBoyLimit1);
            this.groupPanel1.Controls.Add(this.lbGrade3Limit);
            this.groupPanel1.Controls.Add(this.Grade2Limit);
            this.groupPanel1.Controls.Add(this.lbGrade1Limit);
            this.groupPanel1.Location = new System.Drawing.Point(281, 10);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(288, 277);
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
            this.groupPanel1.TabIndex = 16;
            this.groupPanel1.Text = "限制";
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
            this.labelX1.Location = new System.Drawing.Point(79, 1);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(47, 21);
            this.labelX1.TabIndex = 29;
            this.labelX1.Text = "总人数";
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(224, 1);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(20, 21);
            this.labelX4.TabIndex = 21;
            this.labelX4.Text = "女";
            // 
            // tbLimit5
            // 
            // 
            // 
            // 
            this.tbLimit5.Border.Class = "TextBoxBorder";
            this.tbLimit5.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbLimit5.Location = new System.Drawing.Point(76, 214);
            this.tbLimit5.Name = "tbLimit5";
            this.tbLimit5.Size = new System.Drawing.Size(53, 25);
            this.tbLimit5.TabIndex = 28;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(158, 1);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(20, 21);
            this.labelX3.TabIndex = 20;
            this.labelX3.Text = "男";
            // 
            // tbLimit4
            // 
            // 
            // 
            // 
            this.tbLimit4.Border.Class = "TextBoxBorder";
            this.tbLimit4.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbLimit4.Location = new System.Drawing.Point(76, 170);
            this.tbLimit4.Name = "tbLimit4";
            this.tbLimit4.Size = new System.Drawing.Size(53, 25);
            this.tbLimit4.TabIndex = 27;
            // 
            // tbGirlLimit5
            // 
            // 
            // 
            // 
            this.tbGirlLimit5.Border.Class = "TextBoxBorder";
            this.tbGirlLimit5.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbGirlLimit5.Location = new System.Drawing.Point(208, 214);
            this.tbGirlLimit5.Name = "tbGirlLimit5";
            this.tbGirlLimit5.Size = new System.Drawing.Size(53, 25);
            this.tbGirlLimit5.TabIndex = 19;
            // 
            // tbLimit3
            // 
            // 
            // 
            // 
            this.tbLimit3.Border.Class = "TextBoxBorder";
            this.tbLimit3.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbLimit3.Location = new System.Drawing.Point(76, 124);
            this.tbLimit3.Name = "tbLimit3";
            this.tbLimit3.Size = new System.Drawing.Size(53, 25);
            this.tbLimit3.TabIndex = 26;
            // 
            // tbGirlLimit4
            // 
            // 
            // 
            // 
            this.tbGirlLimit4.Border.Class = "TextBoxBorder";
            this.tbGirlLimit4.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbGirlLimit4.Location = new System.Drawing.Point(208, 170);
            this.tbGirlLimit4.Name = "tbGirlLimit4";
            this.tbGirlLimit4.Size = new System.Drawing.Size(53, 25);
            this.tbGirlLimit4.TabIndex = 17;
            // 
            // tbLimit2
            // 
            // 
            // 
            // 
            this.tbLimit2.Border.Class = "TextBoxBorder";
            this.tbLimit2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbLimit2.Location = new System.Drawing.Point(76, 81);
            this.tbLimit2.Name = "tbLimit2";
            this.tbLimit2.Size = new System.Drawing.Size(53, 25);
            this.tbLimit2.TabIndex = 25;
            // 
            // tbGirlLimit3
            // 
            // 
            // 
            // 
            this.tbGirlLimit3.Border.Class = "TextBoxBorder";
            this.tbGirlLimit3.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbGirlLimit3.Location = new System.Drawing.Point(208, 124);
            this.tbGirlLimit3.Name = "tbGirlLimit3";
            this.tbGirlLimit3.Size = new System.Drawing.Size(53, 25);
            this.tbGirlLimit3.TabIndex = 15;
            // 
            // tbLimit1
            // 
            // 
            // 
            // 
            this.tbLimit1.Border.Class = "TextBoxBorder";
            this.tbLimit1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbLimit1.Location = new System.Drawing.Point(76, 36);
            this.tbLimit1.Name = "tbLimit1";
            this.tbLimit1.Size = new System.Drawing.Size(53, 25);
            this.tbLimit1.TabIndex = 24;
            // 
            // tbGirlLimit2
            // 
            // 
            // 
            // 
            this.tbGirlLimit2.Border.Class = "TextBoxBorder";
            this.tbGirlLimit2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbGirlLimit2.Location = new System.Drawing.Point(208, 81);
            this.tbGirlLimit2.Name = "tbGirlLimit2";
            this.tbGirlLimit2.Size = new System.Drawing.Size(53, 25);
            this.tbGirlLimit2.TabIndex = 13;
            // 
            // tbGirlLimit1
            // 
            // 
            // 
            // 
            this.tbGirlLimit1.Border.Class = "TextBoxBorder";
            this.tbGirlLimit1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbGirlLimit1.Location = new System.Drawing.Point(208, 36);
            this.tbGirlLimit1.Name = "tbGirlLimit1";
            this.tbGirlLimit1.Size = new System.Drawing.Size(53, 25);
            this.tbGirlLimit1.TabIndex = 11;
            // 
            // tbBoyLimit5
            // 
            // 
            // 
            // 
            this.tbBoyLimit5.Border.Class = "TextBoxBorder";
            this.tbBoyLimit5.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbBoyLimit5.Location = new System.Drawing.Point(142, 214);
            this.tbBoyLimit5.Name = "tbBoyLimit5";
            this.tbBoyLimit5.Size = new System.Drawing.Size(53, 25);
            this.tbBoyLimit5.TabIndex = 9;
            // 
            // lbGrade5Limit
            // 
            this.lbGrade5Limit.AutoSize = true;
            // 
            // 
            // 
            this.lbGrade5Limit.BackgroundStyle.Class = "";
            this.lbGrade5Limit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbGrade5Limit.Location = new System.Drawing.Point(9, 216);
            this.lbGrade5Limit.Name = "lbGrade5Limit";
            this.lbGrade5Limit.Size = new System.Drawing.Size(54, 21);
            this.lbGrade5Limit.TabIndex = 8;
            this.lbGrade5Limit.Text = "五 年 级";
            // 
            // tbBoyLimit4
            // 
            // 
            // 
            // 
            this.tbBoyLimit4.Border.Class = "TextBoxBorder";
            this.tbBoyLimit4.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbBoyLimit4.Location = new System.Drawing.Point(142, 170);
            this.tbBoyLimit4.Name = "tbBoyLimit4";
            this.tbBoyLimit4.Size = new System.Drawing.Size(53, 25);
            this.tbBoyLimit4.TabIndex = 7;
            // 
            // lbGrade4Limit
            // 
            this.lbGrade4Limit.AutoSize = true;
            // 
            // 
            // 
            this.lbGrade4Limit.BackgroundStyle.Class = "";
            this.lbGrade4Limit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbGrade4Limit.Location = new System.Drawing.Point(9, 172);
            this.lbGrade4Limit.Name = "lbGrade4Limit";
            this.lbGrade4Limit.Size = new System.Drawing.Size(54, 21);
            this.lbGrade4Limit.TabIndex = 6;
            this.lbGrade4Limit.Text = "四 年 级";
            // 
            // tbBoyLimit3
            // 
            // 
            // 
            // 
            this.tbBoyLimit3.Border.Class = "TextBoxBorder";
            this.tbBoyLimit3.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbBoyLimit3.Location = new System.Drawing.Point(142, 124);
            this.tbBoyLimit3.Name = "tbBoyLimit3";
            this.tbBoyLimit3.Size = new System.Drawing.Size(53, 25);
            this.tbBoyLimit3.TabIndex = 5;
            this.tbBoyLimit3.TextChanged += new System.EventHandler(this.tbGrade3Limit_TextChanged);
            // 
            // tbBoyLimit2
            // 
            // 
            // 
            // 
            this.tbBoyLimit2.Border.Class = "TextBoxBorder";
            this.tbBoyLimit2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbBoyLimit2.Location = new System.Drawing.Point(142, 81);
            this.tbBoyLimit2.Name = "tbBoyLimit2";
            this.tbBoyLimit2.Size = new System.Drawing.Size(53, 25);
            this.tbBoyLimit2.TabIndex = 3;
            this.tbBoyLimit2.TextChanged += new System.EventHandler(this.tbGrade2Limit_TextChanged);
            // 
            // tbBoyLimit1
            // 
            // 
            // 
            // 
            this.tbBoyLimit1.Border.Class = "TextBoxBorder";
            this.tbBoyLimit1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbBoyLimit1.Location = new System.Drawing.Point(142, 36);
            this.tbBoyLimit1.Name = "tbBoyLimit1";
            this.tbBoyLimit1.Size = new System.Drawing.Size(53, 25);
            this.tbBoyLimit1.TabIndex = 1;
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
            this.lbGrade3Limit.Location = new System.Drawing.Point(9, 126);
            this.lbGrade3Limit.Name = "lbGrade3Limit";
            this.lbGrade3Limit.Size = new System.Drawing.Size(54, 21);
            this.lbGrade3Limit.TabIndex = 4;
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
            this.Grade2Limit.Location = new System.Drawing.Point(9, 83);
            this.Grade2Limit.Name = "Grade2Limit";
            this.Grade2Limit.Size = new System.Drawing.Size(54, 21);
            this.Grade2Limit.TabIndex = 2;
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
            this.lbGrade1Limit.Location = new System.Drawing.Point(9, 38);
            this.lbGrade1Limit.Name = "lbGrade1Limit";
            this.lbGrade1Limit.Size = new System.Drawing.Size(54, 21);
            this.lbGrade1Limit.TabIndex = 0;
            this.lbGrade1Limit.Text = "一 年 级";
            // 
            // cbTeacher
            // 
            this.cbTeacher.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTeacher.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTeacher.DisplayMember = "Text";
            this.cbTeacher.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbTeacher.FormattingEnabled = true;
            this.cbTeacher.ItemHeight = 19;
            this.cbTeacher.Location = new System.Drawing.Point(68, 196);
            this.cbTeacher.Name = "cbTeacher";
            this.cbTeacher.Size = new System.Drawing.Size(207, 25);
            this.cbTeacher.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbTeacher.TabIndex = 9;
            // 
            // lbTeacher
            // 
            this.lbTeacher.AutoSize = true;
            this.lbTeacher.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbTeacher.BackgroundStyle.Class = "";
            this.lbTeacher.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbTeacher.Location = new System.Drawing.Point(11, 198);
            this.lbTeacher.Name = "lbTeacher";
            this.lbTeacher.Size = new System.Drawing.Size(47, 21);
            this.lbTeacher.TabIndex = 8;
            this.lbTeacher.Text = "老师１";
            // 
            // lbLocation
            // 
            this.lbLocation.AutoSize = true;
            this.lbLocation.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbLocation.BackgroundStyle.Class = "";
            this.lbLocation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbLocation.Location = new System.Drawing.Point(11, 136);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(47, 21);
            this.lbLocation.TabIndex = 4;
            this.lbLocation.Text = "场　地";
            // 
            // cbLocation
            // 
            this.cbLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbLocation.DisplayMember = "Text";
            this.cbLocation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLocation.FormattingEnabled = true;
            this.cbLocation.ItemHeight = 19;
            this.cbLocation.Location = new System.Drawing.Point(68, 134);
            this.cbLocation.Name = "cbLocation";
            this.cbLocation.Size = new System.Drawing.Size(207, 25);
            this.cbLocation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbLocation.TabIndex = 5;
            // 
            // lbCategory
            // 
            this.lbCategory.AutoSize = true;
            this.lbCategory.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbCategory.BackgroundStyle.Class = "";
            this.lbCategory.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbCategory.Location = new System.Drawing.Point(11, 167);
            this.lbCategory.Name = "lbCategory";
            this.lbCategory.Size = new System.Drawing.Size(47, 21);
            this.lbCategory.TabIndex = 6;
            this.lbCategory.Text = "类　型";
            // 
            // cbCategory
            // 
            this.cbCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCategory.DisplayMember = "Text";
            this.cbCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.ItemHeight = 19;
            this.cbCategory.Location = new System.Drawing.Point(68, 165);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(207, 25);
            this.cbCategory.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbCategory.TabIndex = 7;
            // 
            // tbClubNumber
            // 
            // 
            // 
            // 
            this.tbClubNumber.Border.Class = "TextBoxBorder";
            this.tbClubNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbClubNumber.Location = new System.Drawing.Point(68, 72);
            this.tbClubNumber.Name = "tbClubNumber";
            this.tbClubNumber.Size = new System.Drawing.Size(207, 25);
            this.tbClubNumber.TabIndex = 3;
            // 
            // lbClubNumber
            // 
            this.lbClubNumber.AutoSize = true;
            this.lbClubNumber.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbClubNumber.BackgroundStyle.Class = "";
            this.lbClubNumber.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbClubNumber.Location = new System.Drawing.Point(11, 74);
            this.lbClubNumber.Name = "lbClubNumber";
            this.lbClubNumber.Size = new System.Drawing.Size(47, 21);
            this.lbClubNumber.TabIndex = 2;
            this.lbClubNumber.Text = "代　码";
            // 
            // cbTeacher2
            // 
            this.cbTeacher2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTeacher2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTeacher2.DisplayMember = "Text";
            this.cbTeacher2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbTeacher2.FormattingEnabled = true;
            this.cbTeacher2.ItemHeight = 19;
            this.cbTeacher2.Location = new System.Drawing.Point(68, 227);
            this.cbTeacher2.Name = "cbTeacher2";
            this.cbTeacher2.Size = new System.Drawing.Size(207, 25);
            this.cbTeacher2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbTeacher2.TabIndex = 11;
            // 
            // lbTeacher2
            // 
            this.lbTeacher2.AutoSize = true;
            this.lbTeacher2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbTeacher2.BackgroundStyle.Class = "";
            this.lbTeacher2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbTeacher2.Location = new System.Drawing.Point(11, 229);
            this.lbTeacher2.Name = "lbTeacher2";
            this.lbTeacher2.Size = new System.Drawing.Size(47, 21);
            this.lbTeacher2.TabIndex = 10;
            this.lbTeacher2.Text = "老师２";
            // 
            // cbTeacher3
            // 
            this.cbTeacher3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbTeacher3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbTeacher3.DisplayMember = "Text";
            this.cbTeacher3.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbTeacher3.FormattingEnabled = true;
            this.cbTeacher3.ItemHeight = 19;
            this.cbTeacher3.Location = new System.Drawing.Point(68, 258);
            this.cbTeacher3.Name = "cbTeacher3";
            this.cbTeacher3.Size = new System.Drawing.Size(207, 25);
            this.cbTeacher3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbTeacher3.TabIndex = 13;
            // 
            // lbTeacher3
            // 
            this.lbTeacher3.AutoSize = true;
            this.lbTeacher3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbTeacher3.BackgroundStyle.Class = "";
            this.lbTeacher3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbTeacher3.Location = new System.Drawing.Point(11, 260);
            this.lbTeacher3.Name = "lbTeacher3";
            this.lbTeacher3.Size = new System.Drawing.Size(47, 21);
            this.lbTeacher3.TabIndex = 12;
            this.lbTeacher3.Text = "老师３";
            // 
            // tbTotalNumberHours
            // 
            // 
            // 
            // 
            this.tbTotalNumberHours.Border.Class = "TextBoxBorder";
            this.tbTotalNumberHours.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbTotalNumberHours.Location = new System.Drawing.Point(68, 103);
            this.tbTotalNumberHours.Name = "tbTotalNumberHours";
            this.tbTotalNumberHours.Size = new System.Drawing.Size(207, 25);
            this.tbTotalNumberHours.TabIndex = 25;
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
            this.labelX2.Location = new System.Drawing.Point(4, 105);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(60, 21);
            this.labelX2.TabIndex = 24;
            this.labelX2.Text = "总课时数";
            // 
            // NewAddClub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 447);
            this.Controls.Add(this.tbTotalNumberHours);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.cbTeacher3);
            this.Controls.Add(this.lbTeacher3);
            this.Controls.Add(this.cbTeacher2);
            this.Controls.Add(this.lbTeacher2);
            this.Controls.Add(this.tbClubNumber);
            this.Controls.Add(this.lbClubNumber);
            this.Controls.Add(this.lbCategory);
            this.Controls.Add(this.cbTeacher);
            this.Controls.Add(this.tbAboutClub);
            this.Controls.Add(this.intSemester);
            this.Controls.Add(this.intSchoolYear);
            this.Controls.Add(this.txtClubName);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lbTeacher);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.lbAboutClub);
            this.Controls.Add(this.lbLocation);
            this.Controls.Add(this.lbClubName);
            this.Controls.Add(this.lbSemester);
            this.Controls.Add(this.lbSchoolYear);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.cbLocation);
            this.DoubleBuffered = true;
            this.Name = "NewAddClub";
            this.Text = "新增拓展性课程";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewAddClub_FormClosing);
            this.Load += new System.EventHandler(this.NewAddClub_Load);
            ((System.ComponentModel.ISupportInitialize)(this.intSchoolYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intSemester)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.Editors.IntegerInput intSchoolYear;
        private DevComponents.Editors.IntegerInput intSemester;
        private DevComponents.DotNetBar.Controls.TextBoxX txtClubName;
        private DevComponents.DotNetBar.LabelX lbSchoolYear;
        private DevComponents.DotNetBar.LabelX lbSemester;
        private DevComponents.DotNetBar.LabelX lbClubName;
        private DevComponents.DotNetBar.Controls.TextBoxX tbAboutClub;
        private DevComponents.DotNetBar.LabelX lbAboutClub;
        private DevComponents.DotNetBar.LabelX lbLocation;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.LabelX lbGrade3Limit;
        private DevComponents.DotNetBar.LabelX Grade2Limit;
        private DevComponents.DotNetBar.LabelX lbGrade1Limit;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbTeacher;
        private DevComponents.DotNetBar.LabelX lbTeacher;
        private DevComponents.DotNetBar.Controls.TextBoxX tbBoyLimit1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbBoyLimit3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbBoyLimit2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbLocation;
        private DevComponents.DotNetBar.LabelX lbCategory;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbCategory;
        private DevComponents.DotNetBar.Controls.TextBoxX tbClubNumber;
        private DevComponents.DotNetBar.LabelX lbClubNumber;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbTeacher2;
        private DevComponents.DotNetBar.LabelX lbTeacher2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbTeacher3;
        private DevComponents.DotNetBar.LabelX lbTeacher3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbGirlLimit5;
        private DevComponents.DotNetBar.Controls.TextBoxX tbGirlLimit4;
        private DevComponents.DotNetBar.Controls.TextBoxX tbGirlLimit3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbGirlLimit2;
        private DevComponents.DotNetBar.Controls.TextBoxX tbGirlLimit1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbBoyLimit5;
        private DevComponents.DotNetBar.LabelX lbGrade5Limit;
        private DevComponents.DotNetBar.Controls.TextBoxX tbBoyLimit4;
        private DevComponents.DotNetBar.LabelX lbGrade4Limit;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbLimit5;
        private DevComponents.DotNetBar.Controls.TextBoxX tbLimit4;
        private DevComponents.DotNetBar.Controls.TextBoxX tbLimit3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbLimit2;
        private DevComponents.DotNetBar.Controls.TextBoxX tbLimit1;
        private DevComponents.DotNetBar.Controls.TextBoxX tbTotalNumberHours;
        private DevComponents.DotNetBar.LabelX labelX2;
    }
}