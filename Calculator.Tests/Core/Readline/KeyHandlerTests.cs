/*
 * MIT License
 *
 * Copyright (c) 2017 Toni Solarin-Sodara
 * Copyright (c) 2022 Aptivi
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 * 
 */

using CalculatorShell.Core.Readline;

using static Calculator.Tests.Core.Readline.CharSequences;

namespace Calculator.Tests.Core.Readline;

public class KeyHandlerTests
{
    // Variables
    private KeyHandler _keyHandler;
    private List<string> _history;
    private AutoCompletionHandler _autoCompleteHandler;
    private string[] _completions;
    private IConsole _console;
    private bool _isOnWindows;
    private string _homeDir;

    /// <summary>
    /// Initialize the variables
    /// </summary>
    [SetUp]
    public void Setup()
    {
        // Initialize the auto completion
        _autoCompleteHandler = new AutoCompletionHandler();
        _completions = _autoCompleteHandler.GetSuggestions("", 0);

        // Initialize the history
        _history = new List<string>(new string[] { "dotnet run", "git init", "clear" });

        // Initialize the key handler
        _console = new DumbConsole();
        _keyHandler = new KeyHandler(_console, _history, null);


        ReadLine._keyHandler = _keyHandler;

        // Initialize custom bindings
        ReadLine.AddCustomBinding(AltShiftF, _keyHandler.BackspaceOrDelete);
        ReadLine.AddCustomBinding(CtrlAltW, _keyHandler.ClearLineUntilSpaceOrSlash);
        ReadLine.AddCustomBinding(CtrlAltB, _keyHandler.CopyBackwardWord);
        ReadLine.AddCustomBinding(CtrlAltF, _keyHandler.CopyForwardWord);
        KeyBindings.InitializeBindings();

        // Initialize platform variables
        _isOnWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;
        _homeDir = _isOnWindows ? Environment.GetEnvironmentVariable("USERPROFILE") : Environment.GetEnvironmentVariable("HOME");

        // Initial writing
        "Hello".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
               .ToList()
               .ForEach(_keyHandler.Handle);
    }

    #region Writing
    /// <summary>
    /// Tests writing the characters
    /// </summary>
    [Test]
    public void TestWriteChar()
    {
        // Ensure that we have initialized this class
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello"));

        // Write this
        " World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                .ToList()
                .ForEach(_keyHandler.Handle);

        // Confirm that everything works
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello World"));
    }

    /// <summary>
    /// Tests inserting the comment
    /// </summary>
    [Test]
    public void TestInsertComment()
    {
        // Simulate the user pressing the SHIFT + ALT + # key
        _keyHandler.Handle(AltShiftD3);

        // Confirm that the hashtag is there
        Assert.That(_keyHandler.Text, Is.EqualTo("#Hello"));
    }

    /// <summary>
    /// Tests inserting the home directory behind the cursor at the end of the line
    /// </summary>
    [Test]
    public void TestInsertHomeDirectoryBehindCursorAtEndOfLine()
    {
        // Write this
        " World~".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                 .ToList()
                 .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the SHIFT + ALT + & key
        _keyHandler.Handle(AltShiftD7);

        // Confirm that everything works
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello World" + _homeDir));
    }

    /// <summary>
    /// Tests inserting the home directory on the cursor at the end of the line
    /// </summary>
    [Test]
    public void TestInsertHomeDirectoryOnCursorAtEndOfLine()
    {
        // Write this
        " World~".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                 .ToList()
                 .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the LEFT ARROW and SHIFT + ALT + & key
        new List<ConsoleKeyInfo>() { LeftArrow, AltShiftD7 }
            .ForEach(_keyHandler.Handle);

        // Confirm that everything works
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello World" + _homeDir));
    }

    /// <summary>
    /// Tests inserting the home directory behind the cursor at the middle of the line
    /// </summary>
    [Test]
    public void TestInsertHomeDirectoryBehindCursorAtMiddleOfLine()
    {
        // Write this
        "~World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                .ToList()
                .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the LEFT ARROW (5x) and SHIFT + ALT + & key
        Enumerable.Repeat(LeftArrow, 5)
                  .Append(AltShiftD7)
                  .ToList()
                  .ForEach(_keyHandler.Handle);

        // Confirm that everything works
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello" + _homeDir + "World"));
    }

    /// <summary>
    /// Tests inserting the home directory on the cursor at the middle of the line
    /// </summary>
    [Test]
    public void TestInsertHomeDirectoryOnCursorAtMiddleOfLine()
    {
        // Write this
        "~World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                .ToList()
                .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the LEFT ARROW (5x) and SHIFT + ALT + & key
        Enumerable.Repeat(LeftArrow, 6)
                  .Append(AltShiftD7)
                  .ToList()
                  .ForEach(_keyHandler.Handle);

        // Confirm that everything works
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello" + _homeDir + "World"));
    }

    /// <summary>
    /// Tests inserting the home directory behind the cursor at the beginning of the line
    /// </summary>
    [Test]
    public void TestInsertHomeDirectoryBehindCursorAtBeginningOfLine()
    {
        // Simulate the user pressing the HOME and ~ key
        new List<ConsoleKeyInfo>() { Home, '~'.ToConsoleKeyInfo() }
            .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the SHIFT + ALT + & key
        _keyHandler.Handle(AltShiftD7);

        // Confirm that everything works
        Assert.That(_keyHandler.Text, Is.EqualTo(_homeDir + "Hello"));
    }

    /// <summary>
    /// Tests inserting the home directory on the cursor at the beginning of the line
    /// </summary>
    [Test]
    public void TestInsertHomeDirectoryOnCursorAtBeginningOfLine()
    {
        // Simulate the user pressing the HOME and ~ key
        new List<ConsoleKeyInfo>() { Home, '~'.ToConsoleKeyInfo() }
            .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the SHIFT + ALT + & key
        new List<ConsoleKeyInfo>() { LeftArrow, AltShiftD7 }
            .ForEach(_keyHandler.Handle);

        // Confirm that everything works
        Assert.That(_keyHandler.Text, Is.EqualTo(_homeDir + "Hello"));
    }

    /// <summary>
    /// Tests writing the tab character verbatim
    /// </summary>
    [Test]
    public void TestWriteTab()
    {
        // Simulate the user pressing the ALT + TAB key
        _keyHandler.Handle(AltTab);

        // Confirm that we've added three spaces
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello   "));
        Assert.That(_keyHandler.Text.Length, Is.EqualTo(8));

        // Simulate the user pressing the ALT + TAB key
        _keyHandler.Handle(AltTab);

        // Confirm that we've added eight more spaces
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello           "));
        Assert.That(_keyHandler.Text.Length, Is.EqualTo(16));
    }
    #endregion

    #region Deletion
    /// <summary>
    /// Tests deleting a character
    /// </summary>
    [Test]
    public void TestBackspace()
    {
        // Simulate the user pressing the BACKSPACE key
        _keyHandler.Handle(Backspace);

        // Ensure that we've erased the last character
        Assert.That(_keyHandler.Text, Is.EqualTo("Hell"));
    }

    /// <summary>
    /// Tests deleting a character
    /// </summary>
    [Test]
    public void TestBackspaceWithControlH()
    {
        // Simulate the user pressing the CTRL + H key
        _keyHandler.Handle(CtrlH);

        // Ensure that we've erased the last character
        Assert.That(_keyHandler.Text, Is.EqualTo("Hell"));
    }

    /// <summary>
    /// Tests deleting a character
    /// </summary>
    [Test]
    public void TestDelete()
    {
        // Simulate the user pressing the LEFT ARROW + DELETE keys
        new List<ConsoleKeyInfo>() { LeftArrow, Delete }
            .ForEach(_keyHandler.Handle);

        // Ensure that we've erased the last character
        Assert.That(_keyHandler.Text, Is.EqualTo("Hell"));
    }

    /// <summary>
    /// Tests deleting a character
    /// </summary>
    [Test]
    public void TestDeleteWithControlD()
    {
        // Simulate the user pressing the LEFT ARROW (4x) + CTRL + D keys
        Enumerable.Repeat(LeftArrow, 4)
                  .Append(CtrlD)
                  .ToList()
                  .ForEach(_keyHandler.Handle);

        // Ensure that we've erased the second character
        Assert.That(_keyHandler.Text, Is.EqualTo("Hllo"));
    }

    /// <summary>
    /// Tries to delete the character from the end of the line
    /// </summary>
    [Test]
    public void TestDeleteAtTheEndOfLine()
    {
        // Simulate the user pressing the DELETE key
        _keyHandler.Handle(Delete);

        // Nothing should be deleted. Or, we've got a serious bug
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello"));
    }

    /// <summary>
    /// Tests deleting a character under the cursor unless we're at the end of the line
    /// </summary>
    [Test]
    public void TestBackspaceOrDelete()
    {
        // Simulate the user pressing the LEFT ARROW (2x) and ALT + SHIFT + F keys
        Enumerable.Repeat(LeftArrow, 2)
                  .Append(AltShiftF)
                  .ToList()
                  .ForEach(_keyHandler.Handle);

        // Ensure that we've erased the fourth character
        Assert.That(_keyHandler.Text, Is.EqualTo("Helo"));

        // Simulate the user pressing the ALT + SHIFT + F key
        _keyHandler.Handle(AltShiftF);

        // Ensure that we've erased the fourth character
        Assert.That(_keyHandler.Text, Is.EqualTo("Hel"));

        // Simulate the user pressing the ALT + SHIFT + F key
        _keyHandler.Handle(AltShiftF);

        // Ensure that we've backspaced
        Assert.That(_keyHandler.Text, Is.EqualTo("He"));
    }

    /// <summary>
    /// Tests clearing the line to the left
    /// </summary>
    [Test]
    public void TestClearLineToLeftWithControlU()
    {
        // Simulate the user pressing the LEFT ARROW + CTRL + U keys
        new List<ConsoleKeyInfo>() { LeftArrow, CtrlU }
            .ForEach(_keyHandler.Handle);

        // Ensure that we wiped all the characters from the position before the "o" character
        Assert.That(_keyHandler.Text, Is.EqualTo("o"));

        // Simulate the user pressing the END + CTRL + U keys
        new List<ConsoleKeyInfo>() { End, CtrlU }
            .ForEach(_keyHandler.Handle);

        // Ensure that nothing is there
        Assert.That(_keyHandler.Text, Is.EqualTo(string.Empty));
    }

    /// <summary>
    /// Tests clearing the line to the left and yanking
    /// </summary>
    [Test]
    public void TestClearLineToLeftWithControlUAndYanking()
    {
        // Simulate the user pressing the LEFT ARROW + CTRL + U keys
        new List<ConsoleKeyInfo>() { LeftArrow, CtrlU }
            .ForEach(_keyHandler.Handle);

        // Ensure that we wiped all the characters from the position before the "o" character, and that the deleted content are all copied to the
        // clipboard (kill buffer)
        Assert.That(_keyHandler.Text, Is.EqualTo("o"));
        Assert.That(_keyHandler.KillBuffer, Is.EqualTo("Hell"));

        // Simulate the user pressing the CTRL + Y key
        _keyHandler.Handle(CtrlY);

        // Ensure that everything is back to what they were
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello"));
    }

    /// <summary>
    /// Tests clearing the line to the current position
    /// </summary>
    [Test]
    public void TestClearLineToRightWithControlK()
    {
        // Simulate the user pressing the LEFT ARROW and CTRL + K keys
        new List<ConsoleKeyInfo>() { LeftArrow, CtrlK }
            .ForEach(_keyHandler.Handle);

        // Ensure that we've removed all the characters to the current position
        Assert.That(_keyHandler.Text, Is.EqualTo("Hell"));

        // Simulate the user pressing the HOME and CTRL + K keys
        new List<ConsoleKeyInfo>() { Home, CtrlK }
            .ForEach(_keyHandler.Handle);

        // Ensure that nothing is there
        Assert.That(_keyHandler.Text, Is.EqualTo(string.Empty));
    }

    /// <summary>
    /// Tests clearing the line to the current position
    /// </summary>
    [Test]
    public void TestClearLineToRightWithControlKAndYanking()
    {
        // Simulate the user pressing the LEFT ARROW and CTRL + K keys
        new List<ConsoleKeyInfo>() { LeftArrow, CtrlK }
            .ForEach(_keyHandler.Handle);

        // Ensure that we've removed all the characters to the current position, and that the deleted content are all copied to the clipboard
        // (kill buffer)
        Assert.That(_keyHandler.Text, Is.EqualTo("Hell"));
        Assert.That(_keyHandler.KillBuffer, Is.EqualTo("o"));

        // Simulate the user pressing the CTRL + Y key
        _keyHandler.Handle(CtrlY);

        // Ensure that everything is back to what they were
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello"));
    }

    /// <summary>
    /// Tests clearing the line until space is spotted
    /// </summary>
    [Test]
    public void TestClearLineUntilSpaceWithControlW()
    {
        // Simulate the user pressing the CTRL + W key while writing the " World" string
        " World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                .Append(CtrlW)
                .ToList()
                .ForEach(_keyHandler.Handle);

        // Ensure that we've erased everything until the space is spotted
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello "));

        // Simulate the user pressing the BACKSPACE and CTRL + W keys
        new List<ConsoleKeyInfo>() { Backspace, CtrlW }
            .ForEach(_keyHandler.Handle);

        // Ensure that nothing is there
        Assert.That(_keyHandler.Text, Is.EqualTo(string.Empty));
    }

    /// <summary>
    /// Tests clearing the line until space is spotted while wiping all whitespaces
    /// </summary>
    [Test]
    public void TestClearLineUntilSpaceWithControlWWipeWhitespace()
    {
        // Simulate the user pressing the CTRL + W key while writing the " World" string
        " World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                .Append(CtrlW)
                .ToList()
                .ForEach(_keyHandler.Handle);

        // Ensure that we've erased everything until the space is spotted
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello "));

        // Simulate the user pressing the CTRL + W key
        _keyHandler.Handle(CtrlW);

        // Ensure that nothing is there
        Assert.That(_keyHandler.Text, Is.EqualTo(string.Empty));
    }

    /// <summary>
    /// Tests clearing the line until space is spotted and yanking
    /// </summary>
    [Test]
    public void TestClearLineUntilSpaceWithControlWAndYanking()
    {
        // Simulate the user pressing the CTRL + W key while writing the " World" string
        " World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                .Append(CtrlW)
                .ToList()
                .ForEach(_keyHandler.Handle);

        // Ensure that we've erased everything until the space is spotted
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello "));
        Assert.That(_keyHandler.KillBuffer, Is.EqualTo("World"));

        // Simulate the user pressing the CTRL + Y key
        _keyHandler.Handle(CtrlY);

        // Ensure that everything is back to what they were
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello World"));
    }

    /// <summary>
    /// Tests clearing the line until space or slash is spotted
    /// </summary>
    [Test]
    public void TestClearLineUntilSpaceOrSlashWithControlAltW()
    {
        // Simulate the user pressing the CTRL + W key while writing the " World" string
        " World/hworld.c".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                         .Append(CtrlAltW)
                         .ToList()
                         .ForEach(_keyHandler.Handle);

        // Ensure that we've erased everything until the slash is spotted
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello World/"));
    }

    /// <summary>
    /// Tests clearing the line until space or slash is spotted and yanking
    /// </summary>
    [Test]
    public void TestClearLineUntilSpaceOrSlashWithControlAltWAndYanking()
    {
        // Simulate the user pressing the CTRL + W key while writing the " World" string
        " World/hworld.c".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                         .Append(CtrlAltW)
                         .ToList()
                         .ForEach(_keyHandler.Handle);

        // Ensure that we've erased everything until the slash is spotted
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello World/"));
        Assert.That(_keyHandler.KillBuffer, Is.EqualTo("hworld.c"));

        // Simulate the user pressing the CTRL + Y key
        _keyHandler.Handle(CtrlY);

        // Ensure that everything is back to what they were
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello World/hworld.c"));
    }

    /// <summary>
    /// Tests clearing the line until space is spotted
    /// </summary>
    [Test]
    public void TestClearLineUntilSpaceWithAltBackspace()
    {
        // Simulate the user pressing the ALT + BACKSPACE key while writing the " World" string
        " World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                .Append(AltBackspace)
                .ToList()
                .ForEach(_keyHandler.Handle);

        // Ensure that we've erased everything until the space is spotted
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello "));

        // Simulate the user pressing the BACKSPACE and ALT + BACKSPACE keys
        new List<ConsoleKeyInfo>() { Backspace, AltBackspace }
            .ForEach(_keyHandler.Handle);

        // Ensure that nothing is there
        Assert.That(_keyHandler.Text, Is.EqualTo(string.Empty));
    }

    /// <summary>
    /// Tests clearing the line until space is spotted while wiping all whitespaces
    /// </summary>
    [Test]
    public void TestClearLineUntilSpaceWithAltBackspaceWipeWhitespace()
    {
        // Simulate the user pressing the ALT + BACKSPACE key while writing the " World" string
        " World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                .Append(AltBackspace)
                .ToList()
                .ForEach(_keyHandler.Handle);

        // Ensure that we've erased everything until the space is spotted
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello "));

        // Simulate the user pressing the ALT + BACKSPACE key
        _keyHandler.Handle(AltBackspace);

        // Ensure that nothing is there
        Assert.That(_keyHandler.Text, Is.EqualTo(string.Empty));
    }

    /// <summary>
    /// Tests clearing the line until space is spotted and yanking
    /// </summary>
    [Test]
    public void TestClearLineUntilSpaceWithAltBackspaceAndYanking()
    {
        // Simulate the user pressing the ALT + BACKSPACE key while writing the " World" string
        " World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                .Append(AltBackspace)
                .ToList()
                .ForEach(_keyHandler.Handle);

        // Ensure that we've erased everything until the space is spotted
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello "));
        Assert.That(_keyHandler.KillBuffer, Is.EqualTo("World"));

        // Simulate the user pressing the CTRL + Y key
        _keyHandler.Handle(CtrlY);

        // Ensure that everything is back to what they were
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello World"));
    }

    /// <summary>
    /// Tests clearing the line after space is spotted
    /// </summary>
    [Test]
    public void TestClearLineAfterSpaceWithAltD()
    {
        // Write this
        " World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                .ToList()
                .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the HOME and ALT + D key
        new List<ConsoleKeyInfo>() { Home, AltD }
            .ForEach(_keyHandler.Handle);

        // Ensure that we've erased everything until the space is spotted
        Assert.That(_keyHandler.Text, Is.EqualTo(" World"));

        // Simulate the user pressing the DELETE and ALT + D keys
        new List<ConsoleKeyInfo>() { Delete, AltD }
            .ForEach(_keyHandler.Handle);

        // Ensure that nothing is there
        Assert.That(_keyHandler.Text, Is.EqualTo(string.Empty));
    }

    /// <summary>
    /// Tests clearing the line after space is spotted while wiping all whitespaces
    /// </summary>
    [Test]
    public void TestClearLineAfterSpaceWithAltDWipeWhitespace()
    {
        // Write this
        " World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                .ToList()
                .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the HOME and ALT + D key
        new List<ConsoleKeyInfo>() { Home, AltD }
            .ForEach(_keyHandler.Handle);

        // Ensure that we've erased everything until the space is spotted
        Assert.That(_keyHandler.Text, Is.EqualTo(" World"));

        // Simulate the user pressing the ALT + D key
        _keyHandler.Handle(AltD);

        // Ensure that nothing is there
        Assert.That(_keyHandler.Text, Is.EqualTo(string.Empty));
    }

    /// <summary>
    /// Tests clearing the line after space is spotted and yanking
    /// </summary>
    [Test]
    public void TestClearLineAfterSpaceWithAltDAndYanking()
    {
        // Write this
        " World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                .ToList()
                .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the HOME and ALT + D key
        new List<ConsoleKeyInfo>() { Home, AltD }
            .ForEach(_keyHandler.Handle);

        // Ensure that we've erased everything until the space is spotted
        Assert.That(_keyHandler.Text, Is.EqualTo(" World"));
        Assert.That(_keyHandler.KillBuffer, Is.EqualTo("Hello"));

        // Simulate the user pressing the CTRL + Y key
        _keyHandler.Handle(CtrlY);

        // Ensure that everything is back to what they were
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello World"));
    }

    /// <summary>
    /// Tests clearing all horizontal space
    /// </summary>
    [Test]
    public void TestClearHorizontalLineWithAltBackslash()
    {
        // Write this
        "     World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                    .ToList()
                    .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the HOME and ALT + D key
        new List<ConsoleKeyInfo>() { Home, AltD }
            .ForEach(_keyHandler.Handle);

        // Ensure that we've erased everything until the space is spotted
        Assert.That(_keyHandler.Text, Is.EqualTo("     World"));

        // Simulate the user pressing the ALT + \ key
        new List<ConsoleKeyInfo>() { LeftArrow, LeftArrow, AltOem5 }
            .ForEach(_keyHandler.Handle);

        // Ensure that only "World" is there
        Assert.That(_keyHandler.Text, Is.EqualTo("World"));
    }
    #endregion

    #region Transposition
    /// <summary>
    /// Tries to swap the two characters
    /// </summary>
    [Test]
    public void TestTransposeWithControlT()
    {
        // Initial console cursor position
        var initialCursorCol = _console.CursorLeft;

        // Simulate the user pressing the CTRL + T keys
        _keyHandler.Handle(CtrlT);

        // Ensure that the O and the L at the end of the string are swapped
        Assert.That(_keyHandler.Text, Is.EqualTo("Helol"));
        Assert.That(_console.CursorLeft, Is.EqualTo(initialCursorCol));
    }

    /// <summary>
    /// Tries to swap the two characters
    /// </summary>
    [Test]
    public void TestTransposeWithMovement()
    {
        // Initial console cursor position
        var initialCursorCol = _console.CursorLeft;

        // Simulate the user pressing the LEFT ARROW and CTRL + T keys
        new List<ConsoleKeyInfo>() { LeftArrow, CtrlT }
            .ForEach(_keyHandler.Handle);

        // Ensure that the O and the L at the end of the string are swapped
        Assert.That(_keyHandler.Text, Is.EqualTo("Helol"));
        Assert.That(_console.CursorLeft, Is.EqualTo(initialCursorCol));
    }

    /// <summary>
    /// Tries to swap the two characters in the middle
    /// </summary>
    [Test]
    public void TestTransposeInMiddle()
    {
        // Simulate the user triple-pressing the LEFT ARROW key
        Enumerable
            .Repeat(LeftArrow, 3)
            .ToList()
            .ForEach(_keyHandler.Handle);

        // Initial console cursor position
        var initialCursorCol = _console.CursorLeft;

        // Simulate the user pressing the CTRL + T keys
        _keyHandler.Handle(CtrlT);

        // Ensure that the O and the L at the middle of the string are swapped
        Assert.That(_keyHandler.Text, Is.EqualTo("Hlelo"));
        Assert.That(_console.CursorLeft, Is.EqualTo(initialCursorCol + 1));
    }

    /// <summary>
    /// Tries to swap the two characters in the very beginning. This should do nothing to the string
    /// </summary>
    [Test]
    public void TestTransposeAtVeryBeginning()
    {
        // Simulate the user pressing the CTRL + A keys
        _keyHandler.Handle(CtrlA);

        // Initial console cursor position
        var initialCursorCol = _console.CursorLeft;

        // Simulate the user pressing the CTRL + T keys
        _keyHandler.Handle(CtrlT);

        // Ensure that nothing is swapped
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello"));
        Assert.That(_console.CursorLeft, Is.EqualTo(initialCursorCol));
    }

    /// <summary>
    /// Tries to swap the two words
    /// </summary>
    [Test]
    public void TestTransposeWordsWithAltT()
    {
        // Write this
        " World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                .ToList()
                .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the ALT + B, LEFT ARROW, and ALT + T keys
        new List<ConsoleKeyInfo>() { AltB, LeftArrow, AltT }
            .ForEach(_keyHandler.Handle);

        // Ensure that "Hello" and "World" are swapped
        Assert.That(_keyHandler.Text, Is.EqualTo("World Hello"));
    }

    /// <summary>
    /// Tries to swap the first word in the string with the second one found in the middle
    /// </summary>
    [Test]
    public void TestTransposeFirstWithSecondWithAltT()
    {
        // Write this
        " Awesome World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                        .ToList()
                        .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the HOME, ALT + F, and ALT + T keys
        new List<ConsoleKeyInfo>() { Home, AltF, AltT }
            .ForEach(_keyHandler.Handle);

        // Ensure that "Hello" and "Awesome" are swapped
        Assert.That(_keyHandler.Text, Is.EqualTo("Awesome Hello World"));
    }

    /// <summary>
    /// Tries to swap the second word in the string with the last one found at the end
    /// </summary>
    [Test]
    public void TestTransposeSecondWithLastWithAltT()
    {
        // Write this
        " Awesome World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                        .ToList()
                        .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the ALT + B, LEFT ARROW, and ALT + T keys
        new List<ConsoleKeyInfo>() { AltB, LeftArrow, AltT }
            .ForEach(_keyHandler.Handle);

        // Ensure that "Hello" and "Awesome" are swapped
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello World Awesome"));
    }

    /// <summary>
    /// Tries to swap the words found in the middle of the string
    /// </summary>
    [Test]
    public void TestTransposeMiddlesWithAltT()
    {
        // Write this
        " Amazingly Awesome World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                                  .ToList()
                                  .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the HOME, ALT + F, RIGHT ARROW, ALT + F, and ALT + T keys
        new List<ConsoleKeyInfo>() { Home, AltF, RightArrow, AltF, AltT }
            .ForEach(_keyHandler.Handle);

        // Ensure that "Amazingly" and "Awesome" are swapped
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello Awesome Amazingly World"));
    }

    /// <summary>
    /// [Counterexample] Tries to swap the two words when the cursor is in the middle of a word
    /// </summary>
    [Test]
    public void TestTransposeWordsWithinWordWithAltT()
    {
        // Write this
        " World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                .ToList()
                .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the LEFT ARROW, and ALT + T keys
        new List<ConsoleKeyInfo>() { LeftArrow, AltT }
            .ForEach(_keyHandler.Handle);

        // Ensure that "Hello" and "World" are not swapped
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello World"));
    }
    #endregion

    #region Movement
    /// <summary>
    /// Tries to go to the beginning of the line and write a character
    /// </summary>
    [Test]
    public void TestHome()
    {
        // Simulate the user pressing the HOME and S keys
        new List<ConsoleKeyInfo>() { Home, 'S'.ToConsoleKeyInfo(specialKeyCharMap) }
            .ForEach(_keyHandler.Handle);

        // Ensure that the S is at the beginning
        Assert.That(_keyHandler.Text, Is.EqualTo("SHello"));
    }

    /// <summary>
    /// Tries to go to the beginning of the line and write a character
    /// </summary>
    [Test]
    public void TestHomeWithControlA()
    {
        // Simulate the user pressing the CTRL + A and S keys
        new List<ConsoleKeyInfo>() { CtrlA, 'S'.ToConsoleKeyInfo(specialKeyCharMap) }
            .ForEach(_keyHandler.Handle);

        // Ensure that the S is at the beginning
        Assert.That(_keyHandler.Text, Is.EqualTo("SHello"));
    }

    /// <summary>
    /// Tries to go to the end of the line and writing a character
    /// </summary>
    [Test]
    public void TestEnd()
    {
        // Simulate the user pressing the HOME, END, and ! keys
        new List<ConsoleKeyInfo>() { Home, End, ExclamationPoint }
            .ForEach(_keyHandler.Handle);

        // Ensure that the exclamation mark is there
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello!"));
    }

    /// <summary>
    /// Tries to go to the end of the line and writing a character
    /// </summary>
    [Test]
    public void TestEndWithControlE()
    {
        // Simulate the user pressing the CTRL + A, CTRL + E, and ! keys
        new List<ConsoleKeyInfo>() { CtrlA, CtrlE, ExclamationPoint }
            .ForEach(_keyHandler.Handle);

        // Ensure that the exclamation mark is there
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello!"));
    }

    /// <summary>
    /// Tests going to the left
    /// </summary>
    [Test]
    public void TestLeftArrow()
    {
        // Simulate the user pressing the LEFT ARROW key while writing the " N" string
        " N".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
            .Prepend(LeftArrow)
            .ToList()
            .ForEach(_keyHandler.Handle);

        // Ensure that we've put the " N" before the "o"
        Assert.That(_keyHandler.Text, Is.EqualTo("Hell No"));
    }

    /// <summary>
    /// Tests going to the left
    /// </summary>
    [Test]
    public void TestLeftArrowWithControlB()
    {
        // Simulate the user pressing the CTRL + B key while writing the " N" string
        " N".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
            .Prepend(CtrlB)
            .ToList()
            .ForEach(_keyHandler.Handle);

        // Ensure that we've put the " N" before the "o"
        Assert.That(_keyHandler.Text, Is.EqualTo("Hell No"));
    }

    /// <summary>
    /// Tests going to the right
    /// </summary>
    [Test]
    public void TestRightArrow()
    {
        // Simulate the user pressing the LEFT ARROW, RIGHT ARROW, and ! key
        new List<ConsoleKeyInfo>() { LeftArrow, RightArrow, ExclamationPoint }
            .ForEach(_keyHandler.Handle);

        // Ensure that the exclamation mark is there
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello!"));
    }

    /// <summary>
    /// Tests going to the right
    /// </summary>
    [Test]
    public void TestRightArrowWithControlF()
    {
        // Simulate the user pressing the LEFT ARROW, CTRL + F, and ! key
        new List<ConsoleKeyInfo>() { LeftArrow, CtrlF, ExclamationPoint }
            .ForEach(_keyHandler.Handle);

        // Ensure that the exclamation mark is there
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello!"));
    }

    /// <summary>
    /// Tests moving cursor to the left of the word
    /// </summary>
    [Test]
    public void TestMoveCursorWordLeft()
    {
        // Simulate the user pressing the ALT + B and ! key
        new List<ConsoleKeyInfo>() { AltB, ExclamationPoint }
            .ForEach(_keyHandler.Handle);

        // Ensure that the exclamation mark is there
        Assert.That(_keyHandler.Text, Is.EqualTo("!Hello"));
    }

    /// <summary>
    /// Tests moving cursor to the right of the word
    /// </summary>
    [Test]
    public void TestMoveCursorWordRight()
    {
        // Simulate the user pressing the ALT + B, ALT + F, and ! key
        new List<ConsoleKeyInfo>() { AltB, AltF, ExclamationPoint }
            .ForEach(_keyHandler.Handle);

        // Ensure that the exclamation mark is there
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello!"));
    }
    #endregion

    #region Case Manipulation
    /// <summary>
    /// Tests making the word UPPERCASE
    /// </summary>
    [Test]
    public void TestUppercaseWord()
    {
        // Simulate the user pressing the HOME and ALT + U key
        new List<ConsoleKeyInfo>() { Home, AltU }
            .ForEach(_keyHandler.Handle);

        // Ensure that the word is all UPPERCASE
        Assert.That(_keyHandler.Text, Is.EqualTo("HELLO"));
    }

    /// <summary>
    /// Tests making the word lowercase
    /// </summary>
    [Test]
    public void TestLowercaseWord()
    {
        // Simulate the user pressing the HOME and ALT + L key
        new List<ConsoleKeyInfo>() { Home, AltL }
            .ForEach(_keyHandler.Handle);

        // Ensure that the word is all lowercase
        Assert.That(_keyHandler.Text, Is.EqualTo("hello"));
    }

    /// <summary>
    /// Tests making the character UPPERCASE and moving to the end of the word
    /// </summary>
    [Test]
    public void TestUppercaseCharMoveToEndOfWord()
    {
        // Simulate the user pressing the LEFT ARROW (2x) and ALT + C key
        new List<ConsoleKeyInfo>() { LeftArrow, LeftArrow, AltC }
            .ForEach(_keyHandler.Handle);

        // Ensure that the word is all UPPERCASE
        Assert.That(_keyHandler.Text, Is.EqualTo("HelLo"));
    }

    /// <summary>
    /// Tests making the character lowercase and moving to the end of the word
    /// </summary>
    [Test]
    public void TestLowercaseCharMoveToEndOfWord()
    {
        // Simulate the user pressing the HOME and ALT + V key
        new List<ConsoleKeyInfo>() { Home, AltV }
            .ForEach(_keyHandler.Handle);

        // Ensure that the word is all UPPERCASE
        Assert.That(_keyHandler.Text, Is.EqualTo("hello"));
    }
    #endregion

    #region History Manipulation
    /// <summary>
    /// Tests going up to reveal the previous history
    /// </summary>
    [Test]
    public void TestPreviousHistoryWithUpArrow()
    {
        // Ensure that we got the right history once UP ARROW is simulated
        _history.AsEnumerable().Reverse().ToList().ForEach((history) =>
        {
            _keyHandler.Handle(UpArrow);
            Assert.That(_keyHandler.Text, Is.EqualTo(history));
        });
    }

    /// <summary>
    /// Tests going up to reveal the previous history
    /// </summary>
    [Test]
    public void TestPreviousHistoryWithControlP()
    {
        // Ensure that we got the right history once CTRL + P is simulated
        _history.AsEnumerable().Reverse().ToList().ForEach((history) =>
        {
            _keyHandler.Handle(CtrlP);
            Assert.That(_keyHandler.Text, Is.EqualTo(history));
        });
    }

    /// <summary>
    /// Tests going down to reveal the next history
    /// </summary>
    [Test]
    public void TestNextHistoryWithDownArrow()
    {
        // Simulate the user pressing the UP ARROW key the number of times as the count of history entries
        Enumerable.Repeat(UpArrow, _history.Count)
                  .ToList()
                  .ForEach(_keyHandler.Handle);

        // Ensure that we got the right history once DOWN ARROW is simulated
        _history.ForEach(history =>
        {
            Assert.That(_keyHandler.Text, Is.EqualTo(history));
            _keyHandler.Handle(DownArrow);
        });
    }

    /// <summary>
    /// Tests going down to reveal the next history
    /// </summary>
    [Test]
    public void TestNextHistoryWithControlN()
    {
        // Simulate the user pressing the UP ARROW key the number of times as the count of history entries
        Enumerable.Repeat(UpArrow, _history.Count)
                  .ToList()
                  .ForEach(_keyHandler.Handle);

        // Ensure that we got the right history once CTRL + N is simulated
        _history.ForEach(history =>
        {
            Assert.That(_keyHandler.Text, Is.EqualTo(history));
            _keyHandler.Handle(CtrlN);
        });
    }

    /// <summary>
    /// Tests moving cursor and going to previous history
    /// </summary>
    [Test]
    public void MoveCursorThenPreviousHistory()
    {
        // Simulate the user pressing the LEFT ARROW and UP ARROW key
        new List<ConsoleKeyInfo>() { LeftArrow, UpArrow }
            .ForEach(_keyHandler.Handle);

        // Ensure that we're at the last history entry
        Assert.That(_keyHandler.Text, Is.EqualTo("clear"));
    }

    /// <summary>
    /// Tests adding the last argument from the last history entry
    /// </summary>
    [Test]
    public void TestAddLastArgument()
    {
        // Simulate the user pressing the SPACE and PERIOD key
        new List<ConsoleKeyInfo>() { Space, AltOemPeriod }
            .ForEach(_keyHandler.Handle);

        // Ensure that we've put the last word from the last history
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello clear"));
    }

    /// <summary>
    /// Tests going to the first history
    /// </summary>
    [Test]
    public void TestFirstHistory()
    {
        // Simulate the user pressing the ALT + SHIFT + < key
        new List<ConsoleKeyInfo>() { AltShiftOemComma }
            .ForEach(_keyHandler.Handle);

        // Ensure that we've put the first history
        Assert.That(_keyHandler.Text, Is.EqualTo(_history[0]));
    }
    #endregion

    #region Automatic Completion
    /// <summary>
    /// Tests auto completion
    /// </summary>
    [Test]
    public void TestTab()
    {
        // Simulate the user pressing the TAB key
        _keyHandler.Handle(Tab);

        // Nothing happens when no auto complete handler is set
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello"));

        // Let's set one up
        _keyHandler = new KeyHandler(new DumbConsole(), _history, _autoCompleteHandler);
        ReadLine._keyHandler = _keyHandler;

        // Initialize bindings
        KeyBindings.InitializeBindings();

        // Write this
        "Hi ".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
             .ToList()
             .ForEach(_keyHandler.Handle);

        // Simulate TAB to ensure that auto completion works
        _completions.ToList().ForEach(completion =>
        {
            _keyHandler.Handle(Tab);
            Assert.That(_keyHandler.Text, Is.EqualTo($"Hi {completion}"));
        });
    }

    /// <summary>
    /// Tests auto completion with CTRL + I
    /// </summary>
    [Test]
    public void TestTabWithControlI()
    {
        // Simulate the user pressing the CTRL + I key
        _keyHandler.Handle(CtrlI);

        // Nothing happens when no auto complete handler is set
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello"));

        // Let's set one up
        _keyHandler = new KeyHandler(new DumbConsole(), _history, _autoCompleteHandler);
        ReadLine._keyHandler = _keyHandler;

        // Initialize bindings
        KeyBindings.InitializeBindings();

        // Write this
        "Hi ".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
             .ToList()
             .ForEach(_keyHandler.Handle);

        // Simulate CTRL + I to ensure that auto completion works
        _completions.ToList().ForEach(completion =>
        {
            _keyHandler.Handle(CtrlI);
            Assert.That(_keyHandler.Text, Is.EqualTo($"Hi {completion}"));
        });
    }

    /// <summary>
    /// Tests reverse auto completion
    /// </summary>
    [Test]
    public void TestBackwardsTab()
    {
        // Simulate the user pressing the TAB key
        _keyHandler.Handle(Tab);

        // Nothing happens when no auto complete handler is set
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello"));

        // Let's set one up
        _keyHandler = new KeyHandler(new DumbConsole(), _history, _autoCompleteHandler);
        ReadLine._keyHandler = _keyHandler;

        // Initialize bindings
        KeyBindings.InitializeBindings();

        // Write this
        "Hi ".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
             .ToList()
             .ForEach(_keyHandler.Handle);

        // Bring up the first suggestion
        _keyHandler.Handle(Tab);

        // Simulate SHIFT + TAB to ensure that auto completion works in reverse
        _completions.Reverse().ToList().ForEach(completion =>
        {
            _keyHandler.Handle(ShiftTab);
            Assert.That(_keyHandler.Text, Is.EqualTo($"Hi {completion}"));
        });
    }

    /// <summary>
    /// Tests inserting completions
    /// </summary>
    [Test]
    public void TestInsertCompletions()
    {
        // Let's set one up
        _keyHandler = new KeyHandler(new DumbConsole(), _history, _autoCompleteHandler);
        ReadLine._keyHandler = _keyHandler;

        // Initialize bindings
        KeyBindings.InitializeBindings();

        // Write this
        "Hi ".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
             .ToList()
             .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the ALT + SHIFT + * key
        _keyHandler.Handle(AltShiftD8);

        // Ensure that everything is written
        Assert.That(_keyHandler.Text, Is.EqualTo("Hi World Angel Love"));
    }
    #endregion

    #region Current line history
    /// <summary>
    /// Tests undoing the action
    /// </summary>
    [Test]
    public void TestUndo()
    {
        // Write this
        " World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                .Append(CtrlW)
                .ToList()
                .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the CTRL + SHIFT + _ key
        _keyHandler.Handle(CtrlShiftMinus);

        // Confirm that the "World" is there
        Assert.That(_keyHandler.Text, Is.EqualTo("Hello World"));
    }

    /// <summary>
    /// Tests undoing all the actions
    /// </summary>
    [Test]
    public void TestUndoAll()
    {
        // Simulate the user pressing the ALT + R key
        _keyHandler.Handle(AltR);

        // Confirm that nothing is there
        Assert.That(_keyHandler.Text, Is.EqualTo(""));
    }
    #endregion

    #region Clipboard management
    /// <summary>
    /// Tests copying the word backward
    /// </summary>
    [Test]
    public void TestCopyBackwardWord()
    {
        // Write this
        " World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                .ToList()
                .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the CTRL + ALT + B key
        _keyHandler.Handle(CtrlAltB);

        // Ensure that we have "World" on the kill buffer
        Assert.That(_keyHandler.KillBuffer, Is.EqualTo("World"));
    }

    /// <summary>
    /// Tests copying the word forward
    /// </summary>
    [Test]
    public void TestCopyForwardWord()
    {
        // Write this
        " World".Select(c => c.ToConsoleKeyInfo(specialKeyCharMap))
                .ToList()
                .ForEach(_keyHandler.Handle);

        // Simulate the user pressing the HOME and CTRL + ALT + F key
        new List<ConsoleKeyInfo>() { Home, CtrlAltF }
            .ForEach(_keyHandler.Handle);

        // Ensure that we have "Hello" on the kill buffer
        Assert.That(_keyHandler.KillBuffer, Is.EqualTo("Hello"));
    }
    #endregion
}
