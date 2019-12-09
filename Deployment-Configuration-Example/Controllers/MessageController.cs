using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using System.Configuration;
using System.IO;
using Microsoft.Extensions.Localization;

namespace Deployment_Configuration_Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController: ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IConfiguration _config;
        private readonly IStringLocalizer<MessageController> _stringLocalizer;

        public MessageController(ILogger<MessageController> logger, 
                                 IStringLocalizer<MessageController> stringLocalizer, 
                                 IConfiguration config)
        {
            this._logger = logger;
            this._config = config;
            this._stringLocalizer = stringLocalizer;
        }

        [HttpGet]
        public Object Get()
        {
            _logger.LogInformation("GET://messages - Retrieving configuration settings");

            return new
            {
                appSettings_json = new 
                {
                    message = _config.GetValue<String>("TheMessage"),
                    connectionString = _config.GetSection("ANestedSetting").GetValue<String>("DatabaseConnection")
                }, 

                web_config = new 
                {
                    webConfigSetting = _config.GetValue<String>("SimonsSpecialSauce")
                }, 

                moreSettings_yml = new
                {
                    firstSpecialSetting = _config.GetValue<String>("firstSpecialSetting"), 
                    secondSpecialSetting = _config.GetValue<String>("secondSpecialSetting")
                },

                lastUpdated = DateTime.Now.ToUniversalTime()
            };
        }
    }

}
