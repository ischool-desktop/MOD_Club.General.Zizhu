using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FISCA;

namespace Campus.ImportZizhu
{
    /// <summary>
    /// 使用者选择字段
    /// 1.若是没有可选择字段，此画面可不用出现，直接进入到下个画面。
    /// 2.显示工作表所有域名部份暂不设计。
    /// </summary>
    public partial class SelectFields : WizardForm
    {
        private ArgDictionary mArgs;
        private ImportFullOption mImportOption;
        private ImportWizard mImportWizard;
        private List<string> mSelectableFields;

        public SelectFields()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 建构式，传入精灵参数
        /// </summary>
        /// <param name="args"></param>
        public SelectFields(ArgDictionary args)
            : base(args)
        {
            InitializeComponent();
            NextButtonTitle = "开始验证";
            mArgs = args;
            mImportOption = mArgs["ImportOption"] as ImportFullOption;
            mImportWizard = mArgs["ImportWizard"] as ImportWizard;
            mSelectableFields = mArgs["SelectableFields"] as List<string>;

            this.Text = mImportWizard.ValidateRule.Root.GetAttributeText("Name") + "-" + this.Text;
            this.Text += "(" + CurrentStep + "/" + TotalStep + ")"; //功能名称(目前页数/总页数)

            RefreshFields();

            chkSelectAll.CheckedChanged += (sender, e) =>
            {
                foreach (ListViewItem Item in lvSourceFieldList.Items)
                {
                    Item.Checked = chkSelectAll.Checked;
                }
            };

            //若是没有使用者可选择的字段，则直接跳到下个画面；目前设这会有问题...
            //if (mSelectableFields.Count == 0)
            //    this.OnNextButtonClick();
        }

        /// <summary>
        /// 列出使用者可选择汇入的字段
        /// </summary>
        private void RefreshFields()
        {
            if (mSelectableFields != null)
            {
                lvSourceFieldList.Items.Clear();

                mSelectableFields.ForEach(x =>
                {
                    ListViewItem Item = new ListViewItem();
                    Item.Name = x;
                    Item.Text = x;
                    Item.Checked = true;
                    Item.ForeColor = Color.Blue;
                    lvSourceFieldList.Items.Add(Item);
                }
                );
            }
        }

        /// <summary>
        /// 验证是否可进行到下个画面
        /// </summary>
        /// <returns></returns>
        private bool ValidateNext()
        {
            return true;
        }

        protected override void OnNextButtonClick()
        {
            if (ValidateNext())
            {
                foreach (ListViewItem Item in lvSourceFieldList.Items)
                    if (Item.Checked && !mImportOption.SelectedFields.Contains(Item.Name))
                        mImportOption.SelectedFields.Add(Item.Name);

                mArgs.SetValue("ImportOption", mImportOption);

                base.OnNextButtonClick();
            }
        }
    }
}
