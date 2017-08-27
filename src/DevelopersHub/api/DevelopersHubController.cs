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



    }



}
