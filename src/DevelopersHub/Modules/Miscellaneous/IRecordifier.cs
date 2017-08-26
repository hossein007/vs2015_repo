using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;


    public interface IRecordifier
    {
        List<string> Fields { get; }
        string this[string i] { get; set; }
    }
