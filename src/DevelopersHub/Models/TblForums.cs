using System;
using System.Collections.Generic;

namespace DevelopersHub.Models
{
    public partial class TblForums
    {
        public int Id { get; set; }
        public int? Mid { get; set; }
        public string Name { get; set; }

        public virtual TblMembers M { get; set; }
    }
}
