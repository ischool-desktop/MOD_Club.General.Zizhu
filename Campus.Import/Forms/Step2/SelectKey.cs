using System;
using System.Collections.Generic;
using System.Linq;
using FISCA;
using FISCA.Presentation.Controls;

namespace Campus.ImportZizhu
{
    /// <summary>
    /// 选择键值及异动操作表单
    /// 1.预设的动作为新增或更新。
    /// 2.待处理，若随时将画面关闭则会爆掉。
    /// </summary>
    public partial class SelectKey : WizardForm
    {
        private Dictionary<string, List<string>> mSelectableKeyFields;
        private ArgDictionary mArgs;
        private ImportFullOption mImportOption;
        private ImportWizard mImportWizard;

        public SelectKey()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 建构式，传入精灵参数
        /// </summary>
        /// <param name="args"></param>
        public SelectKey(ArgDictionary args)
            : base(args)
        {
            InitializeComponent();
            mArgs = args;
            mImportOption = args["ImportOption"] as ImportFullOption;
            mImportWizard = args["ImportWizard"] as ImportWizard;
            this.Text = mImportWizard.ValidateRule.Root.GetAttributeText("Name") + "-" + this.Text;
            this.Text += "(" + CurrentStep + "/" + TotalStep + ")"; //功能名称(目前页数/总页数)

            #region 将用户可选择的键值显示在画面上
            mSelectableKeyFields = args["SelectableKeyFields"] as Dictionary<string, List<string>>;

            int cboIdFieldLen = cboIdField.Width;

            foreach (List<string> KeyFields in mSelectableKeyFields.Values)
            {
                string NewItem = string.Join(",", KeyFields.ToArray());

                cboIdField.Items.Add(NewItem);

                if (NewItem.Length > cboIdFieldLen)
                    cboIdFieldLen = NewItem.Length;
            }

            if (cboIdField.Items.Count > 0)
            {
                cboIdField.SelectedIndex = 0;
                //cboIdField.Width = cboIdFieldLen;
            }
            #endregion

            #region 将用户可选择的数据动作
            ImportAction Actions = mImportWizard.GetSupportActions();

            bool IsInsert = (Actions & ImportAction.Insert) == ImportAction.Insert;
            bool IsUpdate = (Actions & ImportAction.Update) == ImportAction.Update;
            bool IsInsertOrUpdate = (Actions & ImportAction.InsertOrUpdate) == ImportAction.InsertOrUpdate;
            bool IsCover = (Actions & ImportAction.Cover) == ImportAction.Cover;
            bool IsDelete = (Actions & ImportAction.Delete) == ImportAction.Delete;

            if (IsInsert)
                lstActions.Items.Add("新增资料");
            if (IsUpdate)
                lstActions.Items.Add("更新数据");
            if (IsInsertOrUpdate)
                lstActions.Items.Add("新增或更新数据");
            if (IsCover)
                lstActions.Items.Add("覆盖数据");

            lstActions.SelectedIndexChanged += (sender, e) =>
            {
                switch ("" + lstActions.SelectedItem)
                {
                    case "新增资料": lblImportActionMessage.Text = "此选项是将所有数据新增到数据库中，不会对现有的数据进行任何修改动作。"; break;
                    case "更新数据": lblImportActionMessage.Text = "此选项将修改数据库中的现有数据，会依据您所指定的识别栏修改数据库中具有相同识别的数据。"; break;
                    case "新增或更新数据": lblImportActionMessage.Text = "此选项是将数据新增或更新到数据库中，会针对您的数据来自动判断新增或更新。"; break;
                    case "覆盖数据": lblImportActionMessage.Text = "此选项是将数据库中的数据都先删除再全部新增"; break;
                    case "删除数据": lblImportActionMessage.Text = "此选项将依汇入数据中的键值删除数据库中的现有数据，请您务必小心谨慎使用。"; break;
                };
            };

            lstActions.KeyDown += (sender, e) =>
            {
                if (e.KeyData == System.Windows.Forms.Keys.Delete)
                    if (IsDelete)
                        lstActions.Items.Add("删除数据");
            };

            lstActions.SelectedIndex = 0;

            #endregion
        }

        /// <summary>
        /// 取得使用者选择的汇入类型
        /// </summary>
        /// <returns></returns>
        private ImportAction GetSelectedImportAction()
        {
            switch ("" + lstActions.SelectedItem)
            {
                case "新增资料": return ImportAction.Insert;
                case "更新数据": return ImportAction.Update;
                case "新增或更新数据": return ImportAction.InsertOrUpdate;
                case "覆盖数据": return ImportAction.Cover;
                case "删除数据": return ImportAction.Delete;
            };

            throw new Exception("使用者没有选择异动类别!");
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
                try
                {
                    #region 将用户所选择键值及动作记录下来
                    //记录选取键值及动作
                    mImportOption.SelectedKeyFields = mSelectableKeyFields[mSelectableKeyFields.Keys.ToList()[cboIdField.SelectedIndex]];
                    //mImportOption.SelectedFields = new List<string>(); //初始化SelectedFields，若是SelectableFields为0，则会判断跳到下个画面。
                    mImportOption.Action = GetSelectedImportAction();

                    //再次取得使用者选取的键值字段
                    List<string> SelectedKeyFields = mImportOption.SelectedKeyFields;

                    SelectedKeyFields.ForEach(x =>
                    {
                        if (!mImportOption.SelectedFields.Contains(x))
                            mImportOption.SelectedFields.Add(x);
                    }
                    );

                    //判断哪些是使用者可以选择的字段，先做此判断，可以直接跳到下个画面
                    List<string> SelectableFields = mImportWizard.FieldProcessor.GetSelectableFields(SelectedKeyFields, mImportOption.SheetFields);

                    mArgs.SetValue("ImportOption", mImportOption);
                    mArgs.SetValue("SelectableFields", SelectableFields);
                    #endregion

                    base.OnNextButtonClick();
                }
                catch (Exception e)
                {
                    MsgBox.Show(e.Message);
                }
            }
        }
    }
}
