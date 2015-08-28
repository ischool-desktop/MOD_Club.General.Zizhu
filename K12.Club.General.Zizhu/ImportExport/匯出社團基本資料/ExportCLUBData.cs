using System.Collections.Generic;
using System.Xml;
using FISCA.UDT;
using SmartSchool.API.PlugIn;
using K12.Data;
using System.Data;
using FISCA.DSAUtil;
using System.Text;

namespace K12.Club.General.Zizhu
{
    class ExportCLUBData : SmartSchool.API.PlugIn.Export.Exporter
    {
        Dictionary<string, string> TeacherDic { get; set; }
        //建構子
        public ExportCLUBData()
        {
            this.Image = null;
            this.Text = "汇出课程基本资料";
        }

        //覆寫
        public override void InitializeExport(SmartSchool.API.PlugIn.Export.ExportWizard wizard)
        {
            List<string> FieldList = new List<string>();
            FieldList.Add("学年度"); //目前字段
            FieldList.Add("学期"); //目前字段
            FieldList.Add("课程名称"); //目前字段
            FieldList.Add("代码");
            FieldList.Add("场地");
            FieldList.Add("类型");

            FieldList.Add("长短课程");
            FieldList.Add("课程领域");
            FieldList.Add("课程属性");
            FieldList.Add("上课形式");

            FieldList.Add("老师1");
            FieldList.Add("老师2");
            FieldList.Add("老师3");

            FieldList.Add("总课时数");
            FieldList.Add("简介");

            FieldList.Add("限制:一年级选课人数限制");
            FieldList.Add("限制:二年级选课人数限制");
            FieldList.Add("限制:三年级选课人数限制");
            FieldList.Add("限制:四年级选课人数限制");
            FieldList.Add("限制:五年级选课人数限制");

            FieldList.Add("限制:一年级选课人数男生限制");
            FieldList.Add("限制:二年级选课人数男生限制");
            FieldList.Add("限制:三年级选课人数男生限制");
            FieldList.Add("限制:四年级选课人数男生限制");
            FieldList.Add("限制:五年级选课人数男生限制");

            FieldList.Add("限制:一年级选课人数女生限制");
            FieldList.Add("限制:二年级选课人数女生限制");
            FieldList.Add("限制:三年级选课人数女生限制");
            FieldList.Add("限制:四年级选课人数女生限制");
            FieldList.Add("限制:五年级选课人数女生限制");


            //取得教師
            TeacherDic = GetTeacher();



            wizard.ExportableFields.AddRange(FieldList);

            wizard.ExportPackage += (sender, e) =>
            {
                //取得學生清單

                AccessHelper helper = new AccessHelper();

                string strCondition = string.Join("','", e.List);
                List<CLUBRecord> records = helper.Select<CLUBRecord>("uid in ('" + strCondition + "')");

                for (int i = 0; i < records.Count; i++)
                {
                    RowData row = new RowData();
                    row.ID = records[i].UID;

                    string teacher1 = "";
                    string teacher2 = "";
                    string teacher3 = "";
                    if (TeacherDic.ContainsKey(records[i].RefTeacherID))
                        teacher1 = TeacherDic[records[i].RefTeacherID];

                    if (TeacherDic.ContainsKey(records[i].RefTeacherID2))
                        teacher2 = TeacherDic[records[i].RefTeacherID2];

                    if (TeacherDic.ContainsKey(records[i].RefTeacherID3))
                        teacher3 = TeacherDic[records[i].RefTeacherID3];

                    foreach (string field in e.ExportFields)
                    {
                        if (wizard.ExportableFields.Contains(field))
                        {
                            switch (field)
                            {
                                case "学年度": row.Add(field, "" + records[i].SchoolYear); break;
                                case "学期": row.Add(field, "" + records[i].Semester); break;
                                case "课程名称": row.Add(field, records[i].ClubName); break;
                                case "代码": row.Add(field, records[i].ClubNumber); break;
                                case "场地": row.Add(field, records[i].Location); break;
                                case "类型": row.Add(field, records[i].ClubCategory); break;

                                case "长短课程": row.Add(field, records[i].FullPhase.HasValue && records[i].FullPhase.Value ? "长课程" : ""); break;
                                case "课程领域": row.Add(field, records[i].Domain); break;
                                case "课程属性": row.Add(field, records[i].Type); break;
                                case "上课形式": row.Add(field, records[i].Formal); break;


                                case "老师1": row.Add(field, teacher1); break;
                                case "老师2": row.Add(field, teacher2); break;
                                case "老师3": row.Add(field, teacher3); break;
                                case "总课时数": row.Add(field, records[i].TotalNumberHours.HasValue ? records[i].TotalNumberHours.Value.ToString() : ""); break;
                                case "简介": row.Add(field, records[i].About); break;

                                case "限制:一年级选课人数限制": row.Add(field, records[i].Grade1Limit.HasValue ? "" + records[i].Grade1Limit.Value : ""); break;
                                case "限制:二年级选课人数限制": row.Add(field, records[i].Grade2Limit.HasValue ? "" + records[i].Grade2Limit.Value : ""); break;
                                case "限制:三年级选课人数限制": row.Add(field, records[i].Grade3Limit.HasValue ? "" + records[i].Grade3Limit.Value : ""); break;
                                case "限制:四年级选课人数限制": row.Add(field, records[i].Grade4Limit.HasValue ? "" + records[i].Grade4Limit.Value : ""); break;
                                case "限制:五年级选课人数限制": row.Add(field, records[i].Grade5Limit.HasValue ? "" + records[i].Grade5Limit.Value : ""); break;

                                case "限制:一年级选课人数男生限制": row.Add(field, records[i].Grade1BoyLimit.HasValue ? "" + records[i].Grade1BoyLimit.Value : ""); break;
                                case "限制:二年级选课人数男生限制": row.Add(field, records[i].Grade2BoyLimit.HasValue ? "" + records[i].Grade2BoyLimit.Value : ""); break;
                                case "限制:三年级选课人数男生限制": row.Add(field, records[i].Grade3BoyLimit.HasValue ? "" + records[i].Grade3BoyLimit.Value : ""); break;
                                case "限制:四年级选课人数男生限制": row.Add(field, records[i].Grade4BoyLimit.HasValue ? "" + records[i].Grade4BoyLimit.Value : ""); break;
                                case "限制:五年级选课人数男生限制": row.Add(field, records[i].Grade5BoyLimit.HasValue ? "" + records[i].Grade5BoyLimit.Value : ""); break;

                                case "限制:一年级选课人数女生限制": row.Add(field, records[i].Grade1GirlLimit.HasValue ? "" + records[i].Grade1GirlLimit.Value : ""); break;
                                case "限制:二年级选课人数女生限制": row.Add(field, records[i].Grade2GirlLimit.HasValue ? "" + records[i].Grade2GirlLimit.Value : ""); break;
                                case "限制:三年级选课人数女生限制": row.Add(field, records[i].Grade3GirlLimit.HasValue ? "" + records[i].Grade3GirlLimit.Value : ""); break;
                                case "限制:四年级选课人数女生限制": row.Add(field, records[i].Grade4GirlLimit.HasValue ? "" + records[i].Grade4GirlLimit.Value : ""); break;
                                case "限制:五年级选课人数女生限制": row.Add(field, records[i].Grade5GirlLimit.HasValue ? "" + records[i].Grade5GirlLimit.Value : ""); break;

                            }
                        }
                    }
                    e.Items.Add(row);
                }
            };
        }

        /// <summary>
        /// 取得教師名稱
        /// </summary>
        /// <param name="TeacherID"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetTeacher()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            DataTable dt = tool._Q.Select("select id,teacher_name,nickname from teacher where status=1");

            foreach (DataRow row in dt.Rows)
            {
                string id = "" + row["id"];
                string teacher_name = "" + row["teacher_name"];
                string nickname = "" + row["nickname"];
                if (!dic.ContainsKey(id))
                {
                    if (string.IsNullOrEmpty(nickname))
                        dic.Add(id, teacher_name);
                    else
                        dic.Add(id, teacher_name + "(" + nickname + ")");
                }
            }

            return dic;
        }

        /// <summary>
        /// 傳入科別內容,回傳以"/"號分隔的科別資料
        /// </summary>
        private string GetRestrict(string RestrictXml)
        {
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(RestrictXml))
            {
                DSXmlHelper dsx = new DSXmlHelper();
                dsx.Load(RestrictXml);
                foreach (XmlElement xml in dsx.BaseElement.SelectNodes("Dept"))
                {
                    list.Add(xml.InnerText);
                }
            }

            return string.Join("/", list);
        }
    }
}