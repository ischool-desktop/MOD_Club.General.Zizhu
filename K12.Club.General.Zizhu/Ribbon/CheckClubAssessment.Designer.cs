namespace K12.Club.General.Zizhu.Ribbon
{
    partial class CheckClubAssessment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTeacher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExam1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExam2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colClassName,
            this.colTeacher,
            this.colExam1,
            this.colExam2});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv.Location = new System.Drawing.Point(14, 12);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 24;
            this.dgv.Size = new System.Drawing.Size(750, 470);
            this.dgv.TabIndex = 10;
            // 
            // colClassName
            // 
            this.colClassName.HeaderText = "课程名称";
            this.colClassName.Name = "colClassName";
            this.colClassName.ReadOnly = true;
            this.colClassName.Width = 290;
            // 
            // colTeacher
            // 
            this.colTeacher.HeaderText = "评鉴教师";
            this.colTeacher.Name = "colTeacher";
            this.colTeacher.ReadOnly = true;
            this.colTeacher.Width = 155;
            // 
            // colExam1
            // 
            this.colExam1.HeaderText = "学生人数";
            this.colExam1.Name = "colExam1";
            this.colExam1.ReadOnly = true;
            this.colExam1.Width = 150;
            // 
            // colExam2
            // 
            this.colExam2.HeaderText = "输入进度";
            this.colExam2.Name = "colExam2";
            this.colExam2.ReadOnly = true;
            this.colExam2.Width = 150;
            // 
            // CheckClubAssessment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 494);
            this.Controls.Add(this.dgv);
            this.DoubleBuffered = true;
            this.Name = "CheckClubAssessment";
            this.Text = "拓展型课程教师评鉴输入进度";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClassName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTeacher;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExam1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExam2;

    }
}