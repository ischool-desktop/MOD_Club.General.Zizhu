using System.Collections.Generic;
using System;

namespace Campus.ImportZizhu
{
    /// <summary>
    /// 完整的使用者匯入選項
    /// </summary>
    public class ImportFullOption : ImportOption
    {
        /// <summary>
        /// 建構式，初始化欄位列表
        /// </summary>
        public ImportFullOption():base()
        {
            SheetFields = new List<string>();
        }

        /// <summary>
        /// 使用者選取的Excel檔
        /// </summary>
        public string SelectedDataFile { get; set; }

        /// <summary>
        /// 匯入程式所使用的驗證檔案，會快取至本機
        /// </summary>
        public string SelectedValidateFile { get; set; }

        /// <summary>
        /// 使用者選取的資料表名稱
        /// </summary>
        public string SelectedSheetName { get; set; }

        /// <summary>
        /// 資料表名稱欄位列表
        /// </summary>
        public List<string> SheetFields { get; set; }
    }
}