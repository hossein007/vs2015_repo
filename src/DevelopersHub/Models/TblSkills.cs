using System;
using System.Collections.Generic;

namespace DevelopersHub.Models
{
    public partial class TblSkills
    {
        public int Id { get; set; }
        public int? Mid { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public double? Years { get; set; }

        public virtual TblMembers M { get; set; }
    }
}
