using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;
using System.Xml;

namespace K12.Club.General.Zizhu
{
    [TableName("K12.CLUBRecord.Universal")]
    class CLUBRecord : ActiveRecord
    {
        /// <summary>
        /// 淺層複製CLUBRecord
        /// </summary>
        public CLUBRecord CopyExtension()
        {
            return (CLUBRecord)this.MemberwiseClone();
        }

        /// <summary>
        /// 社團名稱
        /// </summary>
        [Field(Field = "club_name", Indexed = true)]
        public string ClubName { get; set; }

        /// <summary>
        /// 學年度
        /// </summary>
        [Field(Field = "school_year", Indexed = false)]
        public int SchoolYear { get; set; }

        /// <summary>
        /// 學期
        /// </summary>
        [Field(Field = "semester", Indexed = false)]
        public int Semester { get; set; }

        /// <summary>
        /// 代碼
        /// </summary>
        [Field(Field = "club_number", Indexed = false)]
        public string ClubNumber { get; set; }

        /// <summary>
        /// 類型
        /// </summary>
        [Field(Field = "club_category", Indexed = false)]
        public string ClubCategory { get; set; }

        ///// <summary>
        ///// 性別限制
        ///// 男 / 女 / 空值(不限制)
        ///// </summary>
        //[Field(Field = "gender_restrict", Indexed = false)]
        //public string GenderRestrict { get; set; }

        #region 總人數限制

        /// <summary>
        /// 一年級選社人數限制
        /// 0不開放 / null不限制 / (預設為null)
        /// </summary>
        [Field(Field = "grade1_limit", Indexed = false)]
        public int? Grade1Limit { get; set; }

        /// <summary>
        /// 二年級選社人數限制
        /// 0不開放 / null不限制 / (預設為null)
        /// </summary>
        [Field(Field = "grade2_limit", Indexed = false)]
        public int? Grade2Limit { get; set; }

        /// <summary>
        /// 三年級選社人數限制
        /// 0不開放 / null不限制 / (預設為0)
        /// </summary>
        [Field(Field = "grade3_limit", Indexed = false)]
        public int? Grade3Limit { get; set; }

        /// <summary>
        /// 四年級選社人數限制
        /// 0不開放 / null不限制 / (預設為0)
        /// </summary>
        [Field(Field = "grade4_limit", Indexed = false)]
        public int? Grade4Limit { get; set; }

        /// <summary>
        /// 五年級選社人數限制
        /// 0不開放 / null不限制 / (預設為0)
        /// </summary>
        [Field(Field = "grade5_limit", Indexed = false)]
        public int? Grade5Limit { get; set; }

        #endregion

        #region 男生人數限制

        /// <summary>
        /// 一年級選社人數男生限制
        /// 0不開放 / null不限制 / (預設為null)
        /// </summary>
        [Field(Field = "grade1_boy_limit", Indexed = false)]
        public int? Grade1BoyLimit { get; set; }

        /// <summary>
        /// 二年級選社人數男生限制
        /// 0不開放 / null不限制 / (預設為null)
        /// </summary>
        [Field(Field = "grade2_boy_limit", Indexed = false)]
        public int? Grade2BoyLimit { get; set; }

        /// <summary>
        /// 三年級選社人數男生限制
        /// 0不開放 / null不限制 / (預設為0)
        /// </summary>
        [Field(Field = "grade3_boy_limit", Indexed = false)]
        public int? Grade3BoyLimit { get; set; }

        /// <summary>
        /// 四年級選社人數男生限制
        /// 0不開放 / null不限制 / (預設為0)
        /// </summary>
        [Field(Field = "grade4_boy_limit", Indexed = false)]
        public int? Grade4BoyLimit { get; set; }

        /// <summary>
        /// 五年級選社人數男生限制
        /// 0不開放 / null不限制 / (預設為0)
        /// </summary>
        [Field(Field = "grade5_boy_limit", Indexed = false)]
        public int? Grade5BoyLimit { get; set; } 

        #endregion

        #region 女生人數限制

        /// <summary>
        /// 一年級選社人數女生限制
        /// 0不開放 / null不限制 / (預設為null)
        /// </summary>
        [Field(Field = "grade1_girl_limit", Indexed = false)]
        public int? Grade1GirlLimit { get; set; }

        /// <summary>
        /// 二年級選社人數女生限制
        /// 0不開放 / null不限制 / (預設為null)
        /// </summary>
        [Field(Field = "grade2_girl_limit", Indexed = false)]
        public int? Grade2GirlLimit { get; set; }

        /// <summary>
        /// 三年級選社人數女生限制
        /// 0不開放 / null不限制 / (預設為0)
        /// </summary>
        [Field(Field = "grade3_girl_limit", Indexed = false)]
        public int? Grade3GirlLimit { get; set; }

        /// <summary>
        /// 四年級選社人數女生限制
        /// 0不開放 / null不限制 / (預設為0)
        /// </summary>
        [Field(Field = "grade4_girl_limit", Indexed = false)]
        public int? Grade4GirlLimit { get; set; }

        /// <summary>
        /// 五年級選社人數女生限制
        /// 0不開放 / null不限制 / (預設為0)
        /// </summary>
        [Field(Field = "grade5_girl_limit", Indexed = false)]
        public int? Grade5GirlLimit { get; set; }

        #endregion

        /// <summary>
        /// 評分老師ID-1
        /// </summary>
        [Field(Field = "ref_teacher_id", Indexed = false)]
        public string RefTeacherID { get; set; }

        /// <summary>
        /// 指導老師ID-2
        /// </summary>
        [Field(Field = "ref_teacher_id_2", Indexed = false)]
        public string RefTeacherID2 { get; set; }

        /// <summary>
        /// 指導老師ID-3
        /// </summary>
        [Field(Field = "ref_teacher_id_3", Indexed = false)]
        public string RefTeacherID3 { get; set; }

        /// <summary>
        /// 社長
        /// 記錄學生系統編號
        /// </summary>
        [Field(Field = "president", Indexed = false)]
        public string President { get; set; }

        /// <summary>
        /// 副社長
        /// 記錄學生ID
        /// </summary>
        [Field(Field = "vice_president", Indexed = false)]
        public string VicePresident { get; set; }

        /// <summary>
        /// 場地
        /// </summary>
        [Field(Field = "location", Indexed = false)]
        public string Location { get; set; }

        /// <summary>
        /// 簡介
        /// </summary>
        [Field(Field = "about", Indexed = false)]
        public string About { get; set; }

        /// <summary>
        /// 照片1
        /// </summary>
        [Field(Field = "photo1", Indexed = false)]
        public string Photo1 { get; set; }

        /// <summary>
        /// 照片2
        /// </summary>
        [Field(Field = "photo2", Indexed = false)]
        public string Photo2 { get; set; }


        //total number of hours

        /// <summary>
        /// 總課時數
        /// </summary>
        [Field(Field = "total_number_hours", Indexed = false)]
        public int? TotalNumberHours { get; set; }


    }
}
