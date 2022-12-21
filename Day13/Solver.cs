using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day13;

public class Solver
{
    private List<Pair> _pairs = new();
    private List<ElementList> _packetList;

    public Solver()
    {
        GetInputs();
    }

    public int Solve1()
    {
        var sortedPairs = new List<int>();

        for (var i = 0; i < _pairs.Count; i++)
        {
            // Console.WriteLine($"== Pair {i+1} ==");
            if (_pairs[i].IsSorted())
            {
                sortedPairs.Add(i+1);
            }
            // Console.WriteLine();
        }
        
        return sortedPairs.Sum();
    }

    public int Solve2()
    {
        var dividerPackets = new List<ElementList>
        {
            ElementFactory.BuildPacket("[[2]]"),
            ElementFactory.BuildPacket("[[6]]")
        };
        
        var packets = _packetList.ToList();
        packets.AddRange(dividerPackets);

        var sorted = packets.OrderBy(x => x).ToList();

        var dividerLocation1 = sorted.IndexOf(dividerPackets[0]) + 1;
        var dividerLocation2 = sorted.IndexOf(dividerPackets[1]) + 1;
        return dividerLocation1 * dividerLocation2;
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

        _packetList = GetAllPackets(text);
    }

    List<ElementList> GetAllPackets(string text)
    {
        var lines = text.Split($"{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries);
        return 
            lines
                .Select(ElementFactory.BuildPacket)
                .ToList();
    }
}