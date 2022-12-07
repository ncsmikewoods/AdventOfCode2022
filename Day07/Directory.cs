using System.Collections.Generic;
using System.Linq;

namespace Day07;

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
        var directories = new List<Directory> { this };
        directories.AddRange(Subdirectories
            .SelectMany(x => x.GetAllSubdirectories())
            .ToList());
        
        return directories;
    }
}