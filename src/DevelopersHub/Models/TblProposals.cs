using System;
using System.Collections.Generic;

namespace DevelopersHub.Models
{
    public partial class TblProposals
    {
        public int Id { get; set; }
        public int? Mid { get; set; }
        public string Title { get; set; }
        public string Describtion { get; set; }
        public string Technologies { get; set; }
        public string SnapshotFile { get; set; }

        public virtual TblMembers M { get; set; }
    }
}
