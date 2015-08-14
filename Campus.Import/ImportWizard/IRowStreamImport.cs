using System;
using System.Collections.Generic;
using Campus.DocumentValidator;

namespace Campus.ImportZizhu
{
    [Flags]
    public enum ImportAction 
    {
        Insert = 1,
        Update = 2,
        InsertOrUpdate = 4,
        Cover = 8,
        Delete = 16
    };

    /// <summary>
    /// 將IRowStream匯入
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRowStreamImport
    {
        /// <summary>
        /// 取得驗證規則描述檔，可以直接是描述內容XML或是URL。
        /// </summary>
        /// <returns></returns>
        string GetValidateRule();

        /// <summary>
        /// 取得支援的匯入動作
        /// </summary>
        /// <returns></returns>
        ImportAction GetSupportActions();

        /// <summary>
        /// 初始化匯入動作並傳入使用者匯入選項
        /// </summary>
        /// <param name="Option"></param>
        void Prepare(ImportOption Option);

        /// <summary>
        /// 根據使用者的選項轉換結構
        /// </summary>
        string Import(List<IRowStream> Rows);
    }
}