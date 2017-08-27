using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevelopersHub.Models;
using DevelopersHub.ViewModels;
namespace DevelopersHub.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(Microsoft​.AspNetCore​.Http.IHttpContextAccessor httpContextAccessor, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment) : base(httpContextAccessor, hostingEnvironment)
        {


        }
        public IActionResult Index()
        {
            List<Models.TblMembers> _TblMembers =
            UnitOfWork.Repository<DevelopersHub.Models.TblMembers>().GetAll().ToList<Models.TblMembers>();

            int _Count = _TblMembers.Count();


            QueryParameters _queryparameters = new QueryParameters();
            _queryparameters.SqlSourceObjectName = "Tbl_Members";
            _queryparameters.PageIndex = 0;
            _queryparameters.PageSize = 11;

            Microsoft.EntityFrameworkCore.Storage.RelationalDataReader
            _RelationalDataReader =
            UnitOfWork.GetResult<DevelopersHub.Models.TblMembers>(_queryparameters);
            IndexViewModel __indexViewModel = new ViewModels.IndexViewModel();

            __indexViewModel.Members = new List<TblMembers>();

            while (
            _RelationalDataReader.DbDataReader.Read())
            {
                TblMembers tblmembers = new TblMembers();
                tblmembers.Fname = _RelationalDataReader.DbDataReader["FName"].ToString();
                tblmembers.Sname = _RelationalDataReader.DbDataReader["SName"].ToString();
                tblmembers.Id = Convert.ToInt32(_RelationalDataReader.DbDataReader["Id"].ToString());
                __indexViewModel.Members.Add(tblmembers);
            }

            UnitOfWork.CloseReader();

            List<TblProposals> __list_proposals = UnitOfWork.Repository<TblProposals>().GetAll().ToList();


            __indexViewModel.Proposals = __list_proposals;

            foreach(TblProposals item in __indexViewModel.Proposals)
            {
                item.M =
            UnitOfWork.Repository<DevelopersHub.Models.TblMembers>().Get(x => x.Id == item.Id);

            }




            ViewModel = __indexViewModel;

            ViewData["Message"] = "Forums " + _Count.ToString();


            return View(__indexViewModel);
            
        }

        public IActionResult Forums()
        {
            

            

            Models.TblMembers[] _TblMembers =
            UnitOfWork.Repository<DevelopersHub.Models.TblMembers>().GetAll().ToArray<Models.TblMembers>();

            int _Count = _TblMembers.Count();
            


            ViewData["Message"] = "Forums "+ _Count.ToString();
            
            return View(_TblMembers);
        }

        
        public IActionResult Proposals()
        {
            ViewData["Message"] = "Proposals";

            return View();
        }
        public IActionResult Signup()
        {
            ViewData["Message"] = "Signup";

            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
