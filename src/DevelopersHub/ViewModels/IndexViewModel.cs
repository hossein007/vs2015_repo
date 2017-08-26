using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevelopersHub.Models;
namespace DevelopersHub.ViewModels
{
    public class IndexViewModel : ViewModel 
    {
       public List<TblMembers> Members;
       public List<TblProposals> Proposals;
    }
}
