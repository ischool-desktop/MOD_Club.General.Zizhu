﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.UDT;
using K12.Data;
using DevComponents.DotNetBar;
using FISCA.Data;
using Campus.Windows;
using DevComponents.DotNetBar.Controls;
using FISCA.Permission;

namespace K12.Club.General.Zizhu
{
    [FISCA.Permission.FeatureCode("K12.Club.Universal.ClubDetailItem.cs", "基本资料")]
    public partial class ClubDetailItem : DetailContentBase
    {
        //背景模式
        private BackgroundWorker BGW = new BackgroundWorker();
        private BackgroundWorker Save_BGW = new BackgroundWorker();

        CLUBRecord ClubPrimary;
        CLUBRecord Log_ClubPrimary;

        //UDT物件
        private AccessHelper _AccessHelper = new AccessHelper();
        private QueryHelper _QueryHelper = new QueryHelper();

        ErrorProvider ep_ClubName = new ErrorProvider();
        ErrorProvider ep_Teacher1 = new ErrorProvider();
        ErrorProvider ep_Teacher2 = new ErrorProvider();
        ErrorProvider ep_Teacher3 = new ErrorProvider();
        ErrorProvider ep_Number = new ErrorProvider();
        //ErrorProvider ep_President = new ErrorProvider();
        //ErrorProvider ep_VicePresident = new ErrorProvider();

        /// <summary>
        /// 社團學生名稱
        /// </summary>
        //Dictionary<string, string> StudentNameDic = new Dictionary<string, string>();

        //背景忙碌
        private bool BkWBool = false;

        private ChangeListener DataListener { get; set; } //資料變更事件引發器

        //List<SCJoin> ClubStudentList = new List<SCJoin>();

        //Dictionary<string, SCJoin> ClubStudentDic = new Dictionary<string, SCJoin>();

        //上課地點
        List<string> ClubLocation = new List<string>();

        List<string> ClubCategory = new List<string>();

        List<TeacherObj> TeacherList = new List<TeacherObj>();

        Dictionary<string, TeacherObj> TeacherDic = new Dictionary<string, TeacherObj>();

        Dictionary<string, TeacherObj> TeacherNameDic = new Dictionary<string, TeacherObj>();

        //權限
        internal static FeatureAce UserPermission;

        public ClubDetailItem()
        {
            InitializeComponent();

            Group = "基本资料";
            UserPermission = UserAcl.Current[FISCA.Permission.FeatureCodeAttribute.GetCode(GetType())];
            this.Enabled = UserPermission.Editable;

            DataListener = new ChangeListener();
            DataListener.Add(new TextBoxSource(txtClubName));
            DataListener.Add(new TextBoxSource(txtAbout));
            DataListener.Add(new TextBoxSource(txtDomain));
            DataListener.Add(new TextBoxSource(txtFormal));
            DataListener.Add(new TextBoxSource(txtType));
            //DataListener.Add(new TextBoxSource(tbCategory));
            DataListener.Add(new TextBoxSource(tbCLUBNumber));
            DataListener.Add(new TextBoxSource(tbTotalNumberHours)); //總課時數
            DataListener.Add(new ComboBoxSource(cbTeacher1, ComboBoxSource.ListenAttribute.Text));
            DataListener.Add(new ComboBoxSource(cbTeacher2, ComboBoxSource.ListenAttribute.Text));
            DataListener.Add(new ComboBoxSource(cbTeacher3, ComboBoxSource.ListenAttribute.Text));
            DataListener.Add(new ComboBoxSource(cbCategory, ComboBoxSource.ListenAttribute.Text));
            //DataListener.Add(new ComboBoxSource(cbPresident, ComboBoxSource.ListenAttribute.Text));
            DataListener.Add(new ComboBoxSource(cbLocation, ComboBoxSource.ListenAttribute.Text));
            DataListener.Add(new ComboBoxSource(cbFullPhase, ComboBoxSource.ListenAttribute.SelectedIndex));
            //DataListener.Add(new ComboBoxSource(cbVicePresident, ComboBoxSource.ListenAttribute.Text));
            DataListener.StatusChanged += new EventHandler<ChangeEventArgs>(DataListener_StatusChanged);

            ClubEvents.ClubChanged += new EventHandler(ClubEvents_ClubChanged);

            BGW.DoWork += new DoWorkEventHandler(BGW_DoWork);
            BGW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGW_RunWorkerCompleted);

            Save_BGW.DoWork += new DoWorkEventHandler(Save_BGW_DoWork);
            Save_BGW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Save_BGW_RunWorkerCompleted);

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

        void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            //StudentNameDic.Clear();

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

            //取得場地[GROUP BY]
            string TableName = Tn._CLUBRecordUDT;
            DataTable dt = _QueryHelper.Select("select location from " + TableName.ToLower() + " group by location ORDER by location");
            ClubLocation.Clear();
            foreach (DataRow row in dt.Rows)
            {
                string loc = "" + row[0];
                ClubLocation.Add(loc);
            }
            ClubLocation.Sort();

            //取得社團類型[Group By]
            TableName = Tn._CLUBRecordUDT;
            dt = _QueryHelper.Select("select club_category from " + TableName.ToLower() + " group by club_category ORDER by club_category");
            ClubCategory.Clear();
            foreach (DataRow row in dt.Rows)
            {
                string loc = "" + row[0];
                if (string.IsNullOrEmpty(loc))
                    continue;
                ClubCategory.Add(loc);
            }
            ClubCategory.Sort();

            //取得老師資料
            TeacherList.Clear();
            TeacherDic.Clear();
            TeacherNameDic.Clear();
            dt = _QueryHelper.Select("select teacher.id,teacher.teacher_name,teacher.nickname from teacher ORDER by teacher_name");
            foreach (DataRow row in dt.Rows)
            {
                TeacherObj obj = new TeacherObj();
                obj.TeacherID = "" + row[0];
                obj.TeacherName = ("" + row[1]).Trim();
                obj.TeacherNickName = ("" + row[2]).Trim();
                TeacherList.Add(obj);

                if (!TeacherDic.ContainsKey(obj.TeacherID))
                {
                    TeacherDic.Add(obj.TeacherID, obj);
                }

                if (!TeacherNameDic.ContainsKey(obj.TeacherFullName))
                {
                    TeacherNameDic.Add(obj.TeacherFullName, obj);
                }
            }

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
                FISCA.Presentation.Controls.MsgBox.Show("取得[基本资料]发生错误!!\n" + e.Error.Message);
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
            ep_ClubName.SetError(txtClubName, null);
            ep_Teacher1.SetError(cbTeacher1, null);
            ep_Teacher2.SetError(cbTeacher2, null);
            ep_Teacher3.SetError(cbTeacher3, null);
            ep_Number.SetError(tbTotalNumberHours, null);
            //ep_President.SetError(cbPresident, null);
            //ep_VicePresident.SetError(cbVicePresident, null);

            //總課時數
            tbTotalNumberHours.Text = ClubPrimary.TotalNumberHours.HasValue ? ClubPrimary.TotalNumberHours.Value.ToString() : "";

            tbCLUBNumber.Text = ClubPrimary.ClubNumber;
            txtClubName.Text = ClubPrimary.ClubName;
            lbSchoolYear.Text = ClubPrimary.SchoolYear + "学年度　第" + ClubPrimary.Semester + "学期";
            txtAbout.Text = ClubPrimary.About;
            txtDomain.Text = ClubPrimary.Domain;
            txtFormal.Text = ClubPrimary.Formal;
            txtType.Text = ClubPrimary.Type;
            //tbCategory.Text = ClubPrimary.ClubCategory;

            #region 社團老師

            cbTeacher1.Items.Clear();
            cbTeacher1.Text = "";
            cbTeacher1.DisplayMember = "TeacherFullName";
            cbTeacher1.Items.AddRange(TeacherList.ToArray());

            foreach (TeacherObj each in TeacherList)
            {
                if (each.TeacherID == ClubPrimary.RefTeacherID)
                {
                    //理論上只會被執行一次
                    cbTeacher1.Text = each.TeacherFullName;
                }
            }

            cbTeacher2.Items.Clear();
            cbTeacher2.Text = "";
            cbTeacher2.DisplayMember = "TeacherFullName";
            cbTeacher2.Items.AddRange(TeacherList.ToArray());

            foreach (TeacherObj each in TeacherList)
            {
                if (each.TeacherID == ClubPrimary.RefTeacherID2)
                {
                    //理論上只會被執行一次
                    cbTeacher2.Text = each.TeacherFullName;
                }
            }

            cbTeacher3.Items.Clear();
            cbTeacher3.Text = "";
            cbTeacher3.DisplayMember = "TeacherFullName";
            cbTeacher3.Items.AddRange(TeacherList.ToArray());

            foreach (TeacherObj each in TeacherList)
            {
                if (each.TeacherID == ClubPrimary.RefTeacherID3)
                {
                    //理論上只會被執行一次
                    cbTeacher3.Text = each.TeacherFullName;
                }
            }

            #endregion

            #region 場地資料

            cbLocation.Items.Clear();
            cbLocation.Text = "";
            cbLocation.Items.AddRange(ClubLocation.ToArray());
            cbLocation.Text = ClubPrimary.Location;

            #endregion

            #region 類型資料

            cbCategory.Items.Clear();
            cbCategory.Text = "";
            cbCategory.Items.AddRange(ClubCategory.ToArray());
            cbCategory.Text = ClubPrimary.ClubCategory;

            #endregion

            cbFullPhase.SelectedIndex = ClubPrimary.FullPhase == true ? 1 : 0;

            BkWBool = false;
            SaveButtonVisible = false;
            CancelButtonVisible = false;

            DataListener.Reset();
            DataListener.ResumeListen();
        }

        void Changed()
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

            if (!CheckData())
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
                MsgBox.Show("资料储存失败!\n" + ex.Message);
                SmartSchool.ErrorReporting.ReportingService.ReportException(ex);
                return;
            }

            StringBuilder sb = LogSet();

            FISCA.LogAgent.ApplicationLog.Log("课程", "修改基本资料", sb.ToString());
        }

        private StringBuilder LogSet()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("已修改基本资料：(学年度「{0}」学期「{1}」课程「{2}」)", Log_ClubPrimary.SchoolYear.ToString(), Log_ClubPrimary.Semester.ToString(), Log_ClubPrimary.ClubName));

            string chenge = GetString("名称", Log_ClubPrimary.ClubName, ClubPrimary.ClubName);
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("代码", Log_ClubPrimary.ClubNumber, ClubPrimary.ClubNumber);
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("场地", Log_ClubPrimary.Location, ClubPrimary.Location);
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("类型", Log_ClubPrimary.ClubCategory, ClubPrimary.ClubCategory);
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("课程领域", Log_ClubPrimary.Domain, ClubPrimary.Domain);
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("课程属性", Log_ClubPrimary.Type, ClubPrimary.Type);
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("长短课程", (Log_ClubPrimary.FullPhase == true ? "长课程" : "短课程"), (ClubPrimary.FullPhase == true ? "长课程" : "短课程"));
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("上课形式", Log_ClubPrimary.Formal, ClubPrimary.Formal);
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("总课时数",
                Log_ClubPrimary.TotalNumberHours.HasValue ? Log_ClubPrimary.TotalNumberHours.Value.ToString() : ""
                , ClubPrimary.TotalNumberHours.HasValue ? ClubPrimary.TotalNumberHours.Value.ToString() : "");
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetString("简介", Log_ClubPrimary.About, ClubPrimary.About);
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetStringTeacher("教师1", Log_ClubPrimary.RefTeacherID, ClubPrimary.RefTeacherID);
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetStringTeacher("教师2", Log_ClubPrimary.RefTeacherID2, ClubPrimary.RefTeacherID2);
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            chenge = GetStringTeacher("教师3", Log_ClubPrimary.RefTeacherID3, ClubPrimary.RefTeacherID3);
            if (!string.IsNullOrEmpty(chenge))
                sb.AppendLine(chenge);

            return sb;
        }

        private string GetString(string a, string b, string c)
        {
            if (b != c)
                return string.Format("{0}由「{1}」修改为「{2}」", a, b, c);
            else
                return "";
        }

        private string GetStringTeacher(string a, string 修改前ID, string 修改後ID)
        {
            if (修改前ID != 修改後ID)
            {
                string 修改前老師名稱 = "";
                if (TeacherDic.ContainsKey(修改前ID))
                {
                    修改前老師名稱 = TeacherDic[修改前ID].TeacherFullName;
                }

                string 修改後老師名稱 = "";
                if (TeacherDic.ContainsKey(修改後ID))
                {
                    修改後老師名稱 = TeacherDic[修改後ID].TeacherFullName;
                }
                return string.Format("{0}由「{1}」修改为「{2}」", a, 修改前老師名稱, 修改後老師名稱);
            }
            else
            {
                return "";
            }
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

        /// <summary>
        /// 將使用者修改內容填入物件
        /// </summary>
        private void GetChengeObj()
        {
            ClubPrimary.ClubName = txtClubName.Text.Trim();
            //社團老師
            if (cbTeacher1.SelectedItem != null)
            {
                TeacherObj teach = (TeacherObj)cbTeacher1.SelectedItem;
                ClubPrimary.RefTeacherID = teach.TeacherID;
            }
            else
            {
                ClubPrimary.RefTeacherID = "";
            }
            //社團老師2
            if (cbTeacher2.SelectedItem != null)
            {
                TeacherObj teach = (TeacherObj)cbTeacher2.SelectedItem;
                ClubPrimary.RefTeacherID2 = teach.TeacherID;
            }
            else
            {
                ClubPrimary.RefTeacherID2 = "";
            }
            //社團老師3
            if (cbTeacher3.SelectedItem != null)
            {
                TeacherObj teach = (TeacherObj)cbTeacher3.SelectedItem;
                ClubPrimary.RefTeacherID3 = teach.TeacherID;
            }
            else
            {
                ClubPrimary.RefTeacherID3 = "";
            }
            //場地
            ClubPrimary.Location = cbLocation.Text.Trim();
            //關於
            ClubPrimary.About = txtAbout.Text;
            //類型
            ClubPrimary.ClubCategory = cbCategory.Text;

            //社團編號(8/7)
            ClubPrimary.ClubNumber = tbCLUBNumber.Text;

            //总课时数
            int x;
            if (int.TryParse(tbTotalNumberHours.Text, out x))
                ClubPrimary.TotalNumberHours = x;
            else
                ClubPrimary.TotalNumberHours = null;
            //長短課程
            ClubPrimary.FullPhase = cbFullPhase.SelectedIndex == 1 ? true : false;
            //領域
            ClubPrimary.Domain = txtDomain.Text;
            //領域
            ClubPrimary.Formal = txtFormal.Text;
            //領域
            ClubPrimary.Type = txtType.Text;
        }

        private bool CheckData()
        {
            bool a = true;

            #region 社團名稱
            if (string.IsNullOrEmpty(txtClubName.Text.Trim()))
            {
                ep_ClubName.SetError(txtClubName, "课程必须有名称");
                a = false;
            }
            else
            {
                if (ClubPrimary.ClubName != txtClubName.Text)
                {
                    //社團名稱+學年度+學期不可重覆
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select club_name from " + Tn._CLUBRecordUDT.ToLower() + " ");
                    sb.Append("where club_name = '" + txtClubName.Text + "' ");
                    sb.Append("and school_year = '" + ClubPrimary.SchoolYear.ToString() + "' ");
                    sb.Append("and semester = '" + ClubPrimary.Semester.ToString() + "'");

                    DataTable dt = _QueryHelper.Select(sb.ToString());


                    if (dt.Rows.Count > 0)
                    {
                        ep_ClubName.SetError(txtClubName, "课程名称重覆");
                        a = false;
                    }
                    else
                    {
                        ep_ClubName.SetError(txtClubName, null);
                    }
                }
                else
                {
                    ep_ClubName.SetError(txtClubName, null);
                }
            }


            #endregion

            //社團老師
            bool b = tool.ComboBoxValueInItemList(cbTeacher1);
            if (!SetComboBoxError(b, cbTeacher1, ep_Teacher1, "老师必须存在于下拉清单中!!"))
                a = false;

            bool c = tool.ComboBoxValueInItemList(cbTeacher2);
            if (!SetComboBoxError(c, cbTeacher2, ep_Teacher2, "老师必须存在于下拉清单中!!"))
                a = false;

            bool d = tool.ComboBoxValueInItemList(cbTeacher3);
            if (!SetComboBoxError(d, cbTeacher3, ep_Teacher3, "老师必须存在于下拉清单中!!"))
                a = false;

            int e;
            if (!string.IsNullOrEmpty(tbTotalNumberHours.Text) && !int.TryParse(tbTotalNumberHours.Text, out e))
            {
                ep_Number.SetError(tbTotalNumberHours, "总课时数必须是数字!!");
                a = false;
            }
            else
            {
                ep_Number.SetError(tbTotalNumberHours, "");
            }

            return a;
        }

        private bool SetComboBoxError(bool d, ComboBoxEx ex, ErrorProvider ep, string message)
        {
            bool k = true;
            if (!d)
            {
                ep.SetError(ex, message);
                k = false;
            }
            else
            {
                ep.SetError(ex, null);
            }

            return k;
        }

        /// <summary>
        /// 取消儲存時
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCancelButtonClick(EventArgs e)
        {
            SaveButtonVisible = false;
            CancelButtonVisible = false;

            this.Loading = true;

            DataListener.SuspendListen(); //終止變更判斷

            //判斷是否忙碌後,開始進行資料重置
            Changed();
        }

        private void cbTeacher1_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbTeacher1.Text))
            {
                if (TeacherNameDic.ContainsKey(cbTeacher1.Text))
                {
                    cbTeacher1.SelectedItem = TeacherNameDic[cbTeacher1.Text];
                }
            }
        }

        private void cbTeacher2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbTeacher2.Text))
            {
                if (TeacherNameDic.ContainsKey(cbTeacher2.Text))
                {
                    cbTeacher2.SelectedItem = TeacherNameDic[cbTeacher2.Text];
                }
            }
        }

        private void cbTeacher3_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cbTeacher3.Text))
            {
                if (TeacherNameDic.ContainsKey(cbTeacher3.Text))
                {
                    cbTeacher3.SelectedItem = TeacherNameDic[cbTeacher3.Text];
                }
            }
        }
    }
}
