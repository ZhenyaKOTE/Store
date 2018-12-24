using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Store.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(
        this HtmlHelper html,
        PagingInfo pagingInfo,
        Func<int, string> pageUrl)
        {
            //StringBuilder result = new StringBuilder();
            //for (int i = 1; i <= pagingInfo.TotalPages; i++)
            //{
            //    TagBuilder tag = new TagBuilder("a"); // Construct an <a> tag
            //    tag.MergeAttribute("href", pageUrl(i));
            //    tag.InnerHtml = i.ToString();
            //    if (i == pagingInfo.CurrentPage)
            //        tag.AddCssClass("selected");

            //    tag.AddCssClass("badge badge-light");
            //    result.Append(tag.ToString());
            //}
            //return MvcHtmlString.Create(result.ToString());
            
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; ++i)
            {             
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.MergeAttribute("style", "font-size: 20px; height: 30px; width:auto; min-width:30px; margine-left:3px;");
                if (i == pagingInfo.CurrentPage)
                {
                    tag.InnerHtml = i.ToString();
                    tag.AddCssClass("badge badge-secondary");
                }
                else
                {
                    if (pagingInfo.CurrentPage - 2 == i || pagingInfo.CurrentPage - 1 == i || pagingInfo.CurrentPage + 2 == i || pagingInfo.CurrentPage + 1 == i || i == 1 || i == pagingInfo.TotalPages)
                    {
                        tag.InnerHtml = i.ToString();
                        tag.AddCssClass("badge badge-light");
                    }
                    else
                    {
                        if ((pagingInfo.CurrentPage - 3 == i || pagingInfo.CurrentPage + 3 == i) && i != 1)
                        {
                            tag.InnerHtml = "...";
                            tag.AddCssClass("badge badge-light");
                        }
                    }
                }
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }

    }
}