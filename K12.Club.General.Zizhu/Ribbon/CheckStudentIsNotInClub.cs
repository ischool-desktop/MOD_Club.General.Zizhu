﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.UDT;
using FISCA.Data;
using K12.Data;
using DevComponents.DotNetBar;

namespace K12.Club.General.Zizhu
{
    public partial class CheckStudentIsNotInClub : BaseForm
    {
        /// <summary>
        /// 開啟畫面即檢查學生
        /// 取得本學期的社團資料
        /// </summary>
        BackgroundWorker BGW = new BackgroundWorker();

        /// <summary>
        /// 儲存學生的社團分配記錄
        /// </summary>
        BackgroundWorker BGWSave = new BackgroundWorker();

        //UDT物件
        private AccessHelper _AccessHelper = new AccessHelper();
        private QueryHelper _QueryHelper = new QueryHelper();

        List<CLUBRecord> CLUBRecordList = new List<CLUBRecord>();
        Dictionary<string, CLUBRecord> ClubRefIDList = new Dictionary<string, CLUBRecord>();

        List<SCJoin> SCJoinList = new List<SCJoin>();

        int Column指定1 = 5;
        int Column指定2 = 6;
        int Column指定3 = 7;

        /// <summary>
        /// 已選社團學生
        /// </summary>
        Dictionary<string, List<SCJoin>> StudentScjoinDic = new Dictionary<string, List<SCJoin>>();

        /// <summary>
        /// 未選社團學生
        /// </summary>
        List<StudRecord> IsStudentList = new List<StudRecord>();

        public CheckStudentIsNotInClub()
        {
            InitializeComponent();

            BGW.DoWork += new DoWorkEventHandler(BGW_DoWork);
            BGW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGW_RunWorkerCompleted);

            BGWSave.DoWork += new DoWorkEventHandler(BGWSave_DoWork);
            BGWSave.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGWSave_RunWorkerCompleted);

            K12.Presentation.NLDPanels.Student.TempSourceChanged += new EventHandler(Student_TempSourceChanged);
            labelX1.Text = string.Format("{0}学年度　第{1}学期　未选课清单：", School.DefaultSchoolYear, School.DefaultSemester);
            labelX3.Text = string.Format("待处理学生：共{0}人", K12.Presentation.NLDPanels.Student.TempSource.Count);
        }

        void Student_TempSourceChanged(object sender, EventArgs e)
        {
            labelX3.Text = string.Format("待处理学生：共{0}人", K12.Presentation.NLDPanels.Student.TempSource.Count);
        }

        private void CheckStudentIsNotInClub_Load(object sender, EventArgs e)
        {
            //畫面開啟
            //1.即檢查本學年度學期,是否有未選社團學生
            //每個Row內存學生的ID

            //2.取得目前學年度/學期的可選擇社團
            //每個Row內存社團的Club UID

            //3.

            if (!BGW.IsBusy)
            {
                btnSave.Enabled = false;
                BGW.RunWorkerAsync();
            }
            else
            {
                MsgBox.Show("请重开本画面");
            }
        }

        void BGW_DoWork(object sender, DoWorkEventArgs e)
        {

            //取得本學期社團資料
            CLUBRecordList.Clear();
            CLUBRecordList = _AccessHelper.Select<CLUBRecord>(string.Format("school_year={0} and semester={1}", School.DefaultSchoolYear, School.DefaultSemester));

            //取得本學期,所有社團參與記錄
            ClubRefIDList.Clear();
            foreach (CLUBRecord record in CLUBRecordList)
            {
                if (!ClubRefIDList.ContainsKey(record.UID))
                {
                    ClubRefIDList.Add(record.UID, record);
                }
            }

            //取得學校所有學生記錄
            //學生記錄來自於社團ID
            string ClubIdString = string.Join("','", ClubRefIDList.Keys);
            List<SCJoin> Scjoin = _AccessHelper.Select<SCJoin>(string.Format("ref_club_id in ('{0}')", ClubIdString));
            foreach (SCJoin join in Scjoin)
            {
                if (!StudentScjoinDic.ContainsKey(join.RefStudentID))
                {
                    StudentScjoinDic.Add(join.RefStudentID, new List<SCJoin>());
                }
                StudentScjoinDic[join.RefStudentID].Add(join);
            }

            //取得學校內所有一般生記錄
            //班級/学号/学籍号/姓名
            //(沒有班級之學生,不列入記錄
            DataTable studentDT = _QueryHelper.Select("select student.id,class.class_name,student.seat_no,student.gender,student.student_number,student.name,class.grade_year from student left outer join class on student.ref_class_id=class.id where student.status=1 or student.status=2 ORDER BY class.grade_year,class.class_name,student.seat_no");

            IsStudentList.Clear();
            List<StudRecord> sTUDlIST = new List<StudRecord>();
            foreach (DataRow row in studentDT.Rows)
            {
                StudRecord re = new StudRecord(row);
                if (!StudentScjoinDic.ContainsKey(re.id))
                {
                    IsStudentList.Add(re);
                }
                else
                {
                    bool hasP1 = false, hasP2 = false, hasError = false;
                    foreach (var scJoin in StudentScjoinDic[re.id])
                    {
                        if (scJoin.Phase == 1)
                        {
                            if (hasP1)
                                hasError = true;
                            else
                                hasP1 = true;
                            if (ClubRefIDList[scJoin.RefClubID].FullPhase.HasValue && ClubRefIDList[scJoin.RefClubID].FullPhase.Value == true)
                            {
                                if (hasP2)
                                    hasError = true;
                                else
                                    hasP2 = true;
                            }
                        }
                        if (scJoin.Phase == 2)
                        {
                            if (hasP2)
                                hasError = true;
                            else
                                hasP2 = true;
                            if (ClubRefIDList[scJoin.RefClubID].FullPhase.HasValue && ClubRefIDList[scJoin.RefClubID].FullPhase.Value == true)
                                hasError = true;
                        }
                    }
                    if (!hasP1 || !hasP2 || hasError)
                    {
                        IsStudentList.Add(re);
                    }
                }
            }
        }

        void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSave.Enabled = true;
            if (e.Cancelled)
            {
                MsgBox.Show("检查作业已停止!");
                return;
            }
            if (e.Error != null)
            {
                SmartSchool.ErrorReporting.ReportingService.ReportException(e.Error);
                MsgBox.Show("检查学生课程参与记录发生错误\n" + e.Error.Message);
                return;
            }

            #region 學生
            labelX1.Text = string.Format("{0}学年度　第{1}学期　未选课清单(共{2}人)：", School.DefaultSchoolYear, School.DefaultSemester, IsStudentList.Count);
            foreach (StudRecord re in IsStudentList)
            {
                DataGridViewRow dataRow = new DataGridViewRow();
                dataRow.CreateCells(dataGridViewX1);
                dataRow.Tag = re;
                dataRow.Cells[0].Value = re.class_name;
                dataRow.Cells[1].Value = re.seat_no;
                dataRow.Cells[2].Value = re.name;
                dataRow.Cells[3].Value = re.gender;
                dataRow.Cells[4].Value = re.student_number;
                if (StudentScjoinDic.ContainsKey(re.id))
                {
                    foreach (var scJoin in StudentScjoinDic[re.id])
                    {
                        if (scJoin.Phase == 1)
                        {
                            dataRow.Cells[5].Value = ClubRefIDList[scJoin.RefClubID].ClubName;
                        }
                        if (scJoin.Phase == 2)
                        {
                            dataRow.Cells[6].Value = ClubRefIDList[scJoin.RefClubID].ClubName;
                        }
                    }
                }
                dataGridViewX1.Rows.Add(dataRow);
            }
            #endregion

            #region 社團

            foreach (CLUBRecord record in CLUBRecordList)
            {
                ButtonItem btnItem = new ButtonItem();
                btnItem.Text = record.ClubName;
                btnItem.Tag = record;
                btnItem.OptionGroup = "itmPnlTimeName";
                btnItem.ButtonStyle = eButtonStyle.ImageAndText;
                btnItem.Click += new EventHandler(btnItem_Click);

                itmPnlTimeName.Items.Add(btnItem);
            }

            itmPnlTimeName.ResumeLayout();
            itmPnlTimeName.Refresh();
            #endregion
        }

        void btnItem_Click(object sender, EventArgs e)
        {
            if (itmPnlTimeName.SelectedItems.Count == 1)
            {
                //取得目前所選擇的Button
                ButtonItem Buttonitem = itmPnlTimeName.SelectedItems[0] as ButtonItem;

                SetClub(Buttonitem);

            }
        }

        private void SetClub(ButtonItem Buttonitem)
        {
            //取得課程Record
            CLUBRecord club = (CLUBRecord)Buttonitem.Tag;

            foreach (DataGridViewRow row in dataGridViewX1.SelectedRows)
            {
                if (string.IsNullOrEmpty("" + row.Cells[Column指定1].Value))
                {
                    row.Cells[Column指定1].Value = "" + club.ClubName;
                    row.Cells[Column指定1].Tag = club;
                }
                else if (string.IsNullOrEmpty("" + row.Cells[Column指定2].Value))
                {
                    row.Cells[Column指定2].Value = "" + club.ClubName;
                    row.Cells[Column指定2].Tag = club;
                }
                else
                {
                    row.Cells[Column指定3].Value = "" + club.ClubName;
                    row.Cells[Column指定3].Tag = club;
                }
            }
        }


        private void 指定1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (itmPnlTimeName.SelectedItems.Count == 1)
            {
                //取得目前所選擇的Button
                ButtonItem Buttonitem = itmPnlTimeName.SelectedItems[0] as ButtonItem;
                //取得課程Record
                CLUBRecord club = (CLUBRecord)Buttonitem.Tag;
                foreach (DataGridViewRow row in dataGridViewX1.SelectedRows)
                {
                    row.Cells[Column指定1].Value = "" + club.ClubName;
                    row.Cells[Column指定1].Tag = club;
                }
            }
        }

        private void 指定2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (itmPnlTimeName.SelectedItems.Count == 1)
            {
                //取得目前所選擇的Button
                ButtonItem Buttonitem = itmPnlTimeName.SelectedItems[0] as ButtonItem;
                //取得課程Record
                CLUBRecord club = (CLUBRecord)Buttonitem.Tag;

                foreach (DataGridViewRow row in dataGridViewX1.SelectedRows)
                {
                    row.Cells[Column指定2].Value = "" + club.ClubName;
                    row.Cells[Column指定2].Tag = club;
                }
            }
        }

        private void 指定3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (itmPnlTimeName.SelectedItems.Count == 1)
            {
                //取得目前所選擇的Button
                ButtonItem Buttonitem = itmPnlTimeName.SelectedItems[0] as ButtonItem;
                //取得課程Record
                CLUBRecord club = (CLUBRecord)Buttonitem.Tag;

                foreach (DataGridViewRow row in dataGridViewX1.SelectedRows)
                {
                    row.Cells[Column指定3].Value = "" + club.ClubName;
                    row.Cells[Column指定3].Tag = club;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //將指定好的學生
            //建立社團參與記錄
            //並加入該社團內

            btnSave.Enabled = false;

            SCJoinList.Clear();
            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                if (row.Cells[Column指定1].Tag != null)
                {
                    StudRecord sr = (StudRecord)row.Tag;
                    CLUBRecord cr = (CLUBRecord)row.Cells[Column指定1].Tag;
                    SCJoin sc = new SCJoin();
                    sc.RefClubID = cr.UID;
                    sc.RefStudentID = sr.id;
                    sc.Phase = 1;
                    SCJoinList.Add(sc);
                }

                if (row.Cells[Column指定2].Tag != null)
                {
                    StudRecord sr = (StudRecord)row.Tag;
                    CLUBRecord cr = (CLUBRecord)row.Cells[Column指定2].Tag;
                    SCJoin sc = new SCJoin();
                    sc.RefClubID = cr.UID;
                    sc.RefStudentID = sr.id;
                    sc.Phase = 2;
                    SCJoinList.Add(sc);
                }

                if (row.Cells[Column指定3].Tag != null)
                {
                    StudRecord sr = (StudRecord)row.Tag;
                    CLUBRecord cr = (CLUBRecord)row.Cells[Column指定3].Tag;
                    SCJoin sc = new SCJoin();
                    sc.RefClubID = cr.UID;
                    sc.RefStudentID = sr.id;
                    sc.Phase = 3;
                    SCJoinList.Add(sc);
                }
            }

            BGWSave.RunWorkerAsync(SCJoinList);
        }

        void BGWSave_DoWork(object sender, DoWorkEventArgs e)
        {

            List<SCJoin> SCJoinList = (List<SCJoin>)e.Argument;

            try
            {
                _AccessHelper.InsertValues(SCJoinList);
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                SmartSchool.ErrorReporting.ReportingService.ReportException(ex);
                return;
            }
        }

        void BGWSave_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSave.Enabled = true;

            if (e.Cancelled)
            {
                MsgBox.Show("储存作业发生错误已停止!");
                return;
            }
            if (e.Error != null)
            {
                SmartSchool.ErrorReporting.ReportingService.ReportException(e.Error);
                MsgBox.Show("储存作业发生错误!\n" + e.Error.Message);
                return;
            }

            MsgBox.Show("学生加入课程成功!!");

            ClubEvents.RaiseAssnChanged();
            this.Close();


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 清除指定社團ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewX1.SelectedRows)
            {
                row.Cells[Column指定1].Value = "";
                row.Cells[Column指定1].Tag = null;

                row.Cells[Column指定2].Value = "";
                row.Cells[Column指定2].Tag = null;

                row.Cells[Column指定3].Value = "";
                row.Cells[Column指定3].Tag = null;
            }
        }

        private void 加入待處理學生ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (DataGridViewRow row in dataGridViewX1.SelectedRows)
            {
                StudRecord sr = (StudRecord)row.Tag;
                list.Add(sr.id);
            }
            K12.Presentation.NLDPanels.Student.AddToTemp(list);
        }

        private void btnOutPut_Click(object sender, EventArgs e)
        {
            #region 匯出
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.FileName = "未完成选课学生清单";
            saveFileDialog1.Filter = "Excel (*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            DataGridViewExport export = new DataGridViewExport(dataGridViewX1);
            export.Save(saveFileDialog1.FileName);

            if (new CompleteForm().ShowDialog() == DialogResult.Yes)
                System.Diagnostics.Process.Start(saveFileDialog1.FileName);
            #endregion
        }

    }

    public class StudRecord
    {
        public StudRecord(DataRow row)
        {
            id = "" + row[0];
            class_name = "" + row[1];
            seat_no = "" + row[2];
            gender = ("" + row[3]) == "1" ? "男" : ("" + row[3]) == "0" ? "女" : "";
            student_number = "" + row[4];
            name = "" + row[5];
            grade_year = "" + row[6];
        }

        public string id { get; set; }
        public string class_name { get; set; }
        public string seat_no { get; set; }
        public string student_number { get; set; }
        public string name { get; set; }
        public string grade_year { get; set; }
        public string gender { get; set; }
    }
}
