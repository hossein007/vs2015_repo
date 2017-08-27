using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;
//using System.Web.Mvc;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
public static class Extensions
{
    public static bool IsDebug(this IHtmlHelper htmlHelper)
    {
#if DEBUG
      return true;
#else
        return false;
#endif
    }

    public static HtmlString RowNumber<T,U>(
    this IHtmlHelper htmlHelper, 
    T model,U item,object PageNumber,object PageSize) 
        
{
    return new HtmlString(
        Convert.ToString(
        ((IList<U>)model).IndexOf(item)+((Convert.ToInt32(PageNumber)-1)*Convert.ToInt32(PageSize)+1)
        )
        );
}

    public static HtmlString IncludeScriptFile(this IHtmlHelper htmlHelper, Microsoft​.AspNetCore​.Http.IHttpContextAccessor hsttpContextAccessor, string fileName)
    {
        return new HtmlString(
            "<script type=\"text/javascript\" src=\""+
            StaticClassMethodLibrary.RootPath(hsttpContextAccessor) + "Scripts/" + StaticClassMethodLibrary.ViewsScriptFolder+fileName+"\">"+
        "</script>"
        );
    }

    public static HtmlString AbsolutePath(this IHtmlHelper htmlHelper, Microsoft​.AspNetCore​.Http.IHttpContextAccessor hsttpContextAccessor, string relPath)
    {
        return new HtmlString(StaticClassMethodLibrary.RootPath(hsttpContextAccessor) +relPath);
    }


    public static HtmlString Template1(this IHtmlHelper htmlHelper, string str)
    {
        return new HtmlString(
            string.Format(
    "< input type = \"text\" name = \"{0}\" id = \"{0}\" />",str)
    );


}

    public static HtmlString ActionSubmit(this IHtmlHelper htmlHelper, string actionmethod, string caption)
    {

        return new HtmlString(
            string.Format(
    "< input type = \"submit\" name = \"{0}\" id = \"{0}\" value = \"{1}\" />"
    )
    );
    }


    public static HtmlString Select(this IHtmlHelper htmlHelper, IEnumerable<IKeyNameItem> obj, string id, bool generateEmptyOption = true, string emptyOptionText = "", string emptyOptionValue = "0", string selectedValue = "")
    {
        string _id = id.Length > 0 ? " id=" + id.ToString() : "";
        string _name = id.Length > 0 ? " id=" + id.ToString() : "";
        string str_items = "";
        foreach (var item in obj)
        {
            str_items +=
            string.Format(
            @"< option value = ""{0}"" : """"){2}>
               {1}
           </ option >",
           item.ItemKey,
           item.ItemName,
           item.ItemKey == selectedValue ? " selected":""

           );

        }
        return new HtmlString(
@"< select id=""{0}"" name=""{1}"" >" +

(
generateEmptyOption ?
string.Format(@"< option value=""{0}"" > {0} </ option >", generateEmptyOption) :
str_items) +



"</ select >");

}

    public static HtmlString PageBar(this IHtmlHelper htmlHelper, Microsoft​.AspNetCore​.Http.IHttpContextAccessor hsttpContextAccessor, string baseUrl, object pageNumber, object pageCount, string className = "PageBar horizontal", string pageNumberLinkFormat = "{0}")
    {
        string str_items = "";
        for (int i = 1; i <= (int)pageCount; i++)
        {
            if (i != Convert.ToInt32(pageNumber))
            {
                str_items +=
                      string.Format(
                          @"<li>
            <a href = ""{0}"" > {1} </a>
 
          </li>"
                  ,
                  PlainMethods.RootPath(hsttpContextAccessor.HttpContext) + baseUrl + string.Format(pageNumberLinkFormat, i),
                  i);
            }
            else
            {
                
                str_items +=


                  string.Format(
                      @"<li class=""active"">
       <span>
            {0}
          </span>
        </li>"
      , i);
            }

        }


        return new HtmlString(
            string.Format(
    @"<ul class=""clearfix {0}"">
    {1}
    </ul>",
    className,
    str_items))
    ;

}


    public static HtmlString ValueOrDefault(this IHtmlHelper htmlHelper, string Default, string Value)
    {
        if (!string.IsNullOrEmpty(Value))
        {
            return new HtmlString(Value);

        }
        else
        {
            return new HtmlString(Default);

        }

    }






}


