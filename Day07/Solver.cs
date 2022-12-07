using System.Collections.Generic;
using System.Linq;

namespace Day07;

public class Solver
{
    private List<CommandBase> _commands;
    private Directory _head;

    public Solver()
    {
        GetInputs();
        _head = BuildFileSystem();
    }

    public double Solve1()
    {
        var size = 0d;
        _head.CalculateSize(ref size);
        return size;
    }
    
    public double Solve2()
    {
        var blah = 0d; // I can't be bothered to refactor this right now
        var maxSize = 70_000_000;
        var totalSpaceNeeded = 30_000_000;
        var currentUnusedSpace = maxSize - _head.CalculateSize(ref blah);
        var currentSpaceNeeded = totalSpaceNeeded - currentUnusedSpace;
        
        var directories = _head.GetAllSubdirectories();
        
        var candidates = 
            directories
                .Select(x => new {x, dir = x.DirectoryName,  size = x.CalculateSize(ref blah)})
                .Where(x => x.size > currentSpaceNeeded)
                .ToList();

        var winner = 
            candidates
                .OrderBy(x => x.size)
                .First(); 
        
        return winner.size;
    }

    Directory BuildFileSystem()
    {
        FileSystemEntity head = new Directory("/");
        FileSystemEntity current = head;

        foreach (var commandRaw in _commands)
        {
            if (commandRaw.IsDirectoryChange)
            {
                var command = (ChangeDirectoryCommand)commandRaw;

                if (command.Directory == "/")
                {
                    current = head;
                    continue;
                }

                if (command.Directory == "..")
                {
                    current = current.Parent;
                    continue;
                }

                // cd into a child
                var newCurrent = current.Subdirectories.Single(x => x.DirectoryName == command.Directory);
                newCurrent.Parent = current;
                current = newCurrent;
            }
            else
            {
                var command = (ListCommand)commandRaw;
                current.Children = command.Entries;
            }
        }

        return (Directory)head;
    }

    void GetInputs()
    {
        var lines = System.IO.File.ReadAllLines("input.txt");

        var commandLines = 
            lines
                .Select((line,i) => new {line,i})
                .Where(x => x.line.StartsWith("$"))
                .ToList();

        _commands = new List<CommandBase>();
        
        for (var index = 0; index < commandLines.Count; index++)
        {
            var commandLine = commandLines[index];
            var command = commandLine.line;
            var i = commandLine.i;

            if (command.StartsWith("$ cd"))
            {
                _commands.Add(new ChangeDirectoryCommand(command));
                continue;
            }

            // Get the outputs of ls
            // bullshittery here because LS output can get terminated by EOF instead of the start of another command
            int? nextCommandIndex = index < commandLines.Count - 1 ? commandLines[index + 1].i : null;
            var lsOutput = nextCommandIndex is not null ? lines[(i + 1)..nextCommandIndex.Value] : lines[(i + 1)..];
            
            _commands.Add(new ListCommand(lsOutput));
        }
    }
}