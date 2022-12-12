using System;
using System.Collections.Generic;
using System.Linq;

namespace Day11;

public class Solver
{
    private List<Monkey> _monkeys;

    public Solver()
    {
        GetInputs();
    }

    public double Solve1()
    {
        int WorryReliefStrategy(int x) => x / 3;

        for (var round = 0; round < 20; round++)
        {
            foreach (var monkey in _monkeys)
            {
                monkey.ThrowInspectAndThrowItems(WorryReliefStrategy, _monkeys);
            }
        }

        var topMonkeys = 
            _monkeys
                .OrderByDescending(x => x.InspectionCount)
                .Take(2)
                .Select(x => x.InspectionCount);
        
        return topMonkeys.First() * topMonkeys.Last();
    }
    
    public double Solve2()
    {
        GetInputs();

        var divisors = _monkeys.Select(x => x.DivisorForTest).ToList();
        var lcm = divisors.Aggregate((total, next) => total * next);
        
        int WorryReliefStrategy(int x) => x % lcm;

        for (var round = 0; round < 10_000; round++)
        {
            foreach (var monkey in _monkeys)
            {
                monkey.ThrowInspectAndThrowItems(WorryReliefStrategy, _monkeys);
            }
        }

        var topMonkeys = 
            _monkeys
                .OrderByDescending(x => x.InspectionCount)
                .Take(2)
                .Select(x => x.InspectionCount)
                .ToList();
        
        return ((double)topMonkeys.First()) * ((double)topMonkeys.Last());
    }

    void GetInputs()
    {
        var text = System.IO.File.ReadAllText("input.txt");

        var chunks = text.Split($"{Environment.NewLine}{Environment.NewLine}");
        _monkeys = chunks.Select(x => new Monkey(x)).ToList();
    }
}