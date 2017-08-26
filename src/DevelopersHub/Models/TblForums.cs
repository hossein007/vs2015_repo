using System;
using System.Collections.Generic;

namespace DevelopersHub.Models
{
    public partial class TblForums
    {
        public TblForums()
        {
            TblForumsThreads = new HashSet<TblForumsThreads>();
        }

        public int Id { get; set; }
        public int? Mid { get; set; }
        public string Name { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<TblForumsThreads> TblForumsThreads { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual TblMembers M { get; set; }
    }
}
