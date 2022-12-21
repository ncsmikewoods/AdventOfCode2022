using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day13;

public class Solver
{
    private List<Pair> _pairs = new();

    public Solver()
    {
        GetInputs();
    }

    public int Solve1()
    {
        var sortedPairs = new List<int>();

        for (var i = 0; i < _pairs.Count; i++)
        {
            var pair = _pairs[i];
            var left = pair.Left;
            var right = pair.Right;

            var result = left.IsSortedWith(right);
            
            Console.WriteLine($"Pair {i+1}: {result}");
            
            if (result == Result.Sorted)
            {
                sortedPairs.Add(i+1);
            }
        }
        
        return sortedPairs.Sum(); //4902 is too high
    }

    public int Solve2()
    {
        return 0;
    }

    void GetInputs()
    {
        // var text = System.IO.File.ReadAllText("inputshort.txt");
        var text = System.IO.File.ReadAllText("input.txt");

        var pairsRaw = text.Split($"{Environment.NewLine}{Environment.NewLine}");

        foreach (var pairRaw in pairsRaw)
        {
            var pair = new Pair(pairRaw);
            _pairs.Add(pair);
        }
    }
}