using FISCA;
using System.Collections.Generic;

namespace Campus.ImportZizhu
{
    public class Program
    {
        [FISCA.MainMethod()]
        public static void Main()
        {
            Features.Register("Zizhu/ImportWizard/SelectSource", arg =>
            {
                ContinueDirection Direction = new SelectSource(arg).ShowWizardDialog();
                return Direction;
            });

            Features.Register("Zizhu/ImportWizard/SelectKey", arg =>
            {
                ImportWizard mImportWizard = arg["ImportWizard"] as ImportWizard;

                if (mImportWizard.FieldProcessor.KeyFields.Count == 0)
                    return ContinueDirection.Skip;

                ContinueDirection Direction = new SelectKey(arg).ShowWizardDialog();

                return Direction;
            });

            Features.Register("Zizhu/ImportWizard/SelectFields", arg =>
            {
                #region 判斷若可選取的欄位為0，則跳過本步驟
                List<string> SelectableFields = arg.TryGetList<string>("SelectableFields");

                if (SelectableFields.Count == 0)
                    return ContinueDirection.Skip;
                #endregion

                ContinueDirection Direction = new SelectFields(arg).ShowWizardDialog();
                return Direction;
            });

            Features.Register("Zizhu/ImportWizard/SelectValidate", arg =>
            {
                ContinueDirection Direction = new SelectValidate(arg).ShowWizardDialog();

                return Direction;
            });

            Features.Register("Zizhu/ImportWizard/SelectImport", arg =>
            {
                ContinueDirection Direction = new SelectImport(arg).ShowWizardDialog();
                return Direction;
            });
        }
    }
}