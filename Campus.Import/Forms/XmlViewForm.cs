using FISCA.Presentation.Controls;

namespace Campus.ImportZizhu
{
    public partial class XmlViewForm : BaseForm
    {
        public XmlViewForm()
        {
            InitializeComponent();

            btnClose.Click += (sender, e) => this.Close();
        }

        public void PopXml(string name, string url)
        {
            this.Text += !string.IsNullOrEmpty(name) ? " - " + name : string.Empty;
            webBrowser1.Navigate(url);
        }
    }
}