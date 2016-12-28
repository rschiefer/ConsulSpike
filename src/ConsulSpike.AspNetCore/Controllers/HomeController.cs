using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConsulSpike.AspNetCore.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Math(string operation, string inputs)
        {
            var result = "test";

            var output = new StringBuilder();
            output.AppendLine($"operation = {operation}");
            output.AppendLine($"inputs = {inputs}");
            output.AppendLine($"result = {result}");

            return Content(output.ToString());
        }
    }
}
