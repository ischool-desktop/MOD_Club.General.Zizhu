using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Aspose.Cells;
using Campus.DocumentValidator;
using Campus.Validator;
using FISCA;
using System.ComponentModel;

namespace Campus.ImportZizhu
{
    /// <summary>
    /// 汇入画面
    /// </summary>
    public partial class SelectImport : WizardForm
    {
        private ArgDictionary mArgs;
        private ImportWizard mImportWizard;
        private ImportFullOption mImportOption;
        private ValidatedInfo mValidatedInfo;
        private string mImportName;

        /// <summary>
        /// 无参数建构式
        /// </summary>
        public SelectImport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 建构式，传入精灵参数
        /// </summary>
        /// <param name="args"></param>
        public SelectImport(ArgDictionary args)
            : base(args)
        {
            InitializeComponent();

            this.PreviousButtonVisible = false;
            this.NextButtonTitle = "完成";
            this.NextButtonEnabled = false;

            //将精灵参数存起来用
            mArgs = args;
            mImportOption = mArgs["ImportOption"] as ImportFullOption;
            mImportWizard = mArgs["ImportWizard"] as ImportWizard;
            mValidatedInfo = mArgs["ValidatedInfo"] as ValidatedInfo;

            mImportName = mImportWizard.ValidateRule.Root.GetAttributeText("Name");

            this.Text = mImportName + "-" + this.Text;
            this.Text += "(5/5)"; //功能名称(目前页数/总页数)
            //this.TitleText += "(" + CurrentStep + "/" + TotalStep + ")"; //功能名称(目前页数/总页数)
        }

        private void ChangeProgress(int Progress)
        {
            //Invoke(new Action(() =>
            //    {
            pgImport.Value = Progress;
            //    }));

            Application.DoEvents();
        }

        /// <summary>
        /// 开始汇入
        /// </summary>
        private void StartImport()
        {
            mImportWizard.Prepare(mImportOption);

            List<string> Result = new List<string>();

            #region 已由Validator提供
            //要汇入的IRowStream列表
            List<IRowStream> Rows = mValidatedInfo.ValidatedPairs[0].Rows;

            pgImport.Maximum = Rows.Count;
            #endregion

            #region 实际执行汇入动作
            if (mImportWizard.IsSplit)
            {
                try
                {
                    FunctionSpliter<IRowStream, string> Spliter = new FunctionSpliter<IRowStream, string>(mImportWizard.SplitSize, mImportWizard.SplitThreadCount);

                    Spliter.Function = ExecuteImport; //指定Spliter的执行函式

                    BackgroundWorker workder = new BackgroundWorker();

                    workder.DoWork += (sender, e) => e.Result = Spliter.Execute(Rows);

                    workder.RunWorkerCompleted += (sender, e) => CompleteImport((List<string>)e.Result);

                    Spliter.ProgressChange = x => Invoke(new Action(() => pgImport.Value = x));

                    workder.RunWorkerAsync();
                }
                catch (Exception e)
                {
                    MessageBox.Show("汇入过程中发生错误，以下为详细讯息：" + System.Environment.NewLine + e.Message);
                    SmartSchool.ErrorReporting.ReportingService.ReportException(e);
                    this.Close();
                    this.Dispose();
                    return;
                }
            }
            else
            {
                #region 重新读取Rows
                //Rows = new List<IRowStream>();

                ////建立SheetRowSource对象，用来读取Excel上的数据
                //SheetRowSource RowSource = new SheetRowSource(mValidatedInfo.ResultHelper);

                //int FirstDataRowIndex = mValidatedInfo.ResultHelper.FirstDataRowIndex;
                //int MaxDataRowIndex = mValidatedInfo.ResultHelper.MaxDataRowIndex;

                ////将所有的SheetRowSource对象复制一份并放到IRowStream列表中
                //for (int i = FirstDataRowIndex; i <= MaxDataRowIndex; i++)
                //{
                //    RowSource.BindRow(i); //将SheetRowSource指定到某列
                //    RowStream RowStream = RowSource.Clone() as RowStream; //将SheetRowSource目前所指向的内容复制
                //    Rows.Add(RowStream); //将RowStream加入到集合中
                //}
                #endregion

                try
                {
                    mImportWizard.PropertyChanged += (sender, e) =>
                    {
                        if (e.PropertyName.Equals("ImportProgress"))
                            Invoke(new Action(() => pgImport.Value = mImportWizard.ImportProgress));
                    };

                    Result = ExecuteImport(Rows);
                    CompleteImport(Result);
                }
                catch (Exception e)
                {
                    MessageBox.Show("汇入过程中发生错误，以下为详细讯息：" + System.Environment.NewLine + e.Message);
                    SmartSchool.ErrorReporting.ReportingService.ReportException(e);
                    this.Close();
                    this.Dispose();
                    return;
                }
            }
            #endregion
        }

        /// <summary>
        /// 汇入完成
        /// </summary>
        /// <param name="Result"></param>
        private void CompleteImport(List<string> Result)
        {
            try
            {
                SaveImportMessage();

                StringBuilder strLogMessage = new StringBuilder();

                Result.ForEach(x => strLogMessage.AppendLine(x));

                string CombineResult = strLogMessage.ToString();

                if (!string.IsNullOrEmpty(CombineResult) && mImportWizard.IsLog)
                    FISCA.LogAgent.ApplicationLog.Log(mImportName, "汇入", CombineResult);

                StringBuilder strCompleteMessage = new StringBuilder();

                if (mImportWizard.Complete != null)
                    strCompleteMessage.AppendLine(mImportWizard.Complete.Invoke());

                txtResult.Text = strCompleteMessage.ToString();
                //显示图标
                picComplete.Visible = true;

                this.NextButtonEnabled = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("写入汇入Log及显示完成讯息发生错误，此错误不影响汇入结果，详细讯息：" + System.Environment.NewLine + e.Message);
            }
        }

        /// <summary>
        /// 将汇入讯息写入到最后的Excel中
        /// </summary>
        private void SaveImportMessage()
        {
            if (mImportWizard.ImportMessages.Positions.Count > 0)
            {
                string ImportFileName = Path.Combine(Constants.ValidationReportsFolder, Path.GetFileNameWithoutExtension(mImportOption.SelectedDataFile) + "(汇入报告).xls");
                int MaxDataColumn = mValidatedInfo.ResultHelper.Sheet.Cells.MaxDataColumn;

                SheetHelper helper = new SheetHelper(mImportOption.SelectedDataFile);
                helper.SwitchSeet(mImportOption.SelectedSheetName);

                InitialMessageHeader(helper.Sheet);

                mImportWizard.ImportMessages.Positions.ForEach(x => helper.SetValue(x, MaxDataColumn, mImportWizard.ImportMessages[x]));

                helper.Book.Save(ImportFileName);

                btnViewResult.Visible = true;

                btnViewResult.Click += (sender, e) =>
                {
                    try
                    {
                        Process.Start(ImportFileName);
                    }
                    catch (Exception ex)
                    {
                        FISCA.Presentation.Controls.MsgBox.Show(ex.Message);
                    }
                };
            }
        }

        /// <summary>
        /// 最后已有『汇入讯息』字段，则将其下所有域值清空，若无的话加上『汇入讯息』表头
        /// </summary>
        private void InitialMessageHeader(Worksheet sheet)
        {
            int ImportMessageColumnIndex;

            string MaxDataColumnValue = "" + sheet.Cells[0, sheet.Cells.MaxDataColumn].Value;

            if (MaxDataColumnValue.Contains("汇入讯息")) //如果已经有此字段
            {
                ImportMessageColumnIndex = sheet.Cells.MaxDataColumn;
                for (int x = 1; x <= sheet.Cells.MaxDataRow; x++) //清空此字段的内容
                    sheet.Cells[x, ImportMessageColumnIndex].PutValue("");
            }
            else //如果没有此字段
            {
                ImportMessageColumnIndex = sheet.Cells.MaxDataColumn + 1;
            }

            sheet.Cells[0, ImportMessageColumnIndex].PutValue("汇入讯息");
        }

        /// <summary>
        /// 包装执行汇入程序，供FunctionSpliter使用
        /// </summary>
        /// <param name="Rows"></param>
        /// <returns></returns>
        private List<string> ExecuteImport(List<IRowStream> Rows)
        {
            List<string> Result = new List<string>();

            string ImportResult = mImportWizard.Import(Rows);

            Result.Add(ImportResult);

            return Result;
        }

        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectImport_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            StartImport();
        }
    }
}
