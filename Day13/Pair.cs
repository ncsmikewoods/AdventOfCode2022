using System;
using System.Linq;

namespace Day13;

public class Pair
{
    public Pair(string packetRaw)
    {
        var lines = packetRaw.Split($"{Environment.NewLine}").ToList();
        Left = ElementFactory.BuildPacket(lines[0]);
        Right = ElementFactory.BuildPacket(lines[1]);
    }

    public ElementList Left { get; set; }
    public ElementList Right { get; set; }

    public bool IsSorted()
    {
        return Left.IsSortedWith(Right) == Result.Sorted;
    }
}