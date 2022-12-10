using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10;

public class Solver
{
    private List<Instruction> _instructions;
    private int[] _signalCheckpoints = { 20, 60, 100, 140, 180, 220 };

    public Solver()
    {
        GetInputs();
    }

    public int Solve1()
    {
        var register = 1;
        var cycle = 1;

        var instructionIndex = 0;
        var instructionCycle = 1;

        var signalStrengths = new List<int>();

        while (instructionIndex < _instructions.Count)
        {
            if (_signalCheckpoints.Contains(cycle))
            {
                signalStrengths.Add(cycle * register);
            }
            
            var instruction = _instructions[instructionIndex];

            if (instruction.isNoop)
            {
                instructionIndex++;
                instructionCycle = 1;
            }
            else // addx
            {
                if (instructionCycle == 2)
                {
                    register += instruction.Magnitude;
                    instructionIndex++;
                    instructionCycle = 1;
                }
                else
                {
                    instructionCycle++;                    
                }
            }
            
            cycle++;
        }

        return signalStrengths.Sum();
    }
    
    public int Solve2()
    {
        Console.WriteLine();
        var register = 1;
        var cycle = 1;

        var instructionIndex = 0;
        var instructionCycle = 1;
        var lineIndex = 0;

        var signalStrengths = new List<int>(); // TODO : how can we use this?

        while (instructionIndex < _instructions.Count)
        {
            if (_signalCheckpoints.Contains(cycle))
            {
                signalStrengths.Add(cycle * register);
            }
            
            Console.Write(Math.Abs(register - lineIndex) < 2 ? "#" : ".");
            
            var instruction = _instructions[instructionIndex];

            if (instruction.isNoop)
            {
                instructionIndex++;
                instructionCycle = 1;
            }
            else // addx
            {
                if (instructionCycle == 2)
                {
                    register += instruction.Magnitude;
                    instructionIndex++;
                    instructionCycle = 1;
                }
                else
                {
                    instructionCycle++;                    
                }
            }
            
            if (lineIndex == 39)
            {
                lineIndex = 0;
                Console.WriteLine();
            }
            else
            {
                lineIndex++;                
            }
            
            cycle++;
        }

        Console.WriteLine();
        return 0;
    }

    void GetInputs()
    {
        var lines = System.IO.File.ReadAllLines("input.txt");
        _instructions = lines.Select(x => new Instruction(x)).ToList();
    }
}

public class Instruction
{
    public Instruction(string lineRaw)
    {
        if (lineRaw.StartsWith("n"))
        {
            Name = "noop";
            return;
        }

        var tokens = lineRaw.Split(" ");
        Name = "addx";
        Magnitude = int.Parse(tokens[1]);
    }

    public string Name { get; set; }
    public int Magnitude { get; set; } = 0;

    public bool isNoop => Name == "noop";
}