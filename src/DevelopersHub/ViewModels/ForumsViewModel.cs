using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevelopersHub.Models;
namespace DevelopersHub.ViewModels
{
    public class ForumsViewModel : ViewModel
    {
       public List<TblForums> Forums;
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
    }
}
