using System.ComponentModel.DataAnnotations;

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Calculator.Configuration;

public abstract class ConfigHandlerBase
{
    protected string FilePath
        => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "calculator.yaml");

    protected bool Exists()
        => File.Exists(FilePath);

    protected class ValidatingNodeDeserializer : INodeDeserializer
    {
        private readonly INodeDeserializer _nodeDeserializer;

        public ValidatingNodeDeserializer(INodeDeserializer nodeDeserializer)
        {
            _nodeDeserializer = nodeDeserializer;
        }

        public bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value)
        {
            if (_nodeDeserializer.Deserialize(reader, expectedType, nestedObjectDeserializer, out value))
            {
                if (value == null)
                    return false;

                var context = new ValidationContext(value, null, null);
                Validator.ValidateObject(value, context, true);
                return true;
            }
            return false;
        }
    }
}
