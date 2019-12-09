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

        config.Descendants()
               .Single(n => n.Name == "connectionStrings")
               .Descendants().ToList().ForEach((n) =>
               {
                   retVal.Add($"ConnectionStrings:{n.Attribute("name").Value}", n.Attribute("connectionString").Value);
               });

        config.Descendants()
            .Single(n => n.Name == "appSettings")
            .Descendants().ToList().ForEach((n) =>
            {
                retVal.Add($"ConnectionStrings:{n.Attribute("name").Value}", n.Attribute("value").Value);
            });

        return retVal;
    }
}