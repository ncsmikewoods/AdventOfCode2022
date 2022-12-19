using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12;

public class Solver
{
    private Node _start;
    private List<Node> _nodes;
    private Node _destination;
    private Dictionary<string, Node> _map;

    public Solver()
    {
        GetInputs();
    }

    public int Solve1()
    {
        //  1  procedure BFS(G, root) is
        //  2      let Q be a queue
        //  3      label root as explored
        //  4      Q.enqueue(root)
        //  5      while Q is not empty do
        //  6          v := Q.dequeue()
        //  7          if v is the goal then
        //  8              return v
        //  9          for all edges from v to w in G.adjacentEdges(v) do
        // 10              if w is not labeled as explored then
        // 11                  label w as explored
        // 12                  w.parent := v
        // 13                  Q.enqueue(w)

        var queue = new Queue<Node>();
        var visited = new Dictionary<Node, List<Node>>{ {_start, new List<Node>()} };
        queue.Enqueue(_start);

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
                
                // something about recording this?
                // store path to that node
            }
        }

        return visited[_destination].Count;
    }
    
    public int Solve2()
    {
        return 0;
    }

    void GetInputs()
    {
        // var lines = System.IO.File.ReadAllLines("inputshort.txt");
        // var lines = System.IO.File.ReadAllLines("inputmedium.txt");
        var lines = System.IO.File.ReadAllLines("input.txt");
        _start = BuildGraph(lines);
    }

    Node BuildGraph(string[] lines)
    {
        _nodes = new List<Node>();
        _map = new Dictionary<string, Node>();
        
        // create graph nodes
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                var element = lines[i][j];
                var here = new Node(element, $"{i},{j}");
                
                _nodes.Add(here);
                _map[$"{i},{j}"] = here;
            }
        }

        // establish neighbors.  Graph has cycles at this point
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                // get the node here
                var node = _map[$"{i},{j}"];
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
                    if (_map.TryGetValue(neighborKey, out var neighbor))
                    {
                        if (neighbor.Height - 1 <= node.Height)
                        {
                            node.Neighbors.Add(neighbor);
                        }
                    }
                }
            }
        }
        

        var head = _nodes.First(x => x.IsStart);
        _destination = _nodes.First(x => x.IsDestination);
        
        return head;
    }
}