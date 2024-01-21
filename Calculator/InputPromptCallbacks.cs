using System.Collections.Immutable;

using CalculatorShell.Core;

using PrettyPrompt;
using PrettyPrompt.Completion;
using PrettyPrompt.Consoles;
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

    private static string GetWordAtCaret(string text, int caret)
    {
        var words = text.Split(new[] { ' ', '\n' });
        string wordAtCaret = string.Empty;
        int currentIndex = 0;
        foreach (var word in words)
        {
            if (currentIndex < caret && caret < currentIndex + word.Length)
            {
                wordAtCaret = word;
                break;
            }
            currentIndex += word.Length + 1; // +1 due to word separator
        }

        return wordAtCaret;
    }

    private static CompletionItem CreateCompletionItem(string name, string description) 
        => new(name,
               commitCharacterRules: new[] { new CharacterSetModificationRule(CharacterSetModificationKind.Add, new[] { ' ' }.ToImmutableArray()) }.ToImmutableArray(),
               getExtendedDescription: (ct) => Task.FromResult(new FormattedString(description)));

    protected override Task<IReadOnlyList<CompletionItem>> GetCompletionItemsAsync(string text, int caret, TextSpan spanToBeReplaced, CancellationToken cancellationToken)
    {
        if (caret > 1)
        {
            var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (words.Length > 0 && AutoCompletableCommands.ContainsKey(words[0]))
            {
                var candidates =  AutoCompletableCommands[words[0]].ProvideAutoCompleteItems(text, caret)
                    .OrderBy(x => x.option)
                    .Select(x => CreateCompletionItem(x.option, x.description))
                    .ToList() as IReadOnlyList<CompletionItem>;

                return Task.FromResult(candidates);
            }
        }

        var keyWord = text.Substring(spanToBeReplaced.Start);

        var canditates = CommandsWithDescription
            .Where(x => x.Key.StartsWith(keyWord, StringComparison.InvariantCultureIgnoreCase))
            .OrderBy(x => x.Key)
            .Select(x => CreateCompletionItem(x.Key, x.Value))
            .ToList() as IReadOnlyList<CompletionItem>;

        return Task.FromResult(canditates);
    }
}
