using Campus.DocumentValidator;

namespace K12.Club.General.Zizhu
{
    /// <summary>
    /// 用來產生排課系統所需的自訂驗證規則
    /// </summary>
    public class CLUBFieldValidatorFactory : IFieldValidatorFactory
    {
        #region IFieldValidatorFactory 成員

        /// <summary>
        /// 根據typeName建立對應的FieldValidator
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="validatorDescription"></param>
        /// <returns></returns>
        public IFieldValidator CreateFieldValidator(string typeName, System.Xml.XmlElement validatorDescription)
        {
            switch (typeName.ToUpper())
            {
                case "TEACHERINISCHOOLCHECK":
                    return new TeacherInischoolCheck(); //取得ischool系統內的所有老師
                default:
                    return null;
            }
        }

        #endregion
    }
}