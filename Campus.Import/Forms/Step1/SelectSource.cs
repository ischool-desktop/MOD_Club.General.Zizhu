using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Aspose.Cells;
using Campus.Validator;
using FISCA;
using FISCA.Presentation.Controls;

namespace Campus.ImportZizhu
{
    /// <summary>
    /// 选择来源档案及数据表：
    /// 重要说明：
    /// 1.
    /// 待处理：
    /// 1.选择数据表时显示字段。
    /// 2.选择下一步时错误讯息呈现方式目前是用MessageBox，呈现方式可以更精致。
    /// 3.撰写验证规则呈现UI。
    /// </summary>
    public partial class SelectSource : WizardForm
    {
        private ImportFullOption mImportOption;
        private ImportWizard mImportWizard;
        private SheetHelper mSheetHelper;
        private Dictionary<string, List<string>> mSelectablKeyFields;
        private ArgDictionary mArgs;
        private string mImportName = string.Empty;


        public SelectSource()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 建构式，传入精灵选项
        /// </summary>
        /// <param name="args"></param>
        public SelectSource(ArgDictionary args)
            : base(args)
        {
            InitializeComponent();

            //初始化参数
            mArgs = args;
            mImportWizard = args["ImportWizard"] as ImportWizard;
            mImportOption = TryGetOption();
            mImportName = mImportWizard.ValidateRule.Root.GetAttributeText("Name");
            mImportWizard.ImportOption = mImportOption;

            this.Text = mImportName + "-选择档案与汇入方式" + "(" + CurrentStep + "/" + TotalStep + ")";

            //加载验证规则及XSLT
            LoadValudateRule();

            //在使用者选择数据表时，将数据表的字段都记录下来
            lstSheetNames.SelectedIndexChanged += (sender, e) =>
            {
                mSheetHelper.SwitchSeet("" + lstSheetNames.SelectedItem);
                mImportOption.SelectedSheetName = "" + lstSheetNames.SelectedItem;
                mImportOption.SheetFields = mSheetHelper.Fields;
                this.NextButtonEnabled = ValidateNext();
            };

            //检视验证规则
            btnViewRule.Click += (sender, e) =>
            {
                XmlViewForm ViewForm = new XmlViewForm();

                ViewForm.PopXml(mImportName, mImportOption.SelectedValidateFile);

                ViewForm.ShowDialog();
            };

            //检视填表说明
            btnViewRuleExcel.Click += (sender, e) =>
            {
                Workbook book = new Workbook();

                string BookAndSheetName = mImportName + "(空白表格)";

                if (!string.IsNullOrEmpty(BookAndSheetName))
                    book.Worksheets[0].Name = BookAndSheetName;

                int Position = 0;

                foreach (XElement Element in mImportWizard.ValidateRule.Root.Element("FieldList").Elements("Field"))
                {
                    StringBuilder strCommentBuilder = new StringBuilder();

                    string Name = Element.GetAttributeText("Name");
                    bool Required = Element.GetAttributeBool("Required", false);

                    book.Worksheets[0].Cells[0, Position].PutValue(Name);
                    book.Worksheets[0].Cells[0, Position].Style.HorizontalAlignment = TextAlignmentType.Center;
                    book.Worksheets[0].Cells[0, Position].Style.VerticalAlignment = TextAlignmentType.Center;

                    if (Required)
                    {
                        book.Worksheets[0].Cells[0, Position].Style.BackgroundColor = System.Drawing.Color.Red;
                        strCommentBuilder.AppendLine("此为必要字段。");
                    }

                    foreach (XElement SubElement in Element.Elements("Validate"))
                        strCommentBuilder.AppendLine(SubElement.GetAttributeText("Description"));

                    book.Worksheets[0].Comments.Add(0, (byte)Position);
                    book.Worksheets[0].Comments[0, Position].Note = strCommentBuilder.ToString();
                    book.Worksheets[0].Comments[0, Position].WidthInch = 3;

                    Position++;
                }

                book.Worksheets[0].AutoFitColumns();
                Campus.Report.ReportSaver.SaveWorkbook(book, Path.Combine(Constants.ValidationReportsFolder, BookAndSheetName));
            };

            //选择源数据档案
            btnSelectFile.Click += (sender, e) =>
            {
                DialogResult dr = SelectSourceFileDialog.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    try
                    {
                        //记录来源文件名
                        string FileName = SelectSourceFileDialog.FileName;

                        txtSourceFile.Text = SelectSourceFileDialog.FileName;
                        mImportOption.SelectedDataFile = FileName;
                        mSheetHelper = new SheetHelper(FileName);

                        //将数据表列表显示在画面上
                        lstSheetNames.Items.Clear();

                        foreach (Worksheet sheet in mSheetHelper.Book.Worksheets)
                            lstSheetNames.Items.Add(sheet.Name);

                        lstSheetNames.SelectedIndex = 0;
                    }
                    catch (Exception ve)
                    {
                        MsgBox.Show(ve.Message);
                    }
                }
            };

            //将前一步不出现，下一步先失效
            this.PreviousButtonVisible = false;
            this.NextButtonEnabled = false;
        }

        /// <summary>
        /// 尝试取得之前的记录
        /// </summary>
        /// <returns></returns>
        private ImportFullOption TryGetOption()
        {
            //待处理
            //ImportFullOption RestoreOption = mArgs["ImportOption"] as ImportFullOption;

            //if (RestoreOption != null)
            //{
            //    txtSourceFile.Text = RestoreOption.SelectedDataFile;

            //    return RestoreOption;    
            //}

            return new ImportFullOption();
        }

        /// <summary>
        /// 将界面启用或失效
        /// </summary>
        /// <param name="IsEnable"></param>
        private void ToggleUI(bool IsEnable)
        {
            this.btnSelectFile.Enabled = IsEnable;
            this.btnViewRule.Enabled = IsEnable;
            this.btnViewRuleExcel.Enabled = IsEnable;
            //变更名称
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.Append(IsEnable ? mImportName + "-选择档案与汇入方式" : mImportName + "-选择档案与汇入方式 初始化准备中..");
            strBuilder.Append("(" + CurrentStep + "/" + TotalStep + ")");

            this.Text = strBuilder.ToString();
        }

        /// <summary>
        /// 加载验证规则
        /// </summary>
        private void LoadValudateRule()
        {
            BackgroundWorker bkwLoadRule = new BackgroundWorker();

            bkwLoadRule.DoWork += (sender, e) =>
            {
                try
                {
                    XDocument Document = XDocument.Load(@"http://sites.google.com/a/ischool.com.tw/leader/modules/format.xsl");

                    Document.Save(Constants.ValidationFormatPath);

                    mImportWizard.ValidateRule.Save(Constants.ValidationRulePath);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            };

            bkwLoadRule.RunWorkerCompleted += (sender, e) =>
            {
                //载入完后将UI启用
                ToggleUI(true);
                mImportOption.SelectedValidateFile = Constants.ValidationRulePath;
            };

            //加载先先将UI失效
            ToggleUI(false);

            bkwLoadRule.RunWorkerAsync();
        }

        /// <summary>
        /// 验证是否可进行到下一步
        /// </summary>
        /// <returns></returns>
        private bool ValidateNext()
        {
            try
            {
                ctlerrors.Clear();

                if (string.IsNullOrEmpty(txtSourceFile.Text))
                {
                    ctlerrors.SetError(lstSheetNames, "您必须选择汇入来源档案。");
                    return false;
                }

                if (!File.Exists(txtSourceFile.Text))
                {
                    ctlerrors.SetError(lstSheetNames, "您指定的来源档案并不存在。");
                    return false;
                }

                if (lstSheetNames.SelectedItem == null)
                {
                    ctlerrors.SetError(lstSheetNames, "必须指定数据表!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                ctlerrors.SetError(lstSheetNames, ex.Message);
                return false;
            }

            #region 验证键值字段及必填字段
            StringBuilder strFieldsErrorBuilder = new StringBuilder();

            bool IsContainKeyFields = mImportWizard.FieldProcessor.KeyFields.Count > 0;

            mSelectablKeyFields = mImportWizard.FieldProcessor.GetSelectableKeyFields(mImportOption.SheetFields);

            if (IsContainKeyFields && (mSelectablKeyFields == null || mSelectablKeyFields.Count == 0))
            {
                strFieldsErrorBuilder.AppendLine("数据表缺少键值字段，以下为键值组合：");

                foreach (string Key in mImportWizard.FieldProcessor.KeyFields.Keys)
                    strFieldsErrorBuilder.AppendLine(Key + "：" + string.Join(",", mImportWizard.FieldProcessor.KeyFields[Key].ToArray()));
            }

            bool IsContainRequiredFields = mImportWizard.FieldProcessor.IsContainRequiredFields(mImportOption.SheetFields);

            if (!IsContainRequiredFields)
            {
                List<string> RequiredFields = new List<string>();

                strFieldsErrorBuilder.AppendLine("数据表缺少必填字段，以下为必填字段：");

                mImportWizard.FieldProcessor.RequiredFields.ForEach(x => RequiredFields.Add(mImportOption.SheetFields.Contains(x) ? x : x + "(缺)"));

                strFieldsErrorBuilder.AppendLine(string.Join(",", RequiredFields.ToArray()));
            }

            string FieldsError = strFieldsErrorBuilder.ToString();

            if (!string.IsNullOrEmpty(FieldsError))
            {
                ctlerrors.SetError(lstSheetNames, strFieldsErrorBuilder.ToString());
                return false;
            }
            #endregion

            return true;
        }

        /// <summary>
        /// 进行到下一步
        /// </summary>
        protected override void OnNextButtonClick()
        {
            if (ValidateNext())
            {
                //判断哪些是使用者可以选择的字段，先做此判断，可以直接跳到下个画面
                List<string> SelectableFields = mImportWizard.FieldProcessor.GetSelectableFields(new List<string> { }, mImportOption.SheetFields);
                mImportOption.SelectedFields = new List<string>();
                mImportOption.SelectedFields.AddRange(mImportWizard.FieldProcessor.RequiredFields);

                mArgs.SetValue("SelectableFields", SelectableFields);
                //将用户的选项记录下来
                mArgs.SetValue("ImportOption", mImportOption);
                //将使用者可选择的键值传到下个画面
                mArgs.SetValue("SelectableKeyFields", mSelectablKeyFields);
                base.OnNextButtonClick();
            }
        }
    }
}
