using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;
//using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class PreventRefreshSubmitAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var __session = filterContext.HttpContext.Session;
               
                if (((Controller)filterContext.Controller).ViewData.ModelState.IsValid)
                {
                    byte[] __sessionValue;
                    __session.TryGetValue("form", out __sessionValue);
                    
                    if (__sessionValue != null)
                    {
                        ((Controller)filterContext.Controller).ViewData.ModelState.AddModelError("Refresh submit", "");
                        ((Controller)filterContext.Controller).TempData["success"] = true;
                        
                    }
                    else
                    {
                        //__session["form"] = 1;
                        __session.Set("form", new byte[] { 1 });
            }
                }
                else
                {
                    __session.Remove("form");
                }



            }
        }


        public class AuthAttribute : ActionFilterAttribute 
        {
            public override void OnActionExecuted(ActionExecutedContext filterContext)
            {
        //filterContext.RequestContext.HttpContext.Response.Write(
        //filterContext.RequestContext.HttpContext.Request.RawUrl);

        
    }
            
        }

    
