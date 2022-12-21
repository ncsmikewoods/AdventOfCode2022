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
            Console.WriteLine($"== Pair {i+1} ==");
            if (_pairs[i].IsSorted())
            {
                sortedPairs.Add(i+1);
            }
            Console.WriteLine();
        }
        
        return sortedPairs.Sum();
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