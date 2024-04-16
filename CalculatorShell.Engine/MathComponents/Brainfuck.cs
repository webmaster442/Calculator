using System.Text;

namespace CalculatorShell.Engine.MathComponents;

public static class Brainfuck
{
    private const char MemorySlotIncement = '>';
    private const char MemorySlotDecrement = '<';
    private const char ValueIncrement = '+';
    private const char ValueDecrement = '-';
    private const char Output = '.';
    private const char Input = ',';
    private const char LoopStart = '[';
    private const char LoopEnd = ']';

    public static void Interpret(IBrainFuckIO iO, string code)
    {
        int codePointer = 0;
        int memoryPointer = 0;
        byte[] memory = new byte[30000];

        while (codePointer < code.Length)
        {
            switch (code[codePointer])
            {
                case MemorySlotIncement:
                    memoryPointer++;
                    if (memoryPointer > memory.Length)
                        throw new EngineException("Brainfuck memory overflow");
                    break;
                case MemorySlotDecrement:
                    memoryPointer--;
                    if (memoryPointer < 0)
                        throw new EngineException("Brainfuck memory underflow");
                    break;
                case ValueIncrement:
                    memory[memoryPointer]++;
                    break;
                case ValueDecrement:
                    memory[memoryPointer]--;
                    break;
                case Output:
                    iO.Print(memory[memoryPointer]);
                    break;
                case Input:
                    memory[memoryPointer] = iO.Read();
                    break;
                case LoopStart:
                    if (memory[memoryPointer] == 0)
                    {
                        int loopCount = 1;
                        while (loopCount > 0)
                        {
                            codePointer++;
                            if (code[codePointer] == '[')
                            {
                                loopCount++;
                            }
                            else if (code[codePointer] == ']')
                            {
                                loopCount--;
                            }
                        }
                    }
                    break;
                case LoopEnd:
                    if (memory[memoryPointer] != 0)
                    {
                        int loopCount = 1;
                        while (loopCount > 0)
                        {
                            codePointer--;
                            if (code[codePointer] == ']')
                            {
                                loopCount++;
                            }
                            else if (code[codePointer] == '[')
                            {
                                loopCount--;
                            }
                        }
                    }
                    break;
            }
            codePointer++;
        }
    }

    private static string Encode(int c)
    {
        var char_code = new StringBuilder();
        var slot = c / 8;
        for (var i = 0; i < slot; i++)
        {
            char_code.Append(MemorySlotIncement);
        }

        int pointer_diff;
        char pointer = ValueIncrement;
        char pointer_reversed = ValueDecrement;

        // set the pointer
        if (c > (slot * 8))
        {
            pointer_diff = c - (slot * 8);
            pointer = ValueIncrement;
            pointer_reversed = ValueDecrement;
        }
        else if (c < (slot * 8))
        {
            pointer_diff = (slot * 8) - c;
            pointer = ValueDecrement;
            pointer_reversed = ValueIncrement;
        }
        else
        {
            pointer_diff = 0;
        }
        for (var i = 0; i < pointer_diff; i++)
        {
            char_code.Append(pointer);
        }
        // output the character
        char_code.Append(Output);

        // reset the pointer
        for (var i = 0; i < pointer_diff; i++)
        {
            char_code.Append(pointer_reversed);
        }
        // reset the slot
        for (var i = 0; i < slot; i++)
        {
            char_code.Append(MemorySlotDecrement);
        }
        // return our bf code
        return char_code.ToString();
    }

    public static string Encode(string s)
    {
        if (string.IsNullOrEmpty(s))
            return string.Empty;

        if (s.Any(x => x > 128))
            throw new EngineException("Only ASCII input is supported");

        StringBuilder final = new("++++++++[>+>++>+++>++++>+++++>++++++>+++++++>++++++++>+++++++++>++++++++++>+++++++++++>++++++++++++>+++++++++++++>++++++++++++++>+++++++++++++++>++++++++++++++++<<<<<<<<<<<<<<<<-]");
        for (var i = 0; i < s.Length; i++)
        {
            final.Append(Encode(s[i]));
        }
        final.Append(Output);
        return final.ToString();
    }
}