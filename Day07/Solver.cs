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
        var directories = _head.GetAllSubdirectories();
        
        return
            directories
                .Select(x => x.CalculateSize())
                .Where(x => x <= 100_000)
                .Sum();
    }
    
    public double Solve2()
    {
        var maxSize = 70_000_000;
        var totalSpaceNeeded = 30_000_000;
        var currentUnusedSpace = maxSize - _head.CalculateSize();
        var currentSpaceNeeded = totalSpaceNeeded - currentUnusedSpace;
        
        var directories = _head.GetAllSubdirectories();
        
        var candidates = 
            directories
                .Select(x => new {x, dir = x.DirectoryName,  size = x.CalculateSize()})
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
                    // Go to the top of the tree
                    current = head;
                    continue;
                }

                if (command.Directory == "..")
                {
                    // Move up a level
                    current = current.Parent;
                    continue;
                }

                // Go into a child (by name)
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
        var lines = System.IO.File.ReadAllLines("inputshort.txt");

        var commandLines = 
            lines
                .Select((line,i) => new {line,i})
                .Where(x => x.line.StartsWith("$"))
                .ToList();
        commandLines.Add(new {line = "", i = lines.Length}); // Make a fake command at EOF

        _commands = new List<CommandBase>();
        
        for (var index = 0; index < commandLines.Count - 1; index++)
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
            var nextCommandIndex = commandLines[index + 1].i;
            var lsOutput = lines[(i + 1)..nextCommandIndex];
            
            _commands.Add(new ListCommand(lsOutput));
        }
    }
}