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
using FISCA.Data;
using K12.Data;
using DevComponents.DotNetBar;
using SmartSchool.API.PlugIn;

namespace K12.Club.General.Zizhu.ImportExport
{
    class ExportSCJoin : SmartSchool.API.PlugIn.Export.Exporter
    {
        public ExportSCJoin()
        {
            this.Image = null;
            this.Text = "汇出学生选课纪录";
        }
        public override void InitializeExport(SmartSchool.API.PlugIn.Export.ExportWizard wizard)
        {
            AccessHelper helper = new AccessHelper();

            Dictionary<string, CLUBRecord> AllClubDic = new Dictionary<string, CLUBRecord>();
            Dictionary<string, List<string>> SemesterClub = new Dictionary<string, List<string>>();
            //社團基本資料
            List<CLUBRecord> ClubList = helper.Select<CLUBRecord>();
            foreach (CLUBRecord each in ClubList)
            {
                if (!AllClubDic.ContainsKey(each.UID))
                {
                    AllClubDic.Add(each.UID, each);
                }

                string SchoolYearSemester = each.SchoolYear.ToString().PadLeft(3, ' ') + "学年度 第" + each.Semester.ToString() + "学期";

                if (!SemesterClub.ContainsKey(SchoolYearSemester))
                {
                    //學年度學期名稱/當學期的社團課程ID清單
                    SemesterClub.Add(SchoolYearSemester, new List<string>());
                }

                SemesterClub[SchoolYearSemester].Add(each.UID);
            }

            var FiltedSemester = School.DefaultSchoolYear.ToString().PadLeft(3, ' ') + "学年度 第" + School.DefaultSemester + "学期";
            List<string> list = new List<string>();

            foreach (string item in SemesterClub.Keys)
            {
                list.Add(item);
            }
            list.Sort();
            if (list.Count > 0 && !list.Contains(FiltedSemester))
                FiltedSemester = list[0];

            foreach (string item in list)
            {
                SmartSchool.API.PlugIn.VirtualRadioButton radioSem = new VirtualRadioButton(item);
                wizard.Options.Add(radioSem);
                if (item == FiltedSemester)
                    radioSem.Checked = true;
                radioSem.CheckedChanged += delegate(object sender1, EventArgs e1)
                {
                    var target = sender1 as SmartSchool.API.PlugIn.VirtualRadioButton;
                    if (target.Checked)
                        FiltedSemester = target.Text;
                };
                //MenuButton mb = e.VirtualButtons[item];
                //mb.AutoCheckOnClick = true;
                //mb.AutoCollapseOnClick = true;
                //mb.Checked = (item == FiltedSemester);
                //mb.Tag = item;
                //mb.CheckedChanged += delegate(object sender1, EventArgs e1)
                //{
                //    MenuButton mb1 = sender1 as MenuButton;
                //    SetClubList(mb1.Text);
                //    FiltedSemester = FilterMenu.Text = mb1.Text;
                //    mb1.Checked = true;
                //};
            }

            wizard.ExportableFields.AddRange(new string[]{
                "班级",
                "座号",
                "姓名",
                "学籍号",
                "性别",
                "第一阶段",
                "第二阶段"
            });
            wizard.ExportPackage += (sender, e) =>
            {
                Dictionary<string, CLUBRecord> ClubRefIDList = new Dictionary<string, CLUBRecord>();
                //List<CLUBRecord> CLUBRecordList = helper.Select<CLUBRecord>(string.Format("uid in ('{0}')", string.Join("','", e.List)));
                //foreach (CLUBRecord record in CLUBRecordList)
                //{
                //    if (!ClubRefIDList.ContainsKey(record.UID))
                //    {
                //        ClubRefIDList.Add(record.UID, record);
                //    }
                //}
                foreach (CLUBRecord each in ClubList)
                {
                    string SchoolYearSemester = each.SchoolYear.ToString().PadLeft(3, ' ') + "学年度 第" + each.Semester.ToString() + "学期";
                    if (FiltedSemester == SchoolYearSemester)
                    {
                        ClubRefIDList.Add(each.UID, each);
                    }
                }

                string ClubIdString = string.Join("','", ClubRefIDList.Keys);
                List<SCJoin> Scjoin = helper.Select<SCJoin>(string.Format("ref_club_id in ('{0}') and ref_student_id in ('{1}')", ClubIdString, string.Join("','", e.List)));

                Dictionary<string, List<SCJoin>> StudentScjoinDic = new Dictionary<string, List<SCJoin>>();
                foreach (SCJoin join in Scjoin)
                {
                    if (!StudentScjoinDic.ContainsKey(join.RefStudentID))
                    {
                        StudentScjoinDic.Add(join.RefStudentID, new List<SCJoin>());
                    }
                    StudentScjoinDic[join.RefStudentID].Add(join);
                }

                var studentList = K12.Data.Student.SelectByIDs(e.List);
                foreach (var stuRec in studentList)
                {
                    RowData row = new RowData();

                    foreach (string field in e.ExportFields)
                    {
                        if (wizard.ExportableFields.Contains(field))
                        {
                            switch (field)
                            {
                                case "班级":
                                    row.Add(field, "" + stuRec.Class == null ? "" : stuRec.Class.Name);
                                    break;
                                case "座号":
                                    row.Add(field, "" + stuRec.SeatNo);
                                    break;
                                case "姓名":
                                    row.Add(field, "" + stuRec.Name);
                                    break;
                                case "学籍号":
                                    row.Add(field, "" + stuRec.StudentNumber);
                                    break;
                                case "性别":
                                    row.Add(field, "" + stuRec.Gender);
                                    break;
                                case "第一阶段":
                                    if (StudentScjoinDic.ContainsKey(stuRec.ID))
                                    {
                                        foreach (var item in StudentScjoinDic[stuRec.ID])
                                        {
                                            if (item.Phase == 1)
                                            {
                                                if (row.ContainsKey(field))
                                                    row[field] += "," + ClubRefIDList[item.RefClubID].ClubName;
                                                else
                                                    row.Add(field, ClubRefIDList[item.RefClubID].ClubName);
                                            }
                                        }
                                    }
                                    break;
                                case "第二阶段":
                                    if (StudentScjoinDic.ContainsKey(stuRec.ID))
                                    {
                                        foreach (var item in StudentScjoinDic[stuRec.ID])
                                        {
                                            if (item.Phase == 1 && ClubRefIDList[item.RefClubID].FullPhase.HasValue && ClubRefIDList[item.RefClubID].FullPhase.Value == true)
                                            {
                                                if (row.ContainsKey(field))
                                                    row[field] += ",--";
                                                else
                                                    row.Add(field, "--");
                                            }
                                            if (item.Phase == 2)
                                            {
                                                if (row.ContainsKey(field))
                                                    row[field] += "," + ClubRefIDList[item.RefClubID].ClubName;
                                                else
                                                    row.Add(field, ClubRefIDList[item.RefClubID].ClubName);
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                    e.Items.Add(row);
                }
            };
        }
    }
}