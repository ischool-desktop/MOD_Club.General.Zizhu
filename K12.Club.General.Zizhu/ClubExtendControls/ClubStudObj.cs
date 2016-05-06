using K12.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12.Club.General.Zizhu
{
    /// <summary>
    /// 處理修課學生的OBJ
    /// </summary>
    class ClubStudObj
    {
        /// <summary>
        /// 已在目前社團之學生ID
        /// </summary>
        public List<string> ReMoveTemp = new List<string>();

        /// <summary>
        /// 重覆參與其它社團參與記錄
        /// </summary>
        public List<SCJoin> ReDoubleTemp = new List<SCJoin>();

        /// <summary>
        /// 由待處理可新增的學生
        /// </summary>
        public List<string> InsertList = new List<string>();

        /// <summary>
        /// LogStudent
        /// </summary>
        public Dictionary<string, StudentRecord> LogStudentList = new Dictionary<string, StudentRecord>();

        /// <summary>
        /// 取得新增學生的記錄
        /// </summary>
        public void GetLogStudent()
        {
            if (InsertList.Count != 0)
            {
                LogStudentList = new Dictionary<string, StudentRecord>();
                List<StudentRecord> studList = Student.SelectByIDs(InsertList);
                foreach (StudentRecord sr in studList)
                {
                    if (!LogStudentList.ContainsKey(sr.ID))
                    {
                        LogStudentList.Add(sr.ID, sr);
                    }
                }
            }
        }
        /// <summary>
        /// 給我 重覆參與之社團ID
        /// </summary>
        public List<string> GetClubID()
        {
            return ReDoubleTemp.Select(x => x.RefClubID).ToList();
        }

        /// <summary>
        /// 給我 重覆參與社團之學生ID
        /// </summary>
        /// <returns></returns>
        public List<string> GetStudentID()
        {
            return ReDoubleTemp.Select(x => x.RefStudentID).ToList();
        }

        /// <summary>
        /// 排除已存在於本社團之學生
        /// </summary>
        public void CheckTempStudentInCourse(List<string> IsSaft, Dictionary<string, List<SCJoin>> SCJoin_Dic, CLUBRecord _CLUBRecord, int phase)
        {
            //檢查已經加入本階段修課的學生
            foreach (string each in IsSaft)
            {
                if (!SCJoin_Dic.ContainsKey(each))
                {
                    //可加入社團之學生
                    InsertList.Add(each);
                }
                else
                {
                    SCJoin scjK = new SCJoin();
                    foreach (SCJoin scj in SCJoin_Dic[each])
                    {
                        if (string.IsNullOrEmpty(scjK.UID))
                        {
                            scjK = scj;
                            continue;
                        }

                        //如果是相同階段別之學生
                        if (scjK.Phase == scj.Phase)
                        {
                            //重覆加入社團之學生
                            ReMoveTemp.Add(each);
                        }
                    }

                }
            }
            //取得可加入學生的社團參與記錄(所有學期)
            List<SCJoin> scjList = tool._A.Select<SCJoin>("ref_student_id in ('" + string.Join("','", InsertList) + "')");

            if (scjList.Count != 0)
            {
                Dictionary<string, CLUBRecord> clubDic = GetDistinctClub(scjList);

                foreach (SCJoin each in scjList)
                {    //增加判斷已不存在的社團
                    if (clubDic.ContainsKey(each.RefClubID))
                    {
                        //學年度學期相同
                        if (clubDic[each.RefClubID].SchoolYear == _CLUBRecord.SchoolYear && clubDic[each.RefClubID].Semester == _CLUBRecord.Semester)
                        {
                            //同階段已加入
                            if (each.Phase == phase)
                                ReDoubleTemp.Add(each);
                            //已加入長課程，或者已加入其他課程但是要加入長課程
                            else if (clubDic[each.RefClubID].FullPhase == true || _CLUBRecord.FullPhase == true)

                                ReDoubleTemp.Add(each);

                            ////判斷待處理學生,是否已經有社團參與記錄(SCJoin)
                            //if (IsSaft.Contains(each.RefStudentID))
                            //{
                            //    //有記錄則先剔除
                            //    IsSaft.Remove(each.RefStudentID);
                            //}
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 由社團參與記錄,取得社團對應資料
        /// </summary>
        private Dictionary<string, CLUBRecord> GetDistinctClub(List<SCJoin> scjList)
        {
            //Distinct - 移除重覆
            List<string> clublilst = scjList.Select(x => x.RefClubID).ToList().Distinct().ToList();

            //取得社團資料
            List<CLUBRecord> clublist = tool._A.Select<CLUBRecord>("uid in ('" + string.Join("','", clublilst) + "')");

            Dictionary<string, CLUBRecord> dic = new Dictionary<string, CLUBRecord>();
            foreach (CLUBRecord each in clublist)
            {
                if (!dic.ContainsKey(each.UID))
                {
                    dic.Add(each.UID, each);
                }
            }

            return dic;
        }
    }
}
