using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using Aspose.Words;
using System.IO;
using FISCA.DSAUtil;
using FISCA.UDT;
using K12.Data;
using System.Xml;
using System.Diagnostics;

namespace K12.Club.General.Zizhu
{
    public partial class ClubPointsListForm : BaseForm
    {
        /// <summary>
        /// 樣版
        /// </summary>
        string ClassPrint_Config_1 = "K12.Club.General.ClubPointsListForm.cs";

        BackgroundWorker BGW = new BackgroundWorker();

        int 學生多少個 = 150;
        int 日期多少天 = 30;

        public ClubPointsListForm()
        {
            InitializeComponent();

        }

        private void ClubPointsListForm_Load(object sender, EventArgs e)
        {
            BGW.DoWork += new DoWorkEventHandler(BGW_DoWork);
            BGW.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGW_RunWorkerCompleted);

            dateTimeInput1.Value = DateTime.Today;
            dateTimeInput2.Value = DateTime.Today.AddDays(6);

            GetDateTime_Click(null, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ClubAdmin.Instance.SelectedSource.Count == 0)
            {
                MsgBox.Show("请选择课程!!");
                return;
            }

            if (BGW.IsBusy)
            {
                MsgBox.Show("忙碌中,稍后再试!!");
                return;
            }

            #region 日期設定

            if (dataGridViewX1.Rows.Count <= 0)
            {
                MsgBox.Show("列印点名单必须有日期!!");
                return;
            }

            DSXmlHelper dxXml = new DSXmlHelper("XmlData");

            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                string 日期 = "" + row.Cells[0].Value;
                dxXml.AddElement(".", "item", 日期);
            }

            #endregion

            btnSave.Enabled = false;
            BGW.RunWorkerAsync(dxXml.BaseElement);

        }

        void BGW_DoWork(object sender, DoWorkEventArgs e)
        {
            Campus.Report.ReportConfiguration ConfigurationInCadre = new Campus.Report.ReportConfiguration(ClassPrint_Config_1);
            Aspose.Words.Document Template;

            if (ConfigurationInCadre.Template == null)
            {
                //如果範本為空,則建立一個預設範本
                Campus.Report.ReportConfiguration ConfigurationInCadre_1 = new Campus.Report.ReportConfiguration(ClassPrint_Config_1);
                ConfigurationInCadre_1.Template = new Campus.Report.ReportTemplate(Properties.Resources.社團點名單_套表列印, Campus.Report.TemplateType.Word);
                //ConfigurationInCadre_1.Template = new Campus.Report.ReportTemplate(Properties.Resources.社團點名表_合併欄位總表, Campus.Report.TemplateType.Word);
                Template = ConfigurationInCadre_1.Template.ToDocument();
            }
            else
            {
                //如果已有範本,則取得樣板
                Template = ConfigurationInCadre.Template.ToDocument();
            }

            SCJoinDataLoad crM = new SCJoinDataLoad();

            #region 日期

            List<string> config = new List<string>();

            XmlElement day = (XmlElement)e.Argument;

            if (day == null)
            {
                MsgBox.Show("第一次使用报表请先进行[日期设定]");
                return;
            }
            else
            {
                config.Clear();
                foreach (XmlElement xml in day.SelectNodes("item"))
                {
                    config.Add(xml.InnerText);
                }
            }

            #endregion

            DataTable table = new DataTable();
            table.Columns.Add("学校名称");
            table.Columns.Add("课程名称");
            table.Columns.Add("学年度");
            table.Columns.Add("学期");

            table.Columns.Add("上课地点");
            table.Columns.Add("课程类型");
            table.Columns.Add("指导老师1");
            table.Columns.Add("指导老师2");
            table.Columns.Add("指导老师3");

            table.Columns.Add("打印日期");
            table.Columns.Add("上课开始");
            table.Columns.Add("上课结束");
            table.Columns.Add("人数");

            for (int x = 1; x <= 日期多少天; x++)
            {
                table.Columns.Add(string.Format("日期_{0}", x));
            }

            for (int x = 1; x <= 學生多少個; x++)
            {
                table.Columns.Add(string.Format("班级_{0}", x));
            }

            for (int x = 1; x <= 學生多少個; x++)
            {
                table.Columns.Add(string.Format("座号_{0}", x));
            }

            for (int x = 1; x <= 學生多少個; x++)
            {
                table.Columns.Add(string.Format("姓名_{0}", x));
            }

            for (int x = 1; x <= 學生多少個; x++)
            {
                table.Columns.Add(string.Format("学号_{0}", x));
            }

            for (int x = 1; x <= 學生多少個; x++)
            {
                table.Columns.Add(string.Format("性别_{0}", x));
            }

            foreach (string each in crM.CLUBRecordDic.Keys)
            {
                //社團資料
                CLUBRecord cr = crM.CLUBRecordDic[each];

                DataRow row = table.NewRow();
                row["学校名称"] = K12.Data.School.ChineseName;
                row["课程名称"] = cr.ClubName;
                row["学年度"] = cr.SchoolYear;
                row["学期"] = cr.Semester;

                row["上课地点"] = cr.Location;
                row["课程类型"] = cr.ClubCategory;
                if (crM.TeacherDic.ContainsKey(cr.RefTeacherID))
                {
                    row["指导老师1"] = crM.TeacherDic[cr.RefTeacherID].Name;
                }
                if (crM.TeacherDic.ContainsKey(cr.RefTeacherID2))
                {
                    row["指导老师2"] = crM.TeacherDic[cr.RefTeacherID2].Name;
                }
                if (crM.TeacherDic.ContainsKey(cr.RefTeacherID3))
                {
                    row["指导老师3"] = crM.TeacherDic[cr.RefTeacherID3].Name;
                }

                //row["外聘老師"] = "";

                row["打印日期"] = DateTime.Today.ToShortDateString();
                row["上课开始"] = config[0];
                row["上课结束"] = config[config.Count - 1];
                row["人数"] = crM.ClubByStudentList[each].Count;

                for (int x = 1; x <= config.Count; x++)
                {
                    row[string.Format("日期_{0}", x)] = config[x - 1];
                }

                int y = 1;
                foreach (StudentRecord obj in crM.ClubByStudentList[each])
                {
                    if (y <= 學生多少個) //限制畫面到100名學生
                    {
                        row[string.Format("班级_{0}", y)] = obj.Class != null ? obj.Class.Name : "";
                        row[string.Format("座号_{0}", y)] = obj.SeatNo.HasValue ? obj.SeatNo.Value.ToString() : "";
                        row[string.Format("姓名_{0}", y)] = obj.Name;
                        row[string.Format("学号_{0}", y)] = obj.StudentNumber;
                        row[string.Format("性别_{0}", y)] = obj.Gender;
                        y++;
                    }
                }

                table.Rows.Add(row);
            }

            Document PageOne = (Document)Template.Clone(true);
            PageOne.MailMerge.Execute(table);
            e.Result = PageOne;
        }

        void BGW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSave.Enabled = true;

            if (e.Cancelled)
            {
                MsgBox.Show("作业已被中止!!");
            }
            else
            {
                if (e.Error == null)
                {
                    Document inResult = (Document)e.Result;

                    try
                    {
                        SaveFileDialog SaveFileDialog1 = new SaveFileDialog();

                        SaveFileDialog1.Filter = "Word (*.doc)|*.doc|所有档案 (*.*)|*.*";
                        SaveFileDialog1.FileName = "拓展性课程点名单(套表列印)";

                        if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            inResult.Save(SaveFileDialog1.FileName);
                            Process.Start(SaveFileDialog1.FileName);
                        }
                        else
                        {
                            FISCA.Presentation.Controls.MsgBox.Show("档案未储存");
                            return;
                        }
                    }
                    catch
                    {
                        FISCA.Presentation.Controls.MsgBox.Show("档案储存错误,请检查档案是否开启中!!");
                        return;
                    }

                    this.Close();
                }
                else
                {
                    MsgBox.Show("列印资料发生错误\n" + e.Error.Message);
                }
            }
        }

        private void GetDateTime_Click(object sender, EventArgs e)
        {
            //建立日期清單
            TimeSpan ts = dateTimeInput2.Value - dateTimeInput1.Value;

            List<DateTime> TList = new List<DateTime>();

            foreach (DataGridViewRow row in dataGridViewX1.Rows)
            {
                DateTime dt;
                DateTime.TryParse("" + row.Cells[0].Value, out dt);
                TList.Add(dt);
            }

            List<DayOfWeek> WeekList = new List<DayOfWeek>();
            if (cbDay1.Checked)
                WeekList.Add(DayOfWeek.Monday);
            if (cbDay2.Checked)
                WeekList.Add(DayOfWeek.Tuesday);
            if (cbDay3.Checked)
                WeekList.Add(DayOfWeek.Wednesday);
            if (cbDay4.Checked)
                WeekList.Add(DayOfWeek.Thursday);
            if (cbDay5.Checked)
                WeekList.Add(DayOfWeek.Friday);

            for (int x = 0; x <= ts.Days; x++)
            {
                DateTime dt = dateTimeInput1.Value.AddDays(x);

                if (WeekList.Contains(dt.DayOfWeek))
                {
                    if (!TList.Contains(dt))
                    {
                        TList.Add(dt);
                    }
                }
            }

            TList.Sort();
            //資料填入
            dataGridViewX1.Rows.Clear();
            foreach (DateTime dt in TList)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridViewX1);
                row.Cells[0].Value = dt.ToShortDateString();
                row.Cells[1].Value = CheckWeek(dt.DayOfWeek.ToString());
                dataGridViewX1.Rows.Add(row);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //取得設定檔
            Campus.Report.ReportConfiguration ConfigurationInCadre = new Campus.Report.ReportConfiguration(ClassPrint_Config_1);
            Campus.Report.TemplateSettingForm TemplateForm;
            //畫面內容(範本內容,預設樣式
            if (ConfigurationInCadre.Template != null)
            {
                TemplateForm = new Campus.Report.TemplateSettingForm(ConfigurationInCadre.Template, new Campus.Report.ReportTemplate(Properties.Resources.社團點名單_套表列印, Campus.Report.TemplateType.Word));
            }
            else
            {
                ConfigurationInCadre.Template = new Campus.Report.ReportTemplate(Properties.Resources.社團點名單_套表列印, Campus.Report.TemplateType.Word);
                TemplateForm = new Campus.Report.TemplateSettingForm(ConfigurationInCadre.Template, new Campus.Report.ReportTemplate(Properties.Resources.社團點名單_套表列印, Campus.Report.TemplateType.Word));
            }

            //預設名稱
            TemplateForm.DefaultFileName = "拓展性课程点名单(套表列印范本)";

            //如果回傳為OK
            if (TemplateForm.ShowDialog() == DialogResult.OK)
            {
                //設定後樣試,回傳
                ConfigurationInCadre.Template = TemplateForm.Template;
                //儲存
                ConfigurationInCadre.Save();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "另存新档";
            sfd.FileName = "拓展性课程点名单_合并栏位总表.doc";
            sfd.Filter = "Word档案 (*.doc)|*.doc|所有档案 (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                    fs.Write(Properties.Resources.社團點名表_合併欄位總表, 0, Properties.Resources.社團點名表_合併欄位總表.Length);
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch
                {
                    FISCA.Presentation.Controls.MsgBox.Show("指定路径无法存取。", "另存档案失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        /// <summary>
        /// 依編號取代為星期
        /// </summary>
        public static string CheckWeek(string x)
        {
            if (x == "Monday")
            {
                return "一";
            }
            else if (x == "Tuesday")
            {
                return "二";
            }
            else if (x == "Wednesday")
            {
                return "三";
            }
            else if (x == "Thursday")
            {
                return "四";
            }
            else if (x == "Friday")
            {
                return "五";
            }
            else if (x == "Saturday")
            {
                return "六";
            }
            else
            {
                return "日";
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dataGridViewX1.Rows.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
