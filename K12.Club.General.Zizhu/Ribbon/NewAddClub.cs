using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.UDT;
using K12.Data;
using DevComponents.DotNetBar;
using System.Xml;
using FISCA.DSAUtil;
using DevComponents.DotNetBar.Controls;
using FISCA.Data;

namespace K12.Club.General.Zizhu
{
    public partial class NewAddClub : BaseForm
    {
        BackgroundWorker BGW = new BackgroundWorker();

        List<TeacherObj> TeacherList = new List<TeacherObj>();

        private QueryHelper _QueryHelper = new QueryHelper();

        List<string> deptList;

        string SetStringIsInt = "必须输入数字!!";

        bool IsChangeNow = false;

        List<string> ClubLocation = new List<string>();

        List<string> ClubCategory = new List<string>();

        ErrorProvider ep_ClubName = new ErrorProvider();
        ErrorProvider ep_Grade1Limit = new ErrorProvider();
        ErrorProvider ep_Grade2Limit = new ErrorProvider();
        ErrorProvider ep_Grade3Limit = new ErrorProvider();
        ErrorProvider ep_Grade4Limit = new ErrorProvider();
        ErrorProvider ep_Grade5Limit = new ErrorProvider();

        Campus.Windows.ChangeListener _ChangeListener = new Campus.Windows.ChangeListener();

        public NewAddClub()
        {
            InitializeComponent();
            BGW.DoWork += new DoWorkEventHandler(BGW_DoWork);
            BGW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGW_RunWorkerCompleted);

            _ChangeListener.StatusChanged += new EventHandler<Campus.Windows.ChangeEventArgs>(_ChangeListener_StatusChanged);
            _ChangeListener.Add(new Campus.Windows.TextBoxSource(txtClubName));
            //_ChangeListener.Add(new Campus.Windows.TextBoxSource(txtCategory));
            _ChangeListener.Add(new Campus.Windows.TextBoxSource(tbClubNumber)); //社團編號
            _ChangeListener.Add(new Campus.Windows.TextBoxSource(tbAboutClub));
            _ChangeListener.Add(new Campus.Windows.TextBoxSource(tbTotalNumberHours)); //总课时数

            _ChangeListener.Add(new Campus.Windows.TextBoxSource(tbLimit1));
            _ChangeListener.Add(new Campus.Windows.TextBoxSource(tbLimit2));
            _ChangeListener.Add(new Campus.Windows.TextBoxSource(tbLimit3));
            _ChangeListener.Add(new Campus.Windows.TextBoxSource(tbLimit4));
            _ChangeListener.Add(new Campus.Windows.TextBoxSource(tbLimit5));

            _ChangeListener.Add(new Campus.Windows.TextBoxSource(tbBoyLimit1));
            _ChangeListener.Add(new Campus.Windows.TextBoxSource(tbBoyLimit2));
            _ChangeListener.Add(new Campus.Windows.TextBoxSource(tbBoyLimit3));
            _ChangeListener.Add(new Campus.Windows.TextBoxSource(tbBoyLimit4));
            _ChangeListener.Add(new Campus.Windows.TextBoxSource(tbBoyLimit5));

            _ChangeListener.Add(new Campus.Windows.TextBoxSource(tbGirlLimit1));
            _ChangeListener.Add(new Campus.Windows.TextBoxSource(tbGirlLimit2));
            _ChangeListener.Add(new Campus.Windows.TextBoxSource(tbGirlLimit3));
            _ChangeListener.Add(new Campus.Windows.TextBoxSource(tbGirlLimit4));
            _ChangeListener.Add(new Campus.Windows.TextBoxSource(tbGirlLimit5));

            _ChangeListener.Add(new Campus.Windows.ComboBoxSource(cbTeacher, Campus.Windows.ComboBoxSource.ListenAttribute.SelectedIndex));

            //社團類型
            _ChangeListener.Add(new Campus.Windows.ComboBoxSource(cbCategory, Campus.Windows.ComboBoxSource.ListenAttribute.Text));
            _ChangeListener.Add(new Campus.Windows.ComboBoxSource(cbLocation, Campus.Windows.ComboBoxSource.ListenAttribute.Text));

        }

        void _ChangeListener_StatusChanged(object sender, Campus.Windows.ChangeEventArgs e)
        {
            IsChangeNow = (e.Status == Campus.Windows.ValueStatus.Dirty);
        }

        int _DefaultSchoolYear;
        int _DefaultSemester;

        private void NewAddClub_Load(object sender, EventArgs e)
        {
            _ChangeListener.SuspendListen();

            this.Text = "新增社團(資料讀取中...)";
            SetFrom = false;

            BGW.RunWorkerAsync();
        }

        private bool SetFrom
        {
            set
            {
                intSchoolYear.Enabled = value;
                intSemester.Enabled = value;
                txtClubName.Enabled = value;
                tbClubNumber.Enabled = value;
                cbLocation.Enabled = value;
                cbCategory.Enabled = value;
                cbTeacher.Enabled = value;
                cbTeacher2.Enabled = value;
                cbTeacher3.Enabled = value;
                tbAboutClub.Enabled = value;
                tbTotalNumberHours.Enabled = value;

                tbLimit1.Enabled = value;
                tbLimit2.Enabled = value;
                tbLimit3.Enabled = value;
                tbLimit4.Enabled = value;
                tbLimit5.Enabled = value;

                tbBoyLimit1.Enabled = value;
                tbBoyLimit2.Enabled = value;
                tbBoyLimit3.Enabled = value;
                tbBoyLimit4.Enabled = value;
                tbBoyLimit5.Enabled = value;

                tbGirlLimit1.Enabled = value;
                tbGirlLimit2.Enabled = value;
                tbGirlLimit3.Enabled = value;
                tbGirlLimit4.Enabled = value;
                tbGirlLimit5.Enabled = value;

                btnSave.Enabled = value;
            }
        }

        void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            _DefaultSchoolYear = tool.StringIsInt_DefIsZero(K12.Data.School.DefaultSchoolYear);
            _DefaultSemester = tool.StringIsInt_DefIsZero(K12.Data.School.DefaultSemester);

            //取得老師資料
            TeacherList.Clear();
            DataTable dt = _QueryHelper.Select("select teacher.id,teacher.teacher_name,teacher.nickname from teacher ORDER by teacher_name");
            foreach (DataRow row in dt.Rows)
            {
                TeacherObj obj = new TeacherObj();
                obj.TeacherID = "" + row[0];
                obj.TeacherName = "" + row[1];
                obj.TeacherNickName = "" + row[2];
                TeacherList.Add(obj);
            }

            //取得場地[GROUP BY]
            string TableName = Tn._CLUBRecordUDT;
            dt = _QueryHelper.Select("select location from " + TableName.ToLower() + " group by location ORDER by location");
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

            //取得科別資料
            deptList = tool.GetQueryDeptList();

        }

        void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetFrom = true;
            this.Text = "新增";

            if (e.Cancelled)
            {
                MsgBox.Show("资料取得中止");
                return;
            }

            if (e.Error != null)
            {
                MsgBox.Show("部份资料取得发生错误!!");
                return;
            }

            intSchoolYear.Value = _DefaultSchoolYear;
            intSemester.Value = _DefaultSemester;

            #region 社團老師

            cbTeacher.Items.Clear();
            cbTeacher.Text = "";
            cbTeacher.DisplayMember = "TeacherFullName";
            cbTeacher.Items.AddRange(TeacherList.ToArray());

            cbTeacher2.Items.Clear();
            cbTeacher2.Text = "";
            cbTeacher2.DisplayMember = "TeacherFullName";
            cbTeacher2.Items.AddRange(TeacherList.ToArray());

            cbTeacher3.Items.Clear();
            cbTeacher3.Text = "";
            cbTeacher3.DisplayMember = "TeacherFullName";
            cbTeacher3.Items.AddRange(TeacherList.ToArray());

            #endregion

            #region 場地資料

            cbLocation.Items.Clear();
            cbLocation.Items.AddRange(ClubLocation.ToArray());

            #endregion

            #region 社團類型
            cbCategory.Items.Clear();
            cbCategory.Items.AddRange(ClubCategory.ToArray());

            #endregion

            _ChangeListener.Reset();
            _ChangeListener.ResumeListen();
            IsChangeNow = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //資料是否輸入檢查
            if (!CheckDataIsError())
            {
                MsgBox.Show("请输入必填栏位!!");
                return;
            }

            if (!CheckClubName())
            {
                MsgBox.Show("名称重覆!!");
                return;
            }

            CLUBRecord club = GetClub();

            BackgroundWorker BGW_Save = new BackgroundWorker();
            BGW_Save.DoWork += new DoWorkEventHandler(BGW_Save_DoWork);
            BGW_Save.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGW_Save_RunWorkerCompleted);

            this.Text = "储存中";
            SetFrom = false;

            //開始儲存資料
            BGW_Save.RunWorkerAsync(new List<CLUBRecord>() { club });

        }

        private CLUBRecord GetClub()
        {
            CLUBRecord club = new CLUBRecord();
            club.ClubName = txtClubName.Text.Trim(); //社團名稱
            club.SchoolYear = intSchoolYear.Value; //學年度
            club.Semester = intSemester.Value; //學期
            club.ClubCategory = cbCategory.Text.Trim(); //類型
            club.ClubNumber = tbClubNumber.Text.Trim(); //代碼

            int x;
            if (int.TryParse(tbTotalNumberHours.Text, out x))
                club.TotalNumberHours = x; //总课时数
            else
                club.TotalNumberHours = null;

            #region 總人數

            if (!string.IsNullOrEmpty(tbLimit1.Text.Trim()))
                club.Grade1Limit = tool.StringIsInt_DefIsZero(tbLimit1.Text.Trim());

            if (!string.IsNullOrEmpty(tbLimit2.Text.Trim()))
                club.Grade2Limit = tool.StringIsInt_DefIsZero(tbLimit2.Text.Trim());

            if (!string.IsNullOrEmpty(tbLimit3.Text.Trim()))
                club.Grade3Limit = tool.StringIsInt_DefIsZero(tbLimit3.Text.Trim());

            if (!string.IsNullOrEmpty(tbLimit4.Text.Trim()))
                club.Grade4Limit = tool.StringIsInt_DefIsZero(tbLimit4.Text.Trim());

            if (!string.IsNullOrEmpty(tbLimit5.Text.Trim()))
                club.Grade5Limit = tool.StringIsInt_DefIsZero(tbLimit5.Text.Trim());

            #endregion

            #region 男生

            if (!string.IsNullOrEmpty(tbBoyLimit1.Text.Trim()))
                club.Grade1BoyLimit = tool.StringIsInt_DefIsZero(tbBoyLimit1.Text.Trim());

            if (!string.IsNullOrEmpty(tbBoyLimit2.Text.Trim()))
                club.Grade2BoyLimit = tool.StringIsInt_DefIsZero(tbBoyLimit2.Text.Trim());

            if (!string.IsNullOrEmpty(tbBoyLimit3.Text.Trim()))
                club.Grade3BoyLimit = tool.StringIsInt_DefIsZero(tbBoyLimit3.Text.Trim());

            if (!string.IsNullOrEmpty(tbBoyLimit4.Text.Trim()))
                club.Grade4BoyLimit = tool.StringIsInt_DefIsZero(tbBoyLimit4.Text.Trim());

            if (!string.IsNullOrEmpty(tbBoyLimit5.Text.Trim()))
                club.Grade5BoyLimit = tool.StringIsInt_DefIsZero(tbBoyLimit5.Text.Trim());

            #endregion

            #region 女生

            if (!string.IsNullOrEmpty(tbGirlLimit1.Text.Trim()))
                club.Grade1GirlLimit = tool.StringIsInt_DefIsZero(tbGirlLimit1.Text.Trim());

            if (!string.IsNullOrEmpty(tbGirlLimit2.Text.Trim()))
                club.Grade2GirlLimit = tool.StringIsInt_DefIsZero(tbGirlLimit2.Text.Trim());

            if (!string.IsNullOrEmpty(tbGirlLimit3.Text.Trim()))
                club.Grade3GirlLimit = tool.StringIsInt_DefIsZero(tbGirlLimit3.Text.Trim());

            if (!string.IsNullOrEmpty(tbGirlLimit4.Text.Trim()))
                club.Grade4GirlLimit = tool.StringIsInt_DefIsZero(tbGirlLimit4.Text.Trim());

            if (!string.IsNullOrEmpty(tbGirlLimit5.Text.Trim()))
                club.Grade5GirlLimit = tool.StringIsInt_DefIsZero(tbGirlLimit5.Text.Trim());

            #endregion


            //社團老師
            if (cbTeacher.SelectedItem != null)
            {
                TeacherObj cbi = (TeacherObj)cbTeacher.SelectedItem;
                club.RefTeacherID = cbi.TeacherID;
            }

            if (cbTeacher2.SelectedItem != null)
            {
                TeacherObj cbi = (TeacherObj)cbTeacher2.SelectedItem;
                club.RefTeacherID2 = cbi.TeacherID;
            }

            if (cbTeacher3.SelectedItem != null)
            {
                TeacherObj cbi = (TeacherObj)cbTeacher3.SelectedItem;
                club.RefTeacherID3 = cbi.TeacherID;
            }

            //社團場地
            if (!string.IsNullOrEmpty(cbLocation.Text.Trim()))
                club.Location = cbLocation.Text.Trim();

            club.About = tbAboutClub.Text.Trim(); //簡介

            return club;
        }

        void BGW_Save_DoWork(object sender, DoWorkEventArgs e)
        {
            List<CLUBRecord> list = (List<CLUBRecord>)e.Argument;
            AccessHelper _accessHelper = new AccessHelper();
            try
            {
                _accessHelper.InsertValues(list);
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("已新增一笔拓展性课程记录：");
            sb.AppendLine(string.Format("學年度「{0}」學期「{1}」", list[0].SchoolYear.ToString(), list[0].Semester));
            sb.AppendLine(string.Format("名稱「{0}」", list[0].ClubName));
            FISCA.LogAgent.ApplicationLog.Log("拓展性课程", "新增", sb.ToString());
        }

        void BGW_Save_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Text = "新增拓展性课程";
            SetFrom = false;

            if (e.Cancelled)
            {
                MsgBox.Show("新增拓展性课程已中止!\n" + e.Error.Message);
                SmartSchool.ErrorReporting.ReportingService.ReportException(e.Error);
                return;
            }

            if (e.Error != null)
            {
                MsgBox.Show("新增拓展性课程失败\n" + e.Error.Message);
                SmartSchool.ErrorReporting.ReportingService.ReportException(e.Error);
                return;
            }

            MsgBox.Show("新增拓展性课程成功！");
            ClubEvents.RaiseAssnChanged();
            IsChangeNow = false;
            this.Close();
        }

        private bool CheckDataIsError()
        {
            bool k = true;

            //社團名稱
            if (string.IsNullOrEmpty(txtClubName.Text))
            {
                k = false;
            }

            int x;
            if (!string.IsNullOrEmpty(tbTotalNumberHours.Text) && !int.TryParse(tbTotalNumberHours.Text, out x))
                k = false;

            //選填欄位是否為正確之資料內容
            bool a = SetLimit(tbLimit1, ep_Grade1Limit, SetStringIsInt);
            bool b = SetLimit(tbLimit2, ep_Grade2Limit, SetStringIsInt);
            bool c = SetLimit(tbLimit3, ep_Grade3Limit, SetStringIsInt);
            bool d = SetLimit(tbLimit4, ep_Grade4Limit, SetStringIsInt);
            bool e = SetLimit(tbLimit5, ep_Grade5Limit, SetStringIsInt);

            bool aa = SetLimit(tbBoyLimit1, ep_Grade1Limit, SetStringIsInt);
            bool bb = SetLimit(tbBoyLimit2, ep_Grade2Limit, SetStringIsInt);
            bool cc = SetLimit(tbBoyLimit3, ep_Grade3Limit, SetStringIsInt);
            bool dd = SetLimit(tbBoyLimit4, ep_Grade4Limit, SetStringIsInt);
            bool ee = SetLimit(tbBoyLimit5, ep_Grade5Limit, SetStringIsInt);

            bool aaa = SetLimit(tbGirlLimit1, ep_Grade1Limit, SetStringIsInt);
            bool bbb = SetLimit(tbGirlLimit2, ep_Grade2Limit, SetStringIsInt);
            bool ccc = SetLimit(tbGirlLimit3, ep_Grade3Limit, SetStringIsInt);
            bool ddd = SetLimit(tbGirlLimit4, ep_Grade4Limit, SetStringIsInt);
            bool eee = SetLimit(tbGirlLimit5, ep_Grade5Limit, SetStringIsInt);



            if (!(a && b && c && d && e &&
                aa && bb && cc && dd && ee &&
                aaa && bbb && ccc && ddd & eee))
                k = false;

            return k;
        }

        private bool CheckClubName()
        {
            //社團名稱+學年度+學期不可重覆
            StringBuilder sb = new StringBuilder();
            sb.Append("select club_name from " + Tn._CLUBRecordUDT.ToLower() + " ");
            sb.Append("where club_name = '" + txtClubName.Text + "' ");
            sb.Append("and school_year = '" + intSchoolYear.Value.ToString() + "' ");
            sb.Append("and semester = '" + intSemester.Value.ToString() + "'");

            DataTable dt = _QueryHelper.Select(sb.ToString());


            if (dt.Rows.Count > 0)
            {
                ep_ClubName.SetError(txtClubName, "课程名称+学年度+学期不可重覆");
                return false;
            }
            else
            {
                ep_ClubName.SetError(txtClubName, null);
                return true;
            }
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void intSchoolYear_ValueChanged(object sender, EventArgs e)
        {
            IsChangeNow = true;
        }

        private void intSemester_ValueChanged(object sender, EventArgs e)
        {
            IsChangeNow = true;
        }

        private void listDepartment_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            IsChangeNow = true;
        }

        private void NewAddClub_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsChangeNow)
            {
                DialogResult dr = FISCA.Presentation.Controls.MsgBox.Show("确认放弃?", "尚未储存资料", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
