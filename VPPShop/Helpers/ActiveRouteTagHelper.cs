using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Routing;

namespace VPPShop.Helpers
{
    [HtmlTargetElement("a", Attributes = "asp-controller,asp-action")]
    public class ActiveRouteTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public string AspController { get; set; }
        public string AspAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var currentController = ViewContext.RouteData.Values["controller"]?.ToString();
            var currentAction = ViewContext.RouteData.Values["action"]?.ToString();

            if (string.Equals(currentController, AspController, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(currentAction, AspAction, StringComparison.OrdinalIgnoreCase))
            {
                var existingClasses = output.Attributes["class"]?.Value?.ToString() ?? "";
                output.Attributes.SetAttribute("class", $"{existingClasses} active");
            }
        }
    }
}
