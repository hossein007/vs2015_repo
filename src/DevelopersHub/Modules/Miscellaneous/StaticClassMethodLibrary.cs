using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;
using System.IO;

    public static class StaticClassMethodLibrary
    {
        public static string RootPath(Microsoft​.AspNetCore​.Http.IHttpContextAccessor httpContextAccessor)
        {
        
                return
                    "http://" +
                  httpContextAccessor.HttpContext.Request.Host+
                  "/";
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

        public static string PhysicalRoot()
        {

            return "";// HttpContext.Current.Server.MapPath(@"~\");
            
        }


        
    }



