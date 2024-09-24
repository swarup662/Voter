using System.ComponentModel.DataAnnotations;

namespace Voter_UI.Models
{
    public class VoterUIModel
    {


        public int VOTER_KEY { get; set; }


        [Required(ErrorMessage = "Please enter name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please enter only characters and whitespaces")]
        public string? NAME { get; set; }


        [Required(ErrorMessage = "Please choose Age")]
        public int AGE { get; set; }


        [Required(ErrorMessage = "Please choose a State")]
        public int STATE_ID { get; set; }
        public string? STATE_NAME { get; set; }


        //public int CITY_ID { get; set; }
        //public string CITY_NAME { get; set; }



        [Required(ErrorMessage = "Please choose Gender")]
        public int GENDER_ID { get; set; }
        public string? GENDER_NAME { get; set; }



        [Required(ErrorMessage = "Please choose Voter card no")]
        public string? VOTERCARD_NO { get; set; }

    }
}
