using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Campus.DocumentValidator;
using FISCA.DSAUtil;
using K12.Data;
using System.Xml;

namespace K12.Club.General.Zizhu
{
    class CLUBImportBOT
    {
        public Dictionary<string, CLUBRecord> ClubDic { get; set; }

        public Dictionary<string, TeacherRecord> TeacherNameDic { get; set; }

        public Dictionary<string, TeacherRecord> TeacherIDDic { get; set; }

        public string GetLogString(CLUBRecord each)
        {
            StringBuilder log = new StringBuilder();
            log.AppendLine(string.Format("学年度「{0}」学期「{1}」社团名称「{2}」", each.SchoolYear, each.Semester, each.ClubName));
            if (!string.IsNullOrEmpty(each.ClubNumber))
                log.AppendLine(string.Format("代码「{0}」", each.ClubNumber));
            if (!string.IsNullOrEmpty(each.Location))
                log.AppendLine(string.Format("场地「{0}」", each.Location));
            if (!string.IsNullOrEmpty(each.ClubCategory))
                log.AppendLine(string.Format("类型「{0}」", each.ClubCategory));

            if (!string.IsNullOrEmpty(each.RefTeacherID))
            {
                if (TeacherIDDic.ContainsKey(each.RefTeacherID))
                {
                    log.AppendLine(string.Format("老师1「{0}」", GetTeacherName(TeacherIDDic[each.RefTeacherID])));
                }
            }
            if (!string.IsNullOrEmpty(each.RefTeacherID2))
            {
                if (TeacherIDDic.ContainsKey(each.RefTeacherID2))
                {
                    log.AppendLine(string.Format("老师2「{0}」", GetTeacherName(TeacherIDDic[each.RefTeacherID2])));
                }
            }

            if (!string.IsNullOrEmpty(each.RefTeacherID3))
            {
                if (TeacherIDDic.ContainsKey(each.RefTeacherID3))
                {
                    log.AppendLine(string.Format("老师3「{0}」", GetTeacherName(TeacherIDDic[each.RefTeacherID3])));
                }
            }
            if (!string.IsNullOrEmpty(each.About))
                log.AppendLine(string.Format("简介「{0}」", each.About));

            if (each.Grade1Limit.HasValue)
                log.AppendLine(string.Format("限制:一年级选社人数限制「{0}」", each.Grade1Limit.Value.ToString()));
            if (each.Grade2Limit.HasValue)
                log.AppendLine(string.Format("限制:二年级选社人数限制「{0}」", each.Grade2Limit.Value.ToString()));
            if (each.Grade3Limit.HasValue)
                log.AppendLine(string.Format("限制:三年级选社人数限制「{0}」", each.Grade3Limit.Value.ToString()));
            if (each.Grade4Limit.HasValue)
                log.AppendLine(string.Format("限制:四年级选社人数限制「{0}」", each.Grade4Limit.Value.ToString()));
            if (each.Grade5Limit.HasValue)
                log.AppendLine(string.Format("限制:五年级选社人数限制「{0}」", each.Grade5Limit.Value.ToString()));

            if (each.Grade1BoyLimit.HasValue)
                log.AppendLine(string.Format("限制:一年级选社人数男生限制「{0}」", each.Grade1BoyLimit.Value.ToString()));
            if (each.Grade2BoyLimit.HasValue)
                log.AppendLine(string.Format("限制:二年级选社人数男生限制「{0}」", each.Grade2BoyLimit.Value.ToString()));
            if (each.Grade3BoyLimit.HasValue)
                log.AppendLine(string.Format("限制:三年级选社人数男生限制「{0}」", each.Grade3BoyLimit.Value.ToString()));
            if (each.Grade4BoyLimit.HasValue)
                log.AppendLine(string.Format("限制:四年级选社人数男生限制「{0}」", each.Grade4BoyLimit.Value.ToString()));
            if (each.Grade5BoyLimit.HasValue)
                log.AppendLine(string.Format("限制:五年级选社人数男生限制「{0}」", each.Grade5BoyLimit.Value.ToString()));

            if (each.Grade1GirlLimit.HasValue)
                log.AppendLine(string.Format("限制:一年级选社人数女生限制「{0}」", each.Grade1GirlLimit.Value.ToString()));
            if (each.Grade2GirlLimit.HasValue)
                log.AppendLine(string.Format("限制:二年级选社人数女生限制「{0}」", each.Grade2GirlLimit.Value.ToString()));
            if (each.Grade3GirlLimit.HasValue)
                log.AppendLine(string.Format("限制:三年级选社人数女生限制「{0}」", each.Grade3GirlLimit.Value.ToString()));
            if (each.Grade4GirlLimit.HasValue)
                log.AppendLine(string.Format("限制:四年级选社人数女生限制「{0}」", each.Grade4GirlLimit.Value.ToString()));
            if (each.Grade5GirlLimit.HasValue)
                log.AppendLine(string.Format("限制:五年级选社人数女生限制「{0}」", each.Grade5GirlLimit.Value.ToString()));

            return log.ToString();

        }

        public void SetClub(IRowStream Row, CLUBRecord club)
        {
            club.ClubNumber = Row.GetValue("代码");
            club.Location = Row.GetValue("场地");
            club.ClubCategory = Row.GetValue("类型");

            club.RefTeacherID = checkTeacherName("" + Row.GetValue("老师1"));
            club.RefTeacherID2 = checkTeacherName("" + Row.GetValue("老师2"));
            club.RefTeacherID3 = checkTeacherName("" + Row.GetValue("老师3"));

            club.About = Row.GetValue("简介");

            int x = 0;

            #region 选社人数限制

            if (int.TryParse("" + Row.GetValue("限制:一年级选社人数限制"), out x))
                club.Grade1Limit = x;
            else
                club.Grade1Limit = null;

            if (int.TryParse("" + Row.GetValue("限制:二年级选社人数限制"), out x))
                club.Grade2Limit = x;
            else
                club.Grade2Limit = null;

            if (int.TryParse("" + Row.GetValue("限制:三年级选社人数限制"), out x))
                club.Grade3Limit = x;
            else
                club.Grade3Limit = null;

            if (int.TryParse("" + Row.GetValue("限制:四年级选社人数限制"), out x))
                club.Grade4Limit = x;
            else
                club.Grade4Limit = null;

            if (int.TryParse("" + Row.GetValue("限制:五年级选社人数限制"), out x))
                club.Grade5Limit = x;
            else
                club.Grade5Limit = null; 

            #endregion

            #region 选社人数男生限制

            if (int.TryParse("" + Row.GetValue("限制:一年级选社人数男生限制"), out x))
                club.Grade1BoyLimit = x;
            else
                club.Grade1BoyLimit = null;

            if (int.TryParse("" + Row.GetValue("限制:二年级选社人数男生限制"), out x))
                club.Grade2BoyLimit = x;
            else
                club.Grade2BoyLimit = null;

            if (int.TryParse("" + Row.GetValue("限制:三年级选社人数男生限制"), out x))
                club.Grade3BoyLimit = x;
            else
                club.Grade3BoyLimit = null;

            if (int.TryParse("" + Row.GetValue("限制:四年级选社人数男生限制"), out x))
                club.Grade4BoyLimit = x;
            else
                club.Grade4BoyLimit = null;

            if (int.TryParse("" + Row.GetValue("限制:五年级选社人数男生限制"), out x))
                club.Grade5BoyLimit = x;
            else
                club.Grade5BoyLimit = null; 

            #endregion

            #region 选社人数女生限制

            if (int.TryParse("" + Row.GetValue("限制:一年级选社人数女生限制"), out x))
                club.Grade1GirlLimit = x;
            else
                club.Grade1GirlLimit = null;

            if (int.TryParse("" + Row.GetValue("限制:二年级选社人数女生限制"), out x))
                club.Grade2GirlLimit = x;
            else
                club.Grade2GirlLimit = null;

            if (int.TryParse("" + Row.GetValue("限制:三年级选社人数女生限制"), out x))
                club.Grade3GirlLimit = x;
            else
                club.Grade3GirlLimit = null;

            if (int.TryParse("" + Row.GetValue("限制:四年级选社人数女生限制"), out x))
                club.Grade4GirlLimit = x;
            else
                club.Grade4GirlLimit = null;

            if (int.TryParse("" + Row.GetValue("限制:五年级选社人数女生限制"), out x))
                club.Grade5GirlLimit = x;
            else
                club.Grade5GirlLimit = null; 

            #endregion

        }

        /// <summary>
        /// 取得XML結構之科別清單狀態
        /// </summary>
        public string GetDeptXML(string Dept)
        {
            if (!string.IsNullOrEmpty(Dept))
            {
                string[] DeptList = Dept.Split('/');
                DSXmlHelper dsXml = new DSXmlHelper("Department");
                foreach (string each in DeptList)
                {
                    dsXml.AddElement("Dept");
                    dsXml.AddText("Dept", each);
                }
                return dsXml.BaseElement.OuterXml;
            }
            else
            {
                DSXmlHelper dsXml = new DSXmlHelper("Department");
                return dsXml.BaseElement.OuterXml;
            }
        }

        public string GetDeptName(string xml)
        {
            List<string> list = new List<string>();
            DSXmlHelper dsXml = new DSXmlHelper();
            dsXml.Load(xml);
            foreach (XmlElement each in dsXml.BaseElement.SelectNodes("Dept"))
            {
                list.Add(each.InnerText);
            }

            return string.Join("/", list);

        }


        /// <summary>
        /// 老師是否存在,則傳回老師ID
        /// </summary>
        public string checkTeacherName(string name)
        {
            if (TeacherNameDic.ContainsKey(name))
                return TeacherNameDic[name].ID;
            else
                return "";
        }

        /// <summary>
        /// 取得老師清單 Name:Record
        /// </summary>
        public Dictionary<string, TeacherRecord> GetTeacherDic()
        {
            TeacherIDDic = new Dictionary<string, TeacherRecord>();

            Dictionary<string, TeacherRecord> dic = new Dictionary<string, TeacherRecord>();
            List<TeacherRecord> TeacherList = K12.Data.Teacher.SelectAll();
            foreach (TeacherRecord each in TeacherList)
            {
                if (each.Status == TeacherRecord.TeacherStatus.一般)
                {
                    #region 老師名稱
                    string teacherName = "";
                    if (string.IsNullOrEmpty(each.Nickname))
                    {
                        teacherName = each.Name;
                    }
                    else
                    {
                        teacherName = each.Name + "(" + each.Nickname + ")";
                    }

                    if (!dic.ContainsKey(teacherName))
                    {
                        dic.Add(teacherName, each);
                    }
                    #endregion

                    //建立老師對照 ID:Record
                    if (!TeacherIDDic.ContainsKey(each.ID))
                    {
                        TeacherIDDic.Add(each.ID, each);
                    }
                }
            }

            return dic;
        }

        /// <summary>
        /// 傳入老師Record,回傳包含老師暱稱的名字
        /// </summary>
        public string GetTeacherName(TeacherRecord tr)
        {
            if (string.IsNullOrEmpty(tr.Nickname))
            {
                return tr.Name;
            }
            else
            {
                return tr.Name + "(" + tr.Nickname + ")";
            }
        }

        /// <summary>
        /// 取得社團清單 school + semester + name:Record
        /// </summary>
        public Dictionary<string, CLUBRecord> GetCLUBDic()
        {
            Dictionary<string, CLUBRecord> dic = new Dictionary<string, CLUBRecord>();

            List<CLUBRecord> CLUBList = tool._A.Select<CLUBRecord>();
            foreach (CLUBRecord each in CLUBList)
            {
                string CourseKey = each.SchoolYear + "," + each.Semester + "," + each.ClubName;
                if (!dic.ContainsKey(CourseKey))
                {
                    dic.Add(CourseKey, each);
                }
            }
            return dic;
        }

        public string SetLog(ImputLog log)
        {
            //检查与确认资料是否被修改
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("学年度「{0}」学期「{1}」社团名称「{2}」", log.New_club.SchoolYear, log.New_club.Semester, log.New_club.ClubName));
            if (log.lo_CLUB.ClubNumber != log.New_club.ClubNumber)
                sb.AppendLine(ByOne("代码", log.lo_CLUB.ClubNumber, log.New_club.ClubNumber));

            if (log.lo_CLUB.Location != log.New_club.Location)
                sb.AppendLine(ByOne("场地", log.lo_CLUB.Location, log.New_club.Location));

            if (log.lo_CLUB.ClubCategory != log.New_club.ClubCategory)
                sb.AppendLine(ByOne("类型", log.lo_CLUB.ClubCategory, log.New_club.ClubCategory));

            if (log.lo_CLUB.RefTeacherID != log.New_club.RefTeacherID)
                ByTeacher("老师1", log.lo_CLUB.RefTeacherID, log.New_club.RefTeacherID);

            if (log.lo_CLUB.RefTeacherID2 != log.New_club.RefTeacherID2)
                ByTeacher("老师2", log.lo_CLUB.RefTeacherID2, log.New_club.RefTeacherID2);

            if (log.lo_CLUB.RefTeacherID3 != log.New_club.RefTeacherID3)
                ByTeacher("老师3", log.lo_CLUB.RefTeacherID3, log.New_club.RefTeacherID3);

            if (log.lo_CLUB.About != log.New_club.About)
                sb.AppendLine(ByOne("简介", log.lo_CLUB.About, log.New_club.About));


            if (log.lo_CLUB.Grade1Limit != log.New_club.Grade1Limit)
                sb.AppendLine(ByOne("限制:一年级选社人数限制", ByInet(log.lo_CLUB.Grade1Limit), ByInet(log.New_club.Grade1Limit)));

            if (log.lo_CLUB.Grade2Limit != log.New_club.Grade2Limit)
                sb.AppendLine(ByOne("限制:二年级选社人数限制", ByInet(log.lo_CLUB.Grade2Limit), ByInet(log.New_club.Grade2Limit)));

            if (log.lo_CLUB.Grade3Limit != log.New_club.Grade3Limit)
                sb.AppendLine(ByOne("限制:三年级选社人数限制", ByInet(log.lo_CLUB.Grade3Limit), ByInet(log.New_club.Grade3Limit)));

            if (log.lo_CLUB.Grade4Limit != log.New_club.Grade4Limit)
                sb.AppendLine(ByOne("限制:四年级选社人数限制", ByInet(log.lo_CLUB.Grade4Limit), ByInet(log.New_club.Grade4Limit)));

            if (log.lo_CLUB.Grade5Limit != log.New_club.Grade5Limit)
                sb.AppendLine(ByOne("限制:五年级选社人数限制", ByInet(log.lo_CLUB.Grade5Limit), ByInet(log.New_club.Grade5Limit)));




            if (log.lo_CLUB.Grade1BoyLimit != log.New_club.Grade1BoyLimit)
                sb.AppendLine(ByOne("限制:一年级选社人数男生限制", ByInet(log.lo_CLUB.Grade1BoyLimit), ByInet(log.New_club.Grade1BoyLimit)));

            if (log.lo_CLUB.Grade2BoyLimit != log.New_club.Grade2BoyLimit)
                sb.AppendLine(ByOne("限制:二年级选社人数男生限制", ByInet(log.lo_CLUB.Grade2BoyLimit), ByInet(log.New_club.Grade2BoyLimit)));

            if (log.lo_CLUB.Grade3BoyLimit != log.New_club.Grade3BoyLimit)
                sb.AppendLine(ByOne("限制:三年级选社人数男生限制", ByInet(log.lo_CLUB.Grade3BoyLimit), ByInet(log.New_club.Grade3BoyLimit)));

            if (log.lo_CLUB.Grade4BoyLimit != log.New_club.Grade4BoyLimit)
                sb.AppendLine(ByOne("限制:四年级选社人数男生限制", ByInet(log.lo_CLUB.Grade4BoyLimit), ByInet(log.New_club.Grade4BoyLimit)));

            if (log.lo_CLUB.Grade5BoyLimit != log.New_club.Grade5BoyLimit)
                sb.AppendLine(ByOne("限制:五年级选社人数男生限制", ByInet(log.lo_CLUB.Grade5BoyLimit), ByInet(log.New_club.Grade5BoyLimit)));



            if (log.lo_CLUB.Grade1GirlLimit != log.New_club.Grade1GirlLimit)
                sb.AppendLine(ByOne("限制:一年级选社人数女生限制", ByInet(log.lo_CLUB.Grade1GirlLimit), ByInet(log.New_club.Grade1GirlLimit)));

            if (log.lo_CLUB.Grade2GirlLimit != log.New_club.Grade2GirlLimit)
                sb.AppendLine(ByOne("限制:二年级选社人数女生限制", ByInet(log.lo_CLUB.Grade2GirlLimit), ByInet(log.New_club.Grade2GirlLimit)));

            if (log.lo_CLUB.Grade3GirlLimit != log.New_club.Grade3GirlLimit)
                sb.AppendLine(ByOne("限制:三年级选社人数女生限制", ByInet(log.lo_CLUB.Grade3GirlLimit), ByInet(log.New_club.Grade3GirlLimit)));

            if (log.lo_CLUB.Grade4GirlLimit != log.New_club.Grade4GirlLimit)
                sb.AppendLine(ByOne("限制:四年级选社人数女生限制", ByInet(log.lo_CLUB.Grade4GirlLimit), ByInet(log.New_club.Grade4GirlLimit)));

            if (log.lo_CLUB.Grade5GirlLimit != log.New_club.Grade5GirlLimit)
                sb.AppendLine(ByOne("限制:五年级选社人数女生限制", ByInet(log.lo_CLUB.Grade5GirlLimit), ByInet(log.New_club.Grade5GirlLimit)));


            sb.AppendLine("");
            return sb.ToString();

        }

        public string ByInet(int? a)
        {
            if (a.HasValue)
                return a.Value.ToString();
            else
                return "";
        }

        public string ByTeacher(string a, string b, string c)
        {
            string teacherA = "";
            string teacherB = "";
            if (TeacherIDDic.ContainsKey(b))
                teacherA = GetTeacherName(TeacherIDDic[b]);

            if (TeacherIDDic.ContainsKey(c))
                teacherB = GetTeacherName(TeacherIDDic[c]);

            return ByOne(a, teacherA, teacherB);
        }

        public string ByOne(string a, string b, string c)
        {
            return string.Format("「{0}」由「{1}」修改為「{2}」", a, b, c);
        }
    }
}
