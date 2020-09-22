using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DS
{
    public class DataSource_XML
    {
        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            xmlSerializer.Serialize(file, source);
            file.Close();
        }
        public static T LoadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result = (T)xmlSerializer.Deserialize(file);
            file.Close();
            return result;
        }
        public static void saveConf()
        {
            XElement confRoot = new XElement("conf");
            confRoot.Add(new XElement("GuestRequestKey", Configuration.GuestRequestKey),
                         new XElement("HostingUnitKey", Configuration.HostingUnitKey),
                         new XElement("OrderKey", Configuration.OrderKey),
                         new XElement("Commission", Configuration.Commission),
                         new XElement("Email", Configuration.Mail),
                         new XElement("Password", Configuration.Password));
           confRoot.Save(Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory.ToString()).FullName).FullName + "/Data/conf.xml");
        }
    }
}
