using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevelopersHub.Models;
namespace DevelopersHub.ViewModels
{
    public class MemberProposalsViewModel : ViewModel
    {
        public TblMembers Member { get; set; }

        public List<TblProposals> Proposals;
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
    }
}
