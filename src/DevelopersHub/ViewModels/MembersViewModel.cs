using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevelopersHub.Models;
namespace DevelopersHub.ViewModels
{
    public class MembersViewModel : ViewModel
    {
       public List<TblMembers> Members;
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
    }
}
