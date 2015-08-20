using Campus.DocumentValidator;
using FISCA;
using FISCA.Permission;
using FISCA.Presentation;
using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                ClubAdmin.Instance.AddDetailBulider(new FISCA.Presentation.DetailBulider<ClubStudent_3>());
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
                ExportStudentV2 wizard = new ExportStudentV2(exporter.Text, exporter.Image);
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

            #endregion

            ClubAdmin.Instance.SelectedSourceChanged += delegate
            {
                //是否選擇大於0的社團
                bool SourceCount = (ClubAdmin.Instance.SelectedSource.Count > 0);
                //刪除社團
                bool a = (SourceCount && Permissions.刪除社團權限);
                ClubAdmin.Instance.ListPaneContexMenu["删除课程"].Enable = a;
                edit["删除课程"].Enable = a;

                //複製社團
                bool b = (SourceCount && Permissions.複製社團權限);
                edit["复制课程"].Enable = b;

                bool h = (SourceCount && Permissions.社團點名單_套表列印權限);
                totle["报表"]["课程点名单(套表列印)"].Enable = h;


                FISCA.Presentation.MotherForm.SetStatusBarMessage("选择「" + ClubAdmin.Instance.SelectedSource.Count + "」个课程");
            };

            Catalog detail1;

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
