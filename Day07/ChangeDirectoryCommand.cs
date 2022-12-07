using System.Linq;

namespace Day07;

public class ChangeDirectoryCommand : CommandBase
{
    public ChangeDirectoryCommand(string commandRaw)
    {
        Directory = commandRaw.Split(" ").Last();
    }
    
    public string Directory { get; }
    public override bool IsDirectoryChange => true;
}