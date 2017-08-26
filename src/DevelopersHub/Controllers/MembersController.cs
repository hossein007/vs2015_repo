using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevelopersHub.Models;
using DevelopersHub.ViewModels;
namespace DevelopersHub.Controllers
{
    public class MembersController : BaseController
    {
        public MembersController(Microsoft​.AspNetCore​.Http.IHttpContextAccessor httpContextAccessor, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment) : base(httpContextAccessor, hostingEnvironment)
        {


        }
        public IActionResult Index()
        {
            List<Models.TblMembers> _TblMembers =
            UnitOfWork.Repository<DevelopersHub.Models.TblMembers>().GetAll().ToList<Models.TblMembers>();

            int _Count = _TblMembers.Count();


            QueryParameters _queryparameters = new QueryParameters();
            _queryparameters.SqlSourceObjectName = "Tbl_Members";
            _queryparameters.PageIndex = Convert.ToInt32(RouteParams["Page"].value); ;
            _queryparameters.PageSize = 2;

            Microsoft.EntityFrameworkCore.Storage.RelationalDataReader
            _RelationalDataReader =
            UnitOfWork.GetResult<DevelopersHub.Models.TblMembers>(_queryparameters);
            MembersViewModel __membersViewModel = new ViewModels.MembersViewModel();

            __membersViewModel.PageCount = _queryparameters.PageCount;
            __membersViewModel.PageNumber = Convert.ToInt32(RouteParams["Page"].value);

            __membersViewModel.Members = new List<TblMembers>();

            ViewModel = __membersViewModel;

            while (
            _RelationalDataReader.DbDataReader.Read())
            {
                TblMembers tblmembers = new TblMembers();
                tblmembers.Fname = _RelationalDataReader.DbDataReader["FName"].ToString();
                tblmembers.Sname = _RelationalDataReader.DbDataReader["SName"].ToString();
                tblmembers.Bdate = _RelationalDataReader.DbDataReader["Bdate"].ToString();
                tblmembers.Id = Convert.ToInt32(_RelationalDataReader.DbDataReader["Id"].ToString());
                __membersViewModel.Members.Add(tblmembers);
            }

            ViewData["Message"] = "Forums " + _Count.ToString();


            return View(__membersViewModel);

        }

        public IActionResult MemberDetail(int Id)
        {
            TblMembers _TblMembers =
            UnitOfWork.Repository<DevelopersHub.Models.TblMembers>().Get(x => x.Id == Id);
            MemberDetailViewModel _MemberDetailViewModel = new MemberDetailViewModel();
            _MemberDetailViewModel.Member = _TblMembers;

            return View(_MemberDetailViewModel);
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
