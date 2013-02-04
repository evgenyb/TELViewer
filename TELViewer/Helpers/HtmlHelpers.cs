using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TELViewer.Helpers
{
    public static class HtmlHelpers
    {
        public static string RegisterJs(this HtmlHelper helper, string scriptLib)
        {
            //get the directory where the scripts are
            string absolutePath = VirtualPathUtility.ToAbsolute(scriptLib);
            const string scriptFormat = "<script src=\"{0}\" type=\"text/javascript\"></script>\r\n";
            return string.Format(scriptFormat, absolutePath);
        }

        public static string Image(this HtmlHelper helper, string id, string url, string alternateText)
        {
            return Image(helper, id, url, alternateText, null);
        }

        public static string Image(this HtmlHelper helper, string url, string alternateText)
        {
            return Image(helper, string.Empty, url, alternateText, null);
        }

        public static string Image(this HtmlHelper helper, string url, string alternateText, object htmlAttributes)
        {
            return Image(helper, string.Empty, url, alternateText, htmlAttributes);
        }

        public static string Image(this HtmlHelper helper, string id, string url, string alternateText, object htmlAttributes)
        {
            // Instantiate a UrlHelper   
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            // Create tag builder  
            var builder = new TagBuilder("img");

            if (!string.IsNullOrEmpty(id))
            {
                builder.GenerateId(id);
            }

            // Add attributes  
            builder.MergeAttribute("src", urlHelper.Content(url));

            if (!string.IsNullOrEmpty(alternateText))
            {
                builder.MergeAttribute("alt", alternateText);
                builder.MergeAttribute("title", alternateText);
            }

            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            // Render tag  
            return builder.ToString(TagRenderMode.SelfClosing);
        }
    }
}