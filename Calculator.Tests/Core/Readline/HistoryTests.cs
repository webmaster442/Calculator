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

using static CalculatorShell.Core.Readline.ReadLine;

namespace Calculator.Tests.Core.Readline;

public class HistoryTest
{
    /// <summary>
    /// Initialize the history
    /// </summary>
    [SetUp]
    public void Setup()
    {
        string[] history = new string[] { "ls -a", "dotnet run", "git init" };
        AddHistory(history);
    }

    /// <summary>
    /// Clears the history after all the tests pass
    /// </summary>
    [TearDown]
    public void Teardown()
    {
        ClearHistory();
    }

    /// <summary>
    /// Tests getting the count of the initial history. To ensure that ReadLine.Reboot initialized the history correctly
    /// </summary>
    [Test]
    public void TestNoInitialHistory()
    {
        Assert.That(GetHistory().Count, Is.EqualTo(3));
    }

    /// <summary>
    /// Tests updating the history with the addition of the string to the history
    /// </summary>
    [Test]
    public void TestUpdateHistoryByAddEntry()
    {
        AddHistory("mkdir");
        Assert.Multiple(() =>
        {
            Assert.That(GetHistory().Count, Is.EqualTo(4));
            Assert.That(GetHistory().Last(), Is.EqualTo("mkdir"));
        });
    }

    /// <summary>
    /// Tests updating the history by setting history
    /// </summary>
    [Test]
    public void TestUpdateHistoryBySetHistory()
    {
        List<string> newHistory = new() { "apt update", "apt dist-upgrade" };
        SetHistory(newHistory);
        Assert.That(GetHistory().Count, Is.EqualTo(2));
        Assert.That(GetHistory().Last(), Is.EqualTo("apt dist-upgrade"));
    }

    /// <summary>
    /// Tests for history correctness
    /// </summary>
    [Test]
    public void TestGetCorrectHistory()
    {
        Assert.That(GetHistory()[0], Is.EqualTo("ls -a"));
        Assert.That(GetHistory()[1], Is.EqualTo("dotnet run"));
        Assert.That(GetHistory()[2], Is.EqualTo("git init"));
    }
}
