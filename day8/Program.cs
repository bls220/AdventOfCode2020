using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace day8
{
    class Program
    {
        const bool DebugExecution = false;
        struct Instruction
        {
            public string op;
            public int arg;

            public override string ToString()
            {
                return $"{op} {arg:+000;-000}";
            }
        }
        static void Main(string[] args)
        {
            var asm = System.IO.File.ReadAllLines("input.txt")
            .Select(line => line.Split(" "))
            .Select(parts => new Instruction() { op = parts[0], arg = int.Parse(parts[1]) })
            .ToArray();

            Instruction[] originalAsm = new Instruction[asm.Length];
            asm.CopyTo(originalAsm, 0);

            int pc, acc;
            HashSet<int> visited;
            int modifyOpIndex = 0;

            void Reset()
            {
                pc = acc = 0;
                originalAsm.CopyTo(asm, 0);
                visited = new HashSet<int>(asm.Count());
                // Find next jmp/nop to try modifying; I know first instruction is acc, don't worry about it.
                var modifiedInstruction = asm.Skip(modifyOpIndex + 1).First(ins => ins.op == "nop" || ins.op == "jmp");
                modifyOpIndex = Array.FindIndex(asm, modifyOpIndex, ins => ins.Equals(modifiedInstruction));
                Debug.Write($"Modifying PC {modifyOpIndex:D3} From {modifiedInstruction} To ");
                modifiedInstruction.op = modifiedInstruction.op == "jmp" ? "nop" : "jmp";
                Debug.WriteLine(modifiedInstruction);
                asm[modifyOpIndex] = modifiedInstruction;
            }

            Reset();
            while (pc < asm.Length)
            {
                if (visited.Contains(pc))
                {
                    Reset();
                }
                visited.Add(pc);

                var instruction = asm[pc];
                Debug.WriteIf(DebugExecution, $"[{pc:D3}] {instruction}");
                switch (instruction.op)
                {
                    case "acc":
                        acc += instruction.arg;
                        pc++;
                        Debug.WriteIf(DebugExecution, $" => acc={acc}");
                        break;
                    case "jmp":
                        pc += instruction.arg;
                        break;
                    case "nop":
                    default:
                        pc++;
                        break;
                }
                Debug.WriteLineIf(DebugExecution, $"");
            }
            Console.WriteLine($"ACC = {acc}");
        }
    }
}
