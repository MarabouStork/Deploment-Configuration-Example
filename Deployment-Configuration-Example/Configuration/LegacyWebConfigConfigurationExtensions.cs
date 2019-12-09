using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Configuration;

public static class LegacyWebConfigConfigurationExtensions
{
    public static IConfigurationBuilder AddLegacyWebConfigFile(this IConfigurationBuilder builder, string path)
    {
        return AddLegacyWebConfigFile(builder, provider: null, path: path, optional: false, reloadOnChange: false);
    }

    public static IConfigurationBuilder AddLegacyWebConfigFile(this IConfigurationBuilder builder, string path, bool optional)
    {
        return AddLegacyWebConfigFile(builder, provider: null, path: path, optional: optional, reloadOnChange: false);
    }

    public static IConfigurationBuilder AddLegacyWebConfigFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
    {
        return AddLegacyWebConfigFile(builder, provider: null, path: path, optional: optional, reloadOnChange: reloadOnChange);
    }

    public static IConfigurationBuilder AddLegacyWebConfigFile(this IConfigurationBuilder builder, IFileProvider provider, string path, bool optional, bool reloadOnChange)
    {
        if (provider == null && Path.IsPathRooted(path))
        {
            provider = new PhysicalFileProvider(Path.GetDirectoryName(path));
            path = Path.GetFileName(path);
        }
        var source = new YamlConfigurationSource
        {
            FileProvider = provider,
            Path = path,
            Optional = optional,
            ReloadOnChange = reloadOnChange
        };
        builder.Add(source);
        return builder;
    }
}