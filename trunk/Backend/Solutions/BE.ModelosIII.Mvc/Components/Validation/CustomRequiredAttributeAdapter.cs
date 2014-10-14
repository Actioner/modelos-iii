using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BE.ModelosIII.Resources;

namespace BE.ModelosIII.Mvc.Components.Validation
{
    public class CustomRequiredAttributeAdapter : RequiredAttributeAdapter
    {
        public CustomRequiredAttributeAdapter(ModelMetadata metadata,
                                          ControllerContext context,
                                          RequiredAttribute attribute)
            : base(metadata, context, attribute)
        {
            if (attribute.ErrorMessage != null || attribute.ErrorMessageResourceType != null) return;
            attribute.ErrorMessageResourceType = typeof(ValidationMessages);
            attribute.ErrorMessageResourceName = "PropertyValueRequired";
        }
    }
}