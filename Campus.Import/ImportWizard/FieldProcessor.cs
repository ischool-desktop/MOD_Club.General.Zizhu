using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Campus.ImportZizhu
{
    public class FieldProcessor
    {
        private XElement mValidateRule;

        public FieldProcessor(XElement ValidateRule)
        {
            //<ValidateRule>
            //    <DuplicateDetection>
            //        <Detector Name="PrimaryKey1" ErrorType="Error">
            //            <Field Name="學號"/>
            //        </Detector>
            //        <Detector Name="PrimaryKey2" ErrorType="Error">
            //            <Field Name="身分證號"/>
            //        </Detector>
            //    </DuplicateDetection>
            //    <FieldList>
            //        <Field Required="True" Name="姓名" Description="ischool建立一名學生的必要資料">
            //            <Validate AutoCorrect="False" Description="「姓名」不允許空白。" ErrorType="Error" Validator="不可空白" When="" />
            //            <Validate AutoCorrect="False" Description="「姓名」長度必須小於20個文字。" ErrorType="Error" Validator="文字50" When="" />
            //        </Field>
            //        <Field Required="True" Name="學號" Description="學生資料的匯入可依學號進行匯入&lt;br/&gt;步驟1基本設定「學生學號清單」&lt;br/&gt;即是指此欄位的內容">
            //            <Validate AutoCorrect="False" Description="「學號」不允許空白。" ErrorType="Error" Validator="不可空白" When=""/>
            //            <Validate AutoCorrect="False" Description="「學號」長度必須小於12個文字。 " ErrorType="Error" Validator="文字12" When="" />
            //        </Field>
            //    </FieldList>
            //</ValidateRule>

            KeyFields = new Dictionary<string, List<string>>();
            RequiredFields = new List<string>();
            NotRequiredFields = new List<string>();

            foreach (XElement Element in ValidateRule.Element("DuplicateDetection").Elements("Detector"))
            {
                string Name = Element.GetAttributeText("Name");
                string ErrorType =  Element.GetAttributeText("ErrorType");
                string IsImporKey = Element.GetAttributeText("IsImportKey");
                //判斷是否為匯入錯誤型態
                bool bIsErrorType = string.IsNullOrEmpty(ErrorType) || ErrorType.ToLower().Equals("error");
                //判是否可為匯入鍵值
                bool bIsImportKey = string.IsNullOrEmpty(IsImporKey) || IsImporKey.ToLower().Equals("true");

                if (bIsErrorType && bIsImportKey)
                {
                    List<string> Fields = Element.Elements("Field").Select(x => x.GetAttributeText("Name")).ToList();

                    if (!KeyFields.ContainsKey(Name))
                        KeyFields.Add(Name, Fields);
                }
            }

            foreach (XElement Element in ValidateRule.Element("FieldList").Elements("Field"))
            {
                string Name = Element.GetAttributeText("Name");
                string Required = Element.GetAttributeText("Required");

                if (Required.ToUpper().Equals("TRUE"))
                    RequiredFields.Add(Name);
                else
                    NotRequiredFields.Add(Name);
            }

            this.mValidateRule = ValidateRule;
        }

        public Dictionary<string, List<string>> KeyFields { get; private set; }

        public List<string> RequiredFields { get; private set; }

        public List<string> NotRequiredFields { get; private set; }

        public Dictionary<string, List<string>> GetSelectableKeyFields(List<string> Fields)
        {
            Dictionary<string, List<string>> Result = new Dictionary<string, List<string>>();

            foreach (string Key in KeyFields.Keys)
            {
                if (KeyFields[Key].TrueForAll(x => Fields.Contains(x)))
                    Result.Add(Key, KeyFields[Key]);
            }

            return Result;
        }

        public bool IsContainRequiredFields(List<string> Fields)
        {
            return RequiredFields.TrueForAll(x => Fields.Contains(x));
        }

        public List<string> GetSelectableFields(List<string> KeyFields,List<string> SheetFields)
        {
            List<string> Result = new List<string>();

            if (KeyFields == null || SheetFields == null)
                return Result;

            Result = NotRequiredFields.Intersect(SheetFields).ToList(); 

            Result.Intersect(KeyFields).ToList().ForEach(x => Result.Remove(x));

            return Result;
        }
    }
}