using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using K12.Data;
using DevComponents.DotNetBar;
using FISCA.Data;
using FISCA.UDT;
using FISCA.LogAgent;
using FISCA.Permission;
using FISCA.Presentation.Controls;

namespace K12.Club.General.Zizhu
{
    //社團的學生增減
    //必須有另一個背景模式進行處理
    [FISCA.Permission.FeatureCode("K12.Club.Universal.ClubStudent.cs", "第三阶段参与学生")]
    public partial class ClubStudent_3 : DetailContentBase
    {
        private BackgroundWorker BGW = new BackgroundWorker();
        private bool BkWBool = false;

        Dictionary<string, StudentRecord> studentDic = new Dictionary<string, StudentRecord>();


        Dictionary<string, ClassRecord> ClassDic = new Dictionary<string, ClassRecord>();

        //權限
        internal static FeatureAce UserPermission;

        //CLUBRecord
        CLUBRecord _CLUBRecord = new CLUBRecord();

        ScJoinMag scMAG;

        ClubStudObj cso { get; set; }

        public ClubStudent_3()
        {
            InitializeComponent();

            Group = "第三阶段参与学生";

            UserPermission = UserAcl.Current[FISCA.Permission.FeatureCodeAttribute.GetCode(GetType())];
            this.Enabled = UserPermission.Editable;

            //K12 - 當待處理學生更新後
            K12.Presentation.NLDPanels.Student.TempSourceChanged += new EventHandler(Student_TempSourceChanged);

            //背景模式取得學生
            BGW.DoWork += new DoWorkEventHandler(BGW_DoWork);
            BGW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGW_RunWorkerCompleted);

            ClubEvents.ClubChanged += new EventHandler(ClubEvents_ClubChanged);

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

        void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            List<CLUBRecord> ClubPrimaryList = tool._A.Select<CLUBRecord>(string.Format("UID = '{0}'", this.PrimaryKey));
            if (ClubPrimaryList.Count != 1)
            {
                //如果取得2門以上 或 沒取得社團時
                e.Cancel = true;
                return;
            }
            _CLUBRecord = ClubPrimaryList[0];

            //本社團的學生參與記錄
            scMAG = new ScJoinMag(this.PrimaryKey, 3);

            ClassDic = tool.GetClassDic(scMAG.SCJoinStudent_LIst);
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
                this.Loading = false;
                FISCA.Presentation.Controls.MsgBox.Show("取得[学生资料]发生错误!!\n" + e.Error.Message);
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

        /// <summary>
        /// 建置畫面內容
        /// </summary>
        private void BindData()
        {
            if (_CLUBRecord == null)
                return;

            #region 更新畫面資料

            listViewEx1.Items.Clear();
            foreach (StudentRecord each in scMAG.SCJoinStudent_LIst)
            {
                listViewEx1.Items.Add(SetListView(each));
            }

            //待處理學生與下拉式按鈕
            CreateStudentMenuItem();

            lbCourseCount.Text = "有效/所有状态：" + scMAG.StudentDic.Keys.Count + "/" + scMAG.SCJoinStudent_LIst.Count.ToString();

            btnInserStudent.Text = "由待处理加入(" + K12.Presentation.NLDPanels.Student.TempSource.Count.ToString() + ")";

            #endregion
        }

        /// <summary>
        /// 傳入學生後
        /// 設定ListView內容
        /// </summary>
        private ListViewItem SetListView(StudentRecord STUD)
        {
            #region 依學生建立ListView
            string ClassName = "";
            string Gen = "";

            //班級
            if (!string.IsNullOrEmpty(STUD.RefClassID))
            {
                if (ClassDic.ContainsKey(STUD.RefClassID))
                {
                    ClassRecord cr = ClassDic[STUD.RefClassID];
                    ClassName = cr.Name;
                    Gen = cr.GradeYear.HasValue ? cr.GradeYear.Value.ToString() : "";
                }
            }

            if (scMAG.SCJoin_Lock.Contains(STUD.ID))
            {
                //学号
                ListViewItem item = new ListViewItem(Gen);
                item.SubItems.Add(ClassName);
                item.SubItems.Add(STUD.SeatNo.HasValue ? STUD.SeatNo.Value.ToString() : "");
                item.SubItems.Add(STUD.Name);
                item.SubItems.Add(STUD.Gender);
                item.SubItems.Add(STUD.StudentNumber);
                ListViewItem.ListViewSubItem subItem = item.SubItems.Add(STUD.StatusStr);
                item.SubItems.Add("是");

                if (scMAG.SCJoin_Lock.Contains(STUD.ID))
                {
                    foreach (ListViewItem.ListViewSubItem each in item.SubItems)
                    {
                        each.BackColor = Color.GreenYellow;
                    }
                }

                //TAG應該儲存修課記錄
                if (scMAG.SCJoin_Dic.ContainsKey(STUD.ID))
                {
                    item.Tag = scMAG.SCJoin_Dic[STUD.ID][0];

                    if (scMAG.SCJoin_Dic[STUD.ID].Count > 1)
                    {
                        foreach (ListViewItem.ListViewSubItem each in item.SubItems)
                        {
                            each.BackColor = Color.Red;
                        }
                        subItem.Text = "错误,重复的记录";
                    }
                }

                return item;
            }
            else
            {
                //学号
                ListViewItem item = new ListViewItem(Gen);
                item.SubItems.Add(ClassName);
                item.SubItems.Add(STUD.SeatNo.HasValue ? STUD.SeatNo.Value.ToString() : "");
                item.SubItems.Add(STUD.Name);
                item.SubItems.Add(STUD.Gender);
                item.SubItems.Add(STUD.StudentNumber);
                ListViewItem.ListViewSubItem subItem = item.SubItems.Add(STUD.StatusStr);
                item.SubItems.Add("　");

                if (scMAG.SCJoin_Lock.Contains(STUD.ID))
                {
                    foreach (ListViewItem.ListViewSubItem each in item.SubItems)
                    {
                        each.BackColor = Color.GreenYellow;
                    }
                }

                //TAG應該儲存修課記錄
                if (scMAG.SCJoin_Dic.ContainsKey(STUD.ID))
                {
                    item.Tag = scMAG.SCJoin_Dic[STUD.ID][0];
                    if (scMAG.SCJoin_Dic[STUD.ID].Count > 1)
                    {
                        foreach (ListViewItem.ListViewSubItem each in item.SubItems)
                        {
                            each.BackColor = Color.Red;
                        }
                        subItem.Text = "错误,重复的记录";
                    }
                }

                return item;
            }

            #endregion
        }

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


        /// <summary>
        /// 開啟下拉式選單時...
        /// </summary>
        private void btnInserStudent_PopupOpen(object sender, EventArgs e)
        {
            //建立待處理學生在按鈕內
            CreateStudentMenuItem();
        }

        /// <summary>
        /// 學生待處理更新後
        /// </summary>
        void Student_TempSourceChanged(object sender, EventArgs e)
        {
            btnInserStudent.Text = "由待处理加入(" + K12.Presentation.NLDPanels.Student.TempSource.Count.ToString() + ")";

            CreateStudentMenuItem();
        }

        /// <summary>
        /// 建立待處理學生在按鈕內
        /// </summary>
        private void CreateStudentMenuItem()
        {
            btnInserStudent.SubItems.Clear();

            if (K12.Presentation.NLDPanels.Student.TempSource.Count == 0)
            {
                LabelItem item = new LabelItem("No", "没有任何学生在待处理");
                btnInserStudent.SubItems.Add(item);
                return;
            }

            List<StudentRecord> StudentList = Student.SelectByIDs(K12.Presentation.NLDPanels.Student.TempSource);

            foreach (ClassRecord cr in tool.GetClassDic(StudentList).Values)
            {
                if (!ClassDic.ContainsKey(cr.ID))
                {
                    ClassDic.Add(cr.ID, cr);
                }
            }

            StudentList = SortClassIndex.K12Data_StudentRecord(StudentList);
            foreach (StudentRecord each in StudentList)
            {
                StringBuilder sbstud = new StringBuilder();
                string classname = "";
                string seatno = "";

                if (!string.IsNullOrEmpty(each.RefClassID))
                {
                    if (ClassDic.ContainsKey(each.RefClassID))
                    {
                        classname = ClassDic[each.RefClassID].Name;
                        seatno = (each.SeatNo.HasValue ? each.SeatNo.Value.ToString() : "");
                    }
                }

                sbstud.Append("班级「" + classname + "」");
                sbstud.Append("学号「" + seatno + "」");
                sbstud.Append("姓名「" + each.Name + "」");
                ButtonItem item = new ButtonItem(each.ID, sbstud.ToString());
                item.Tag = each;
                item.Click += new EventHandler(item_Click);
                btnInserStudent.SubItems.Add(item);


            }
        }

        #region 加入修課清單

        /// <summary>
        /// 將所有待處理學生都加入本社團
        /// </summary>
        private void btnInserStudent_Click(object sender, EventArgs e)
        {
            //將待處理學生加入修課清單
            AddListViewInTemp(K12.Presentation.NLDPanels.Student.TempSource);
        }

        /// <summary>
        /// 當使用者點擊下拉式清單內任意學生時...
        /// 將此學生加入課程
        /// </summary>
        void item_Click(object sender, EventArgs e)
        {
            ButtonItem each = (ButtonItem)sender;
            if (each.Tag != null)
            {
                StudentRecord eachTag = (StudentRecord)each.Tag;
                List<string> list = new List<string>();
                list.Add(eachTag.ID);

                AddListViewInTemp(list);
            }
            else
            {
                MsgBox.Show("未选择学生!");
            }
        }

        /// <summary>
        /// 將傳入的學生ID,加入此課程
        /// </summary>
        private void AddListViewInTemp(List<string> IsSaft)
        {
            if (IsSaft.Count != 0)
            {
                cso = new ClubStudObj();

                //Log
                StringBuilder sb_Message = new StringBuilder();

                #region 可加入的學生清單

                //排除已存在本課程的學生
                cso.CheckTempStudentInCourse(IsSaft, scMAG.SCJoin_Dic, _CLUBRecord, 3);

                StringBuilder sb_Log = new StringBuilder();
                sb_Log.AppendLine(string.Format("加入「{0}」名课程参与学生：(学年度「{1}」学期「{2}」课程「{3}」)", cso.InsertList.Count.ToString(), _CLUBRecord.SchoolYear.ToString(), _CLUBRecord.Semester.ToString(), _CLUBRecord.ClubName));

                if (cso.ReMoveTemp.Count != 0)
                {
                    #region 是否有重覆加入本社學生 - 錯誤

                    sb_Message.AppendLine("共有" + cso.ReMoveTemp.Count + "名学生,存在本课程!!");
                    List<StudentRecord> studlist = Student.SelectByIDs(cso.ReMoveTemp);
                    studlist = SortClassIndex.K12Data_StudentRecord(studlist);
                    foreach (StudentRecord stud in studlist)
                    {
                        string class_981 = string.IsNullOrEmpty(stud.RefClassID) ? "" : stud.Class.Name;
                        string SeatNo_981 = stud.SeatNo.HasValue ? stud.SeatNo.Value.ToString() : "";
                        sb_Message.AppendLine("班级「" + class_981 + "」学号「" + SeatNo_981 + "」姓名「" + stud.Name + "」");
                    }

                    #endregion

                    MsgBox.Show(sb_Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cso.ReDoubleTemp.Count != 0)
                {
                    #region 重覆參與其它社團記錄 -警告

                    sb_Message.AppendLine("共有" + cso.ReDoubleTemp.Count + "笔,重复参与其它课程记录");


                    //取得重覆社團
                    List<CLUBRecord> cludlist = tool._A.Select<CLUBRecord>("uid in ('" + string.Join("','", cso.GetClubID()) + "')");
                    Dictionary<string, CLUBRecord> clubDic = new Dictionary<string, CLUBRecord>();
                    foreach (CLUBRecord each in cludlist)
                    {
                        if (!clubDic.ContainsKey(each.UID))
                            clubDic.Add(each.UID, each);

                    }

                    studentDic.Clear();
                    foreach (StudentRecord each in Student.SelectByIDs(cso.GetStudentID()))
                    {
                        if (!studentDic.ContainsKey(each.ID))
                            studentDic.Add(each.ID, each);

                    }
                    cso.ReDoubleTemp.Sort(SortSCJ);
                    foreach (SCJoin SCJ in cso.ReDoubleTemp)
                    {
                        StudentRecord stud = studentDic[SCJ.RefStudentID];
                        CLUBRecord cr = clubDic[SCJ.RefClubID];

                        string class_981 = string.IsNullOrEmpty(stud.RefClassID) ? "" : stud.Class.Name;
                        string SeatNo_981 = stud.SeatNo.HasValue ? stud.SeatNo.Value.ToString() : "";
                        sb_Message.AppendLine("班级「" + class_981 + "」学号「" + SeatNo_981 + "」姓名「" + stud.Name + "」课程「" + cr.ClubName + "」阶段「" + SCJ.Phase + "」");
                    }

                    #endregion

                    DialogResult dr = MsgBox.Show(sb_Message.ToString() + "\n您是否要继续进行此作业?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (dr != DialogResult.Yes)
                    {
                        MsgBox.Show("作业已中止!!");
                        return;
                    }
                }

                if (cso.InsertList.Count != 0)
                {
                    #region InsertList

                    //取得Log學生物件
                    cso.GetLogStudent();

                    List<SCJoin> SCJoinlist = new List<SCJoin>();
                    foreach (string each in cso.InsertList)
                    {
                        SCJoin JHs = new SCJoin();
                        JHs.RefStudentID = each; //修課學生
                        JHs.RefClubID = this.PrimaryKey;
                        JHs.Phase = 3;
                        SCJoinlist.Add(JHs);

                        //加入修課LOG
                        if (!string.IsNullOrEmpty(GetLogMessage(each)))
                            sb_Log.AppendLine(GetLogMessage(each));

                    }

                    try
                    {
                        tool._A.InsertValues(SCJoinlist);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("新增资料失败\n" + ex.Message);
                        SmartSchool.ErrorReporting.ReportingService.ReportException(ex);
                        return;
                    }

                    //移出待處理
                    StringBuilder sbHelp = new StringBuilder();
                    sbHelp.AppendLine("已由待处理加入\n共「" + cso.InsertList.Count.ToString() + "」名学生\n");
                    MsgBox.Show(sbHelp.ToString());

                    FISCA.LogAgent.ApplicationLog.Log("课程", "加入", sb_Log.ToString());
                    K12.Presentation.NLDPanels.Student.RemoveFromTemp(cso.InsertList);

                    ClubEvents.RaiseAssnChanged();

                    #endregion
                }

                #endregion
            }
            else
            {
                string message = "请检查\n1.待处理无学生\n2.学生状态有误(非一般生)";
                FISCA.Presentation.Controls.MsgBox.Show(message);
            }
        }

        #endregion

        #region 移除選擇學生

        /// <summary>
        /// 移除選擇學生
        /// </summary>
        private void btnClearStudent_Click(object sender, EventArgs e)
        {
            ClearStudent();
        }

        private void 移除選擇學生ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearStudent();
        }

        private void ClearStudent()
        {
            DialogResult dr = FISCA.Presentation.Controls.MsgBox.Show("是否移除選取的學生?", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);

            if (dr == DialogResult.No)
                return;

            //取得選擇學生的修課記錄
            List<SCJoin> list = new List<SCJoin>();
            foreach (ListViewItem each in listViewEx1.SelectedItems)
            {
                SCJoin stud = (SCJoin)each.Tag;
                list.Add(stud);
            }

            //Log
            StringBuilder sb_Log = new StringBuilder();
            sb_Log.AppendLine(string.Format("已移除「{0}」名课程学生：(学年度「{1}」学期「{2}」课程「{3}」)", list.Count.ToString(), _CLUBRecord.SchoolYear.ToString(), _CLUBRecord.Semester.ToString(), _CLUBRecord.ClubName));

            List<string> StudentIDList = new List<string>();
            List<string> ClubIDList = new List<string>();
            foreach (SCJoin each in list)
            {
                if (!StudentIDList.Contains(each.RefStudentID))
                {
                    StudentIDList.Add(each.RefStudentID);
                    //移除社團學生Log
                    if (!string.IsNullOrEmpty(GetLogMessage(each.RefStudentID)))
                        sb_Log.AppendLine(GetLogMessage(each.RefStudentID));

                }

                if (!ClubIDList.Contains(each.RefClubID))
                {
                    ClubIDList.Add(each.RefClubID);
                }
            }

            Dictionary<string, CadresRecord> CadresDic = GetCadreList(StudentIDList, ClubIDList);

            //1.判斷該學生是否為本社社長
            //2.副社長
            //3.指導老師所指定的其他社團幹部
            //以上均需清除

            bool CheckIsCadre = false;

            StringBuilder sb_Message = new StringBuilder();
            sb_Message.AppendLine("移除列表中包含担任干部记录!!");

            foreach (string each in StudentIDList)
            {
                //社長
                if (_CLUBRecord.President == each)
                {
                    CheckIsCadre = true;
                    if (scMAG.StudentDic.ContainsKey(each))
                    {
                        sb_Message.AppendLine("社长:" + scMAG.StudentDic[each].Name);
                    }
                }
                //副社長
                if (_CLUBRecord.VicePresident == each)
                {
                    CheckIsCadre = true;
                    if (scMAG.StudentDic.ContainsKey(each))
                    {
                        sb_Message.AppendLine("副社长:" + scMAG.StudentDic[each].Name);
                    }
                }
                //其他幹部資料
                if (CadresDic.ContainsKey(each))
                {
                    CheckIsCadre = true;
                    if (scMAG.StudentDic.ContainsKey(CadresDic[each].RefStudentID))
                    {

                        sb_Message.AppendLine(CadresDic[each].CadreName + ":" + scMAG.StudentDic[CadresDic[each].RefStudentID].Name);
                    }
                }
            }
            sb_Message.AppendLine("");
            sb_Message.AppendLine("请确认操作:");
            sb_Message.AppendLine("[是]移除学生,清除干部记录");
            sb_Message.AppendLine("[否]中止所有操作");


            if (CheckIsCadre) //如果有學生是擔任幹部
            {
                DialogResult dr1 = FISCA.Presentation.Controls.MsgBox.Show(sb_Message.ToString(), MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);

                if (dr1 == DialogResult.Yes)
                {
                    foreach (string each in StudentIDList)
                    {
                        //社長
                        if (_CLUBRecord.President == each)
                        {
                            _CLUBRecord.President = "";
                        }
                        //副社長
                        if (_CLUBRecord.VicePresident == each)
                        {
                            _CLUBRecord.VicePresident = "";

                        }
                    }
                    //如果是社長副社長,就要更新一下資料
                    List<CLUBRecord> UpdataCLUBList = new List<CLUBRecord>() { _CLUBRecord };
                    tool._A.UpdateValues(UpdataCLUBList);

                    //刪除社團幹部
                    tool._A.DeletedValues(CadresDic.Values.ToList());
                }
                else
                {
                    return;
                }
            }

            try
            {
                tool._A.DeletedValues(list);
            }
            catch (Exception ex)
            {
                FISCA.Presentation.Controls.MsgBox.Show("移除学生失败\n" + ex.Message);
                SmartSchool.ErrorReporting.ReportingService.ReportException(ex);
                return;
            }

            FISCA.LogAgent.ApplicationLog.Log("课程", "移除课程学生", sb_Log.ToString());
            ClubEvents.RaiseAssnChanged();
        }

        #endregion

        /// <summary>
        /// 傳入學生ID,以取得Log字串
        /// </summary>
        private string GetLogMessage(string StudentID)
        {
            if (scMAG.StudentAllDic.ContainsKey(StudentID))
            {
                StudentRecord sr = scMAG.StudentAllDic[StudentID];
                return GetStudentString(sr);
            }
            else
            {
                if (cso != null)
                {
                    if (cso.LogStudentList != null)
                    {
                        if (cso.LogStudentList.ContainsKey(StudentID))
                        {
                            StudentRecord sr = cso.LogStudentList[StudentID];
                            return GetStudentString(sr);
                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }

            }
        }

        private string GetStudentString(StudentRecord sr)
        {
            string className = "";
            string seatno = sr.SeatNo.HasValue ? sr.SeatNo.Value.ToString() : "";
            if (ClassDic.ContainsKey(sr.RefClassID))
            {
                className = ClassDic[sr.RefClassID].Name;
            }
            return string.Format("班级「{0}」学号「{1}」学生「{2}」", className, seatno, sr.Name);
        }

        /// <summary>
        /// 透過學生清單,取得學生的幹部參與記錄
        /// </summary>
        private Dictionary<string, CadresRecord> GetCadreList(List<string> StudentIDList, List<string> ClubIDList)
        {
            //透過學生ID單,取得社團幹部
            Dictionary<string, CadresRecord> CadresDic = new Dictionary<string, CadresRecord>();
            string Text1 = string.Join("','", StudentIDList);
            string Text2 = string.Join("','", ClubIDList);

            List<CadresRecord> cadres = tool._A.Select<CadresRecord>(string.Format("ref_student_id in ('{0}') and ref_club_id in ('{1}')", Text1, Text2));
            foreach (CadresRecord each in cadres)
            {
                if (!CadresDic.ContainsKey(each.RefStudentID))
                {
                    CadresDic.Add(each.RefStudentID, each);
                }
            }
            return CadresDic;
        }

        #region 單純的程式碼

        /// <summary>
        /// 將選擇學生加入待處理
        /// </summary>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();

            foreach (ListViewItem each in listViewEx1.SelectedItems)
            {
                if (each.Tag == null)
                    continue;

                SCJoin stud = (SCJoin)each.Tag;
                list.Add(stud.RefStudentID);
            }
            K12.Presentation.NLDPanels.Student.AddToTemp(list);
        }

        private void 清空學生待處理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            K12.Presentation.NLDPanels.Student.RemoveFromTemp(K12.Presentation.NLDPanels.Student.TempSource);
        }

        /// <summary>
        /// 排序 參與學生 清單
        /// </summary>
        private int SortSCJ(SCJoin scj1, SCJoin scj2)
        {
            StudentRecord sr1 = studentDic[scj1.RefStudentID];
            StudentRecord sr2 = studentDic[scj2.RefStudentID];

            string stringStudent1 = (string.IsNullOrEmpty(sr1.RefClassID) ? "" : sr1.Class.Name).PadLeft(10);
            string stringStudent2 = (string.IsNullOrEmpty(sr2.RefClassID) ? "" : sr2.Class.Name).PadLeft(10);

            stringStudent1 += (sr1.SeatNo.HasValue ? sr1.SeatNo.Value.ToString() : "").PadLeft(10);
            stringStudent2 += (sr2.SeatNo.HasValue ? sr2.SeatNo.Value.ToString() : "").PadLeft(10);

            stringStudent1 += sr1.Name;
            stringStudent2 += sr2.Name;

            return stringStudent1.CompareTo(stringStudent2);
        }

        #endregion

        private void 鎖定學生選社ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<SCJoin> scjList = new List<SCJoin>();
            List<string> list = new List<string>();

            foreach (ListViewItem item in listViewEx1.SelectedItems)
            {
                SCJoin scj = (SCJoin)item.Tag;
                scj.Lock = true;
                scjList.Add(scj);

                list.Add(scj.RefStudentID);

                foreach (ListViewItem.ListViewSubItem each in item.SubItems)
                {
                    each.BackColor = Color.GreenYellow;
                }
                item.SubItems[7].Text = "是";
            }
            tool._A.UpdateValues(scjList);

            //Log
            LockStudent("锁定学生选课", list);
        }

        private void 移除選擇學生ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<SCJoin> scjList = new List<SCJoin>();
            List<string> list = new List<string>();

            foreach (ListViewItem item in listViewEx1.SelectedItems)
            {
                SCJoin scj = (SCJoin)item.Tag;
                scj.Lock = false;
                scjList.Add(scj);

                list.Add(scj.RefStudentID);

                foreach (ListViewItem.ListViewSubItem each in item.SubItems)
                {
                    each.BackColor = Color.White;
                }
                item.SubItems[7].Text = "　";
            }
            tool._A.UpdateValues(scjList);

            LockStudent("解除锁定选课", list);
        }

        /// <summary>
        /// 鎖定學生選社 or 解除鎖定選社
        /// </summary>
        private void LockStudent(string name, List<string> list)
        {
            List<StudentRecord> studList = Student.SelectByIDs(list);
            StringBuilder sb_Log = new StringBuilder();
            sb_Log.AppendLine(string.Format("已{0}共「{1}」名学生：(学年度「{2}」学期「{3}」课程「{4}」)", name, list.Count.ToString(), _CLUBRecord.SchoolYear.ToString(), _CLUBRecord.Semester.ToString(), _CLUBRecord.ClubName));

            foreach (StudentRecord each in studList)
            {
                sb_Log.AppendLine(GetStudentString(each));
            }

            FISCA.LogAgent.ApplicationLog.Log("课程", name, sb_Log.ToString());
        }
    }
}
