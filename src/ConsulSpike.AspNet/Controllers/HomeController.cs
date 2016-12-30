using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using ConsulSpike.AspNet.Config;
using RestSharp;
using System.Web.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConsulSpike.AspNet.Controllers
{
    public class HomeController : Controller
    {
        private ConfigData _configData;

        public HomeController(IConfigDataProvider provider)
        {
            _configData = provider.GetConfigData();
        }
        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Math(string operation, string inputs)
        {
            var result = string.Empty;

            var client = new RestClient(_configData.ServiceUrl);
            var request = new RestRequest("math");
            request.AddParameter("operation", operation);
            foreach (var input in inputs.Split('\n'))
            {
                request.AddParameter("inputs", input.Trim());
            }
            result = client.ExecuteTaskAsync<string>(request).Result.Content;            

            var output = new StringBuilder();
            output.AppendLine($"operation = {operation}");
            output.AppendLine($"inputs = {inputs}");
            output.AppendLine($"result = {result}");

            return Content(output.ToString());
        }
    }
}
