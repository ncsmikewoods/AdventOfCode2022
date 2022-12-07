using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace Day07;

public class Solver
{
    private List<CommandBase> _commands;

    public Solver()
    {
        GetInputs();
    }

    public double Solve1()
    {
        var head = BuildFileSystem();

        var size = 0d;
        
        ((Directory)head).CalculateSize(ref size);

        return size;
    }

    FileSystemEntity BuildFileSystem()
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

        return head;
    }

    public int Solve2()
    {
        return 0;
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

public abstract class FileSystemEntity
{
    public abstract bool IsFile { get; set; }

    public FileSystemEntity Parent { get; set; }

    public List<FileSystemEntity> Children { get; set; } = new();

    public List<Directory> Subdirectories => 
        Children
            .Where(x => !x.IsFile)
            .Select(x => (Directory)x)
            .ToList();
    
    public List<File> Files => 
        Children
            .Where(x => x.IsFile)
            .Select(x => (File)x)
            .ToList();
}

public class File : FileSystemEntity
{
    public File(string entryRaw)
    {
        var tokens = entryRaw.Split(" ");
        Size = int.Parse(tokens[0]);
        FileName = tokens[1];
    }
    
    public override bool IsFile { get; set; } = true;

    public string FileName { get; }

    public int Size { get; }
}

public class Directory : FileSystemEntity
{
    public Directory(string entryRaw)
    {
        var tokens = entryRaw.Split(" ");
        DirectoryName = tokens.Last();
    }
    
    public override bool IsFile { get; set; } = false;

    public string DirectoryName { get; }
    
    public double CalculateSize(ref double runningSize)
    {
        var fileSizes = Files.Sum(x => x.Size);
        double directorySizes = 0;
        foreach (var x in Subdirectories)
        {
            directorySizes += x.CalculateSize(ref runningSize);
        };

        var combined = fileSizes + directorySizes;
        
        if (combined <= 100_000) runningSize += combined;

        return combined;
    }

    public List<Directory> GetAllSubdirectories()
    {
        return 
            Subdirectories
                .SelectMany(x => x.Subdirectories)
                .ToList();
    }
}