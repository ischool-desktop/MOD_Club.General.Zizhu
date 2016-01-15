using FISCA.Data;
using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace K12.Club.General.Zizhu.Ribbon
{
    public partial class CheckClubAssessment : BaseForm
    {
        public CheckClubAssessment()
        {
            InitializeComponent();

            DataTable table = new DataTable();
            table.Columns.Add("uid");
            table.Columns.Add("club_name");
            table.Columns.Add("teacher_name");
            table.Columns.Add("phase1");
            table.Columns.Add("phase2");
            table.Columns.Add("finished");
            Dictionary<string, DataRow> rows = new Dictionary<string, DataRow>();

            BackgroundWorker bkw = new BackgroundWorker();
            bkw.DoWork += delegate
            {
                var dt = new QueryHelper().Select(string.Format(@"
SELECT 
	$k12.clubrecord.universal.uid,
	$k12.clubrecord.universal.club_name,
    teacher.teacher_name,
	$k12.scjoin.universal.phase,
	asmT.detial as detialT
FROM
	$k12.clubrecord.universal
	LEFT OUTER JOIN $k12.scjoin.universal on $k12.clubrecord.universal.uid = $k12.scjoin.universal.ref_club_id::bigint
	LEFT OUTER JOIN student on student.id = $k12.scjoin.universal.ref_student_id::bigint AND student.status = 1
	LEFT OUTER JOIN $ischool.club.assessment as asmT on asmT.ref_student_id = student.id and asmT.ref_club_id = $k12.clubrecord.universal.uid and asmT.assessment_type='teacher'
    LEFT OUTER JOIN teacher on $k12.clubrecord.universal.ref_teacher_id::bigint = teacher.id
WHERE
    $k12.clubrecord.universal.school_year = {0} and $k12.clubrecord.universal.semester = {1}
ORDER BY club_number
", K12.Data.School.DefaultSchoolYear, K12.Data.School.DefaultSemester));

                foreach (DataRow row in dt.Rows)
                {
                    string uid = "" + row["uid"];
                    string name = "" + row["club_name"];
                    string teacherName = "" + row["teacher_name"];
                    string phase = "" + row["phase"];

                    if (!rows.ContainsKey(uid))
                    {
                        var r = table.Rows.Add();
                        rows.Add(uid, r);
                        r[table.Columns.IndexOf("uid")] = uid;
                        r[table.Columns.IndexOf("club_name")] = name;
                        r[table.Columns.IndexOf("teacher_name")] = teacherName;
                        r[table.Columns.IndexOf("phase1")] = 0;
                        r[table.Columns.IndexOf("phase2")] = 0;
                        r[table.Columns.IndexOf("finished")] = 0;
                    }

                    var dataRow = rows[uid];
                    if (phase == "1")
                        dataRow[table.Columns.IndexOf("phase1")] = int.Parse("" + dataRow[table.Columns.IndexOf("phase1")]) + 1;
                    if (phase == "2")
                        dataRow[table.Columns.IndexOf("phase2")] = int.Parse("" + dataRow[table.Columns.IndexOf("phase2")]) + 1;

                    bool allFinished = true;
                    try
                    {
                        if ("" + row["detialT"] != "")
                        {
                            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                            doc.LoadXml("" + row["detialT"]);
                            foreach (System.Xml.XmlNode node in doc.DocumentElement.ChildNodes)
                            {
                                if (node.InnerText == "")
                                    allFinished = false;
                            }
                        }
                        else
                            allFinished = false;
                    }
                    catch
                    {
                        allFinished = false;
                    }
                    if (allFinished)
                    {
                        dataRow[table.Columns.IndexOf("finished")] = int.Parse("" + dataRow[table.Columns.IndexOf("finished")]) + 1;
                    }
                }
            };
            bkw.RunWorkerCompleted += delegate
            {
                foreach (DataRow rowData in rows.Values)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgv);
                    row.Cells[0].Value = "" + rowData[table.Columns.IndexOf("club_name")];
                    row.Cells[1].Value = "" + rowData[table.Columns.IndexOf("teacher_name")];
                    row.Cells[2].Value = "" + rowData[table.Columns.IndexOf("phase1")] + (("" + rowData[table.Columns.IndexOf("phase2")] != "0") ? (", " + "" + rowData[table.Columns.IndexOf("phase2")]) : "");
                    row.Cells[3].Value = "" + rowData[table.Columns.IndexOf("finished")];

                    if (int.Parse("" + rowData[table.Columns.IndexOf("phase1")]) + int.Parse("" + rowData[table.Columns.IndexOf("phase2")]) > int.Parse("" + rowData[table.Columns.IndexOf("finished")]))
                        row.Cells[3].Style.ForeColor = Color.Red;

                    dgv.Rows.Add(row);
                }
            };
            bkw.RunWorkerAsync();
        }
    }
}
