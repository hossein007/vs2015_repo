using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;


    public interface IKeyNameItem
    {
        string ItemKey { get; }
        string ItemName { get; }
    }

    public class GenericKeyValueItem : IKeyNameItem
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public string ItemKey
        {
            get
            {
                return Value.ToString();
            }
        }
        public string ItemName
        {
            get
            {
                return Name;

            }
        }

    }


    public class GenericKeyValueItemList : List<GenericKeyValueItem>
    {
        public GenericKeyValueItemList()
        {

        }
        public GenericKeyValueItemList(List<GenericKeyValueItem> list)
        {
            this.InsertRange(0, list);
        }
    }


