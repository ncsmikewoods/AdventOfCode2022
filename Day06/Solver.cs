using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06;

public class Solver
{
    private string _input;

    public Solver()
    {
        GetInputs();
    }

    public int Solve1()
    {
        var marker = (new[] { 'a' }).Concat(_input[..3]).ToArray(); 

        for (var i = 3; i < _input.Length; i++)
        {
            marker = marker[1..4].Concat(new[] { _input[i] }).ToArray();

            if (IsPacketStart(marker, 4)) return i+1;
        }
        
        Console.WriteLine("Oops.  We hit the end of the signal");
        return -1;
    }

    public int Solve2()
    {
        var marker = (new[] { 'a' }).Concat(_input[..13]).ToArray(); 

        for (var i = 13; i < _input.Length; i++)
        {
            marker = marker[1..14].Concat(new[] { _input[i] }).ToArray();

            if (IsPacketStart(marker, 14)) return i+1;
        }
        
        Console.WriteLine("Oops.  We hit the end of the signal");
        return -1;
    }

    bool IsPacketStart(char[] marker, int size)
    {
        var set = new HashSet<char>(marker);
        return set.Count == size;
    }

    void GetInputs()
    {
        _input = File.ReadAllText("input.txt");
    }
}