using Aspose.Words;
using Aspose.Words.Tables;
using Campus.DocumentValidator;
using FISCA;
using FISCA.Data;
using FISCA.Permission;
using FISCA.Presentation;
using FISCA.Presentation.Controls;
using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace K12.Club.General.Zizhu
{
    /// <summary>
    /// 拓展性课程
    /// </summary>
    public class Program
    {
        [MainMethod()]
        static public void Main()
        {

            MotherForm.AddPanel(ClubAdmin.Instance);

            ClubAdmin.Instance.AddView(new ExtracurricularActivitiesView());

            //驗證規則
            FactoryProvider.FieldFactory.Add(new CLUBFieldValidatorFactory());


            #region 匯出學生選課資料

            K12.Presentation.NLDPanels.Student.RibbonBarItems["资料统计"]["汇出"].Size = RibbonBarButton.MenuButtonSize.Large;
            K12.Presentation.NLDPanels.Student.RibbonBarItems["资料统计"]["汇出"].Image = Properties.Resources.Export_Image;
            K12.Presentation.NLDPanels.Student.RibbonBarItems["资料统计"]["汇出"]["拓展性课程"]["汇出学生选课纪录"].Enable = Permissions.匯出社團學生名單權限;
            K12.Presentation.NLDPanels.Student.RibbonBarItems["资料统计"]["汇出"]["拓展性课程"]["汇出学生选课纪录"].Click += delegate
            {
                SmartSchool.API.PlugIn.Export.Exporter exporter = new K12.Club.General.Zizhu.ImportExport.ExportSCJoin();

                ExportStudentV2 wizard = new ExportStudentV2(exporter.Text, exporter.Image);
                exporter.InitializeExport(wizard);
                wizard.ShowDialog();
            };

            #endregion

            #region 產生修課成績報表

            K12.Presentation.NLDPanels.Student.RibbonBarItems["资料统计"]["报表"]["拓展性课程"]["打印成绩单"].Enable = false;
            K12.Presentation.NLDPanels.Student.RibbonBarItems["资料统计"]["报表"]["拓展性课程"]["打印成绩单"].Click += delegate
            {
                #region 列印成績單
                QueryHelper _Q = new QueryHelper();
                List<string> _ids = new List<string>(K12.Presentation.NLDPanels.Student.SelectedSource);
                bool fieldMode = false;
                Dictionary<Document, string> documents = new Dictionary<Document, string>();

                BackgroundWorker bkw = new BackgroundWorker();
                bkw.WorkerReportsProgress = true;
                bkw.RunWorkerCompleted += delegate
                {
                    FISCA.Presentation.MotherForm.SetStatusBarMessage("拓展性课程学生成绩单产生完成。", 100);
                    List<string> files = new List<string>();
                    foreach (var doc in documents.Keys)
                    {

                        SaveFileDialog save = new SaveFileDialog();
                        save.Title = "另存新档";
                        save.FileName = documents[doc];
                        save.Filter = "Word档案 (*.docx)|*.docx|Word档案 (*.doc)|*.doc|所有档案 (*.*)|*.*";

                        if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            try
                            {
                                doc.Save(save.FileName);
                                files.Add(save.FileName);
                            }
                            catch
                            {
                                MessageBox.Show("档案储存失败。");
                            }
                        }
                    }
                    foreach (var file in files)
                    {
                        System.Diagnostics.Process.Start(file);
                    }
                };
                bkw.ProgressChanged += delegate(object sender, ProgressChangedEventArgs e)
                {
                    FISCA.Presentation.MotherForm.SetStatusBarMessage("拓展性课程学生成绩单产生中...", e.ProgressPercentage);
                };

                bkw.DoWork += delegate
                {
                    Dictionary<string, DataRow> dicStudentRow = new Dictionary<string, DataRow>();
                    DataTable mergeDT = new DataTable();

                    mergeDT.Columns.Add("学年度");
                    mergeDT.Columns.Add("学期");
                    mergeDT.Columns.Add("姓名");
                    mergeDT.Columns.Add("学籍号");
                    mergeDT.Columns.Add("年级");
                    mergeDT.Columns.Add("班级");
                    mergeDT.Columns.Add("学号");

                    DataTable dt = _Q.Select(string.Format(@"
SELECT 
    $k12.clubrecord.universal.school_year,
    $k12.clubrecord.universal.semester,
    $k12.clubrecord.universal.club_category,
    $k12.clubrecord.universal.club_name,
    $k12.clubrecord.universal.club_number,
    phase,
    student.id,
    student.name,
    student.student_number, 
    student.seat_no,
    class.class_name as classname,
    class.grade_year,
    asmS.detial as detialS,
    asmT.detial as detialT
FROM
    student
    LEFT OUTER JOIN class on student.ref_class_id = class.id
    LEFT OUTER JOIN $k12.scjoin.universal on student.id = $k12.scjoin.universal.ref_student_id::bigint
    LEFT OUTER JOIN $k12.clubrecord.universal on $k12.clubrecord.universal.uid = $k12.scjoin.universal.ref_club_id::bigint
    LEFT OUTER JOIN $ischool.club.assessment as asmS on asmS.ref_student_id = student.id and asmS.ref_club_id = $k12.clubrecord.universal.uid and asmS.assessment_type='student'
    LEFT OUTER JOIN $ischool.club.assessment as asmT on asmT.ref_student_id = student.id and asmT.ref_club_id = $k12.clubrecord.universal.uid and asmT.assessment_type='teacher'
WHERE
    student.id in ({0}) and $k12.clubrecord.universal.school_year = {1} and $k12.clubrecord.universal.semester = {2}
ORDER BY class.grade_year, class.display_order, student.seat_no, phase
                        ", string.Join(",", _ids), K12.Data.School.DefaultSchoolYear, K12.Data.School.DefaultSemester));
                    var count = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        count++;
                        if (!dicStudentRow.ContainsKey("" + row["id"]))
                        {
                            var stuRow = mergeDT.Rows.Add();
                            stuRow[mergeDT.Columns.IndexOf("学年度")] = "" + row["school_year"];
                            stuRow[mergeDT.Columns.IndexOf("学期")] = "" + row["semester"];
                            stuRow[mergeDT.Columns.IndexOf("姓名")] = "" + row["name"];
                            stuRow[mergeDT.Columns.IndexOf("学籍号")] = "" + row["student_number"];
                            stuRow[mergeDT.Columns.IndexOf("年级")] = "" + row["grade_year"];
                            stuRow[mergeDT.Columns.IndexOf("班级")] = "" + row["classname"];
                            stuRow[mergeDT.Columns.IndexOf("学号")] = "" + row["seat_no"];
                            dicStudentRow.Add("" + row["id"], stuRow);
                        }
                        var studentRow = dicStudentRow["" + row["id"]];


                        var level = "阶段" + row["phase"];
                        {
                            var key = level + "_课程类别";
                            if (!mergeDT.Columns.Contains(key))
                                mergeDT.Columns.Add(key);
                            studentRow[mergeDT.Columns.IndexOf(key)] = "" + row["club_category"];
                        }
                        {
                            var key = level + "_课程名称";
                            if (!mergeDT.Columns.Contains(key))
                                mergeDT.Columns.Add(key);
                            studentRow[mergeDT.Columns.IndexOf(key)] = "" + row["club_name"];
                        }
                        {
                            var key = level + "_课程代码";
                            if (!mergeDT.Columns.Contains(key))
                                mergeDT.Columns.Add(key);
                            studentRow[mergeDT.Columns.IndexOf(key)] = "" + row["club_number"]; ;
                        }

                        var doc = new XmlDocument();
                        if ("" + row["detialS"] != "")
                        {
                            doc.LoadXml("" + row["detialS"]);
                            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                            {
                                var key = level + "_学生自评_" + node.Name;
                                var value = "" + node.InnerText;
                                if (!mergeDT.Columns.Contains(key))
                                    mergeDT.Columns.Add(key);//Q1評語
                                studentRow[mergeDT.Columns.IndexOf(key)] = value;
                            }
                        }
                        if ("" + row["detialT"] != "")
                        {
                            doc.LoadXml("" + row["detialT"]);
                            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                            {
                                var key = level + "_教师评鉴_" + node.Name;
                                var value = "" + node.InnerText;
                                if (!mergeDT.Columns.Contains(key))
                                    mergeDT.Columns.Add(key);//Q1評語
                                studentRow[mergeDT.Columns.IndexOf(key)] = value;
                            }
                        }
                        bkw.ReportProgress(100 * count / dt.Rows.Count);
                    }

                    if (fieldMode)
                    {
                        Document doc = new Document();
                        DocumentBuilder bu = new DocumentBuilder(doc);
                        bu.MoveToDocumentStart();
                        bu.CellFormat.Borders.LineStyle = LineStyle.Single;
                        bu.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
                        Table table = bu.StartTable();
                        foreach (DataColumn col in mergeDT.Columns)
                        {

                            bu.InsertCell();
                            bu.CellFormat.Width = 15;
                            bu.InsertField("MERGEFIELD " + col.Caption + @" \* MERGEFORMAT", "«.»");
                            bu.ParagraphFormat.Alignment = ParagraphAlignment.Center;

                            bu.InsertCell();
                            bu.CellFormat.Width = 125;
                            bu.Write(col.Caption);
                            bu.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                            bu.EndRow();
                        }
                        //table.AllowAutoFit = false;
                        bu.EndTable();
                        documents.Add(doc, "拓展性课程学生成绩单合并栏位表.doc");
                    }
                    else
                    {
                        var doc = new Document(new MemoryStream(Properties.Resources.華東師範大學附屬紫竹小學拓展性課程成績單));
                        //        //合併，儲存
                        doc.MailMerge.Execute(mergeDT);
                        doc.MailMerge.DeleteFields();

                        documents.Add(doc, "拓展性课程学生成绩单.doc");
                    }
                };
                bkw.RunWorkerAsync();
                FISCA.Presentation.MotherForm.SetStatusBarMessage("拓展性课程学生成绩单产生中...", 0);
                #endregion
            };
            K12.Presentation.NLDPanels.Student.SelectedSourceChanged += delegate
            {
                K12.Presentation.NLDPanels.Student.RibbonBarItems["资料统计"]["报表"]["拓展性课程"]["打印成绩单"].Enable = K12.Presentation.NLDPanels.Student.SelectedSource.Count > 0;
            };

            #endregion

            #region 社團基本資料

            FeatureAce UserPermission = FISCA.Permission.UserAcl.Current[Permissions.社團基本資料];
            if (UserPermission.Editable || UserPermission.Viewable)
                ClubAdmin.Instance.AddDetailBulider(new FISCA.Presentation.DetailBulider<ClubDetailItem>());

            //社團照片
            UserPermission = FISCA.Permission.UserAcl.Current[Permissions.社團照片];
            if (UserPermission.Editable || UserPermission.Viewable)
                ClubAdmin.Instance.AddDetailBulider(new FISCA.Presentation.DetailBulider<ClubImageItem>());


            UserPermission = FISCA.Permission.UserAcl.Current[Permissions.社團限制];
            if (UserPermission.Editable || UserPermission.Viewable)
                ClubAdmin.Instance.AddDetailBulider(new FISCA.Presentation.DetailBulider<ClubRestrictItem>());


            UserPermission = FISCA.Permission.UserAcl.Current[Permissions.社團參與學生];
            if (UserPermission.Editable || UserPermission.Viewable)
            {
                ClubAdmin.Instance.AddDetailBulider(new FISCA.Presentation.DetailBulider<ClubStudent>());
                ClubAdmin.Instance.AddDetailBulider(new FISCA.Presentation.DetailBulider<ClubStudent_2>());
                //ClubAdmin.Instance.AddDetailBulider(new FISCA.Presentation.DetailBulider<ClubStudent_3>());
            }

            UserPermission = FISCA.Permission.UserAcl.Current[Permissions.學生社團成績_資料項目];
            if (UserPermission.Editable || UserPermission.Viewable)
                K12.Presentation.NLDPanels.Student.AddDetailBulider(new FISCA.Presentation.DetailBulider<StudentResultItem>());


            #endregion

            #region 編輯

            RibbonBarItem edit = ClubAdmin.Instance.RibbonBarItems["编辑"];
            edit["新增课程"].Size = RibbonBarButton.MenuButtonSize.Large;
            edit["新增课程"].Image = Properties.Resources.health_and_leisure_add_64;
            edit["新增课程"].Enable = Permissions.新增社團權限;
            edit["新增课程"].Click += delegate
            {
                NewAddClub insert = new NewAddClub();
                insert.ShowDialog();
            };

            edit["复制课程"].Size = RibbonBarButton.MenuButtonSize.Large;
            edit["复制课程"].Image = Properties.Resources.rotate_64;
            edit["复制课程"].Enable = false;
            edit["复制课程"].Click += delegate
            {
                CopyClub insert = new CopyClub();
                insert.ShowDialog();
            };

            edit["删除课程"].Size = RibbonBarButton.MenuButtonSize.Large;
            edit["删除课程"].Image = Properties.Resources.health_and_leisure_remove_64;
            edit["删除课程"].Enable = false;
            edit["删除课程"].Click += delegate
            {
                DeleteClub();
            };

            RibbonBarItem totle = ClubAdmin.Instance.RibbonBarItems["资料统计"];
            totle["汇出"].Size = RibbonBarButton.MenuButtonSize.Large;
            totle["汇出"].Image = Properties.Resources.Export_Image;

            totle["汇入"].Size = RibbonBarButton.MenuButtonSize.Large;
            totle["汇入"].Image = Properties.Resources.Import_Image;

            totle["报表"].Size = RibbonBarButton.MenuButtonSize.Large;
            totle["报表"].Image = Properties.Resources.Report;


            totle["汇出"]["汇出课程基本资料"].Enable = Permissions.匯出社團基本資料權限;
            totle["汇出"]["汇出课程基本资料"].Click += delegate
            {
                SmartSchool.API.PlugIn.Export.Exporter exporter = new ExportCLUBData();
                ExportClub wizard = new ExportClub(exporter.Text, exporter.Image);
                exporter.InitializeExport(wizard);
                wizard.ShowDialog();
            };

            totle["汇入"]["汇入课程基本资料"].Enable = Permissions.匯入社團基本資料權限;
            totle["汇入"]["汇入课程基本资料"].Click += delegate
            {
                new ImportCLUBData().Execute();
            };


            totle["报表"]["课程点名单(套表列印)"].Enable = false;
            totle["报表"]["课程点名单(套表列印)"].Click += delegate
            {
                ClubPointsListForm insert = new ClubPointsListForm();
                insert.ShowDialog();
            };

            RibbonBarItem oder = ClubAdmin.Instance.RibbonBarItems["其它"];

            oder["开放选课时间"].Size = RibbonBarButton.MenuButtonSize.Medium;
            oder["开放选课时间"].Image = Properties.Resources.time_frame_refresh_128;
            oder["开放选课时间"].Enable = Permissions.開放選社時間權限;
            oder["开放选课时间"].Click += delegate
            {
                OpenClubJoinDateTime insert = new OpenClubJoinDateTime();
                insert.ShowDialog();
            };

            RibbonBarItem check = ClubAdmin.Instance.RibbonBarItems["检查"];

            check["未选课程学生检查"].Size = RibbonBarButton.MenuButtonSize.Medium;
            check["未选课程学生检查"].Image = Properties.Resources.group_help_64;
            check["未选课程学生检查"].Enable = Permissions.未選社團學生權限;
            check["未选课程学生检查"].Click += delegate
            {
                CheckStudentIsNotInClub insert = new CheckStudentIsNotInClub();
                insert.ShowDialog();
            };

            check["教师评鉴输入进度"].Click += delegate
            {
                new K12.Club.General.Zizhu.Ribbon.CheckClubAssessment().ShowDialog();
            };
            #endregion
            #region 轉出照片

            //ClubAdmin.Instance.RibbonBarItems["照片"]["轉出"].Click += delegate
            //{
            //    foreach (var clubRec in new AccessHelper().Select<CLUBRecord>("uid in (" + string.Join(",", ClubAdmin.Instance.SelectedSource) + ")"))
            //    {
            //        if (clubRec.Photo1 != "")
            //        {
            //            var bytes = Convert.FromBase64String(clubRec.Photo1);
            //            using (var imageFile = new FileStream(@"C:\Users\lelala\Desktop\ischool desktop\zzxx.mhedu.sh.cn\ClubPhotos\Club" + clubRec.UID + "Photo1.png", FileMode.Create))
            //            {
            //                imageFile.Write(bytes, 0, bytes.Length);
            //                imageFile.Flush();
            //                imageFile.Close();
            //            }
            //        }
            //        if (clubRec.Photo2 != "")
            //        {
            //            var bytes = Convert.FromBase64String(clubRec.Photo2);
            //            using (var imageFile = new FileStream(@"C:\Users\lelala\Desktop\ischool desktop\zzxx.mhedu.sh.cn\ClubPhotos\Club" + clubRec.UID + "Photo2.png", FileMode.Create))
            //            {
            //                imageFile.Write(bytes, 0, bytes.Length);
            //                imageFile.Flush();
            //                imageFile.Close();
            //            }
            //        }
            //    }

            //};
            #endregion

            ClubAdmin.Instance.SelectedSourceChanged += delegate
            {
                //是否選擇大於0的社團
                bool SourceCount = (ClubAdmin.Instance.SelectedSource.Count > 0);
                //刪除社團
                bool a = (SourceCount && Permissions.刪除社團權限);
                //ClubAdmin.Instance.ListPaneContexMenu["删除课程"].Enable = a;
                edit["删除课程"].Enable = a;

                //複製社團
                bool b = (SourceCount && Permissions.複製社團權限);
                edit["复制课程"].Enable = b;

                bool h = (SourceCount && Permissions.社團點名單_套表列印權限);
                totle["报表"]["课程点名单(套表列印)"].Enable = h;

                FISCA.Presentation.MotherForm.SetStatusBarMessage("选择「" + ClubAdmin.Instance.SelectedSource.Count + "」个课程");
            };

            Catalog detail1;

            detail1 = RoleAclSource.Instance["学生"]["功能按钮"];
            detail1.Add(new RibbonFeature(Permissions.匯出社團學生名單, "汇出学生选课纪录权限"));

            detail1 = RoleAclSource.Instance["拓展性课程"]["功能按钮"];
            detail1.Add(new RibbonFeature(Permissions.新增社團, "新增课程"));
            detail1.Add(new RibbonFeature(Permissions.複製社團, "复制课程"));
            detail1.Add(new RibbonFeature(Permissions.刪除社團, "删除课程"));
            detail1.Add(new RibbonFeature(Permissions.未選社團學生, "未选课程学生检查"));
            detail1.Add(new RibbonFeature(Permissions.開放選社時間, "开放选课时间"));
            detail1.Add(new RibbonFeature(Permissions.匯出社團基本資料, "汇出课程基本资料权限"));
            detail1.Add(new RibbonFeature(Permissions.匯入社團基本資料, "汇入课程基本资料权限"));

            detail1 = RoleAclSource.Instance["拓展性课程"]["报表"];
            detail1.Add(new RibbonFeature(Permissions.社團點名單_套表列印, "课程点名单(套表列印)"));

            detail1 = RoleAclSource.Instance["拓展性课程"]["资料项目"];
            detail1.Add(new DetailItemFeature(Permissions.社團基本資料, "基本资料"));
            detail1.Add(new DetailItemFeature(Permissions.社團照片, "照片"));
            detail1.Add(new DetailItemFeature(Permissions.社團限制, "限制"));
            detail1.Add(new DetailItemFeature(Permissions.社團參與學生, "学生"));
            detail1.Add(new DetailItemFeature(Permissions.社團幹部, "干部"));

            detail1 = RoleAclSource.Instance["学生"]["资料项目"];
            detail1.Add(new DetailItemFeature(Permissions.學生社團成績_資料項目, "拓展性课程成绩"));
        }

        /// <summary>
        /// 刪除社團學生
        /// </summary>
        static private void DeleteClub()
        {
            DialogResult dr = MsgBox.Show("确认删除所选课团?", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("已删除选择课程：");
                List<CLUBRecord> ClubList = tool._A.Select<CLUBRecord>(UDT_S.PopOneCondition("UID", ClubAdmin.Instance.SelectedSource));

                foreach (CLUBRecord each in ClubList)
                {
                    sb.AppendLine(string.Format("学年度「{0}」学期「{1}」课程名称「{2}」", each.SchoolYear.ToString(), each.Semester.ToString(), each.ClubName));
                }

                List<SCJoin> SCJList = tool._A.Select<SCJoin>(UDT_S.PopOneCondition("ref_club_id", ClubAdmin.Instance.SelectedSource));
                if (SCJList.Count != 0)
                {
                    MsgBox.Show("删除课程必须清空课程参与学生!");
                    return;
                }

                try
                {
                    tool._A.DeletedValues(ClubList);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("课程删除失败!!\n" + ex.Message);
                    SmartSchool.ErrorReporting.ReportingService.ReportException(ex);
                    return;

                }
                FISCA.LogAgent.ApplicationLog.Log("课程", "删除课程", sb.ToString());
                MsgBox.Show("课程删除成功!!");
                ClubEvents.RaiseAssnChanged();
            }
            else
            {
                MsgBox.Show("已中止删除课程操作!!");
            }
        }
    }
}
