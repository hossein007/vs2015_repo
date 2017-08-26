using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevelopersHub.Models;
using DevelopersHub.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace DevelopersHub.Controllers
{
    public class ForumsController : BaseController
    {
        public ForumsController(Microsoft​.AspNetCore​.Http.IHttpContextAccessor httpContextAccessor, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment) : base(httpContextAccessor, hostingEnvironment)
        {


        }
        public IActionResult Index()
        {

            List<TblForums> __list_forums = UnitOfWork.Repository<TblForums>().GetAll().ToList();


            ForumsViewModel __forumsViewModel = new ViewModels.ForumsViewModel();



            __forumsViewModel.Forums = __list_forums; ;

            ViewModel = __forumsViewModel;



            return View(__forumsViewModel);

        }

        public void FillForumThreadsList(int index,ViewModels.ForumsThreads forumThreads, List<TblForumsThreads> tblForumThreads)
        {
            Dictionary<int, TblMembers> _list_members = new Dictionary<int, Models.TblMembers>();

            for (int i=index+1;i<tblForumThreads.Count;i++)
            {
                if (tblForumThreads[i].Ftid == forumThreads.Id)
                {
                    ForumsThreads __forumThreads = new ForumsThreads();
                    __forumThreads.Id = tblForumThreads[i].Id;
                    __forumThreads.Fid = tblForumThreads[i].Fid;
                    __forumThreads.Ftid = tblForumThreads[i].Ftid;
                    __forumThreads.Mid = tblForumThreads[i].Mid;


                    if (!_list_members.ContainsKey(tblForumThreads[i].Mid))
                    {
                        _list_members.Add(
                            tblForumThreads[i].Mid,
                            UnitOfWork.Repository<DevelopersHub.Models.TblMembers>().Get(x => x.Id == tblForumThreads[i].Mid)
                            );
                    }

                    __forumThreads.M = _list_members[tblForumThreads[i].Mid];


                    __forumThreads.Text = tblForumThreads[i].Text;
                    __forumThreads.Adddate = tblForumThreads[i].Adddate;
                    if (forumThreads.Childs==null)
                    forumThreads.Childs = new List<ForumsThreads>();
                    forumThreads.Childs.Add(__forumThreads);

                    FillForumThreadsList(i, forumThreads.Childs[forumThreads.Childs.Count - 1], tblForumThreads);

                }

            }


           

        }
        public IActionResult ViewForum(int Id)
        {
            TblForums _TblForums =
            UnitOfWork.Repository<DevelopersHub.Models.TblForums>().Get(x => x.Id == Id);

            _TblForums.M = 
            UnitOfWork.Repository<DevelopersHub.Models.TblMembers>().Get(x => x.Id == _TblForums.Id);

            

            List<TblForumsThreads> _list_TblForumsThreads = UnitOfWork.Repository<DevelopersHub.Models.TblForumsThreads>().GetAll(x => x.Fid == Id).ToList();
            List<ForumsThreads> _list_ForumsThreads = new List<ViewModels.ForumsThreads>();

            Dictionary<int, TblMembers> _list_members = new Dictionary<int, Models.TblMembers>();

            List<int> _ids_added = new List<int>();

            ForumsThreads __new_ForumsThreads = new ForumsThreads();

            __new_ForumsThreads.Id = _list_TblForumsThreads[0].Id;
            __new_ForumsThreads.Mid = _list_TblForumsThreads[0].Mid;
            __new_ForumsThreads.Fid = _list_TblForumsThreads[0].Fid;
            __new_ForumsThreads.Ftid = _list_TblForumsThreads[0].Ftid;
            __new_ForumsThreads.Text = _list_TblForumsThreads[0].Text;
            __new_ForumsThreads.Adddate = _list_TblForumsThreads[0].Adddate;


            if(!_list_members.ContainsKey(_list_TblForumsThreads[0].Mid))
            {
                _list_members.Add(
                    _list_TblForumsThreads[0].Mid,
                    UnitOfWork.Repository<DevelopersHub.Models.TblMembers>().Get(x => x.Id == _list_TblForumsThreads[0].Mid)
                    );
            }

            __new_ForumsThreads.M = _list_members[_list_TblForumsThreads[0].Mid];


            _list_ForumsThreads.Add(__new_ForumsThreads);

            FillForumThreadsList(0, _list_ForumsThreads[0], _list_TblForumsThreads);





        

            ViewForumViewModel _ViewForumViewModel = new ViewForumViewModel();
            _ViewForumViewModel.Forum = _TblForums;

            _ViewForumViewModel.ForumThreads = _list_ForumsThreads[0];

            ViewModel = _ViewForumViewModel;

            return View(_ViewForumViewModel);
        }


        public IActionResult PostForumThread()
        {
            ViewData["memberId"] = LoggedInUserId;
            ViewData["forumId"] = RouteParams["ForumId"].value.ToString();

            if(RouteParams["ForumThreadId"].isSet)
            { 
            ViewData["forumThreadId"] = RouteParams["ForumThreadId"].value.ToString();
            }
            else
            {
                ViewData["forumThreadId"] = 0;

            }
            if (RouteParams["ToForumThreadId"].isSet)
            { 
                ViewData["toForumThreadId"] = RouteParams["ToForumThreadId"].value.ToString();
            }
            else
            {
                ViewData["toForumThreadId"] = 0;

            }
            return View();
        }
        

        protected override void SetRouteParams()
        {
            base.SetRouteParams();

            string __actionMethodName =
                GetRouteValue(
                    "action"
                //HttpContext.Request.RequestContext.RouteData.Values["action"].ToString().ToLower()
                );

            switch (__actionMethodName)
            {
                case "index":
                    RouteParams.Add("Page", new RouteParam() { name = "Page", defaultValue = "1" });


                    break;
                case "postforumthread":
                    RouteParams.Add("ForumId", new RouteParam() { name = "ForumId" });
                    RouteParams.Add("ForumThreadId", new RouteParam() { name = "ForumThreadId" });
                    RouteParams.Add("ToForumThreadId", new RouteParam() { name = "ToForumThreadId" });
                    break;

            }

        }
        
        //protected void RenderThreads(ForumsThreads forumThreads)
        //{
        //    string acc = "";
        //    acc += "<ul>";
        //    acc += "<li>";
        //    acc += string.Format("{0}",
        //        forumThreads.Id+" "+forumThreads.Fid+" "+forumThreads.Ftid+" "+forumThreads.Text);
        //    if(forumThreads.Childs!=null)
        //    {
        //        acc += "<ul>";
        //        for (int i = 0; i < forumThreads.Childs.Count; i++)
        //        {
        //            acc += "<li>";
        //            acc += string.Format("{0}",
        //                forumThreads.Id + " " + forumThreads.Fid + " " + forumThreads.Ftid + " " + forumThreads.Text);

        //            RenderThreads(forumThreads.Childs[i]);

        //            acc += "</li>";
        //        }
        //        acc += "</ul>";
        //    }
        //    acc += "</li>";
        //    acc += "</ul>";
        //}
        protected override void FillViewData()
        {
            base.FillViewData(); 

        }


    }
}
