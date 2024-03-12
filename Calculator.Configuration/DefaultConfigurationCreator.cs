//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Calculator.Configuration;

public sealed class DefaultConfigurationCreator : ConfigHandlerBase
{
    public void CreateDefaultConfigIfNeeded()
    {
        if (Exists())
            return;

        using var configFile = File.CreateText(FilePath);

        var serializer = new SerializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .WithEnumNamingConvention(PascalCaseNamingConvention.Instance)
            .Build();

        serializer.Serialize(configFile, new Config());
    }
}