﻿using CalculatorShell.Core;

using Spectre.Console;

namespace Calculator;

internal sealed class TerminalDialogs : IDialogs
{
    private const string LevelUpText = "Go to upper level";
    private const string ActualFolderText = "Selected Folder";
    private const string MoreChoicesText = "Use arrows Up and Down to select";
    private const string SelectFileText = "Select File";
    private const string SelectFolderText = "Select Folder";
    private const string SelectDriveText = "Select Drive";
    private const string SelectActualText = "Select Actual Folder";

    private readonly bool _isWindows;

    public TerminalDialogs()
    {
        if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            _isWindows = true;
    }

    private async Task<string> PathSelectPrompt(string startFolder, bool isFileSelector, CancellationToken cancellationToken = default)
    {
        int pageSize = Console.WindowHeight - 3;
        string lastFolder = startFolder;
        while (true)
        {
            string headerText = isFileSelector ? SelectFileText : SelectFolderText;
            string[] directoriesInFolder;
            Directory.SetCurrentDirectory(startFolder);

            AnsiConsole.Clear();
            AnsiConsole.WriteLine();
            var rule = new Rule($"[b][green]{headerText}[/][/]").Centered();
            AnsiConsole.Write(rule);

            AnsiConsole.WriteLine();
            AnsiConsole.Markup($"[b][Yellow]{ActualFolderText}: [/][/]");
            var path = new TextPath(startFolder);
            path.RootStyle = new Style(foreground: Color.Green);
            path.SeparatorStyle = new Style(foreground: Color.Green);
            path.StemStyle = new Style(foreground: Color.Blue);
            path.LeafStyle = new Style(foreground: Color.Yellow);
            AnsiConsole.Write(path);
            AnsiConsole.WriteLine();

            Dictionary<string, string> folders = new Dictionary<string, string>();
            // get list of drives

            try
            {
                directoriesInFolder = Directory.GetDirectories(Directory.GetCurrentDirectory());
                lastFolder = startFolder;
            }
            catch
            {
                if (startFolder == lastFolder) startFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                else startFolder = lastFolder;
                Directory.SetCurrentDirectory(startFolder);
                directoriesInFolder = Directory.GetDirectories(Directory.GetCurrentDirectory());
            }

            if (_isWindows)
            {
                folders.Add("[green]:computer_disk: " + SelectDriveText + "[/]", "/////");
            }
            try
            {
                if (new DirectoryInfo(startFolder).Parent != null)
                {
                    folders.Add("[green]:upwards_button: " + LevelUpText + "[/]", new DirectoryInfo(startFolder).Parent?.FullName ?? string.Empty);
                }
            }
            catch { }
            if (!isFileSelector)
            {
                folders.Add(":ok_button: [green]" + SelectActualText + "[/]", Directory.GetCurrentDirectory());
            }
            foreach (var d in directoriesInFolder)
            {
                int cut = 0;
                if (new DirectoryInfo(startFolder).Parent != null) cut = 1;
                string FolderName = d.Substring((startFolder.Length) + cut);
                string FolderPath = d;
                folders.Add(":file_folder: " + FolderName, FolderPath);
            }

            if (isFileSelector)
            {
                var fileList = Directory.GetFiles(startFolder);
                foreach (string file in fileList)
                {
                    string result = Path.GetFileName(file);
                    folders.Add(":abacus: " + result, file);
                }
            }
            // We got two sets of lists list files and list folders
            string title = isFileSelector ? SelectFileText : SelectFolderText;

            var prompt = new SelectionPrompt<string>()
                .Title($"[green]{title}:[/]")
                .PageSize(pageSize)
                .MoreChoicesText($"[grey]{MoreChoicesText}[/]")
                .AddChoices(folders.Keys);

            var selected = await prompt.ShowAsync(AnsiConsole.Console, cancellationToken);

            lastFolder = startFolder;

            var record = folders
                .Where(s => s.Key == selected).Select(s => s.Value)
                .FirstOrDefault() ?? string.Empty;

            if (record == "/////")
            {
                record = await DriveSelectPrompt(pageSize, cancellationToken);
                startFolder = record;
            }

            string responseType = Directory.Exists(record) ? "Directory" : "File";
            if (record == Directory.GetCurrentDirectory())
            {
                return startFolder;
            }

            if (responseType == "Directory")
            {
                try
                {
                    startFolder = record;
                }
                catch
                {
                    AnsiConsole.WriteLine("[red]You have no access to this folder[/]");
                }
            }
            else
            {
                return record;
            }
        }
    }

    private static async Task<string> DriveSelectPrompt(int pageSize, CancellationToken cancellationToken)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        foreach (string drive in Directory.GetLogicalDrives())
        {
            result.Add(":computer_disk: " + drive, drive);
        }
        AnsiConsole.Clear();
        AnsiConsole.WriteLine();
        var rule = new Rule($"[b][green]{SelectDriveText}[/][/]").Centered();
        AnsiConsole.Write(rule);

        AnsiConsole.WriteLine();
        string title = SelectDriveText;

        var prompt = new SelectionPrompt<string>()
            .Title($"[green]{title}:[/]")
            .PageSize(pageSize)
            .MoreChoicesText($"[grey]{MoreChoicesText}[/]")
            .AddChoices(result.Keys);

        var selected = await prompt.ShowAsync(AnsiConsole.Console, cancellationToken);

        return result
            .Where(s => s.Key == selected).Select(s => s.Value)
            .FirstOrDefault() ?? string.Empty;
    }

    public Task<string> SelectFile(CancellationToken cancellationToken)
    {
        return PathSelectPrompt(Environment.CurrentDirectory, true, cancellationToken);
    }
}
