using System.IO;
using Microsoft.Extensions.Configuration;

public class YamlConfigurationProvider : FileConfigurationProvider
{
    public YamlConfigurationProvider(YamlConfigurationSource source) : base(source) { }

    public override void Load(Stream stream)
    {
        var parser = new YamlConfigurationFileParser();

        Data = parser.Parse(stream);
    }
}
