﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using RestSharp.Portable;
using RestSharp.Portable.WebRequest;
using Microsoft.Extensions.Options;
using ConsulSpike.AspNetCore.Config;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConsulSpike.AspNetCore.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private IOptions<ConfigData> _configData;

        public HomeController(IOptions<ConfigData> configData)
        {
            _configData = configData;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Math(string operation, string inputs)
        {
            var result = string.Empty;

            using (var client = new RestClient(_configData.Value.ServiceUrl))
            {
                var request = new RestRequest("math");
                request.AddParameter("operation", operation);
                foreach (var input in inputs.Split('\n'))
                {
                    request.AddParameter("inputs", input.Trim());
                }
                result = (await client.Execute<string>(request)).Content;
            }

            var output = new StringBuilder();
            output.AppendLine($"operation = {operation}");
            output.AppendLine($"inputs = {inputs}");
            output.AppendLine($"result = {result}");

            return Content(output.ToString());
        }
    }
}
