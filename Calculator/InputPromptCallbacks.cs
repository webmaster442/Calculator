using CalculatorShell.Core;

using PrettyPrompt;
using PrettyPrompt.Completion;
using PrettyPrompt.Documents;
using PrettyPrompt.Highlighting;

namespace Calculator;
internal sealed class InputPromptCallbacks : PromptCallbacks
{
    public IReadOnlyDictionary<string, string> CommandsWithDescription { get; set; }
    public IReadOnlyDictionary<string, IArgumentCompleter> AutoCompletableCommands { get; set; }

    public InputPromptCallbacks()
    {
        CommandsWithDescription = new Dictionary<string, string>();
        AutoCompletableCommands = new Dictionary<string, IArgumentCompleter>();
    }

    protected override Task<IReadOnlyList<CompletionItem>> GetCompletionItemsAsync(string text, int caret, TextSpan spanToBeReplaced, CancellationToken cancellationToken)
    {
        if (caret > 1)
            return Task.FromResult(Array.Empty<CompletionItem>() as IReadOnlyList<CompletionItem>);

        var keyWord = text.Substring(spanToBeReplaced.Start);

        var canditates = CommandsWithDescription
            .Where(x => x.Key.StartsWith(keyWord, StringComparison.InvariantCultureIgnoreCase))
            .OrderBy(x => x.Key)
            .Select(x => new CompletionItem(x.Key, getExtendedDescription: (ct) =>
            {
                return Task.FromResult(new FormattedString(x.Value));
            }))
            .ToList() as IReadOnlyList<CompletionItem>;

        return Task.FromResult(canditates);
    }
}
