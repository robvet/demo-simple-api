using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Controllers
{
    // Kudos to CSA Scott Rutz for the idea for this class!!
    
    [ApiController]
    public class StatusController : Controller
    {
        private readonly IConfiguration _configuration;

        public StatusController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpGet("status")]
        public IActionResult Get()
        {
            var systemInfo = new
            {
                Environment = _configuration["APPLICATION_VERSION"],
                OS = OperatingSystem.IsLinux() ? "Linux" : "Windows",
                OSVersion = Environment.OSVersion,
                MachineName = Environment.MachineName
            };
            
            return new OkObjectResult(systemInfo);
        }

        [HttpGet("status/full")]
        public IEnumerable<KeyValuePair<string, string>> GetFull()
        {
            var values = _configuration.AsEnumerable().Select(c => new KeyValuePair<string,string>(c.Key,c.Value));
            return values;

            //var variables = new List<KeyValuePair<string, string>>();
            //foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
            //    variables.Add(new KeyValuePair<string, string>(de.Key.ToString(), de.Value.ToString()));
            //return variables;
        }
    }
}
