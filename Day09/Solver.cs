using System;
using System.Collections.Generic;
using System.Linq;

namespace Day09;

public class Solver
{
    private List<Move> _moves;
    private int _width;
    private int _height;
    private int _totalMoves;
    private HashSet<string> _visited;

    public Solver()
    {
        GetInputs();
    }

    public int Solve1()
    {
        var head = new Location(0, 0);
        var tail = new Location(0, 0);

        _visited.Add($"{tail.X},{tail.Y}");
        
        foreach (var move in _moves)
        {
            SimulateMove(move, head, tail);
        }

        return _visited.Count;

        // var visitedSum = 0;
        // for (var i = 0; i < _visited.GetLength(0); i++)
        // {
        //     for (var j = 0; j < _visited.GetLength(1); j++)
        //     {
        //         visitedSum += _visited[i, j] ? 1 : 0;
        //     }
        // }
        //
        // return visitedSum;
    }

    void SimulateMove(Move move, Location head, Location tail)
    {
        for (var i = 0; i < move.Magnitude; i++)
        {
            if (move.Direction == "R") head.X++;
            if (move.Direction == "L") head.X--;
            if (move.Direction == "U") head.Y++;
            if (move.Direction == "D") head.Y--;
            
            MoveTail(head, tail);
            _visited.Add($"{tail.X},{tail.Y}");
        }
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
    
    public int Solve2()
    {
        return 0;
    }
    
    void GetInputs()
    {
        var lines = System.IO.File.ReadAllLines("input.txt");

        _moves = lines.Select(ParseLine).ToList();
        _totalMoves = _moves.Sum(x => x.Magnitude);

        var totalRightLength = _moves.Where(x => x.Direction == "R").Sum(x => x.Magnitude);
        var totalLeftLength = _moves.Where(x => x.Direction == "L").Sum(x => x.Magnitude);
        _width = Math.Abs(totalRightLength - totalLeftLength) + 1;
        
        var totalUpLength = _moves.Where(x => x.Direction == "U").Sum(x => x.Magnitude);
        var totalDownLength = _moves.Where(x => x.Direction == "D").Sum(x => x.Magnitude);
        _height = Math.Abs(totalUpLength - totalDownLength) + 1;

        var (width, height) = CalculateGridSize(_moves);
        _visited = new HashSet<string>();
    }

    (int, int) CalculateGridSize(List<Move> moves)
    {
        var x = 0;
        var y = 0;
        var highestX = 0;
        var highestY = 0;
        
        foreach (var move in moves)
        {
            if (move.Direction == "R")
            {
                x += move.Magnitude;
                highestX = x > highestX ? x : highestX;
            }
            
            if (move.Direction == "L")
            {
                x -= move.Magnitude;
            }
            
            if (move.Direction == "U")
            {
                y += move.Magnitude;
                highestY = y > highestY ? y : highestY;
            }
            
            if (move.Direction == "D")
            {
                y -= move.Magnitude;
            }
        }

        return (highestX + 1, highestY + 1);
    }

    Move ParseLine(string line)
    {
        var tokens = line.Split(" ");

        return new Move(tokens[0], int.Parse(tokens[1]));
    }
}

public class Move
{
    public Move(string Direction, int Magnitude)
    {
        this.Direction = Direction;
        this.Magnitude = Magnitude;
    }

    public string Direction { get; set; }
    public int Magnitude { get; set; }
}

public class Location
{
    public Location(int X, int Y)
    {
        this.X = X;
        this.Y = Y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public bool Overlaps(Location other)
    {
        return X == other.X && Y == other.Y;
    }
}