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
        _visited = new HashSet<string>();
        
        var rope = new List<Location>();
        for (var i = 0; i < 2; i++) rope.Add(new Location(0, 0));

        _visited.Add($"{rope.Last().X},{rope.Last().Y}");
        
        foreach (var move in _moves)
        {
            SimulateMove(move, rope);
        }

        return _visited.Count;
    }
    
    public int Solve2()
    {
        _visited = new HashSet<string>();

        var rope = new List<Location>();
        for (var i = 0; i < 10; i++) rope.Add(new Location(0, 0));

        _visited.Add($"{rope.Last().X},{rope.Last().Y}");

        foreach (var move in _moves)
        {
            SimulateMove(move, rope);
        }

        return _visited.Count;
    }

    void SimulateMove(Move move, List<Location> rope)
    {
        var head = rope[0];
        
        for (var i = 0; i < move.Magnitude; i++)
        {
            MoveHead(move, head);
            MoveTails(rope);
        }
    }

    void MoveHead(Move move, Location head)
    {
        if (move.Direction == "R") head.X++;
        if (move.Direction == "L") head.X--;
        if (move.Direction == "U") head.Y++;
        if (move.Direction == "D") head.Y--;
    }

    void MoveTails(List<Location> rope)
    {
        for (var i = 0; i < rope.Count - 1; i++)
        {
            var segmentHead = rope[i];
            var segmentTail = rope[i+1];
                
            MoveTail(segmentHead, segmentTail);
                
            var isEndOfRope = i == rope.Count - 2;
            if (isEndOfRope) _visited.Add($"{segmentTail.X},{segmentTail.Y}");
        }
    }
    
    void MoveTail(Location head, Location tail)
    {
        var xDiff = Math.Abs(head.X - tail.X);
        var yDiff = Math.Abs(head.Y - tail.Y);
        
        if (head.Overlaps(tail)) return;
        if (xDiff <= 1 && yDiff <= 1) return;

        if (xDiff != 0)
        {
            var xOffset = head.X - tail.X;
            tail.X += xOffset > 0 ? 1 : -1;            
        }

        if (yDiff != 0)
        {
            var yOffset = head.Y - tail.Y;
            tail.Y += yOffset > 0 ? 1 : -1;            
        }
    }
    
    void GetInputs()
    {
        var lines = System.IO.File.ReadAllLines("input.txt");
        _moves = lines.Select(ParseLine).ToList();
    }

    Move ParseLine(string line)
    {
        var tokens = line.Split(" ");
        return new Move(tokens[0], int.Parse(tokens[1]));
    }
}