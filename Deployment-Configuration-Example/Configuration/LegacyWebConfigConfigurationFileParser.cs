using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;

internal class LegacyWebConfigConfigurationFileParser
{
    public IDictionary<string, string> Parse(Stream input)
    {
        var retVal = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        var config = XDocument.Load(input);

        var databaseConnectionStrings = config.Descendants().SingleOrDefault(n => n.Name == "connectionStrings");
        if (databaseConnectionStrings != null)
        {
            databaseConnectionStrings.Descendants().ToList().ForEach((n) =>
            {
                retVal.Add($"ConnectionStrings:{n.Attribute("name").Value}", n.Attribute("connectionString").Value);
            });
        }

        var appSettings = config.Descendants().SingleOrDefault(n => n.Name == "appSettings");
        if (appSettings != null)
        {
            appSettings.Descendants().ToList().ForEach((n) =>
            {
                retVal.Add($"{n.Attribute("key").Value}", n.Attribute("value").Value);
            });
        }

        return retVal;
    }
}