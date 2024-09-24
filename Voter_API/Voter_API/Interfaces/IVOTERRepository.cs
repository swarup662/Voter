using Voter_API.Models;

namespace Voter_API.Interfaces
{
    public interface IVOTERRepository
    {
        public int SaveVote(VoterAPIModel model, string REC_TYPE);

        public List<VoterAPIModel> GetVote(int VOTER_kEY);

        public int DeleteVote(VoterAPIModel model, string REC_TYPE);

        public List<VoterAPIModel> getState(int STATE_ID);

 //     public List<VoterModel> getCity(int STATE_ID, int CITY_ID);

        public List<VoterAPIModel> getGender(int GENDER_ID);
    }
}
