using System;
using FISCA;
using FISCA.Presentation.Controls;

namespace Campus.ImportZizhu
{
    public partial class WizardForm : BaseForm
    {
        private FISCA.ContinueDirection WizardResult { get; set; }

        protected ArgDictionary Arguments { get; set; }
        protected int CurrentStep { get; set; }
        protected int TotalStep { get; set; }

        public WizardForm()
        {
            InitializeComponent();
        }

        public WizardForm(ArgDictionary args)
        {
            InitializeComponent();

            Arguments = args;
            CurrentStep  = Arguments.TryGetInteger("CurrentStep", 1);
            TotalStep = Arguments.TryGetInteger("TotalStep", 1);             

            if (TotalStep == 1 || CurrentStep == 1)
                btnPrevious.Enabled = false;

            if (CurrentStep == TotalStep)
                btnNext.Text = "完成";
        }

        public ContinueDirection ShowWizardDialog()
        {
            WizardResult = ContinueDirection.Cancel;
            if (ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return WizardResult;
            }
            else
            {
                return ContinueDirection.Cancel;
            }            
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            OnPreviousButtonClick();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            OnNextButtonClick();
        }

        protected bool PreviousButtonVisible
        {
            get { return btnPrevious.Visible; }
            set { btnPrevious.Visible = value; }
        }

        protected bool PreviousButtonEnabled
        {
            get { return btnPrevious.Enabled; }
            set { btnPrevious.Enabled = value; }
        }

        protected string PreviousButtonTitle
        {
            get { return btnPrevious.Text; }
            set { btnPrevious.Text = value; }
        }

        protected bool NextButtonVisible
        {
            get { return btnNext.Visible; }
            set { btnNext.Visible = value; }
        }

        protected bool NextButtonEnabled
        {
            get { return btnNext.Enabled; }
            set { btnNext.Enabled = value; }
        }

        protected string NextButtonTitle
        {
            get { return btnNext.Text; }
            set { btnNext.Text = value; }
        }

        protected virtual void OnPreviousButtonClick()
        {
            WizardResult = ContinueDirection.Previous;
            Close();
        }

        protected virtual void OnNextButtonClick()
        {
            WizardResult = ContinueDirection.Next;
            Close();
        }
    }
}