using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ConsulSpike.MathService.Controllers
{
    [Route("api/[controller]")]
    public class MathController : Controller
    {
        // GET: api/values
        [HttpGet]
        public string Operations(string operation, double[] inputs)
        {
            switch (operation)
            {
                case "add":
                    return inputs.Sum().ToString();
                case "subtract":
                    return (inputs.First() - inputs.Skip(1).Sum()).ToString();
                case "multiply":
                    return (inputs.Aggregate((total, next) => total * next)).ToString();
                case "divide":
                    return (inputs.Aggregate((total, next) => total / next)).ToString();
                default:
                    return "add|subtract|multiply|divide";
            }
        }
    }
}
