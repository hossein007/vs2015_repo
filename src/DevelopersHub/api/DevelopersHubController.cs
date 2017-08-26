using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevelopersHub.Models;





// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DevelopersHubWebApp.api
{
    //[Route("api/[controller]")]
    //public class RecipesController : Controller
    //{
    //    //Repository _repository = Repository.Instance;
    //    //// GET: api/recipes
    //    //[HttpGet]
    //    //public IEnumerable<Recipe> Get()
    //    //{
    //    //    return _repository.GetAllRecipes();
    //    //}


    //}



    public class DevelopersHubController : BaseController
    {
        [Route("api/Proposal", Name = "GetProposal")]
        [HttpGet]
        public List<TblProposals> GetProposals(int id,string FilterType)
        {
            
            if (FilterType == "member")
            {
                List<TblProposals> proposals = UnitOfWork.Repository<DevelopersHub.Models.TblProposals>().GetAll(x => x.Mid == id).ToList();
                return proposals;
            }
            else
            {
                List<TblProposals> proposals = UnitOfWork.Repository<DevelopersHub.Models.TblProposals>().GetAll(x => x.Id == id).ToList();
                return proposals;
            }

        }


        // POST api/values
        [Route("api/Proposal", Name = "CreateProposal")]
        [HttpPost]
        public TblProposals CreateProposal([FromBody] Newtonsoft.Json.Linq.JObject json_proposal)
        {
            TblProposals proposal = json_proposal.ToObject<TblProposals>();
            proposal.Id = 0;
            var __newProposal = UnitOfWork.Repository<DevelopersHub.Models.TblProposals>().Add(proposal);

            UnitOfWork.SaveChanges();

           return __newProposal.Entity;


        }

        [Route("api/CCC", Name = "CCC")]
        [HttpGet]
        public string CCC()
        {
           

            return "ppp";


        }



        [Route("api/PostForumThread", Name = "PostForumThread")]
        [HttpPost]
        public TblForumsThreads PostForumThread([FromBody] Newtonsoft.Json.Linq.JObject json_proposal)
        {
            TblForumsThreads forumThread = json_proposal.ToObject<TblForumsThreads>();
            forumThread.Id = 0;
            var __newforumThread = UnitOfWork.Repository<DevelopersHub.Models.TblForumsThreads>().Add(forumThread);

            UnitOfWork.SaveChanges();

            return __newforumThread.Entity;


        }

        [Route("api/PostForumThread_Update", Name = "PostForumThread_Update")]
        [HttpPut]
        public TblForumsThreads PostForumThread_Update([FromBody] Newtonsoft.Json.Linq.JObject json_proposal)
        {
            int __Id = Convert.ToInt32(json_proposal["ftid"].ToString());
            TblForumsThreads forumThread =
            UnitOfWork.Repository<DevelopersHub.Models.TblForumsThreads>().Get(x => x.Id == __Id);

            forumThread.Text = json_proposal["Text"].ToString();


            UnitOfWork.Repository<DevelopersHub.Models.TblForumsThreads>().Attach(forumThread);

            UnitOfWork.SaveChanges();

            return forumThread;


        }

        [Route("api/PostForumThread", Name = "PostForumThread")]
        [HttpGet]
        public PostForumThreadViewModel PostForumThread(int id)
        {
            if (id == 0)
                return null;
            PostForumThreadViewModel _PostForumThreadViewModel = new PostForumThreadViewModel();

             
            TblForumsThreads forumThread =
            UnitOfWork.Repository<DevelopersHub.Models.TblForumsThreads>().Get(x => x.Id == id);

            TblMembers member = UnitOfWork.Repository<DevelopersHub.Models.TblMembers>().Get(x => x.Id == forumThread.Mid);


            TblForums forum = UnitOfWork.Repository<DevelopersHub.Models.TblForums>().Get(x => x.Id == forumThread.Fid);


            _PostForumThreadViewModel.forum = forum;
            _PostForumThreadViewModel.member = member;
            _PostForumThreadViewModel.forumThread = forumThread;

            Newtonsoft.Json.JsonSerializerSettings _ = new Newtonsoft.Json.JsonSerializerSettings();
            
            return 
               _PostForumThreadViewModel
                ;


        }



    }

    //didnt json member
    public class PostForumThreadViewModel
    {
        public TblMembers member { get; set; }
        public TblForums forum { get; set; }

        public TblForumsThreads forumThread { get; set; }

    }



}
