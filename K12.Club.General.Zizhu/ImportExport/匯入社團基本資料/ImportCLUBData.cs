using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Presentation.Controls;
using Campus.DocumentValidator;
using K12.Data;
using FISCA.DSAUtil;
using System.Xml;
using Campus.ImportZizhu;

namespace K12.Club.General.Zizhu
{
    class ImportCLUBData : ImportWizard
    {
        //設定檔
        private ImportOption mOption;

        CLUBImportBOT Importbot = new CLUBImportBOT();

        Dictionary<string, ImputLog> Log_Dic = new Dictionary<string, ImputLog>();

        public override string GetValidateRule()
        {
            return Properties.Resources.ImportCLUBDataValRule;
        }

        public override string Import(List<Campus.DocumentValidator.IRowStream> Rows)
        {
            if (mOption.Action == ImportAction.InsertOrUpdate)
            {
                List<CLUBRecord> ClubInsertList = new List<CLUBRecord>();
                List<CLUBRecord> ClubUpdateList = new List<CLUBRecord>();

                foreach (IRowStream Row in Rows)
                { //教师名称
                    string SchoolYear = Row.GetValue("学年度");
                    string Semester = Row.GetValue("学期");
                    string CLUBName = Row.GetValue("社团名称");
                    string name = SchoolYear + "," + Semester + "," + CLUBName;

                    if (Importbot.ClubDic.ContainsKey(name)) //更新
                    {
                        CLUBRecord club = Importbot.ClubDic[name];
                        if (!Log_Dic.ContainsKey(club.UID))
                        {
                            ImputLog i_n = new ImputLog();
                            i_n.lo_CLUB = club.CopyExtension();
                            Log_Dic.Add(club.UID, i_n);
                        }

                        Importbot.SetClub(Row, club);
                        ClubUpdateList.Add(club);
                    }
                    else
                    {
                        //新增
                        CLUBRecord club = new CLUBRecord();
                        club.SchoolYear = int.Parse(SchoolYear);
                        club.Semester = int.Parse(Semester);
                        club.ClubName = CLUBName;

                        Importbot.SetClub(Row, club);
                        ClubInsertList.Add(club);
                    }
                }

                if (ClubInsertList.Count > 0)
                {
                    StringBuilder mstrLog1 = new StringBuilder();
                    mstrLog1.AppendLine("新增汇入社团：");
                    foreach (CLUBRecord each in ClubInsertList)
                    {
                        mstrLog1.AppendLine(Importbot.GetLogString(each));
                    }
                    tool._A.InsertValues(ClubInsertList);
                    FISCA.LogAgent.ApplicationLog.Log("社团", "新增汇入", mstrLog1.ToString());
                }

                if (ClubUpdateList.Count > 0)
                {
                    StringBuilder mstrLog2 = new StringBuilder();
                    mstrLog2.AppendLine("更新汇入社团：");
                    foreach (CLUBRecord each in ClubUpdateList)
                    {
                        if (Log_Dic.ContainsKey(each.UID))
                        {
                            Log_Dic[each.UID].New_club = each.CopyExtension();
                            mstrLog2.AppendLine(Importbot.SetLog(Log_Dic[each.UID]));
                        }

                    }
                    tool._A.UpdateValues(ClubUpdateList);
                    FISCA.LogAgent.ApplicationLog.Log("社团", "更新汇入", mstrLog2.ToString());
                }
                ClubEvents.RaiseAssnChanged();
            }

            return "";

        }

        /// <summary>
        /// 準備資料
        /// </summary>
        public override void Prepare(ImportOption Option)
        {
            mOption = Option;

            Importbot.ClubDic = Importbot.GetCLUBDic();

            Importbot.TeacherNameDic = Importbot.GetTeacherDic();
        }

        public override ImportAction GetSupportActions()
        {
            //新增或更新
            return ImportAction.InsertOrUpdate;
        }
    }
}
