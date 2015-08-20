using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.UDT;
using Campus.Windows;
using System.Xml;
using FISCA.DSAUtil;
using DevComponents.DotNetBar.Controls;
using FISCA.Data;
using FISCA.Permission;

namespace K12.Club.General.Zizhu
{
    [FISCA.Permission.FeatureCode("K12.Club.Universal.ClubRestrictItem.cs", "限制")]
    public partial class ClubRestrictItem : DetailContentBase
    {
        //背景模式
        private BackgroundWorker BGW = new BackgroundWorker();
        private BackgroundWorker Save_BGW = new BackgroundWorker();

        private AccessHelper _AccessHelper = new AccessHelper();
        private QueryHelper _QueryHelper = new QueryHelper();
        private CLUBRecord ClubPrimary;
        private CLUBRecord Log_ClubPrimary;

        string SetStringIsInt = "必须输入数字!!";

        //權限
        internal static FeatureAce UserPermission;

        //資料變更事件引發器
        private ChangeListener DataListener { get; set; }

        //背景忙碌
        private bool BkWBool = false;

        //資料檢查功能
        ErrorProvider ep_Grade1Limit = new ErrorProvider();
        ErrorProvider ep_Grade2Limit = new ErrorProvider();
        ErrorProvider ep_Grade3Limit = new ErrorProvider();
        ErrorProvider ep_Grade4Limit = new ErrorProvider();
        ErrorProvider ep_Grade5Limit = new ErrorProvider();

        ErrorProvider ep_Grade1BoyLimit = new ErrorProvider();
        ErrorProvider ep_Grade2BoyLimit = new ErrorProvider();
        ErrorProvider ep_Grade3BoyLimit = new ErrorProvider();
        ErrorProvider ep_Grade4BoyLimit = new ErrorProvider();
        ErrorProvider ep_Grade5BoyLimit = new ErrorProvider();

        ErrorProvider ep_Grade1GirlLimit = new ErrorProvider();
        ErrorProvider ep_Grade2GirlLimit = new ErrorProvider();
        ErrorProvider ep_Grade3GirlLimit = new ErrorProvider();
        ErrorProvider ep_Grade4GirlLimit = new ErrorProvider();
        ErrorProvider ep_Grade5GirlLimit = new ErrorProvider();

        public ClubRestrictItem()
        {
            InitializeComponent();

            Group = "限制";

            UserPermission = UserAcl.Current[FISCA.Permission.FeatureCodeAttribute.GetCode(GetType())];
            this.Enabled = UserPermission.Editable;

            BGW.DoWork += new DoWorkEventHandler(BGW_DoWork);
            BGW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGW_RunWorkerCompleted);

            Save_BGW.DoWork += new DoWorkEventHandler(Save_BGW_DoWork);
            Save_BGW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Save_BGW_RunWorkerCompleted);

            ClubEvents.ClubChanged += new EventHandler(ClubEvents_ClubChanged);

            DataListener = new ChangeListener();

            DataListener.Add(new TextBoxSource(tbLimit1));
            DataListener.Add(new TextBoxSource(tbLimit2));
            DataListener.Add(new TextBoxSource(tbLimit3));
            DataListener.Add(new TextBoxSource(tbLimit4));
            DataListener.Add(new TextBoxSource(tbLimit5));

            DataListener.Add(new TextBoxSource(tbBoyLimit1));
            DataListener.Add(new TextBoxSource(tbBoyLimit2));
            DataListener.Add(new TextBoxSource(tbBoyLimit3));
            DataListener.Add(new TextBoxSource(tbBoyLimit4));
            DataListener.Add(new TextBoxSource(tbBoyLimit5));

            DataListener.Add(new TextBoxSource(tbGirlLimit1));
            DataListener.Add(new TextBoxSource(tbGirlLimit2));
            DataListener.Add(new TextBoxSource(tbGirlLimit3));
            DataListener.Add(new TextBoxSource(tbGirlLimit4));
            DataListener.Add(new TextBoxSource(tbGirlLimit5));

            DataListener.StatusChanged += new EventHandler<ChangeEventArgs>(DataListener_StatusChanged);
        }

        void ClubEvents_ClubChanged(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, EventArgs>(ClubEvents_ClubChanged), sender, e);
            }
            else
            {
                Changed();
            }
        }

        //切換學生
        protected override void OnPrimaryKeyChanged(EventArgs e)
        {
            Changed();
        }

        private void Changed()
        {
            #region 更新時
            if (this.PrimaryKey != "")
            {
                this.Loading = true;

                if (BGW.IsBusy)
                {
                    BkWBool = true;
                }
                else
                {
                    BGW.RunWorkerAsync();
                }
            }
            #endregion
        }

        void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            //取得社團資料
            List<CLUBRecord> ClubPrimaryList = _AccessHelper.Select<CLUBRecord>(string.Format("UID = '{0}'", this.PrimaryKey));
            if (ClubPrimaryList.Count != 1)
            {
                //如果取得2門以上 或 沒取得社團時
                e.Cancel = true;
                return;
            }

            ClubPrimary = ClubPrimaryList[0];
            Log_ClubPrimary = ClubPrimary.CopyExtension();
        }

        void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Loading = false;

            if (e.Cancelled)
            {
                return;
            }

            if (e.Error != null)
            {
                FISCA.Presentation.Controls.MsgBox.Show("取得[选课限制]发生错误!!\n" + e.Error.Message);
                SmartSchool.ErrorReporting.ReportingService.ReportException(e.Error);
                return;
            }

            if (BkWBool) //如果有其他的更新事件
            {
                BkWBool = false;
                BGW.RunWorkerAsync();
                return;
            }

            BindData();
        }

        private void BindData()
        {

            #region 總數

            //一年級人數上限
            tbLimit1.Text = string.Empty;
            if (ClubPrimary.Grade1Limit.HasValue)
            {
                tbLimit1.Text = ClubPrimary.Grade1Limit.Value.ToString();
            }
            //二年級人數上限
            tbLimit2.Text = string.Empty;
            if (ClubPrimary.Grade2Limit.HasValue)
            {
                tbLimit2.Text = ClubPrimary.Grade2Limit.Value.ToString();
            }
            //三年級人數上限
            tbLimit3.Text = string.Empty;
            if (ClubPrimary.Grade3Limit.HasValue)
            {
                tbLimit3.Text = ClubPrimary.Grade3Limit.Value.ToString();
            }

            //四年級人數上限
            tbLimit4.Text = string.Empty;
            if (ClubPrimary.Grade4Limit.HasValue)
            {
                tbLimit4.Text = ClubPrimary.Grade4Limit.Value.ToString();
            }

            //五年級人數上限
            tbLimit5.Text = string.Empty;
            if (ClubPrimary.Grade5Limit.HasValue)
            {
                tbLimit5.Text = ClubPrimary.Grade5Limit.Value.ToString();
            }

            #endregion

            #region 男生

            //一年級男生人數上限
            tbBoyLimit1.Text = string.Empty;
            if (ClubPrimary.Grade1BoyLimit.HasValue)
            {
                tbBoyLimit1.Text = ClubPrimary.Grade1BoyLimit.Value.ToString();
            }
            //二年級男生人數上限
            tbBoyLimit2.Text = string.Empty;
            if (ClubPrimary.Grade2BoyLimit.HasValue)
            {
                tbBoyLimit2.Text = ClubPrimary.Grade2BoyLimit.Value.ToString();
            }
            //三年級男生人數上限
            tbBoyLimit3.Text = string.Empty;
            if (ClubPrimary.Grade3BoyLimit.HasValue)
            {
                tbBoyLimit3.Text = ClubPrimary.Grade3BoyLimit.Value.ToString();
            }

            //四年級男生人數上限
            tbBoyLimit4.Text = string.Empty;
            if (ClubPrimary.Grade4BoyLimit.HasValue)
            {
                tbBoyLimit4.Text = ClubPrimary.Grade4BoyLimit.Value.ToString();
            }

            //五年級男生人數上限
            tbBoyLimit5.Text = string.Empty;
            if (ClubPrimary.Grade5BoyLimit.HasValue)
            {
                tbBoyLimit5.Text = ClubPrimary.Grade5BoyLimit.Value.ToString();
            }

            #endregion

            #region 女生

            //一年級女生人數上限
            tbGirlLimit1.Text = string.Empty;
            if (ClubPrimary.Grade1GirlLimit.HasValue)
            {
                tbGirlLimit1.Text = ClubPrimary.Grade1GirlLimit.Value.ToString();
            }
            //二年級女生人數上限
            tbGirlLimit2.Text = string.Empty;
            if (ClubPrimary.Grade2GirlLimit.HasValue)
            {
                tbGirlLimit2.Text = ClubPrimary.Grade2GirlLimit.Value.ToString();
            }
            //三年級女生人數上限
            tbGirlLimit3.Text = string.Empty;
            if (ClubPrimary.Grade3GirlLimit.HasValue)
            {
                tbGirlLimit3.Text = ClubPrimary.Grade3GirlLimit.Value.ToString();
            }

            //四年級女生人數上限
            tbGirlLimit4.Text = string.Empty;
            if (ClubPrimary.Grade4GirlLimit.HasValue)
            {
                tbGirlLimit4.Text = ClubPrimary.Grade4GirlLimit.Value.ToString();
            }

            //五年級女生人數上限
            tbGirlLimit5.Text = string.Empty;
            if (ClubPrimary.Grade5GirlLimit.HasValue)
            {
                tbGirlLimit5.Text = ClubPrimary.Grade5GirlLimit.Value.ToString();
            }

            #endregion

            BkWBool = false;
            SaveButtonVisible = false;
            CancelButtonVisible = false;

            DataListener.Reset();
            DataListener.ResumeListen();
        }

        /// <summary>
        /// 當資料變更時
        /// </summary>
        void DataListener_StatusChanged(object sender, ChangeEventArgs e)
        {
            SaveButtonVisible = (e.Status == ValueStatus.Dirty);
            CancelButtonVisible = (e.Status == ValueStatus.Dirty);
        }

        /// <summary>
        /// 按下儲存時
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSaveButtonClick(EventArgs e)
        {
            if (Save_BGW.IsBusy)
            {
                MsgBox.Show("系统忙碌中...");
                return;
            }

            if (this.PrimaryKey != ClubPrimary.UID)
            {
                MsgBox.Show("资料不同步\n储存失败");
                return;
            }

            if (!CheckDataIsError())
            {
                MsgBox.Show("请修正资料再储存!!");
                return;
            }

            GetChengeObj();
            Save_BGW.RunWorkerAsync();
        }

        void Save_BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _AccessHelper.UpdateValues(new List<CLUBRecord>() { ClubPrimary });
            }
            catch (Exception ex)
            {
                MsgBox.Show("修改选课限制失败\n" + ex.Message);
                SmartSchool.ErrorReporting.ReportingService.ReportException(ex);
                return;
            }

            StringBuilder sb = LogSet();

            FISCA.LogAgent.ApplicationLog.Log("拓展性课程", "修改限制", sb.ToString());
        }

        private StringBuilder LogSet()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("已修改限制：(学年度「{0}」学期「{1}」课程「{2}」)", Log_ClubPrimary.SchoolYear.ToString(), Log_ClubPrimary.Semester.ToString(), Log_ClubPrimary.ClubName));

            string chenge;

            #region 總數

            chenge = GetString("一年级人数限制", GetIntString(Log_ClubPrimary.Grade1Limit), GetIntString(ClubPrimary.Grade1Limit));
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("二年级人数限制", GetIntString(Log_ClubPrimary.Grade2Limit), GetIntString(ClubPrimary.Grade2Limit));
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("三年级人数限制", GetIntString(Log_ClubPrimary.Grade3Limit), GetIntString(ClubPrimary.Grade3Limit));
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("四年级人数限制", GetIntString(Log_ClubPrimary.Grade4Limit), GetIntString(ClubPrimary.Grade4Limit));
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("五年级人数限制", GetIntString(Log_ClubPrimary.Grade5Limit), GetIntString(ClubPrimary.Grade5Limit));
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            #endregion

            #region 男生

            chenge = GetString("一年级男生人数限制", GetIntString(Log_ClubPrimary.Grade1BoyLimit), GetIntString(ClubPrimary.Grade1BoyLimit));
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("二年级男生人数限制", GetIntString(Log_ClubPrimary.Grade2BoyLimit), GetIntString(ClubPrimary.Grade2BoyLimit));
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("三年级男生人数限制", GetIntString(Log_ClubPrimary.Grade3BoyLimit), GetIntString(ClubPrimary.Grade3BoyLimit));
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("四年级男生人数限制", GetIntString(Log_ClubPrimary.Grade4BoyLimit), GetIntString(ClubPrimary.Grade4BoyLimit));
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("五年级男生人数限制", GetIntString(Log_ClubPrimary.Grade5BoyLimit), GetIntString(ClubPrimary.Grade5BoyLimit));
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            #endregion

            #region 女生

            chenge = GetString("一年级女生人数限制", GetIntString(Log_ClubPrimary.Grade1GirlLimit), GetIntString(ClubPrimary.Grade1GirlLimit));
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("二年级女生人数限制", GetIntString(Log_ClubPrimary.Grade2GirlLimit), GetIntString(ClubPrimary.Grade2GirlLimit));
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("三年级女生人数限制", GetIntString(Log_ClubPrimary.Grade3GirlLimit), GetIntString(ClubPrimary.Grade3GirlLimit));
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("四年级女生人数限制", GetIntString(Log_ClubPrimary.Grade4GirlLimit), GetIntString(ClubPrimary.Grade4GirlLimit));
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("五年级女生人数限制", GetIntString(Log_ClubPrimary.Grade5GirlLimit), GetIntString(ClubPrimary.Grade5GirlLimit));
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            #endregion

            return sb;
        }

        private string GetString(string a, string b, string c)
        {
            if (b != c)
                return string.Format("{0}由「{1}」修改为「{2}」", a, b, c);
            else
                return "";
        }

        private string GetIntString(int? a)
        {
            string name = a.HasValue ? a.Value.ToString() : "";
            return name;
        }

        private string GetDeptString(string a)
        {
            string b = "";
            if (a != "")
            {
                List<string> list = new List<string>();
                XmlElement xmlBase = DSXmlHelper.LoadXml(a);
                foreach (XmlElement xml in xmlBase.SelectNodes("Dept"))
                {
                    list.Add(xml.InnerText);
                }
                b = string.Join(",", list);
            }

            return b;
        }

        private void GetChengeObj()
        {
            #region 總數

            if (!string.IsNullOrEmpty(tbBoyLimit1.Text.Trim())) //一年級總數上限
                ClubPrimary.Grade1Limit = tool.StringIsInt_DefIsZero(tbLimit1.Text.Trim());
            else
                ClubPrimary.Grade1Limit = null;

            if (!string.IsNullOrEmpty(tbBoyLimit2.Text.Trim())) //二年級總數上限
                ClubPrimary.Grade2Limit = tool.StringIsInt_DefIsZero(tbLimit2.Text.Trim());
            else
                ClubPrimary.Grade2Limit = null;

            if (!string.IsNullOrEmpty(tbBoyLimit3.Text.Trim())) //三年級總數上限
                ClubPrimary.Grade3Limit = tool.StringIsInt_DefIsZero(tbLimit3.Text.Trim());
            else
                ClubPrimary.Grade3Limit = null;

            if (!string.IsNullOrEmpty(tbBoyLimit4.Text.Trim())) //四年級總數上限
                ClubPrimary.Grade4Limit = tool.StringIsInt_DefIsZero(tbLimit4.Text.Trim());
            else
                ClubPrimary.Grade4Limit = null;

            if (!string.IsNullOrEmpty(tbBoyLimit5.Text.Trim())) //五年級總數上限
                ClubPrimary.Grade5Limit = tool.StringIsInt_DefIsZero(tbLimit5.Text.Trim());
            else
                ClubPrimary.Grade5Limit = null;

            #endregion

            #region 男生

            if (!string.IsNullOrEmpty(tbBoyLimit1.Text.Trim())) //一年級人限
                ClubPrimary.Grade1BoyLimit = tool.StringIsInt_DefIsZero(tbBoyLimit1.Text.Trim());
            else
                ClubPrimary.Grade1BoyLimit = null;

            if (!string.IsNullOrEmpty(tbBoyLimit2.Text.Trim())) //二年級人限
                ClubPrimary.Grade2BoyLimit = tool.StringIsInt_DefIsZero(tbBoyLimit2.Text.Trim());
            else
                ClubPrimary.Grade2BoyLimit = null;

            if (!string.IsNullOrEmpty(tbBoyLimit3.Text.Trim())) //三年級人限
                ClubPrimary.Grade3BoyLimit = tool.StringIsInt_DefIsZero(tbBoyLimit3.Text.Trim());
            else
                ClubPrimary.Grade3BoyLimit = null;

            if (!string.IsNullOrEmpty(tbBoyLimit4.Text.Trim())) //四年級人限
                ClubPrimary.Grade4BoyLimit = tool.StringIsInt_DefIsZero(tbBoyLimit4.Text.Trim());
            else
                ClubPrimary.Grade4BoyLimit = null;

            if (!string.IsNullOrEmpty(tbBoyLimit5.Text.Trim())) //五年級人限
                ClubPrimary.Grade5BoyLimit = tool.StringIsInt_DefIsZero(tbBoyLimit5.Text.Trim());
            else
                ClubPrimary.Grade5BoyLimit = null;

            #endregion

            #region 女生

            if (!string.IsNullOrEmpty(tbGirlLimit1.Text.Trim())) //一年級人限
                ClubPrimary.Grade1GirlLimit = tool.StringIsInt_DefIsZero(tbGirlLimit1.Text.Trim());
            else
                ClubPrimary.Grade1GirlLimit = null;

            if (!string.IsNullOrEmpty(tbGirlLimit2.Text.Trim())) //二年級人限
                ClubPrimary.Grade2GirlLimit = tool.StringIsInt_DefIsZero(tbGirlLimit2.Text.Trim());
            else
                ClubPrimary.Grade2GirlLimit = null;

            if (!string.IsNullOrEmpty(tbGirlLimit3.Text.Trim())) //三年級人限
                ClubPrimary.Grade3GirlLimit = tool.StringIsInt_DefIsZero(tbGirlLimit3.Text.Trim());
            else
                ClubPrimary.Grade3GirlLimit = null;

            if (!string.IsNullOrEmpty(tbGirlLimit4.Text.Trim())) //四年級人限
                ClubPrimary.Grade4GirlLimit = tool.StringIsInt_DefIsZero(tbGirlLimit4.Text.Trim());
            else
                ClubPrimary.Grade4GirlLimit = null;

            if (!string.IsNullOrEmpty(tbGirlLimit5.Text.Trim())) //五年級人限
                ClubPrimary.Grade5GirlLimit = tool.StringIsInt_DefIsZero(tbGirlLimit5.Text.Trim());
            else
                ClubPrimary.Grade5GirlLimit = null;

            #endregion
        }

        void Save_BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                if (e.Error == null)
                {
                    ClubEvents.RaiseAssnChanged();
                }
                else
                {
                    MsgBox.Show("取得资料发生错误!!\n" + e.Error.Message);
                }
            }
            else
            {
                MsgBox.Show("作业中止!!");
            }
        }

        private bool CheckDataIsError()
        {
            bool k = true;

            //總數
            bool a = SetLimit(tbLimit1, ep_Grade1Limit, SetStringIsInt);
            bool b = SetLimit(tbLimit2, ep_Grade2Limit, SetStringIsInt);
            bool c = SetLimit(tbLimit3, ep_Grade3Limit, SetStringIsInt);
            bool d = SetLimit(tbLimit4, ep_Grade4Limit, SetStringIsInt);
            bool e = SetLimit(tbLimit5, ep_Grade5Limit, SetStringIsInt);

            //男生
            bool aa = SetLimit(tbBoyLimit1, ep_Grade1BoyLimit, SetStringIsInt);
            bool bb = SetLimit(tbBoyLimit2, ep_Grade2BoyLimit, SetStringIsInt);
            bool cc = SetLimit(tbBoyLimit3, ep_Grade3BoyLimit, SetStringIsInt);
            bool dd = SetLimit(tbBoyLimit4, ep_Grade4BoyLimit, SetStringIsInt);
            bool ee = SetLimit(tbBoyLimit5, ep_Grade5BoyLimit, SetStringIsInt);

            //女生
            bool aaa = SetLimit(tbGirlLimit1, ep_Grade1GirlLimit, SetStringIsInt);
            bool bbb = SetLimit(tbGirlLimit2, ep_Grade2GirlLimit, SetStringIsInt);
            bool ccc = SetLimit(tbGirlLimit3, ep_Grade3GirlLimit, SetStringIsInt);
            bool ddd = SetLimit(tbGirlLimit4, ep_Grade4GirlLimit, SetStringIsInt);
            bool eee = SetLimit(tbGirlLimit5, ep_Grade5GirlLimit, SetStringIsInt);
            //只要一個有錯就是錯誤狀態
            if (!(a && b && c && d && aa
                && bb && cc && dd && ee
                && aaa && bbb && ccc && ddd && eee))
            {
                k = false;
            }

            return k;
        }

        private bool SetLimit(TextBoxX x1, ErrorProvider ep, string ErrorString)
        {
            bool k = true;
            if (!string.IsNullOrEmpty(x1.Text.Trim()))
            {
                if (tool.StringIsInt_Bool(x1.Text.Trim()))
                {
                    ep.SetError(x1, null);
                    k = true;
                }
                else
                {
                    ep.SetError(x1, ErrorString);
                    k = false;
                }
            }
            else
            {
                ep.SetError(x1, null);
                k = true;
            }

            return k;
        }

        /// <summary>
        /// 取消儲存時
        /// </summary>
        protected override void OnCancelButtonClick(EventArgs e)
        {
            SaveButtonVisible = false;
            CancelButtonVisible = false;

            this.Loading = true;

            DataListener.SuspendListen(); //終止變更判斷

            //判斷是否忙碌後,開始進行資料重置
            Changed();
        }

        private void listDepartment_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            SaveButtonVisible = true;
            CancelButtonVisible = true;
        }

        private void tbGrade1Limit_TextChanged(object sender, EventArgs e)
        {
            SetLimit(tbBoyLimit1, ep_Grade1Limit, SetStringIsInt);
        }

        private void tbGrade2Limit_TextChanged(object sender, EventArgs e)
        {
            SetLimit(tbBoyLimit2, ep_Grade2Limit, SetStringIsInt);
        }

        private void tbGrade3Limit_TextChanged(object sender, EventArgs e)
        {
            SetLimit(tbBoyLimit3, ep_Grade3Limit, SetStringIsInt);
        }

        private void tbBoyLimit4_TextChanged(object sender, EventArgs e)
        {
            SetLimit(tbBoyLimit4, ep_Grade4Limit, SetStringIsInt);
        }

        private void tbBoyLimit5_TextChanged(object sender, EventArgs e)
        {
            SetLimit(tbBoyLimit5, ep_Grade5Limit, SetStringIsInt);
        }

        private void tbGirlLimit1_TextChanged(object sender, EventArgs e)
        {
            SetLimit(tbGirlLimit1, ep_Grade1GirlLimit, SetStringIsInt);
        }

        private void tbGirlLimit2_TextChanged(object sender, EventArgs e)
        {
            SetLimit(tbGirlLimit2, ep_Grade2GirlLimit, SetStringIsInt);
        }

        private void tbGirlLimit3_TextChanged(object sender, EventArgs e)
        {
            SetLimit(tbGirlLimit3, ep_Grade3GirlLimit, SetStringIsInt);
        }

        private void tbGirlLimit4_TextChanged(object sender, EventArgs e)
        {
            SetLimit(tbGirlLimit4, ep_Grade4GirlLimit, SetStringIsInt);

        }

        private void tbGirlLimit5_TextChanged(object sender, EventArgs e)
        {
            SetLimit(tbGirlLimit5, ep_Grade5GirlLimit, SetStringIsInt);
        }

        private void tbLimit1_TextChanged(object sender, EventArgs e)
        {
            SetLimit(tbLimit1, ep_Grade5GirlLimit, SetStringIsInt);
        }

        private void tbLimit2_TextChanged(object sender, EventArgs e)
        {
            SetLimit(tbLimit2, ep_Grade5GirlLimit, SetStringIsInt);
        }

        private void tbLimit3_TextChanged(object sender, EventArgs e)
        {
            SetLimit(tbLimit3, ep_Grade5GirlLimit, SetStringIsInt);
        }

        private void tbLimit4_TextChanged(object sender, EventArgs e)
        {
            SetLimit(tbLimit4, ep_Grade5GirlLimit, SetStringIsInt);
        }

        private void tbLimit5_TextChanged(object sender, EventArgs e)
        {
            SetLimit(tbLimit5, ep_Grade5GirlLimit, SetStringIsInt);
        }

    }
}
