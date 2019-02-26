using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SecondHandStoreASPNET_Project.Helpers
{
    public static class HtmlElementsHelper
    {
        public static MvcHtmlString ImageConvertHelper(this HtmlHelper helper, byte[] src, string height, string width)
        {
            //string base64String = Convert.ToBase64String(arr, 0, arr.Length);
            //img.Src = "data:image/jpg;base64," + base64String;
            if (src != null)
            {
                var base64 = Convert.ToBase64String(src);
                var imgSrc = String.Format("data:png;base64,{0}", base64);
                return new MvcHtmlString(string.Format($"<img src ={imgSrc} width = {width} height = {height}/>"));
            }
            else
            {
                return new MvcHtmlString(string.Format($""));
            }

        }

        public static MvcHtmlString ListItemAction(this HtmlHelper helper, string name, string actionName, string controllerName)
        {
            var currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];
            var currentActionName = (string)helper.ViewContext.RouteData.Values["action"];
            var sb = new StringBuilder();

            sb.AppendFormat("<li{0}", (currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase) && currentActionName.Equals(actionName, StringComparison.CurrentCultureIgnoreCase) ? " class=\"Boldline\">" : ">"));

            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);

            sb.AppendFormat("<a href=\"{0}\">{1}</a>", url.Action(actionName, controllerName), name);

            sb.Append("</li>");

            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            string currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            if (actionName == currentAction && controllerName == currentController)
            {
                return htmlHelper.ActionLink(linkText, actionName, controllerName, null, new { @class = "selected" });
            }

            return htmlHelper.ActionLink(linkText, actionName, controllerName);
        }
    }
}
