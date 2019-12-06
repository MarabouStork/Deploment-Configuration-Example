using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Deployment_Configuration_Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController: ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IConfiguration _config;

        public MessageController(ILogger<MessageController> logger, IConfiguration config)
        {
            this._logger = logger;
            this._config = config;
        }

        [HttpGet]
        public Object Get()
        {
            var message = _config.GetValue<String>("TheMessage");

            var connectionString = _config
                                        .GetSection("ANestedSetting")
                                        .GetValue<String>("DatabaseConnection");

            var substitutedValue = _config
                                        .GetSection("SomethingVeryNested")
                                        .GetSection("DeeperStill")
                                        .GetSection("OKThatsEnough")
                                        .GetValue<String>("ASubstitutedValue");
              
            return new
            {
                message = message,
                connectionString = connectionString, 
                substitutedValue = substitutedValue
            };


            // return _config.GetValue<String>("TheMessage");
        }
    }

}
