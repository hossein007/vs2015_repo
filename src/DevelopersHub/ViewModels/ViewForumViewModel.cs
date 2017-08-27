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

        public List<ForumsThreads> ForumThreads;
    }

    public class ForumsThreads :TblForumsThreads
    {

    }
}
