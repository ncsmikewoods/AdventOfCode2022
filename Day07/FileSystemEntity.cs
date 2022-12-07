using System.Collections.Generic;
using System.Linq;

namespace Day07;

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