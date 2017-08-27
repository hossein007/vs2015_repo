using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevelopersHub.Models;
using DevelopersHub.ViewModels;
namespace DevelopersHub.Controllers
{
    public class ProposalsController : BaseController
    {
        public ProposalsController(Microsoft​.AspNetCore​.Http.IHttpContextAccessor httpContextAccessor, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment) : base(httpContextAccessor, hostingEnvironment)
        {


        }
        public IActionResult Index()
        {

            List<TblProposals> __list_proposals = UnitOfWork.Repository<TblProposals>().GetAll().ToList();


            ProposalsViewModel __proposalsViewModel = new ViewModels.ProposalsViewModel();



            __proposalsViewModel.Proposals = __list_proposals; ;

            ViewModel = __proposalsViewModel;



            return View(__proposalsViewModel);

        }

        public IActionResult ViewProposal(int Id)
        {
            TblProposals _TblProposals =
            UnitOfWork.Repository<DevelopersHub.Models.TblProposals>().Get(x => x.Id == Id);

            _TblProposals.M = 
            UnitOfWork.Repository<DevelopersHub.Models.TblMembers>().Get(x => x.Id == _TblProposals.Id);




            ViewProposalViewModel _ViewProposalViewModel = new ViewProposalViewModel();
            _ViewProposalViewModel.Proposal = _TblProposals;

            return View(_ViewProposalViewModel);
        }
        public IActionResult MemberProposals(int Id)
        {
            TblMembers _member = UnitOfWork.Repository<DevelopersHub.Models.TblMembers>().Get(x => x.Id == Id);

            //List<TblProposals> _list_TblProposals =
            //UnitOfWork.Repository<DevelopersHub.Models.TblProposals>().GetAll(x => x.Mid == MemberId).ToList();


            //int _Count = _TblMembers.Count();


            QueryParameters _queryparameters = new QueryParameters();
            _queryparameters.SqlSourceObjectName = "Tbl_Proposals";
            _queryparameters.Where = "mid=@mid";
            _queryparameters.SqlParams.Add(new System.Data.SqlClient.SqlParameter("@mid", Id));
            _queryparameters.PageIndex = Convert.ToInt32(RouteParams["Page"].value);
            _queryparameters.PageSize = 2;

            Microsoft.EntityFrameworkCore.Storage.RelationalDataReader
            _RelationalDataReader =
            UnitOfWork.GetResult<DevelopersHub.Models.TblProposals>(_queryparameters);
            MemberProposalsViewModel __MemberProposalsViewModel = new ViewModels.MemberProposalsViewModel();

            __MemberProposalsViewModel.PageCount = _queryparameters.PageCount;
            __MemberProposalsViewModel.PageNumber = Convert.ToInt32(RouteParams["Page"].value);

            __MemberProposalsViewModel.Proposals = new List<TblProposals>();

            ViewModel = __MemberProposalsViewModel;

            while (
            _RelationalDataReader.DbDataReader.Read())
            {
                TblProposals tblproposals = new TblProposals();
                tblproposals.Title = _RelationalDataReader.DbDataReader["Title"].ToString();
                tblproposals.Technologies = _RelationalDataReader.DbDataReader["Technologies"].ToString();
                tblproposals.SnapshotFile = _RelationalDataReader.DbDataReader["SnapshotFile"].ToString();
                tblproposals.Id = Convert.ToInt32(_RelationalDataReader.DbDataReader["Id"].ToString());
                __MemberProposalsViewModel.Proposals.Add(tblproposals);
            }



            __MemberProposalsViewModel.Member = _member; 


            return View(__MemberProposalsViewModel);



        }

        public IActionResult CreateProposal()
        {
            ViewData["memberId"] = LoggedInUserId;
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
                case "memberproposals":

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
