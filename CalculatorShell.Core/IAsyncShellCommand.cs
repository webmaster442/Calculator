﻿//-----------------------------------------------------------------------------
// (c) 2024 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace CalculatorShell.Core;

/// <summary>
/// Represents a shell command that will be executed asyncronously
/// </summary>
public interface IAsyncShellCommand : IShellCommand
{
    /// <summary>
    /// The entry point of the command
    /// </summary>
    /// <param name="args">Command arguments</param>
    /// <param name="cancellationToken">A CancellationToken</param>
    /// <returns>A task</returns>
    Task Execute(Arguments args, CancellationToken cancellationToken);
}
