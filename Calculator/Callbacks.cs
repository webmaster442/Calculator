using PrettyPrompt;
using PrettyPrompt.Completion;
using PrettyPrompt.Documents;

namespace Calculator;
internal class Callbacks : PromptCallbacks
{
    public string[] Data { get; set; }

    public Callbacks()
    {
        Data = Array.Empty<string>();
    }

    protected override Task<IReadOnlyList<CompletionItem>> GetCompletionItemsAsync(string text, int caret, TextSpan spanToBeReplaced, CancellationToken cancellationToken)
    {
        var keyWord = text.Substring(spanToBeReplaced.Start);

        var canditates = Data
            .Where(x => x.StartsWith(keyWord, StringComparison.InvariantCultureIgnoreCase))
            .Order()
            .Select(x => new CompletionItem(x))
            .ToList() as IReadOnlyList<CompletionItem>;

        return Task.FromResult(canditates);
    }
}
