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

            var nestedSection = _config.GetSection("ANestedSetting");
            var connectionString = nestedSection != null ? nestedSection.GetValue<String>("DatabaseConnection") : null;

            var legacySettingA = _config.GetValue<String>("SettingA");
            var legacyConnectionString = _config.GetValue<String>("ConnectionStrings:LegacyConnectionString");


            return new
            {
                appSettings_json = new {
                    message = message,
                    connectionString = connectionString, 
                }, 

                web_config = new
                {
                    setting = legacySettingA, 
                    connectionString = legacyConnectionString
                },

                lastUpdated = DateTime.Now.ToUniversalTime()
            };
        }
    }

}
