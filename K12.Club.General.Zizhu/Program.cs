using Aspose.Cells;
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

            //            K12.Presentation.NLDPanels.Student.RibbonBarItems["资料统计"]["报表"]["拓展性课程"]["打印成绩单"].Enable = false;
            //            K12.Presentation.NLDPanels.Student.RibbonBarItems["资料统计"]["报表"]["拓展性课程"]["打印成绩单"].Click += delegate
            //            {
            //                #region 列印成績單
            //                QueryHelper _Q = new QueryHelper();
            //                List<string> _ids = new List<string>(K12.Presentation.NLDPanels.Student.SelectedSource);
            //                bool fieldMode = false;
            //                Dictionary<Document, string> documents = new Dictionary<Document, string>();

            //                BackgroundWorker bkw = new BackgroundWorker();
            //                bkw.WorkerReportsProgress = true;
            //                bkw.RunWorkerCompleted += delegate
            //                {
            //                    FISCA.Presentation.MotherForm.SetStatusBarMessage("拓展性课程学生成绩单产生完成。", 100);
            //                    List<string> files = new List<string>();
            //                    foreach (var doc in documents.Keys)
            //                    {

            //                        SaveFileDialog save = new SaveFileDialog();
            //                        save.Title = "另存新档";
            //                        save.FileName = documents[doc];
            //                        save.Filter = "Word档案 (*.docx)|*.docx|Word档案 (*.doc)|*.doc|所有档案 (*.*)|*.*";

            //                        if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //                        {
            //                            try
            //                            {
            //                                doc.Save(save.FileName);
            //                                files.Add(save.FileName);
            //                            }
            //                            catch
            //                            {
            //                                MessageBox.Show("档案储存失败。");
            //                            }
            //                        }
            //                    }
            //                    foreach (var file in files)
            //                    {
            //                        System.Diagnostics.Process.Start(file);
            //                    }
            //                };
            //                bkw.ProgressChanged += delegate(object sender, ProgressChangedEventArgs e)
            //                {
            //                    FISCA.Presentation.MotherForm.SetStatusBarMessage("拓展性课程学生成绩单产生中...", e.ProgressPercentage);
            //                };

            //                bkw.DoWork += delegate
            //                {
            //                    Dictionary<string, DataRow> dicStudentRow = new Dictionary<string, DataRow>();
            //                    DataTable mergeDT = new DataTable();

            //                    mergeDT.Columns.Add("学年度");
            //                    mergeDT.Columns.Add("学期");
            //                    mergeDT.Columns.Add("姓名");
            //                    mergeDT.Columns.Add("学籍号");
            //                    mergeDT.Columns.Add("年级");
            //                    mergeDT.Columns.Add("班级");
            //                    mergeDT.Columns.Add("学号");

            //                    DataTable dt = _Q.Select(string.Format(@"
            //SELECT 
            //    $k12.clubrecord.universal.school_year,
            //    $k12.clubrecord.universal.semester,
            //    $k12.clubrecord.universal.club_category,
            //    $k12.clubrecord.universal.club_name,
            //    $k12.clubrecord.universal.club_number,
            //    phase,
            //    student.id,
            //    student.name,
            //    student.student_number, 
            //    student.seat_no,
            //    class.class_name as classname,
            //    class.grade_year,
            //    asmS.detial as detialS,
            //    asmT.detial as detialT
            //FROM
            //    student
            //    LEFT OUTER JOIN class on student.ref_class_id = class.id
            //    LEFT OUTER JOIN $k12.scjoin.universal on student.id = $k12.scjoin.universal.ref_student_id::bigint
            //    LEFT OUTER JOIN $k12.clubrecord.universal on $k12.clubrecord.universal.uid = $k12.scjoin.universal.ref_club_id::bigint
            //    LEFT OUTER JOIN $ischool.club.assessment as asmS on asmS.ref_student_id = student.id and asmS.ref_club_id = $k12.clubrecord.universal.uid and asmS.assessment_type='student'
            //    LEFT OUTER JOIN $ischool.club.assessment as asmT on asmT.ref_student_id = student.id and asmT.ref_club_id = $k12.clubrecord.universal.uid and asmT.assessment_type='teacher'
            //WHERE
            //    student.id in ({0}) and $k12.clubrecord.universal.school_year = {1} and $k12.clubrecord.universal.semester = {2}
            //ORDER BY class.grade_year, class.display_order, student.seat_no, phase
            //                        ", string.Join(",", _ids), K12.Data.School.DefaultSchoolYear, K12.Data.School.DefaultSemester));
            //                    var count = 0;
            //                    foreach (DataRow row in dt.Rows)
            //                    {
            //                        count++;
            //                        if (!dicStudentRow.ContainsKey("" + row["id"]))
            //                        {
            //                            var stuRow = mergeDT.Rows.Add();
            //                            stuRow[mergeDT.Columns.IndexOf("学年度")] = "" + row["school_year"];
            //                            stuRow[mergeDT.Columns.IndexOf("学期")] = "" + row["semester"];
            //                            stuRow[mergeDT.Columns.IndexOf("姓名")] = "" + row["name"];
            //                            stuRow[mergeDT.Columns.IndexOf("学籍号")] = "" + row["student_number"];
            //                            stuRow[mergeDT.Columns.IndexOf("年级")] = "" + row["grade_year"];
            //                            stuRow[mergeDT.Columns.IndexOf("班级")] = "" + row["classname"];
            //                            stuRow[mergeDT.Columns.IndexOf("学号")] = "" + row["seat_no"];
            //                            dicStudentRow.Add("" + row["id"], stuRow);
            //                        }
            //                        var studentRow = dicStudentRow["" + row["id"]];


            //                        var level = "阶段" + row["phase"];
            //                        {
            //                            var key = level + "_课程类别";
            //                            if (!mergeDT.Columns.Contains(key))
            //                                mergeDT.Columns.Add(key);
            //                            studentRow[mergeDT.Columns.IndexOf(key)] = "" + row["club_category"];
            //                        }
            //                        {
            //                            var key = level + "_课程名称";
            //                            if (!mergeDT.Columns.Contains(key))
            //                                mergeDT.Columns.Add(key);
            //                            studentRow[mergeDT.Columns.IndexOf(key)] = "" + row["club_name"];
            //                        }
            //                        {
            //                            var key = level + "_课程代码";
            //                            if (!mergeDT.Columns.Contains(key))
            //                                mergeDT.Columns.Add(key);
            //                            studentRow[mergeDT.Columns.IndexOf(key)] = "" + row["club_number"]; ;
            //                        }

            //                        var doc = new XmlDocument();
            //                        if ("" + row["detialS"] != "")
            //                        {
            //                            doc.LoadXml("" + row["detialS"]);
            //                            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            //                            {
            //                                var key = level + "_学生自评_" + node.Name;
            //                                var value = "" + node.InnerText;
            //                                if (!mergeDT.Columns.Contains(key))
            //                                    mergeDT.Columns.Add(key);//Q1評語
            //                                studentRow[mergeDT.Columns.IndexOf(key)] = value;
            //                            }
            //                        }
            //                        if ("" + row["detialT"] != "")
            //                        {
            //                            doc.LoadXml("" + row["detialT"]);
            //                            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            //                            {
            //                                var key = level + "_教师评鉴_" + node.Name;
            //                                var value = "" + node.InnerText;
            //                                if (!mergeDT.Columns.Contains(key))
            //                                    mergeDT.Columns.Add(key);//Q1評語
            //                                studentRow[mergeDT.Columns.IndexOf(key)] = value;
            //                            }
            //                        }
            //                        bkw.ReportProgress(100 * count / dt.Rows.Count);
            //                    }

            //                    if (fieldMode)
            //                    {
            //                        Document doc = new Document();
            //                        DocumentBuilder bu = new DocumentBuilder(doc);
            //                        bu.MoveToDocumentStart();
            //                        bu.CellFormat.Borders.LineStyle = LineStyle.Single;
            //                        bu.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
            //                        Table table = bu.StartTable();
            //                        foreach (DataColumn col in mergeDT.Columns)
            //                        {

            //                            bu.InsertCell();
            //                            bu.CellFormat.Width = 15;
            //                            bu.InsertField("MERGEFIELD " + col.Caption + @" \* MERGEFORMAT", "«.»");
            //                            bu.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            //                            bu.InsertCell();
            //                            bu.CellFormat.Width = 125;
            //                            bu.Write(col.Caption);
            //                            bu.ParagraphFormat.Alignment = ParagraphAlignment.Left;

            //                            bu.EndRow();
            //                        }
            //                        //table.AllowAutoFit = false;
            //                        bu.EndTable();
            //                        documents.Add(doc, "拓展性课程学生成绩单合并栏位表.doc");
            //                    }
            //                    else
            //                    {
            //                        var doc = new Document(new MemoryStream(Properties.Resources.華東師範大學附屬紫竹小學拓展性課程成績單));
            //                        //        //合併，儲存
            //                        doc.MailMerge.Execute(mergeDT);
            //                        doc.MailMerge.DeleteFields();

            //                        documents.Add(doc, "拓展性课程学生成绩单.doc");
            //                    }
            //                };
            //                bkw.RunWorkerAsync();
            //                FISCA.Presentation.MotherForm.SetStatusBarMessage("拓展性课程学生成绩单产生中...", 0);
            //                #endregion
            //            };
            //            K12.Presentation.NLDPanels.Student.SelectedSourceChanged += delegate
            //            {
            //                K12.Presentation.NLDPanels.Student.RibbonBarItems["资料统计"]["报表"]["拓展性课程"]["打印成绩单"].Enable = K12.Presentation.NLDPanels.Student.SelectedSource.Count > 0;
            //            };

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

            totle["报表"]["打印学习过程记录表"].Enable = false;
            totle["报表"]["打印学习过程记录表"].Click += delegate
            {
                MotherForm.SetStatusBarMessage("学习过程记录表產生中...", 0);
                #region 打印成绩单
                BackgroundWorker bkw = new BackgroundWorker();
                bkw.WorkerReportsProgress = true;
                bkw.ProgressChanged += delegate(object sender, ProgressChangedEventArgs e)
                {
                    MotherForm.SetStatusBarMessage("学习过程记录表產生中...", e.ProgressPercentage);
                };
                bkw.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    Dictionary<string, DataRow> scJoinRow = new Dictionary<string, DataRow>();
                    Dictionary<string, DataRow> teacherAssessmentRow = new Dictionary<string, DataRow>();
                    Dictionary<string, DataRow> studentAssessmentRow = new Dictionary<string, DataRow>();
                    Dictionary<string, List<DataRow>> mateAssessmentRow = new Dictionary<string, List<DataRow>>();
                    QueryHelper queryHelper = new QueryHelper();
                    var rows = queryHelper.Select(string.Format(@"
SELECT $k12.scjoin.universal.score, 
	clevel.C优秀, 
	clevel.C良好, 
	clevel.C合格, 
	clevel.C需努力, 
	domainSum.SUM文学与艺术,
	domainSum.SUM社会与生活,
	domainSum.SUM运动与生命,
	domainSum.SUM科技与创新,
	domainSum.SUM世界与未来,
	$ischool.club.assessment.* 
FROM $k12.scjoin.universal
	LEFT OUTER JOIN $ischool.club.assessment on $k12.scjoin.universal.ref_club_id::bigint = $ischool.club.assessment.ref_club_id AND $k12.scjoin.universal.ref_student_id::bigint = $ischool.club.assessment.ref_student_id  
	LEFT OUTER JOIN ( 
		SELECT ref_club_id, phase,  
			COUNT(CASE (CASE WHEN score is null THEN 0 WHEN score::int <18 THEN 0 WHEN score::int < 24 THEN 1 WHEN score::int < 27 THEN 2 ELSE 3 END) WHEN 3 THEN 1 END ) as C优秀, 
			COUNT(CASE (CASE WHEN score is null THEN 0 WHEN score::int <18 THEN 0 WHEN score::int < 24 THEN 1 WHEN score::int < 27 THEN 2 ELSE 3 END) WHEN 2 THEN 1 END ) as C良好, 
			COUNT(CASE (CASE WHEN score is null THEN 0 WHEN score::int <18 THEN 0 WHEN score::int < 24 THEN 1 WHEN score::int < 27 THEN 2 ELSE 3 END) WHEN 1 THEN 1 END ) as C合格, 
			COUNT(CASE (CASE WHEN score is null THEN 0 WHEN score::int <18 THEN 0 WHEN score::int < 24 THEN 1 WHEN score::int < 27 THEN 2 ELSE 3 END) WHEN 0 THEN 1 END ) as C需努力 
		FROM $k12.scjoin.universal 
		GROUP BY ref_club_id, phase 
	) as clevel on clevel.ref_club_id = $k12.scjoin.universal.ref_club_id AND clevel.phase = $k12.scjoin.universal.phase 
	LEFT OUTER JOIN (
		SELECT $k12.scjoin.universal.ref_student_id,
			SUM( CASE LEFT(club_domain, 5) WHEN '文学与艺术' THEN  (CASE WHEN score is null THEN 0 WHEN score::int <18 THEN 0 WHEN score::int < 24 THEN 1 WHEN score::int < 27 THEN 2 ELSE 3 END * CASE full_phase WHEN true THEN 2 ELSE 1 END) END ) as SUM文学与艺术,
			SUM( CASE LEFT(club_domain, 5) WHEN '社会与生活' THEN  (CASE WHEN score is null THEN 0 WHEN score::int <18 THEN 0 WHEN score::int < 24 THEN 1 WHEN score::int < 27 THEN 2 ELSE 3 END * CASE full_phase WHEN true THEN 2 ELSE 1 END) END ) as SUM社会与生活,
			SUM( CASE LEFT(club_domain, 5) WHEN '运动与生命' THEN  (CASE WHEN score is null THEN 0 WHEN score::int <18 THEN 0 WHEN score::int < 24 THEN 1 WHEN score::int < 27 THEN 2 ELSE 3 END * CASE full_phase WHEN true THEN 2 ELSE 1 END) END ) as SUM运动与生命,
			SUM( CASE LEFT(club_domain, 5) WHEN '科技与创新' THEN  (CASE WHEN score is null THEN 0 WHEN score::int <18 THEN 0 WHEN score::int < 24 THEN 1 WHEN score::int < 27 THEN 2 ELSE 3 END * CASE full_phase WHEN true THEN 2 ELSE 1 END) END ) as SUM科技与创新,
			SUM( CASE LEFT(club_domain, 5) WHEN '世界与未来' THEN  (CASE WHEN score is null THEN 0 WHEN score::int <18 THEN 0 WHEN score::int < 24 THEN 1 WHEN score::int < 27 THEN 2 ELSE 3 END * CASE full_phase WHEN true THEN 2 ELSE 1 END) END ) as SUM世界与未来
		FROM $k12.scjoin.universal LEFT OUTER JOIN $k12.clubrecord.universal on $k12.scjoin.universal.ref_club_id::bigint = $k12.clubrecord.universal.uid
		WHERE $k12.scjoin.universal.grade_year is not null 
			AND score is not null
		GROUP BY $k12.scjoin.universal.ref_student_id
	) as domainSum on domainSum.ref_student_id = $k12.scjoin.universal.ref_student_id
WHERE $ischool.club.assessment.ref_club_id in ('" + string.Join("','", ClubAdmin.Instance.SelectedSource) + "')"));
                    foreach (DataRow row in rows.Rows)
                    {
                        var ref_student_id = "" + row["ref_student_id"];
                        var ref_club_id = "" + row["ref_club_id"];
                        var assessment_type = "" + row["assessment_type"];

                        var key = ref_club_id + "^" + ref_student_id;
                        if (!scJoinRow.ContainsKey(key))
                            scJoinRow.Add(key, row);

                        if (assessment_type == "teacher")
                        {
                            teacherAssessmentRow.Add(key, row);
                        }
                        if (assessment_type == "student")
                        {
                            studentAssessmentRow.Add(key, row);
                        }
                        if (assessment_type == "mate")
                        {
                            if (!mateAssessmentRow.ContainsKey(key))
                                mateAssessmentRow.Add(key, new List<DataRow>());
                            mateAssessmentRow[key].Add(row);
                        }
                    }

                    SCJoinDataLoad crM1 = new SCJoinDataLoad(1);
                    SCJoinDataLoad crM2 = new SCJoinDataLoad(2);

                    DataTable table = new DataTable();
                    table.Columns.Add("学校名称");
                    table.Columns.Add("课程名称");
                    table.Columns.Add("学年度");
                    table.Columns.Add("学期");

                    table.Columns.Add("上课地点");
                    table.Columns.Add("课程类型");
                    table.Columns.Add("课程领域");
                    table.Columns.Add("指导老师1");
                    table.Columns.Add("指导老师2");
                    table.Columns.Add("指导老师3");

                    table.Columns.Add("打印日期");

                    table.Columns.Add("班级");
                    table.Columns.Add("学号");
                    table.Columns.Add("学籍号");
                    table.Columns.Add("姓名");

                    table.Columns.Add("总体评价(分数)");
                    table.Columns.Add("总体评价(文字)");

                    table.Columns.Add("C优秀");
                    table.Columns.Add("C良好");
                    table.Columns.Add("C合格");
                    table.Columns.Add("C需努力");

                    table.Columns.Add("SUM文学与艺术");
                    table.Columns.Add("SUM社会与生活");
                    table.Columns.Add("SUM运动与生命");
                    table.Columns.Add("SUM科技与创新");
                    table.Columns.Add("SUM世界与未来");

                    foreach (var field in new string[] { "家长评价", "感想", "继续学习", "学习动力", "学习成果", "合作分享", "探究兴趣", "学习态度" })
                    {
                        table.Columns.Add(field);
                    }

                    foreach (var field in new string[] { "参与度", "合作力", "实效性" })
                    {
                        table.Columns.Add(field);
                    }

                    foreach (var field in new string[] { "参与度.出勤率", "参与度.交流表达", "实效性.作业完成", "实效性.作品呈现", "学习力1", "学习力2", "学习力1Title", "学习力2Title", "教师的话" })
                    {
                        table.Columns.Add(field);
                    }
                    //table.Columns.Add("雷达图", typeof(byte[]));
                    //table.Columns.Add("雷达图", typeof(System.Drawing.Bitmap));
                    table.Columns.Add("雷达图", typeof(System.Windows.Forms.DataVisualization.Charting.Chart));

                    int progressCount = 0;
                    foreach (string clubID in crM1.CLUBRecordDic.Keys)
                    {
                        bkw.ReportProgress(++progressCount * 100 / crM1.CLUBRecordDic.Count);
                        foreach (SCJoinDataLoad crM in new SCJoinDataLoad[] { crM1, crM2 })
                        {
                            //社團資料
                            CLUBRecord clubRec = crM.CLUBRecordDic[clubID];
                            foreach (K12.Data.StudentRecord studentRec in crM.ClubByStudentList[clubID])
                            {
                                var key = "" + clubRec.UID + "^" + studentRec.ID;
                                if (scJoinRow.ContainsKey(key))
                                {
                                    //沒有成績不印
                                    if (("" + scJoinRow[key]["score"]) == "")
                                        continue;
                                    Dictionary<string, decimal> radarValues = new Dictionary<string, decimal>();
                                    DataRow row = table.NewRow();
                                    row["学校名称"] = K12.Data.School.ChineseName;
                                    row["课程名称"] = clubRec.ClubName;
                                    row["学年度"] = clubRec.SchoolYear;
                                    row["学期"] = clubRec.Semester;

                                    row["上课地点"] = clubRec.Location;
                                    row["课程类型"] = clubRec.ClubCategory;
                                    row["课程领域"] = clubRec.Domain.Length >= 5 ? clubRec.Domain.Substring(0, 5) : clubRec.Domain;
                                    if (crM.TeacherDic.ContainsKey(clubRec.RefTeacherID))
                                    {
                                        row["指导老师1"] = crM.TeacherDic[clubRec.RefTeacherID].Name;
                                    }
                                    if (crM.TeacherDic.ContainsKey(clubRec.RefTeacherID2))
                                    {
                                        row["指导老师2"] = crM.TeacherDic[clubRec.RefTeacherID2].Name;
                                    }
                                    if (crM.TeacherDic.ContainsKey(clubRec.RefTeacherID3))
                                    {
                                        row["指导老师3"] = crM.TeacherDic[clubRec.RefTeacherID3].Name;
                                    }

                                    row["打印日期"] = DateTime.Today.ToShortDateString();

                                    row["班级"] = studentRec.Class != null ? studentRec.Class.Name : "";
                                    row["学号"] = studentRec.SeatNo.HasValue ? studentRec.SeatNo.Value.ToString() : "";
                                    row["姓名"] = studentRec.Name;
                                    row["学籍号"] = studentRec.StudentNumber;

                                    var score = int.Parse("" + scJoinRow[key]["score"]);
                                    row["总体评价(分数)"] = score;
                                    row["总体评价(文字)"] = score >= 27 ? "优秀" : score >= 24 ? "良好" : score >= 18 ? "合格" : "需努力";

                                    foreach (var field in new string[] { "C优秀", "C良好", "C合格", "C需努力", "SUM文学与艺术", "SUM社会与生活", "SUM运动与生命", "SUM科技与创新", "SUM世界与未来" })
                                    {
                                        row[field] = "" + scJoinRow[key][field] == "" ? "--" : scJoinRow[key][field];
                                    }



                                    if (teacherAssessmentRow.ContainsKey(key))
                                    {
                                        XmlDocument doc = new XmlDocument();
                                        doc.LoadXml("" + teacherAssessmentRow[key]["detial"]);
                                        foreach (var field in new string[] { "参与度.出勤率", "参与度.交流表达", "实效性.作业完成", "实效性.作品呈现" })
                                        {
                                            XmlElement ele = doc.DocumentElement.SelectSingleNode(field) as XmlElement;
                                            if (ele != null)
                                            {
                                                row[field] = ele.InnerText;
                                                //radarValues
                                            }
                                        }

                                        switch ("" + row["课程领域"])
                                        {
                                            case "文学与艺术":
                                                row["学习力1Title"] = "审美力";
                                                row["学习力2Title"] = "表现力";
                                                {
                                                    XmlElement ele = doc.DocumentElement.SelectSingleNode("学习力.审美力") as XmlElement;
                                                    if (ele != null)
                                                        row["学习力1"] = ele.InnerText;
                                                }
                                                {
                                                    XmlElement ele = doc.DocumentElement.SelectSingleNode("学习力.表现力") as XmlElement;
                                                    if (ele != null)
                                                        row["学习力2"] = ele.InnerText;
                                                }
                                                break;

                                            case "社会与生活":
                                                row["学习力1Title"] = "情趣性";
                                                row["学习力2Title"] = "实践力";
                                                {
                                                    XmlElement ele = doc.DocumentElement.SelectSingleNode("学习力.情趣性") as XmlElement;
                                                    if (ele != null)
                                                        row["学习力1"] = ele.InnerText;
                                                }
                                                {
                                                    XmlElement ele = doc.DocumentElement.SelectSingleNode("学习力.实践力") as XmlElement;
                                                    if (ele != null)
                                                        row["学习力2"] = ele.InnerText;
                                                }
                                                break;

                                            case "运动与生命":
                                                row["学习力1Title"] = "健康度";
                                                row["学习力2Title"] = "调适力";
                                                {
                                                    XmlElement ele = doc.DocumentElement.SelectSingleNode("学习力.健康度") as XmlElement;
                                                    if (ele != null)
                                                        row["学习力1"] = ele.InnerText;
                                                }
                                                {
                                                    XmlElement ele = doc.DocumentElement.SelectSingleNode("学习力.调适力") as XmlElement;
                                                    if (ele != null)
                                                        row["学习力2"] = ele.InnerText;
                                                }
                                                break;

                                            case "科技与创新":
                                                row["学习力1Title"] = "思维度";
                                                row["学习力2Title"] = "创造力";
                                                {
                                                    XmlElement ele = doc.DocumentElement.SelectSingleNode("学习力.思维度") as XmlElement;
                                                    if (ele != null)
                                                        row["学习力1"] = ele.InnerText;
                                                }
                                                {
                                                    XmlElement ele = doc.DocumentElement.SelectSingleNode("学习力.创造力") as XmlElement;
                                                    if (ele != null)
                                                        row["学习力2"] = ele.InnerText;
                                                }
                                                break;

                                            case "世界与未来":
                                                row["学习力1Title"] = "责任心";
                                                row["学习力2Title"] = "自信力";
                                                {
                                                    XmlElement ele = doc.DocumentElement.SelectSingleNode("学习力.责任心") as XmlElement;
                                                    if (ele != null)
                                                        row["学习力1"] = ele.InnerText;
                                                }
                                                {
                                                    XmlElement ele = doc.DocumentElement.SelectSingleNode("学习力.自信力") as XmlElement;
                                                    if (ele != null)
                                                        row["学习力2"] = ele.InnerText;
                                                }
                                                break;
                                        }

                                        {

                                            XmlElement ele = doc.DocumentElement.SelectSingleNode("教师的话") as XmlElement;
                                            if (ele != null)
                                                row["教师的话"] = ele.InnerText;
                                        }
                                        foreach (var field in new string[] { "参与度.出勤率", "参与度.交流表达", "实效性.作业完成", "实效性.作品呈现" })
                                        {
                                            if (("" + row[field]).StartsWith("A"))
                                                radarValues.Add(field.Split('.')[1], 3);
                                            else if (("" + row[field]).StartsWith("B"))
                                                radarValues.Add(field.Split('.')[1], 2);
                                            else if (("" + row[field]).StartsWith("C"))
                                                radarValues.Add(field.Split('.')[1], 1);
                                            else
                                                radarValues.Add(field.Split('.')[1], 0);
                                        }
                                        foreach (var field in new string[] { "学习力1", "学习力2" })
                                        {
                                            if (("" + row[field]).StartsWith("A"))
                                                radarValues.Add("" + row[field + "Title"], 3);
                                            else if (("" + row[field]).StartsWith("B"))
                                                radarValues.Add("" + row[field + "Title"], 2);
                                            else if (("" + row[field]).StartsWith("C"))
                                                radarValues.Add("" + row[field + "Title"], 1);
                                            else
                                                radarValues.Add("" + row[field + "Title"], 0);
                                        }
                                    }

                                    if (studentAssessmentRow.ContainsKey(key))
                                    {
                                        XmlDocument doc = new XmlDocument();
                                        doc.LoadXml("" + studentAssessmentRow[key]["detial"]);

                                        foreach (var field in new string[] { "家长评价", "感想", "继续学习", "学习动力", "学习成果", "合作分享", "探究兴趣", "学习态度" })
                                        {
                                            XmlElement ele = doc.DocumentElement.SelectSingleNode(field) as XmlElement;
                                            if (ele != null)
                                                row[field] = ele.InnerText;
                                        }
                                    }

                                    if (mateAssessmentRow.ContainsKey(key))
                                    {
                                        XmlDocument doc = new XmlDocument();
                                        foreach (var field in new string[] { "参与度", "合作力", "实效性" })
                                        {
                                            decimal count = 0;
                                            decimal sum = 0;
                                            foreach (var r in mateAssessmentRow[key])
                                            {
                                                doc.LoadXml("" + r["detial"]);
                                                XmlElement ele = doc.DocumentElement.SelectSingleNode(field) as XmlElement;
                                                switch (ele.InnerText)
                                                {
                                                    case "优秀":
                                                        sum += 3;
                                                        break;
                                                    case "良好":
                                                        sum += 2;
                                                        break;
                                                    case "合格":
                                                        sum += 1;
                                                        break;
                                                }
                                                count++;
                                            }
                                            switch ("" + (int)Math.Round(sum / count, 0, MidpointRounding.AwayFromZero))
                                            {
                                                case "3":
                                                    row[field] = "优秀";
                                                    break;
                                                case "2":
                                                    row[field] = "良好";
                                                    break;
                                                default:
                                                case "1":
                                                    row[field] = "合格";
                                                    break;
                                            }
                                            radarValues.Add(field, sum / count);
                                        }
                                    }
                                    {
                                        var Chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
                                        Chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series());

                                        Chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Radar;
                                        Chart1.Series[0].Points.DataBindXY(radarValues.Keys, radarValues.Values);

                                        // Set radar chart style (Area, Line or Marker)
                                        Chart1.Series[0]["RadarDrawingStyle"] = "Area";
                                        // Set circular area drawing style (Circle or Polygon)
                                        Chart1.Series[0]["AreaDrawingStyle"] = "Polygon";
                                        // Set labels style (Auto, Horizontal, Circular or Radial)
                                        Chart1.Series[0]["CircularLabelsStyle"] = "Auto";

                                        //Chart1.Series[0]["Font"] = "SimSun";
                                        Chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
                                        Chart1.Legends.Add(new System.Windows.Forms.DataVisualization.Charting.Legend());
                                        Chart1.Legends[0].Enabled = false;

                                        switch ("" + row["课程领域"])
                                        //switch (new string[] { "文学与艺术", "社会与生活", "运动与生命", "科技与创新", "世界与未来" }[new Random().Next(4)])
                                        {
                                            case "文学与艺术":
                                                Chart1.Series[0].Color = System.Drawing.Color.FromArgb(168, 244, 164, 37);
                                                break;

                                            case "社会与生活":
                                                Chart1.Series[0].Color = System.Drawing.Color.FromArgb(168, 219, 210, 61);
                                                break;

                                            case "运动与生命":
                                                Chart1.Series[0].Color = System.Drawing.Color.FromArgb(168, 137, 200, 48);
                                                break;

                                            case "科技与创新":
                                                Chart1.Series[0].Color = System.Drawing.Color.FromArgb(168, 86, 208, 198);
                                                break;

                                            case "世界与未来":
                                                Chart1.Series[0].Color = System.Drawing.Color.FromArgb(168, 0, 124, 229);
                                                break;
                                        }

                                        var chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
                                        chartArea.Position.X = 0;
                                        chartArea.Position.Y = 0;
                                        chartArea.Position.Width = 100;
                                        chartArea.Position.Height = 100;

                                        chartArea.AxisX.LabelAutoFitMinFontSize = 9;
                                        chartArea.AxisX.LabelAutoFitMaxFontSize = 9;
                                        chartArea.AxisX.LabelStyle.Font = new System.Drawing.Font("SimSun", 9, System.Drawing.FontStyle.Regular);
                                        chartArea.AxisX.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(38, 38, 38);

                                        chartArea.AxisX.IsMarginVisible = false;

                                        chartArea.AxisY.Maximum = 3;
                                        chartArea.AxisY.Interval = 1;

                                        chartArea.AxisY.LabelStyle.Enabled = false;
                                        chartArea.AxisY2.MajorGrid.Enabled = false;


                                        Chart1.ChartAreas.Add(chartArea);

                                        row["雷达图"] = Chart1;


                                    }
                                    table.Rows.Add(row);

                                }
                            }
                        }

                    }

                    Document pageOne = new Document(new MemoryStream(Properties.Resources.紫竹成績單樣板));
                    pageOne.MailMerge.FieldMergingCallback = new 雷达图HandleMergeImageFieldFromBlob();
                    pageOne.MailMerge.Execute(table);
                    pageOne.MailMerge.DeleteFields();
                    e.Result = pageOne;
                };
                bkw.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
                {
                    if (e.Cancelled)
                    {
                        MsgBox.Show("作业已被中止!!");
                    }
                    else
                    {
                        if (e.Error == null)
                        {
                            MotherForm.SetStatusBarMessage("学习过程记录表產生完成", 100);
                            Document inResult = (Document)e.Result;

                            try
                            {
                                SaveFileDialog SaveFileDialog1 = new SaveFileDialog();

                                SaveFileDialog1.Filter = "Word (*.doc)|*.doc|所有档案 (*.*)|*.*";
                                SaveFileDialog1.FileName = "学习过程记录表";

                                if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                                {
                                    inResult.Save(SaveFileDialog1.FileName);
                                    System.Diagnostics.Process.Start(SaveFileDialog1.FileName);
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
                        }
                        else
                        {
                            MsgBox.Show("打印学习过程记录表发生错误\n" + e.Error.Message);
                        }
                    }
                };
                bkw.RunWorkerAsync();
                #endregion
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

                totle["报表"]["打印学习过程记录表"].Enable = SourceCount && FISCA.Permission.UserAcl.Current["AA262CBE-96AD-45DE-A4E0-FFACA7632A1E"].Executable;

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

            detail1 = RoleAclSource.Instance["拓展性课程"]["报表"];
            detail1.Add(new RibbonFeature("AA262CBE-96AD-45DE-A4E0-FFACA7632A1E", "打印学习过程记录表"));

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



        public class 雷达图HandleMergeImageFieldFromBlob : Aspose.Words.Reporting.IFieldMergingCallback
        {
            void Aspose.Words.Reporting.IFieldMergingCallback.FieldMerging(Aspose.Words.Reporting.FieldMergingArgs e)
            {
                // Do nothing.
                //if (e.FieldName == "雷达图")
                //{
                //    System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)e.FieldValue;
                //    // The field value is a byte array, just cast it and create a stream on it.
                //    MemoryStream imageStream = new MemoryStream();
                //    bmp.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                //    // Now the mail merge engine will retrieve the image from the stream.
                //    DocumentBuilder builder = new DocumentBuilder(e.Document);
                //    builder.MoveTo(e.Field.Start);
                //    builder.InsertImage(bmp);
                //    e.Text = "";
                //}
            }

            /// <summary>
            /// This is called when mail merge engine encounters Image:XXX merge field in the document.
            /// You have a chance to return an Image object, file name or a stream that contains the image.
            /// </summary>
            void Aspose.Words.Reporting.IFieldMergingCallback.ImageFieldMerging(Aspose.Words.Reporting.ImageFieldMergingArgs e)
            {
                if (e.FieldName == "雷达图")
                {
                    var chart = (System.Windows.Forms.DataVisualization.Charting.Chart)e.FieldValue;
                    chart.Size = new System.Drawing.Size(240, 190);
                    MemoryStream imageStream = new MemoryStream();
                    chart.SaveImage(imageStream, System.Drawing.Imaging.ImageFormat.Png);
                    e.ImageStream = imageStream;
                }
            }
        }
    }
}
