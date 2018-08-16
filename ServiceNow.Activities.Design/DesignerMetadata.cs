using System.Activities.Presentation.Metadata;
using System.Activities;
using System.ComponentModel;

namespace ServiceNow.Activities.Design
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            //AttributeTableBuilder attributeTableBuilder = new AttributeTableBuilder();

            //attributeTableBuilder.AddCustomAttributes(typeof(ServiceNowScope), new DesignerAttribute(typeof(ServiceNowScopeDesigner)));

            //MetadataStore.AddAttributeTable(attributeTableBuilder.CreateTable());
        }
    }
}
