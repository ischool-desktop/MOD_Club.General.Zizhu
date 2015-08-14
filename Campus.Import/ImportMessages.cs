using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Campus.ImportZizhu
{
    /// <summary>
    /// 匯入訊息
    /// </summary>
    public class ImportMessages : IEnumerable<string>
    {
        private Dictionary<int, string> Messages { get; set; }

        /// <summary>
        /// 建構式
        /// </summary>
        public ImportMessages()
        {
            Messages = new Dictionary<int,string>();
        }

        /// <summary>
        /// 回傳匯入訊息所有位置
        /// </summary>
        public List<int> Positions
        {
            get
            {
                return Messages.Keys.ToList();
            }
        }

        /// <summary>
        /// 取得或設定特定位置匯入訊息
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        public string this[int Position]
        {
            get
            {
                if (!Messages.ContainsKey(Position))
                    Messages.Add(Position,string.Empty);

                return Messages[Position];
            }
            set
            {
                if (!Messages.ContainsKey(Position))
                    Messages.Add(Position, value);

                Messages[Position] = value;
            }
        }

        /// <summary>
        /// 清除所有訊息
        /// </summary>
        public void Clear()
        {
            Messages.Clear();
        }

        #region IEnumerable<RowMessage> 成員

        public IEnumerator<string> GetEnumerator()
        {
            return Messages.Values.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成員

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Messages.Values.GetEnumerator();
        }

        #endregion
    }
}