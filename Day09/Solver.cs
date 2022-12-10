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
        return 0;
        _visited = new HashSet<string>();
        
        var rope = new List<Location>();
        for (var i = 0; i < 2; i++) rope.Add(new Location(0, 0));

        _visited.Add($"{rope.Last().X},{rope.Last().Y}");
        
        foreach (var move in _moves)
        {
            SimulateMove(move, rope);
            // PrintVisited(_visited);
        }

        return _visited.Count;
    }
    
    public int Solve2()
    {
        // return 0;
        _visited = new HashSet<string>();

        var rope = new List<Location>();
        for (var i = 0; i < 10; i++) rope.Add(new Location(0, 0));

        _visited.Add($"{rope.Last().X},{rope.Last().Y}");

        // PrintRope(rope);

        // PrintGrid(rope);        
        
        var moveCount = 1;
        foreach (var move in _moves)
        {
            var dir = move.Direction;
            var mag = move.Magnitude;
            SimulateMove(move, rope);
            // PrintVisited(_visited);
            moveCount++;
            
            // PrintGrid(rope);
        }

        return _visited.Count;
    }

    void SimulateMove(Move move, List<Location> rope)
    {
        // Console.WriteLine($"Move: {move.Direction} {move.Magnitude}");
        var head = rope[0];
        
        for (var i = 0; i < move.Magnitude; i++)
        {
            MoveHead(move, head);
            // PrintRope(rope);
            MoveTails(rope);
            // PrintRope(rope);
        }
        // Console.WriteLine($"-------------------");
    }

    void MoveHead(Move move, Location head)
    {
        // Console.WriteLine("Moving head...");
        
        if (move.Direction == "R") head.X++;
        if (move.Direction == "L") head.X--;
        if (move.Direction == "U") head.Y++;
        if (move.Direction == "D") head.Y--;
    }

    void MoveTails(List<Location> rope)
    {
        // Console.WriteLine("Moving tails...");
        for (var i = 0; i < rope.Count - 1; i++)
        {
            // Console.WriteLine("Moving tail " + (j + 1));
            var effectiveHead = rope[i];
            var effectiveTail = rope[i+1];
                
            MoveTail(effectiveHead, effectiveTail);
                
            // PrintRope(rope);
                
            var isEndOfRope = i == rope.Count - 2;
            if (isEndOfRope) _visited.Add($"{effectiveTail.X},{effectiveTail.Y}");
            // PrintVisited(_visited);
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
    
    void PrintVisited(HashSet<string> visited)
    {
        Console.Write("Visited: ");
        foreach (var location in visited)
        {
            Console.Write($"{location}|");
        }
        Console.WriteLine();
    }

    void PrintRope(List<Location> rope)
    {
        foreach (var location in rope)
        {
            Console.Write($"{location.X},{location.Y}|");
        }
        Console.WriteLine();
    }

    void PrintGrid(List<Location> rope)
    {
        Console.WriteLine();

        var leftmost = Math.Min(rope.Min(l => l.X), 0);
        var rightmost = Math.Max(rope.Max(l => l.X), 0);
        var topmost = Math.Max(rope.Max(l => l.Y), 0);
        var bottommost = Math.Min(rope.Min(l => l.Y), 0);

        for (var x = leftmost - 1; x < rightmost + 1; x++)
        {
            for (var y = topmost + 1; y > bottommost - 1; y--)
            {
                if (rope[0].X == x && rope[0].Y == y)
                {
                    Console.Write("H");
                    continue;
                }
                
                var occupyingTail =
                    rope
                        .Select((loc, i) => new { loc, i })
                        .Skip(1)
                        .Where(elem => elem.loc.X == x && elem.loc.Y == y)
                        .Select(elem => elem.i)
                        .FirstOrDefault();

                if (occupyingTail != default)
                {
                    Console.Write(occupyingTail);
                    continue;
                }
                
                if (rope[0].X == 0 && rope[0].Y == 0)
                {
                    Console.Write("s");
                    continue;
                }
                
                Console.Write(".");
            }
            Console.WriteLine();
        }
        
        Console.WriteLine();
    }
}