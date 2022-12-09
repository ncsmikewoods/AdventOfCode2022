using System;
using System.Collections.Generic;
using System.Linq;

namespace Day09;

public class Solver
{
    private List<Move> _moves;
    private HashSet<string> _visited;

    public Solver()
    {
        GetInputs();
    }

    public int Solve1()
    {
        var rope = new List<Location> { new(0, 0), new(0, 0) };

        _visited.Add($"{rope[1].X},{rope[1].Y}");
        
        foreach (var move in _moves)
        {
            SimulateMove(move, rope[0], rope[1], true);
        }

        return _visited.Count;
    }

    void SimulateMove(Move move, Location head, Location tail, bool isRealTail)
    {
        for (var i = 0; i < move.Magnitude; i++)
        {
            if (move.Direction == "R") head.X++;
            if (move.Direction == "L") head.X--;
            if (move.Direction == "U") head.Y++;
            if (move.Direction == "D") head.Y--;
            
            MoveTail(head, tail);
            if (isRealTail) _visited.Add($"{tail.X},{tail.Y}");
        }
    }
    
    public int Solve2()
    {
        return 0;
    }

    void MoveTail(Location head, Location tail)
    {
        var xDiff = Math.Abs(head.X - tail.X);
        var yDiff = Math.Abs(head.Y - tail.Y);
        
        // if they overlap do nothing
        // if they are within 1 do nothing
        if (head.Overlaps(tail)) return;
        if (xDiff <= 1 && yDiff <= 1) return;

        // if head is 2 away to the left make tail follow
        // moves towards it and matches same Y
        // if head is 2 away to the right make tail follow
        // moves towards it and matches same Y
        if (xDiff == 2)
        {
            tail.X += (head.X - tail.X) / 2;
            tail.Y = head.Y;
            return;
        }

        // if head is 2 away to the top make tail follow
        // moves towards it and matches same X
        // if head is 2 away to the bottom make tail follow
        // moves towards it and matches same X
        if (yDiff == 2)
        {
            tail.Y += (head.Y - tail.Y) / 2;
            tail.X = head.X;
            return;
        }

        throw new Exception($"Unexpected tail move situation: xDiff={xDiff} yDiff={yDiff}");
    }
    
    
    
    void GetInputs()
    {
        var lines = System.IO.File.ReadAllLines("input.txt");

        _moves = lines.Select(ParseLine).ToList();
        _visited = new HashSet<string>();
    }

    Move ParseLine(string line)
    {
        var tokens = line.Split(" ");

        return new Move(tokens[0], int.Parse(tokens[1]));
    }
}