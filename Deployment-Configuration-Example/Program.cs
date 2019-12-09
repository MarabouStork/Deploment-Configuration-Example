using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Deployment_Configuration_Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddYamlFile("moreSettings.yml");


                    config.AddXmlFile("web.config", optional: false, reloadOnChange: false);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }



    //public class LegacyConfigurationProvider : ConfigurationProvider, IConfigurationSource
    //{
    //    public LegacyConfigurationProvider(FileConfigurationSource source) : base(source)
    //    {
    //    }

    //    public IConfigurationProvider Build(IConfigurationBuilder builder)
    //    {
    //        return this;
    //    }

    //    public override void Load(Stream stream)
    //    {
    //        var config = XDocument.Load(stream);

    //        config.Descendants()
    //               .Single(n => n.Name == "connectionStrings")
    //               .Descendants().ToList().ForEach((n) =>
    //               {
    //                   Data.Add($"ConnectionStrings:{n.Attribute("name").Value}", n.Attribute("connectionString").Value);
    //               });

    //        config.Descendants()
    //            .Single(n => n.Name == "appSettings")
    //            .Descendants().ToList().ForEach((n) =>
    //            {
    //                Data.Add($"ConnectionStrings:{n.Attribute("name").Value}", n.Attribute("value").Value);
    //            });
    //    }
    //}
}