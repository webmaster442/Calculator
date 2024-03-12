using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.NodeDeserializers;

namespace Calculator.Configuration;

public sealed class ConfigurationReader : ConfigHandlerBase
{
    public Config Deserialize()
    {
        using var configFile = File.OpenText(FilePath);
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .WithEnumNamingConvention(PascalCaseNamingConvention.Instance)
            .WithNodeDeserializer(inner => new ValidatingNodeDeserializer(inner), s => s.InsteadOf<ObjectNodeDeserializer>())
            .Build();

        return deserializer.Deserialize<Config>(configFile);
    }
}
