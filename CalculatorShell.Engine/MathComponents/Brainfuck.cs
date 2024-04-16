using System.Data;
using System.Text;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CalculatorShell.Engine.MathComponents;

public static class Brainfuck
{
    public static void Interpret(IBrainFuckIO iO, string code)
    {
        int codePointer = 0;
        int memoryPointer = 0;
        byte[] memory = new byte[30000];

        while (codePointer < code.Length)
        {
            switch (code[codePointer])
            {
                case '>':
                    memoryPointer++;
                    if (memoryPointer > memory.Length)
                        throw new EngineException("Brainfuck memory overflow");
                    break;
                case '<':
                    memoryPointer--;
                    if (memoryPointer < 0)
                        throw new EngineException("Brainfuck memory underflow");
                    break;
                case '+':
                    memory[memoryPointer]++;
                    break;
                case '-':
                    memory[memoryPointer]--;
                    break;
                case '.':
                    iO.Print(memory[memoryPointer]);
                    break;
                case ',':
                    memory[memoryPointer] = iO.Read();
                    break;
                case '[':
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
                case ']':
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
}