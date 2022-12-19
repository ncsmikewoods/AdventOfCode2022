using System.Collections.Generic;

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

    public char Value { get; set; }
    public string Id { get; }
    public int Height { get; set; }
    public List<Node> Neighbors { get; set; } = new();
    public bool IsDestination { get; set; }
    public bool IsStart { get; set; }
}