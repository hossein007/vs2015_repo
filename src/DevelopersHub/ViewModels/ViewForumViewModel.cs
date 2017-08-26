using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevelopersHub.Models;
namespace DevelopersHub.ViewModels
{
    public class ViewForumViewModel : ViewModel
    {
        public TblForums Forum;

        public ForumsThreads ForumThreads;
    }

    public class ForumsThreads :TblForumsThreads
    {
        public List<ForumsThreads> Childs
        {
            get;
            set;
        }
    }
}
