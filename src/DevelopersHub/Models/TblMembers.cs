using System;
using System.Collections.Generic;

namespace DevelopersHub.Models
{
    public partial class TblMembers
    {
        public TblMembers()
        {
            TblForums = new HashSet<TblForums>();
            TblProposals = new HashSet<TblProposals>();
            TblSkills = new HashSet<TblSkills>();
        }

        public int Id { get; set; }
        public string Fname { get; set; }
        public string Sname { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Bdate { get; set; }
        public string Experience { get; set; }
        public string Picture { get; set; }

        public virtual ICollection<TblForums> TblForums { get; set; }
        public virtual ICollection<TblProposals> TblProposals { get; set; }
        public virtual ICollection<TblSkills> TblSkills { get; set; }
    }
}
