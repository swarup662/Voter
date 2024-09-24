using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using Voter_UI.Models;

namespace Voter_UI.Controllers
{
    public class VoterUIController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly string BaseUrl;



        public VoterUIController(IConfiguration configuration)
        {
            _configuration = configuration;
            BaseUrl = configuration["BaseUrl"];
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                string urlParameters = "GetVote";
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(BaseUrl + urlParameters);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        List<VoterUIModel> lst = JsonConvert.DeserializeObject<List<VoterUIModel>>(responseContent);

                        ViewBag.Vote = lst;
                        ViewBag.genderlist = await GetGender();
                      
                        var statedropdown = await GetState();
                        ViewBag.state = new SelectList(statedropdown, "Value", "Text");
                        return View();
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        // this is the State method
        [HttpGet]
        public async Task<List<SelectListItem>> GetState()
        {
            try
            {
                List<SelectListItem> statedropdown = new List<SelectListItem>();
                string urlParameters = "GetState/";
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(BaseUrl + urlParameters + "0");

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        statedropdown = JsonConvert.DeserializeObject<List<SelectListItem>>(responseContent);
                    }
                }
                return statedropdown;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }







        // this is a Gender method

        [HttpGet]
        public async Task<List<VoterUIModel>> GetGender()
        {
            try
            {
                List<VoterUIModel> lst = new List<VoterUIModel>();
                string urlParameters = "GetGender/";
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(BaseUrl + urlParameters + "0");

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        lst = JsonConvert.DeserializeObject<List<VoterUIModel>>(responseContent);
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        // this is a HttpPost method
        [HttpPost]
        public async Task<IActionResult> SaveVote(VoterUIModel model)
        {
            try
            {


                string urlParameters = "SaveVote";
                string data = JsonConvert.SerializeObject(model);
                using (var httpClient = new HttpClient())
                {
                    StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(BaseUrl + urlParameters, Content);



                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        int r = JsonConvert.DeserializeObject<int>(responseData);

                        if (r > 0)
                        {
                            TempData["MSG"] = "success";

                        }
                        else if (r == -1)
                        {
                            TempData["MSG"] = "exist";

                        }
                        else
                        {
                            TempData["MSG"] = "Fail";
                        }



                        return Redirect("/VoterUI/Index");



                    }
                    else
                    {
                        return Json("Error");

                    }

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        // this is a HttpGet method
        [HttpPost]
        public async Task<IActionResult> DeleteVote(int id)
        {
            try
            {


                string urlParameters = "DeleteVote";

                using (var httpClient = new HttpClient())
                {

                    HttpResponseMessage response = await httpClient.GetAsync(BaseUrl + urlParameters + "/" + id);



                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        int r = JsonConvert.DeserializeObject<int>(responseData);
                        return Json(r);

                    }
                    else
                    {
                        return Json("Error");

                    }

                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

        }






        [HttpGet]
        public async Task<JsonResult> EditVote(int id)
        {

            try
            {


                using (var httpClient = new HttpClient())
                {

                    string urlParameters = "EditVote";
                    HttpResponseMessage response = await httpClient.GetAsync(BaseUrl + urlParameters + "/" + id);



                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();

                        VoterUIModel lst = JsonConvert.DeserializeObject<VoterUIModel>(data);


                        var info = lst;
                        return Json(info);

                    }
                    else
                    {

                        return Json("Error");
                    }

                }

            }

            catch (Exception ex)
            {
                throw ex;
            }


        }

    }
}
