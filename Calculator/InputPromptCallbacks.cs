using PrettyPrompt;
using PrettyPrompt.Completion;
using PrettyPrompt.Documents;
using PrettyPrompt.Highlighting;

namespace Calculator;
internal sealed class InputPromptCallbacks : PromptCallbacks
{
    public Dictionary<string, string> Data { get; set; }

    public InputPromptCallbacks()
    {
        Data = new();
    }

    protected override Task<IReadOnlyList<CompletionItem>> GetCompletionItemsAsync(string text, int caret, TextSpan spanToBeReplaced, CancellationToken cancellationToken)
    {
        if (caret > 1)
            return Task.FromResult(Array.Empty<CompletionItem>() as IReadOnlyList<CompletionItem>);

        var keyWord = text.Substring(spanToBeReplaced.Start);

        var canditates = Data
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
