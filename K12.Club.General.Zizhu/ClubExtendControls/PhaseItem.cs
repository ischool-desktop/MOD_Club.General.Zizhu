using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace K12.Club.General.Zizhu
{
    public partial class PhaseItem : BaseForm
    {
        public int Phase { get; set; }

        public PhaseItem()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Phase = integerInput1.Value;
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
        }
    }
}
