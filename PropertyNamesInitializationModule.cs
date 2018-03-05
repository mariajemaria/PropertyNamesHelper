using System;
using System.Collections.Generic;
using System.Xml.Linq;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using InitializationModule = EPiServer.Web.InitializationModule;

namespace PropertyNamesHelper
{
    [ModuleDependency(typeof(InitializationModule))]
    public class PropertyNamesInitializationModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
			// add your base types here
            var variantBaseTypes = new List<Type>
            {
				typeof(VariationContent),
                typeof(EntryContentBase),
                typeof(CatalogContentBase),
                typeof(ContentData)
            };

            var nodeBaseTypes = new List<Type>
            {
                typeof(NodeContent),
                typeof(NodeContentBase),
                typeof(CatalogContentBase),
                typeof(ContentData)
            };

            var bundleBaseTypes = new List<Type>
            {
                typeof(BundleContent),
                typeof(EntryContentBase),
                typeof(CatalogContentBase),
                typeof(ContentData)
            };

            var propertiesXElement = new XElement("properties");

            // two usages, either by setting base types to exclude or excluding all base types (excludeAllBase = true in AppendTypeProperties)
            // examples:
            //propertiesXElement.AppendTypeProperties(new GenericProduct());
            //propertiesXElement.AppendTypeProperties(new GenericVariant(), baseTypesToExcludePropsFrom: variantBaseTypes);
            //propertiesXElement.AppendTypeProperties(new GenericNode(), baseTypesToExcludePropsFrom: nodeBaseTypes);
            //propertiesXElement.AppendTypeProperties(new GenericBundle(), baseTypesToExcludePropsFrom: bundleBaseTypes);
            //propertiesXElement.AppendTypeProperties(new Accessory(), baseTypesToExcludePropsFrom: variantBaseTypes);
            //propertiesXElement.AppendTypeProperties(new SparePart(), baseTypesToExcludePropsFrom: variantBaseTypes);

            PropertyAppender.AppendMissingPropertyNames(propertiesXElement);
        }

        public void Uninitialize(InitializationEngine context)
        {

        }
    }
}