using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Campus.ImportZizhu
{
    public static class Constants
    {
        public static string ValidationRulePath
        {
            get 
            {
                string Folder = Constants.ValidationReportsFolder + "\\xml";

                if (!System.IO.Directory.Exists(Folder))
                {
                    Directory.CreateDirectory(Folder);
                }

                return Path.Combine(Constants.ValidationReportsFolder + "\\xml", "validaterule.xml");
            }
        }

        public static string ValidationFormatPath
        {
            get 
            {
                string Folder = Constants.ValidationReportsFolder + "\\xml";

                if (!System.IO.Directory.Exists(Folder))
                {
                    Directory.CreateDirectory(Folder);
                }

                return Path.Combine(Constants.ValidationReportsFolder + "\\xml", "format.xsl");
            }
        }

        /// <summary>
        /// 取得設定檔 - 資料夾路徑
        /// </summary>
        public static string ValidationReportsFolder
        {
            get 
            {
                string Folder = Path.Combine(Application.StartupPath, "ValidationReports");

                if (!System.IO.Directory.Exists(Folder))
                {
                    Directory.CreateDirectory(Folder);
                }

                return Folder; 
            }
        }
    }
}