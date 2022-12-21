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
        return 0;
    }
    
    public int Solve2()
    {
        return 0;
    }

    void GetInputs()
    {
        var text = System.IO.File.ReadAllText("inputshort.txt");
        // var text = System.IO.File.ReadAllText("input.txt");

        var pairsRaw = text.Split($"{Environment.NewLine}{Environment.NewLine}");

        foreach (var pairRaw in pairsRaw)
        {
            var pair = new Pair(pairRaw);
            _pairs.Add(pair);
        }
    }
}

public class Pair
{
    public Pair(string packetRaw)
    {
        var lines = packetRaw.Split($"{Environment.NewLine}").ToList();
        Left = ElementFactory.BuildPacket(lines[0]);
        Right = ElementFactory.BuildPacket(lines[1]);
    }

    // public List<ElementList> Packets { get; set; } = new();

    public ElementList Left { get; set; }
    public ElementList Right { get; set; }
    
}

// public class Packet // same as ElementList?
// {
//     public Packet(string packetRaw)
//     {
//         
//     }
//     
//     public List<Element> Elements { get; set; }
// }