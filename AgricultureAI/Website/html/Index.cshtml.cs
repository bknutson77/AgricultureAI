using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AgricultureAI.Persistence.HigherLevel;
using AgricultureAI.MachineLearning;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MachineLearning;

namespace AgricultureAI.Pages
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        // Attempt Login Redirect:
        public JsonResult OnGetAttemptLogin()
        {
            // Get the query string:
            var queryStringObject = Request.QueryString;
            string queryString = queryStringObject.ToString();

            // Parse the query string and extract parameters:
            string[] queryParams = queryString.Split("&");
            string username = queryParams[1].Replace("username=", "");
            string password = queryParams[2].Replace("password=", "");

            // Attempt the login and return the response:
            return new JsonResult(UserManagement.AttemptLogin(username, password));
        }

        // Attempt Register Redirect:
        public JsonResult OnGetAttemptRegister()
        {
            // Get the query string:
            var queryStringObject = Request.QueryString;
            string queryString = queryStringObject.ToString();

            // Parse the query string and extract parameters:
            string[] queryParams = queryString.Split("&");
            string name = queryParams[1].Replace("name=", "");
            string email = queryParams[2].Replace("email=", "");
            string occupation = queryParams[3].Replace("occupation=", "");
            string plantExpert = queryParams[4].Replace("plantExpert=", "");
            string username = queryParams[5].Replace("username=", "");
            string password = queryParams[6].Replace("password=", "");

            // Attempt the login and return the response:
            return new JsonResult(UserManagement.AttemptRegister(name, email, occupation, plantExpert, username, password));
        }

        // Attempt Getting Image Keys:
        public JsonResult OnGetImageKeys()
        {
            // Attempt to return the response:
            return new JsonResult(GroundTruth.GetImageKeys());
        }

        // Attempt AI Prediction:
        public JsonResult OnGetAIPrediction()
        {
            // Get the query string:
            var queryStringObject = Request.QueryString;
            string queryString = queryStringObject.ToString();

            // Parse the query string and extract parameters:
            string[] queryParams = queryString.Split("&");
            string imageURL = queryParams[1].Replace("imageURL=", "");

            // Attempt to return the response:
            return new JsonResult(MLModel.Predict(imageURL).Prediction);
        }

        // Attempt AI Prediction:
        public JsonResult OnGetGroundTruth()
        {
            // Get the query string:
            var queryStringObject = Request.QueryString;
            string queryString = queryStringObject.ToString();

            // Parse the query string and extract parameters:
            string[] queryParams = queryString.Split("&");
            string imageURL = queryParams[1].Replace("imageURL=", "");

            // Attempt to return the response:
            return new JsonResult(GroundTruth.Lookup(imageURL));
        }
    }
}
