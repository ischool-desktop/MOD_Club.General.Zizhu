using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using Campus.DocumentValidator;
using Campus.Validator;
using FISCA;

namespace Campus.ImportZizhu
{
    /// <summary>
    /// 匯入功能抽象類別
    /// </summary>
    public abstract class ImportWizard : IRowStreamImport,INotifyPropertyChanged
    {
        private int mImportProgress;
        private List<string> mCommands = new List<string>(new string[]{
                "Zizhu/ImportWizard/SelectSource",
                "Zizhu/ImportWizard/SelectKey",
                "Zizhu/ImportWizard/SelectFields",
                "Zizhu/ImportWizard/SelectValidate",
                "Zizhu/ImportWizard/SelectImport"
            });

        /// <summary>
        /// 驗證規則
        /// </summary>
        private XDocument mValidateRule;

        /// <summary>
        /// 驗證欄位解析器
        /// </summary>
        private FieldProcessor mFieldProcessor;

        /// <summary>
        /// 匯入選項
        /// </summary>
        public ImportOption ImportOption { get; set; }

        /// <summary>
        /// 驗證規則
        /// </summary>
        public XDocument ValidateRule { get { return mValidateRule; } }

        /// <summary>
        /// 驗證欄位解析器
        /// </summary>
        public FieldProcessor FieldProcessor { get { return mFieldProcessor; } }

        /// <summary>
        /// 客製驗證規則
        /// </summary>
        public Action<List<IRowStream>, RowMessages> CustomValidate { get; set; }

        /// <summary>
        /// 匯入完成函式，函式可傳回要在畫面上顯示的資訊，例如成功新增或更新幾筆。
        /// </summary>
        public Func<string> Complete { get; set; }

        /// <summary>
        /// 匯入訊息
        /// </summary>
        public ImportMessages ImportMessages { get; set; }

        /// <summary>
        /// 是否分批，預設為True
        /// </summary>
        public bool IsSplit { get; set; }

        /// <summary>
        /// 將傳回的字串做Log
        /// </summary>
        public bool IsLog { get; set; }

        /// <summary>
        /// 分批的最大執行緒數量，預設為3，只有在IsSplit為True才會有用
        /// </summary>
        public int SplitThreadCount { get; set; }

        /// <summary>
        /// 每次分批的資料量，預設為1000，只有在IsSplit為True才會有用
        /// </summary>
        public int SplitSize { get; set;}

        /// <summary>
        /// 匯入進度
        /// </summary>
        public int ImportProgress
        {
            get
            {
                return mImportProgress;
            }
            set
            {
                mImportProgress = value;
                OnPropertyChanged("ImportProgress");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// 建構式
        /// </summary>
        public ImportWizard()
        {
            ImportMessages = new ImportMessages();
            IsSplit = true;
            IsLog = true;
            SplitThreadCount = 3;
            SplitSize = 1000;
        }

        /// <summary>
        /// 執行匯入功能
        /// </summary>
        public void Execute()
        {
            try
            {
                ImportOption = new ImportOption();

                LoadRule();

                mFieldProcessor = new FieldProcessor(mValidateRule.Root);
            }
            catch (Exception e)
            {
                MessageBox.Show("下載驗證規則時發生錯誤，以下為詳細訊息："+ System.Environment.NewLine +e.Message);

                return;
            }

            try
            {
                ArgDictionary args = new ArgDictionary();

                args.SetValue("ImportWizard", this);

                Features.Invoke(args, mCommands.ToArray());
            }
            catch (Exception e)
            {
                MessageBox.Show("開啟匯入表單時發生錯誤，以下為詳細訊息："+System.Environment.NewLine+e.Message);

                return;
            }
        }

        #region LoadRule
        /// <summary>
        /// 載入驗證規則
        /// </summary>
        public void LoadRule()
        {
            string Rule = GetValidateRule();

            if (Rule.Trim().StartsWith("http://") || Rule.Trim().StartsWith("https://"))
            {
                try
                {
                    mValidateRule = XDocument.Load(Rule);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                try
                {
                    mValidateRule = XDocument.Load(new StringReader(Rule));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        #endregion

        #region IRowStreamImport 成員
        /// <summary>
        /// 取得驗證規則
        /// </summary>
        /// <returns></returns>
        public abstract string GetValidateRule();

        /// <summary>
        /// 取得支援的匯入動作
        /// </summary>
        /// <returns></returns>
        public abstract ImportAction GetSupportActions();

        /// <summary>
        /// 準備匯入
        /// </summary>
        /// <param name="Option"></param>
        public abstract void Prepare(ImportOption Option);

        /// <summary>
        /// 實際分批匯入
        /// </summary>
        /// <param name="Rows"></param>
        /// <returns></returns>
        public abstract string Import(List<IRowStream> Rows);
        #endregion

        #region INotifyPropertyChanged 成員

        /// <summary>
        /// 屬性改變通知
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}