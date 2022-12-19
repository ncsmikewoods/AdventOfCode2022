using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12;

public class Solver
{
    private List<Node> _nodes;    
    private Node _start;
    private Node _destination;

    public Solver()
    {
        GetInputs();
    }

    public int Solve1()
    {
        return GetDistance(_start);
    }
    
    public int Solve2()
    {
        return _nodes
            .Where(x => x.Height == 1)
            .Select(GetDistance)
            .Min();
    }

    int GetDistance(Node start)
    {
        var queue = new Queue<Node>();
        var visited = new Dictionary<Node, List<Node>>{ {start, new List<Node>()} };
        queue.Enqueue(start);

        // Breadth First Search
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.IsDestination)
            {
                break;
            }

            foreach (var neighbor in current.Neighbors)
            {
                if (visited.ContainsKey(neighbor)) continue;

                var pathToNeighbor = visited[current].ToList();
                pathToNeighbor.Add(neighbor);
                
                visited[neighbor] = pathToNeighbor;
                queue.Enqueue(neighbor);
            }
        }

        return visited.ContainsKey(_destination) 
            ? visited[_destination].Count 
            : int.MaxValue;
    }

    void GetInputs()
    {
        // var lines = System.IO.File.ReadAllLines("inputshort.txt");
        var lines = System.IO.File.ReadAllLines("input.txt");
        BuildGraph(lines);
    }

    void BuildGraph(string[] lines)
    {
        _nodes = new List<Node>();
        var map = new Dictionary<string, Node>();
        
        // Create graph nodes
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                var element = lines[i][j];
                var here = new Node(element, $"{i},{j}");
                
                _nodes.Add(here);
                map[$"{i},{j}"] = here;
            }
        }

        // Calculate valid neighbors
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                var node = map[$"{i},{j}"];
                if (node.IsDestination) continue;
        
                var neighborKeys = new[]
                {
                    $"{i-1},{j}",
                    $"{i+1},{j}",
                    $"{i},{j-1}",
                    $"{i},{j+1}"
                };
        
                foreach (var neighborKey in neighborKeys)
                {
                    if (map.TryGetValue(neighborKey, out var neighbor))
                    {
                        if (neighbor.Height - 1 <= node.Height)
                        {
                            node.Neighbors.Add(neighbor);
                        }
                    }
                }
            }
        }
        
        _start = _nodes.First(x => x.IsStart);
        _destination = _nodes.First(x => x.IsDestination);
    }
}