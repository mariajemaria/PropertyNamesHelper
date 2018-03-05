using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace PropertyNamesHelper
{
    public static class PropertyLoader
    {
        public static void AppendTypeProperties(this XElement propertiesRoot, object obj, bool excludeAllBase = true, IEnumerable<Type> baseTypesToExcludePropsFrom = null)
        {
            IEnumerable<PropertyInfo> properties;
            if (excludeAllBase)
            {
                properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            }
            else if (baseTypesToExcludePropsFrom != null)
            {
                properties = obj.GetType().GetProperties().Where(p => !baseTypesToExcludePropsFrom.Contains(p.DeclaringType));
            }
            else
            {
                properties = obj.GetType().GetProperties();
            }

            foreach (var property in properties)
            {
                var propElement = new XElement(property.Name.ToLowerInvariant());
                var captionElement = new XElement("caption", Regex.Replace(property.Name, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 "));
                propElement.Add(captionElement);
                propertiesRoot.Add(propElement);
            }
        }
    }
}