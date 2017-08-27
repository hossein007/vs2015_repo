using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevelopersHub.Models;
namespace DevelopersHub.ViewModels
{
    public class ProposalsViewModel : ViewModel
    {
       public List<TblProposals> Proposals;
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
    }
}
