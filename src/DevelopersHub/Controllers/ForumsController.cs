using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevelopersHub.Models;
using DevelopersHub.ViewModels;
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

        public IActionResult ViewForum(int Id)
        {
            TblForums _TblForums =
            UnitOfWork.Repository<DevelopersHub.Models.TblForums>().Get(x => x.Id == Id);

            _TblForums.M = 
            UnitOfWork.Repository<DevelopersHub.Models.TblMembers>().Get(x => x.Id == _TblForums.Id);


            List<TblForumsThreads> _list_TblForumsThreads = UnitOfWork.Repository<DevelopersHub.Models.TblForumsThreads>().GetAll(x => x.Fid == Id).ToList();
            List<ForumsThreads> _list_ForumsThreads = new List<ViewModels.ForumsThreads>();
            for (int i=0;i< _list_TblForumsThreads.Count;i++) 
            {
                
                if (_list_ForumsThreads.Find(x=>x.Id== _list_TblForumsThreads[i].Id)==null)
                { 
                ForumsThreads _ForumsThreads = new ForumsThreads();
                _ForumsThreads.Fid = _list_TblForumsThreads[i].Fid;
                _ForumsThreads.Ftid = _list_TblForumsThreads[i].Ftid;
                _ForumsThreads.Id = _list_TblForumsThreads[i].Id;
                _ForumsThreads.Mid = _list_TblForumsThreads[i].Mid;
                _ForumsThreads.Text = _list_TblForumsThreads[i] .Text;
                _list_ForumsThreads.Add(_ForumsThreads);
                }
                else
                {
                    continue;

                }
                for (int j = i; j < _list_TblForumsThreads.Count; j++)
                {
                    if (_list_TblForumsThreads[i].Id == _list_TblForumsThreads[j].Ftid)
                    {
                        ForumsThreads _ForumsThreads = new ForumsThreads();
                        _ForumsThreads.Fid = _list_TblForumsThreads[j].Fid;
                        _ForumsThreads.Ftid = _list_TblForumsThreads[j].Ftid;
                        _ForumsThreads.Id = _list_TblForumsThreads[j].Id;
                        _ForumsThreads.Mid = _list_TblForumsThreads[j].Mid;
                        _ForumsThreads.Text = _list_TblForumsThreads[j].Text;
                        _list_ForumsThreads.Add(_ForumsThreads);

                    }


                }
               

            }


            ViewForumViewModel _ViewForumViewModel = new ViewForumViewModel();
            _ViewForumViewModel.Forum = _TblForums;
            _ViewForumViewModel.ForumThreads = _list_ForumsThreads;

            return View(_ViewForumViewModel);
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


            }

        }

        protected override void FillViewData()
        {
            base.FillViewData(); 

        }


    }
}
