using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Campus.ImportZizhu
{
    /// <summary>
    /// 
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        /// 取得 Element 的字串值，如果該 Element 不存在，則回傳 string.Emtpy。
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetElementText(this XElement xml, XName name)
        {
            return GetElementText(xml, name, string.Empty);
        }

        /// <summary>
        /// 取得 Element 的字串值。
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetElementText(this XElement xml, XName name, string defaultValue)
        {
            XElement n = xml.Element(name);

            if (n == null)
                return string.Empty;
            else
                return n.Value;
        }

        /// <summary>
        /// 取得 Element 的數字值。
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetElementInt(this XElement xml, XName name, int defaultValue)
        {
            string val = xml.GetElementText(name);
            int result;

            if (int.TryParse(val, out result))
                return result;
            else
                return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool GetElementBool(this XElement xml, XName name, bool defaultValue)
        {
            string val = xml.GetElementText(name);
            bool result;

            if (bool.TryParse(val, out result))
                return result;
            else
                return defaultValue;
        }

        /// <summary>
        /// 取得 Attribute 的字串值，如果該 Attribute 不存在，則回傳 string.Empty。
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetAttributeText(this XElement xml, XName name)
        {
            return GetAttributeText(xml, name, string.Empty);
        }

        /// <summary>
        /// 取得 Attribute 的字串值。
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetAttributeText(this XElement xml, XName name, string defaultValue)
        {
            XAttribute n = xml.Attribute(name);

            if (n == null)
                return defaultValue;
            else
                return n.Value;
        }

        /// <summary>
        /// 取得 Attribute 的數字值。
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetAttributeInt(this XElement xml, XName name, int defaultValue)
        {
            string val = xml.GetAttributeText(name);
            int result;

            if (int.TryParse(val, out result))
                return result;
            else
                return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool GetAttributeBool(this XElement xml, XName name, bool defaultValue)
        {
            string val = xml.GetAttributeText(name);
            bool result;

            if (bool.TryParse(val, out result))
                return result;
            else
                return defaultValue;
        }

        /// <summary>
        /// 取得指定名稱的 Element，如果該 Element 存在，則執行 action 所指定的方法。
        /// </summary>
        /// <returns></returns>
        public static void GetThenAction(this XElement xml, XName name, Action<XElement> action)
        {
            XElement n = xml.Element(name);
            if (n != null)
                action(n);
        }

        /// <summary>
        /// 取得指定 Xml Namespace 的 Element。
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="name"></param>
        /// <param name="ns"></param>
        /// <returns></returns>
        public static XElement SelectElement(this XElement xml, XName name, string ns)
        {
            XmlNamespaceManager nm = new XmlNamespaceManager(new NameTable());
            nm.AddNamespace("ns", ns);
            return xml.XPathSelectElement(string.Format("{0}:{1}", "ns", name), nm);
        }

        /// <summary>
        /// 將 XElement 轉換成 XmlElement。
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static XmlElement ToXmlElement(this XElement xml)
        {
            return ToXmlElement(xml, true);
        }

        /// <summary>
        /// 將 XElement 轉換成 XmlElement。
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="preserveWhitespace"></param>
        /// <returns></returns>
        public static XmlElement ToXmlElement(this XElement xml, bool preserveWhitespace)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = preserveWhitespace;
            doc.LoadXml(xml.ToString());
            return doc.DocumentElement;
        }

        /// <summary>
        /// 將字串轉換成 XLinq 物件。
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static XElement ToXLinq(this string xml)
        {
            return XElement.Parse(xml);
        }
    }
}