using System.Collections.Generic;

namespace Campus.ImportZizhu
{
    /// <summary>
    /// 使用者匯入選項
    /// </summary>
    public class ImportOption
    {
        /// <summary>
        /// 建構式，初始化鍵值及匯入欄位
        /// </summary>
        public ImportOption()
        {
            SelectedKeyFields = new List<string>();
            SelectedFields = new List<string>();
        }

        /// <summary>
        /// 使用者選擇的鍵值
        /// </summary>
        public List<string> SelectedKeyFields { get; set; }

        /// <summary>
        /// 使用者選擇的匯入欄位
        /// </summary>
        public List<string> SelectedFields { get; set; }

        /// <summary>
        /// 匯入動作，新增或更新、刪除
        /// </summary>
        public ImportAction Action { get; set; }
    }
}