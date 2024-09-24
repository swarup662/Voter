using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Voter_API.Interfaces;
using Voter_API.Models;
using Voter_API.Repository;

namespace Voter_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoterAPIController : ControllerBase
    {
        private IVOTERRepository IVOTERRepository;
        public VoterAPIController(IVOTERRepository VoterRepository)
        {
            IVOTERRepository = VoterRepository;
        }



        [HttpGet]
        [Route("GetVote")]
        public List<VoterAPIModel> GetVote()
        { 
            List<VoterAPIModel> Vote = IVOTERRepository.GetVote(0);
            return Vote;
           
        }


        [HttpGet]
        [Route("GetGender/{id}")]
        public async Task<List<VoterAPIModel>> GetGender(int id)
        {
            List<VoterAPIModel> Vote = IVOTERRepository.getGender(0);

            return Vote;
            
        }



        [HttpGet]
        [Route("GetState/{id}")]
        public async Task<List<SelectListItem>> GetState(int id)
        {
            List<VoterAPIModel> statelist = IVOTERRepository.getState(0);

            List<SelectListItem> statedropdown = new List<SelectListItem>();
            statedropdown.Add(new SelectListItem { Text = "Select State", Value = "" });

            foreach (var st in statelist)
            {
                statedropdown.Add(new SelectListItem { Text = st.STATE_NAME, Value = st.STATE_ID.ToString() });
            }
            return statedropdown;
        }


        [HttpPost]
        [Route("SaveVote")]
       //public async Task<IActionResult> SaveVote(VoterModel model)
        public async Task<int> SaveVote(VoterAPIModel model)
        {


            int r;
            if (model.VOTER_KEY == 0)
            {
                 r = IVOTERRepository.SaveVote(model, "INSERT");

            }
            else
            {
                 r = IVOTERRepository.SaveVote(model, "UPDATE");


            }
;
            return r ;
        }




       [HttpGet]
       [Route("DeleteVote/{id}")]
        //public IActionResult DeleteVote(int id)
        public async Task<int> DeleteVote(int id)
        {
            int r;
            VoterAPIModel model = new VoterAPIModel();
            model.VOTER_KEY = id;
            r = IVOTERRepository.DeleteVote(model, "DELETE");

            return r;
           // return Json(new { res = r });
        }




        
        [HttpGet]
        [Route("EditVote/{id}")]
        public async Task<VoterAPIModel> EditVote(int id)
        {

            List<VoterAPIModel> lstobj = IVOTERRepository.GetVote(id);

            if (lstobj.Count == 1)
             {
                return lstobj.FirstOrDefault();
            }

            return null;
            }
    }
}
