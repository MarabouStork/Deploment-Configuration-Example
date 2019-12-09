using System.IO;
using Microsoft.Extensions.Configuration;

public class LegacyWebConfigConfigurationProvider : FileConfigurationProvider
{
    public LegacyWebConfigConfigurationProvider(LegacyWebConfigConfigurationSource source) : base(source) { }

    public override void Load(Stream stream)
    {
        var parser = new LegacyWebConfigConfigurationFileParser();

        Data = parser.Parse(stream);
    }
}
