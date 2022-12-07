using System.Diagnostics;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using missionreport;
using Xunit.Abstractions;

namespace MissionReport._07;

public class Missions
{
    private readonly ITestOutputHelper _output;

    public Missions(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void FirstStar()
    {
        var history = Input.AsLines("07");

        var fs = CreateFilesystem(history);
        
        var threshold = fs.Flatten()
            .Where(x => x.IsDirectory)
            .Where(x => x.Size < 100000);
        
        _output.WriteLine($"Total size of dirs under threshold: {threshold.Sum(x => x.Size)}");
    }
    
    [Fact]
    public void SecondStar()
    {
        var history = Input.AsLines("07");
        var fs = CreateFilesystem(history);

        var capacity = 70000000;
        var requirement = 30000000;
        var freeSpace = capacity - fs.Size;

        var candidate = fs.Flatten()
            .Where(x => x.IsDirectory)
            .Where(x => freeSpace + x.Size >= requirement)
            .MinBy(x => freeSpace + x.Size - requirement);

        _output.WriteLine($"Delete this directory: {candidate.Name} (size {candidate.Size})");
    }

    private Node CreateFilesystem(IEnumerable<string> history)
    {
        var fs = new Node { Name = "/"};
        var currentDir = fs;
        
        foreach (var line in history.Select(x => x.Split(" ")))
        {
            if (line[0] == "$")
            {
                if (line[1] == "cd")
                {
                    currentDir = line[2] switch
                    {
                        "/" => fs,
                        ".." => currentDir.Parent,
                        _ => currentDir.Children.Single(x => x.Name == line[2])
                    };
                } 
            }
            else
            {
                currentDir.Children.Add(line[0] == "dir"
                    ? new Node { Name = line[1], Parent = currentDir }
                    : new Node { Name = line[1], Size = int.Parse(line[0]), Parent = currentDir });
            }
        }
        
        void CalculateSize(Node entry)
        {
            foreach (var child in entry.Children.Where(x => x.IsDirectory))
            {
                CalculateSize(child);
            }
        
            if (entry.IsDirectory)
            {
                entry.Size = entry.Children
                    .Sum(x => x.Size);
            }
        }

        CalculateSize(fs);
        
        return fs;
    }

    public class Node
    {
        public Node Parent { get; set; }
        public List<Node> Children { get; set; } = new List<Node>();
        public string Name { get; set; }
        public bool IsDirectory => Children.Any();
        public int Size { get; set; }
        
        public IEnumerable<Node> Flatten()
        {
            return new List<Node>(this.Children.SelectMany(x => x.Flatten())) { this };
        }
    }
}