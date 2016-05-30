﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;
using System.Xml;

namespace K12.Club.General.Zizhu
{
    //學生社團參與記錄
    [TableName("K12.SCJoin.Universal")]
    public class SCJoin : ActiveRecord
    {
        /// <summary>
        /// 淺層複製CLUBRecord
        /// </summary>
        public SCJoin CopyExtension()
        {
            return (SCJoin)this.MemberwiseClone();
        }

        /// <summary>
        /// 社團ID
        /// </summary>
        [Field(Field = "ref_club_id", Indexed = true)]
        public string RefClubID { get; set; }

        /// <summary>
        /// 學生ID
        /// </summary>
        [Field(Field = "ref_student_id", Indexed = true)]
        public string RefStudentID { get; set; }

        /// <summary>
        /// 選社鎖定
        /// </summary>
        [Field(Field = "lock", Indexed = false)]
        public bool Lock { get; set; }

        /// <summary>
        /// 階段別
        /// </summary>
        [Field(Field = "phase", Indexed = false)]
        public int Phase { get; set; }

        /// <summary>
        /// 成績(依據成績規則而不同)
        /// </summary>
        [Field(Field = "score", Indexed = false)]
        public string Score { get; set; }

        /// <summary>
        /// 修課年級
        /// </summary>
        [Field(Field = "grade_year", Indexed = false)]
        public int GradeYear { get; set; }

        /// <summary>
        /// 修課年級
        /// </summary>
        [Field(Field = "group", Indexed = false)]
        public int Group { get; set; }

        ///// <summary>
        ///// 成績_平時活動
        ///// </summary>
        //[Field(Field = "pa_Score", Indexed = false)]
        //public decimal? PAScore { get; set; }

        ///// <summary>
        ///// 成績_出缺席率
        ///// </summary>
        //[Field(Field = "ar_Score", Indexed = false)]
        //public decimal? ARScore { get; set; }

        ///// <summary>
        ///// 成績_活動力及服務
        ///// </summary>
        //[Field(Field = "aas_Score", Indexed = false)]
        //public decimal? AASScore { get; set; }

        ///// <summary>
        ///// 成績_成品成果考驗
        ///// </summary>
        //[Field(Field = "far_Score", Indexed = false)]
        //public decimal? FARScore { get; set; }

        ///// <summary>
        ///// 選社時間撮記
        ///// </summary>
        //[Field(Field = "Selected_Time", Indexed = false)]
        //public DateTime? SelectedTime { get; set; }
    }
}
