using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft​.AspNetCore​.Hosting;
public static class PlainMethods
    {
        //public static string RootPath
        //{
        //    get
        //    {
        //        return
        //            "http://" +
        //          HttpContext.Current.Request.Url.Authority.ToString()+
        //          "/";
        //    }
        //}
    public static string RootPath(HttpContext HttpContext)
    {
        try
        {
            return
                "http://" +
              HttpContext.Request.Host.ToString() +
              "/";
        }
        catch
        {

            return "";
        }
        
    }
    public static void DeleteIfExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);

            }
            
        }

        public static string ViewsScriptFolder
        {
            get
            {
                return "Pages/";
            }

        }

        public static string GetGuidName()
        {
            System.Guid guid = Guid.NewGuid();
            return guid.ToString();

        }

        public static string GetExtension(string FileName)
        {

            return
            FileName//.Substring(FileName.Length - FileName.LastIndexOf('.'), 3);
            .Substring(
                FileName.LastIndexOf('.'));

        }

        //public static string PhysicalRoot
        //{
        //    get
        //    {
        //        return HttpContext.Current.Server.MapPath(@"~\");
        //    }
        //}

    public static string PhysicalRoot(HttpContext HttpContext)
    {
        return
        ((IHostingEnvironment)HttpContext.RequestSe‌​rvices.GetService(Type.GetType("Microsoft​.AspNetCore​.Hosting.IHostingEnviron‌​ment"))).ContentRootP‌​ath;
            
        
    }

    

}



