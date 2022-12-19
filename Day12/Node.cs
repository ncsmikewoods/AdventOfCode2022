using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12;

public class Node
{
    public Node(char heightRaw, string id)
    {
        Value = heightRaw;
        Height = heightRaw - 96;

        switch (heightRaw)
        {
            case 'S':
                IsStart = true;
                Height = 1;
                break;
            case 'E':
                IsDestination = true;
                Height = 26;
                break;
        }
        
        Id = id;
    }

    public static int Comparisons { get; set; }
    
    public char Value { get; set; }
    public string Id { get; }
    public int Height { get; set; }
    public List<Node> Neighbors { get; set; } = new();
    public Node Parent { get; set; }
    public bool IsDestination { get; set; }
    public bool IsStart { get; set; }
    public bool IsLeaf => Neighbors.Count == 0;

    public void EstablishParenthood(HashSet<string> visited =  null)
    {
        visited ??= new HashSet<string>();
        visited.Add(Id);

        var neighborsToCull = new List<Node>();
        
        foreach (var neighbor in Neighbors)
        {
            if (visited.Contains(neighbor.Id))
            {
                neighborsToCull.Add(neighbor);
                continue;
            }
            neighbor.Parent = this;
            neighbor.EstablishParenthood(visited);
        }

        // this disallows multiple paths to the end.  I don't think we can use visited here
        Neighbors = Neighbors.Except(neighborsToCull).ToList();
    }

    public void RemoveCycles()
    {
        // if any neighbors are also my parent, remove them
        foreach (var neighbor in Neighbors)
        {
            if (neighbor == Parent)
            {
                Neighbors.Remove(neighbor);
            }
            else
            {
                neighbor.RemoveCycles();
            }
        }
    }

    #region bullshit

    // public void BuildPath2(List<Node> pathToHere)
    // {
    //     // Console.WriteLine($"Visiting node {Id}");
    //     // if pathToHere already contains me, bail
    //     if (pathToHere.Contains(this)) return;
    //
    //     // add path to Paths prop
    //     Paths.Add(GetPathString(pathToHere));
    //
    //     // add myself to copy of pathToHere
    //     var newPath = pathToHere.ToList();
    //     newPath.Add(this);
    //     
    //     // call buildPath2 on children
    //     foreach (var neighbor in Neighbors)
    //     {
    //         neighbor.BuildPath2(newPath);
    //     }
    // }
    //
    // public List<List<Node>> BuildPath(List<Node> pathToHere)
    // {
    //     // Console.WriteLine($"Building path for node: {GetPathString(pathToHere)}");
    //     Comparisons++;
    //     
    //     // base case
    //     if (IsLeaf)
    //     {
    //         if (IsDestination)
    //         {
    //             Console.WriteLine($"   Successful path found!: {GetPathString(pathToHere)}");
    //             return new List<List<Node>> { pathToHere };
    //         }
    //
    //         Console.WriteLine($"Leaf node: {GetPathString(pathToHere)}");
    //         return new List<List<Node>>();
    //     }
    //
    //     var paths = new List<List<Node>>();
    //     
    //     foreach (var neighbor in Neighbors)
    //     {
    //         if (pathToHere.Contains(neighbor)) continue; // cycle busting
    //         
    //         var pathToNeighbor = pathToHere.ToList();
    //         pathToNeighbor.Add(neighbor);
    //         
    //         var neighborPaths = neighbor.BuildPath(pathToNeighbor);
    //         if (neighborPaths.Count != 0) paths.AddRange(neighborPaths);
    //     }
    //
    //     return paths;
    // }
    //
    // public void BuildPath3(Dictionary<Node, List<Node>> map, List<Node> pathToHere)
    // {
    //     // start at destination
    //     // find what all can feed into it
    //     // visit each node
    //     // store in a map?
    //     // at each node, go to their valid neighbors if they aren't already in the map
    //     //  in reverse, you can go up as high as needed, but only down in height by a single step
    //     // repeat?
    //     // stop when you get the starting point?
    //     // find the entry in the map with the shortest path?
    //
    //     if (IsLeaf || IsStart)
    //     {
    //         Console.WriteLine($"Bailing from leaf at {Id}");
    //         return;
    //     }
    //
    //     var newPath = pathToHere.ToList();
    //     newPath.Add(this);
    //
    //     map[this] = newPath;
    //     
    //     foreach (var neighbor in Neighbors)
    //     {
    //         if (map.ContainsKey(neighbor)) continue;
    //         
    //         neighbor.BuildPath3(map, newPath);
    //     }
    // }
    

    #endregion//
    string GetPathString(List<Node> path)
    {
        if (path.Count == 0) return "";
        return new string(path.Select(x => x.Value).ToArray());
    }
}