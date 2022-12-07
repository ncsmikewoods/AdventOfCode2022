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
    
    public double CalculateSize()
    {
        var fileSizes = Files.Sum(x => x.Size);
        double directorySizes = 0;
        foreach (var x in Subdirectories)
        {
            directorySizes += x.CalculateSize();
        };

        return fileSizes + directorySizes;
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