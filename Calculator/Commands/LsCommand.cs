//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System.Text;

using CalculatorShell.Core;

namespace Calculator.Commands;
internal class LsCommand : ShellCommand
{
    public LsCommand(IHost host) : base(host)
    {
    }

    public override string[] Names => ["ls"];

    public override string Synopsys
        => "List files and directories in current directory";

    public override string HelpMessage
        => this.BuildHelpMessage<OptionsBase>();

    public override void ExecuteInternal(Arguments args)
    {
        DirectoryInfo di = new(Host.CurrentDirectory);
        TableData table = new("Name", "Extension", "Last write time", "Size", "Attributes");
        FillDirs(table, di.GetDirectories());
        FillFiles(table, di.GetFiles());
        Host.Output.Table(table);
    }

    private static string GetAttributeString(FileAttributes attributes)
    {
        List<string> attrs = [];

        if (attributes.HasFlag(FileAttributes.ReadOnly))
            attrs.Add("Ro");
        if (attributes.HasFlag(FileAttributes.Hidden))
            attrs.Add("H");
        if (attributes.HasFlag(FileAttributes.System))
            attrs.Add("System");
        if (attributes.HasFlag(FileAttributes.Archive))
            attrs.Add("A");
        if (attributes.HasFlag(FileAttributes.Device))
            attrs.Add("Dev");
        if (attributes.HasFlag(FileAttributes.Normal))
            attrs.Add("Norm");
        if (attributes.HasFlag(FileAttributes.Temporary))
            attrs.Add("Temp");
        if (attributes.HasFlag(FileAttributes.SparseFile))
            attrs.Add("Sparse");
        if (attributes.HasFlag(FileAttributes.ReparsePoint))
            attrs.Add("Reparse");
        if (attributes.HasFlag(FileAttributes.Compressed))
            attrs.Add("Comp");
        if (attributes.HasFlag(FileAttributes.Offline))
            attrs.Add("Comp");
        if (attributes.HasFlag(FileAttributes.NotContentIndexed))
            attrs.Add("NotIndexed");
        if (attributes.HasFlag(FileAttributes.Encrypted))
            attrs.Add("Encrypted");
        if (attributes.HasFlag(FileAttributes.IntegrityStream))
            attrs.Add("IntegrityStream");
        if (attributes.HasFlag(FileAttributes.NoScrubData))
            attrs.Add("IntegrityStream");

        attrs.Sort();

        var chunks = attrs.Chunk(3).ToArray();

        if (chunks.Length == 1)
            return string.Join(',', chunks.First());

        StringBuilder sb = new();

        foreach (var chunk in chunks)
        {
            _ = sb.AppendLine(string.Join(',', chunk));
        }
        return sb.ToString();
    }

    private void FillFiles(TableData table, FileInfo[] fileInfos)
    {
        foreach (var file in fileInfos)
        {
            table.AddRow(Path.GetFileNameWithoutExtension(file.Name),
                         file.Extension,
                         file.LastWriteTime.ToString(Host.CultureInfo),
                         file.Length.ToString(Host.CultureInfo),
                         GetAttributeString(file.Attributes));
        }
    }

    private void FillDirs(TableData table, DirectoryInfo[] directoryInfos)
    {
        foreach (var dir in directoryInfos)
        {
            table.AddRow(dir.Name,
                         "Dir",
                         dir.LastWriteTime.ToString(Host.CultureInfo),
                         "n/a",
                         GetAttributeString(dir.Attributes));
        }
    }
}
