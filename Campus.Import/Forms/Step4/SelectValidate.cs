using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Campus.Validator;
using FISCA;

namespace Campus.ImportZizhu
{
    /// <summary>
    /// 数据验证画面
    /// </summary>
    public partial class SelectValidate : WizardForm
    {
        private ArgDictionary mArgs;
        private ImportWizard mImportWizard;
        private ImportFullOption mImportOption;
        private ValidatedInfo mValidatedInfo;
        private string mResultFilename;
        private BackgroundWorker worker = new BackgroundWorker();

        public SelectValidate()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 建构式，传入精灵参数
        /// </summary>
        /// <param name="args"></param>
        public SelectValidate(ArgDictionary args)
            : base(args)
        {
            InitializeComponent();

            //接口初始化设定
            NextButtonTitle = "开始汇入";
            NextButtonEnabled = false;
            btnViewResult.Enabled = false;

            //将精灵参数存起来用
            mArgs = args;
            mImportOption = mArgs["ImportOption"] as ImportFullOption;
            mImportWizard = mArgs["ImportWizard"] as ImportWizard;
            mImportWizard.ImportOption = mImportOption;

            mImportWizard.LoadRule();
            mImportWizard.ValidateRule.Save(Constants.ValidationRulePath);

            mResultFilename = Path.Combine(Constants.ValidationReportsFolder, Path.GetFileNameWithoutExtension(mImportOption.SelectedDataFile) + "(验证报告).xls");
            this.Text = mImportWizard.ValidateRule.Root.GetAttributeText("Name") + "-" + this.Text;
            this.Text += "(" + CurrentStep + "/" + TotalStep + ")"; //功能名称(目前页数/总页数)
            #region 初始化事件
            //加入可停止执行的事件内容
            lnkCancelValid.Click += (sender, e) => worker.CancelAsync();

            //加入检查验证结果程序代码
            btnViewResult.Click += (sender, e) =>
            {
                try
                {
                    Process.Start(mResultFilename);
                }
                catch (Exception ex)
                {
                    FISCA.Presentation.Controls.MsgBox.Show(ex.Message);
                }
            };
            #endregion

            StartValidate();
        }

        /// <summary>
        /// 开始进行数据验证
        /// </summary>
        private void StartValidate()
        {
            //建立数据验证组合
            ValidatePair Pair = new ValidatePair();
            Pair.DataFile = mImportOption.SelectedDataFile; //数据验证来源档案
            Pair.DataSheet = mImportOption.SelectedSheetName; //数据验证来源档案数据表
            Pair.ValidatorFile = mImportOption.SelectedValidateFile; //数据验证描述文件

            Validator.Validator valStart = new Validator.Validator();

            if (mImportWizard.CustomValidate != null)
                valStart.CustomValidate = mImportWizard.CustomValidate;

            //执行数据验证方法
            worker.DoWork += (sender, e) => valStart.Validate(Pair, mResultFilename);

            //将验证过程显示在画面上
            worker.ProgressChanged += (sender, e) =>
            {
                //取得信息对象(数量/讯息/文件名/工作表名)
                ValidatingPair obj = (ValidatingPair)e.UserState;

                //指定接口进度
                pgValidProgress.Value = e.ProgressPercentage;

                lblProgress.Text = obj.Message;

                //如果错误大于0
                if (obj.ErrorCount + obj.WarningCount + obj.AutoCorrectCount > 0)
                {
                    lblErrorCount.Text = "" + obj.ErrorCount;
                    lblWarningCount.Text = "" + obj.WarningCount;
                    lblCorrectCount.Text = "" + obj.AutoCorrectCount;
                }
            };

            //数据验证完成
            worker.RunWorkerCompleted += (sender, e) =>
            {
                if (e.Error != null)
                    throw e.Error;

                int ErrorText = int.Parse(lblErrorCount.Text);
                int WarningText = int.Parse(lblWarningCount.Text);
                int CorrectText = int.Parse(lblCorrectCount.Text);

                //若是错误数量为0才可进行到下一步
                if (lblErrorCount.Text.Equals("0"))
                    this.NextButtonEnabled = true;

                if (ErrorText >= 1) //错误大于1
                    pictureBox1.Image = Properties.Resources.filter_data_close_64;
                else if (WarningText >= 1) //警告大于1
                    pictureBox1.Image = Properties.Resources.filter_data_info_64;
                else //无错误亦无警告时
                    pictureBox1.Image = Properties.Resources.filter_data_ok_64;

                //将检视验证报告的按钮启用
                btnViewResult.Enabled = true;

                //将可暂停异步操作的按钮取消
                lnkCancelValid.Enabled = false;

                if (mValidatedInfo.ValidatedPairs[0].Exceptions.Count > 0)
                {
                    string ExceptionMessage = string.Empty;

                    foreach (Exception Exception in mValidatedInfo.ValidatedPairs[0].Exceptions)
                        ExceptionMessage += Exception.Message + System.Environment.NewLine;

                    if (!string.IsNullOrEmpty(ExceptionMessage))
                    {
                        ExceptionMessage = "验证过程中发生错误，以下为详细错误讯息：" + System.Environment.NewLine + ExceptionMessage;
                        MessageBox.Show(ExceptionMessage);
                    }

                    pictureBox1.Image = Properties.Resources.filter_data_close_64;

                    this.NextButtonEnabled = false;
                }
            };

            //接收数据验证进度回报函式
            valStart.Progress = (message, progress) => worker.ReportProgress(progress, message);
            //接收数据验证完成函式
            valStart.Complete = (message) =>
            {
                mValidatedInfo = message;
            };

            //支持异步取消及进度回报
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            //运用异步执行数据验证
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// 按下进到汇入画面，并将验证结果储存起来
        /// </summary>
        protected override void OnNextButtonClick()
        {
            //将验证结果储存起来
            mArgs.SetValue("ValidatedInfo", mValidatedInfo);
            base.OnNextButtonClick();
        }
    }
}
