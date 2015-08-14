namespace Campus.ImportZizhu
{
    partial class WizardForm
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
            this.btnNext = new DevComponents.DotNetBar.ButtonX();
            this.btnPrevious = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // btnNext
            // 
            this.btnNext.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.AutoSize = true;
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnNext.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnNext.Location = new System.Drawing.Point(405, 329);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 25);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "下一步";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevious.AutoSize = true;
            this.btnPrevious.BackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrevious.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnPrevious.Location = new System.Drawing.Point(320, 329);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 25);
            this.btnPrevious.TabIndex = 0;
            this.btnPrevious.Text = "上一步";
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // WizardForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(492, 366);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Name = "WizardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WizardForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        protected DevComponents.DotNetBar.ButtonX btnNext;
        protected DevComponents.DotNetBar.ButtonX btnPrevious;
    }
}