using Microsoft.Extensions.Configuration;

public class LegacyWebConfigConfigurationSource : FileConfigurationSource
{
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        FileProvider ??= builder.GetFileProvider();
        return new LegacyWebConfigConfigurationProvider(this);
    }
}

