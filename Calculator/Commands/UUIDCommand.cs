//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using CalculatorShell.Core;
using CalculatorShell.Engine.MathComponents;

using CommandLine;

namespace Calculator.Commands;

internal class UUIDCommand : ShellCommand
{
    public UUIDCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["uuid"];

    public override string Category 
        => CommandCategories.Hash;

    public override string Synopsys 
        => "Generates a Universally Unique Identifier (UUID)";

    public override string HelpMessage => throw new NotImplementedException();

    internal class UUIDOptions
    {
        [Option('v', "version", HelpText = "UUID version to generate", Required = true, Min = 1, Max = 5)]
        public int Version { get; set; }

        [Option('n', "namespace", HelpText = "Namespace for version 3 and 5 UUIDs")]
        public string Namespace { get; set; }

        [Option('N', "name", HelpText = "Name for version 3 and 5 UUIDs")]
        public string Name { get; set; }

        public UUIDOptions()
        {
            Namespace = string.Empty;
            Name = string.Empty;
        }
    }

    public override void ExecuteInternal(Arguments args)
    {
        var options = args.Parse<UUIDOptions>(Host);

        if (options.Version == 2)
        {
            Host.Output.Error("Version 2 UUID is not supported");
            return;
        }

        UUID uuid = Create(options.Version, options.Namespace, options.Name);
        Host.Output.Result(uuid);
    }

    private UUID Create(int version, string @namespace, string name)
    {
        if (version == 3 || version == 5)
        {
            if (string.IsNullOrEmpty(@namespace))
                throw new CommandException("Namespace is required for version 3 and 5 UUIDs");

            if (string.IsNullOrEmpty(name))
                throw new CommandException("Name is required for version 3 and 5 UUIDs");
        }

        return version switch
        {
            1 => UUIDGenerator.GenerateVersion1(),
            3 => UUIDGenerator.GenerateVersion3(UUID.Parse(@namespace, null), name),
            4 => UUIDGenerator.GenerateVersion4(),
            5 => UUIDGenerator.GenerateVersion5(UUID.Parse(@namespace, null), name),
            _ => throw new CommandException($"UUID v{version} is not supported"),
        };
    }
}
