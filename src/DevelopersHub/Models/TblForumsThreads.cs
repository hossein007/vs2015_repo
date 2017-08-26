using System;
using System.Collections.Generic;

namespace DevelopersHub.Models
{
    public partial class TblForumsThreads
    {
        public int Id { get; set; }
        public int Fid { get; set; }
        public int Mid { get; set; }
        public int? Ftid { get; set; }
        public string Text { get; set; }
        public DateTime? Adddate { get; set; }

        public virtual TblForums F { get; set; }
        public virtual TblForumsThreads Ft { get; set; }
        public virtual ICollection<TblForumsThreads> InverseFt { get; set; }
        public virtual TblMembers M { get; set; }
    }
}
