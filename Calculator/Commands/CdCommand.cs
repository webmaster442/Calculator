using Calculator.Messages;

using CalculatorShell.Core;

namespace Calculator.Commands;

internal sealed class CdCommand : ShellCommandAsync
{
    public CdCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["cd"];

    public override string Synopsys 
        => "Changes the current working directory";
    
    public override async Task ExecuteInternal(Arguments args, CancellationToken cancellationToken)
    {
        string folder;
        bool pingBack = false;
        if (args.Length < 1)
        {
            folder = await Host.Dialogs.SelectDirectory(cancellationToken);
            pingBack = true;
        }
        else
        {
            folder = args[0];
        }
        Host.MessageBus.Broadcast(new CurrentDirMessage(Guid.Empty, folder));

        if (pingBack)
        {
            folder = Host.MessageBus
                .Request<string, RequestCurrentDirMessage>(new RequestCurrentDirMessage(Guid.Empty))
                .First();

            Host.Output.Result(folder);
        }
    }
}
