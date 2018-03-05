using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PropertyNamesHelper
{
    public static class PropertyAppender
    {
        public static void AppendMissingPropertyNames(XElement propertiesXElement)
        {
            var filePath = ConfigurationManager.AppSettings["AddOn:PropertyHelper:PropertyNamesFileLocation"] ?? "/Resources/LanguageFiles/PropertyNames.xml";
            var serverFilePath = HttpContext.Current.Server.MapPath(filePath);
            var loadedFile = XDocument.Load(serverFilePath);
            var propertiesNodeInPropertyNames = loadedFile.Root?.Descendants("properties").FirstOrDefault();

            if (propertiesNodeInPropertyNames == null) return;
            foreach (var element in propertiesXElement.Elements())
            {
                var findInXml = propertiesNodeInPropertyNames.Element(element.Name);
                if (findInXml == null)
                {
                    propertiesNodeInPropertyNames.Add(element);
                }
            }

            loadedFile.Save(serverFilePath);
        }
    }
}