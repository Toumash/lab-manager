using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.HtmlHelper
{
    public static class VisualHelper
    {
        public static MvcHtmlString Status(this System.Web.Mvc.HtmlHelper htmlHelper, bool? yesNo)
        {
            if (yesNo.HasValue && yesNo.Value)
            {
                return
                    new MvcHtmlString("<img src=\"" +
                                      new UrlHelper(htmlHelper.ViewContext.RequestContext).Content(
                                          "~/Content/images/green.png") + "\"/>");
            }
            return new MvcHtmlString("");
        }
    }
}