using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Xunit.Abstractions;

namespace MissionReport._05;

public class Missions
{
    private readonly ITestOutputHelper _output;

    private static IEnumerable<string> ReadData()
    {
        return File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "05", "data.txt")).Skip(10);
    }

    public Missions(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void FirstStar()
    {
        //         [F] [Q]         [Q]        
        // [B]     [Q] [V] [D]     [S]        
        // [S] [P] [T] [R] [M]     [D]        
        // [J] [V] [W] [M] [F]     [J]     [J]
        // [Z] [G] [S] [W] [N] [D] [R]     [T]
        // [V] [M] [B] [G] [S] [C] [T] [V] [S]
        // [D] [S] [L] [J] [L] [G] [G] [F] [R]
        // [G] [Z] [C] [H] [C] [R] [H] [P] [D]
        // 1   2   3   4   5   6   7   8   9 

        var stacks = new []
        {
            "BSJZVDG".ToList(),
            "PVGMSZ".ToList(),
            "FQTWSBLC".ToList(),
            "QVRMWGJH".ToList(),
            "DMFNSLC".ToList(),
            "DCGR".ToList(),
            "QSDJRTGH".ToList(),
            "VFP".ToList(),
            "JTSRD".ToList()
        };
        
        var instructions = ReadData()
            .Select(x => x.Split(" "));

        // move 3 from 5 to 2
        // move 3 from 8 to 4
        
        foreach (var instruction in instructions)
        {
            var count = int.Parse(instruction[1]);
            var from = int.Parse(instruction[3]) - 1;
            var to = int.Parse(instruction[5]) - 1;
            
            var candidates = stacks[from].Take(count).ToList();
            candidates.Reverse();
            
            stacks[from].RemoveRange(0, count);
            stacks[to].InsertRange(0, candidates);
        }

        var top = stacks.Select(x => x.First());
        
        _output.WriteLine($"Top items: {string.Join("", top)}");
    }

    [Fact]
    public void SecondStar()
    {
        //         [F] [Q]         [Q]        
        // [B]     [Q] [V] [D]     [S]        
        // [S] [P] [T] [R] [M]     [D]        
        // [J] [V] [W] [M] [F]     [J]     [J]
        // [Z] [G] [S] [W] [N] [D] [R]     [T]
        // [V] [M] [B] [G] [S] [C] [T] [V] [S]
        // [D] [S] [L] [J] [L] [G] [G] [F] [R]
        // [G] [Z] [C] [H] [C] [R] [H] [P] [D]
        // 1   2   3   4   5   6   7   8   9 

        var stacks = new []
        {
            "BSJZVDG".ToList(),
            "PVGMSZ".ToList(),
            "FQTWSBLC".ToList(),
            "QVRMWGJH".ToList(),
            "DMFNSLC".ToList(),
            "DCGR".ToList(),
            "QSDJRTGH".ToList(),
            "VFP".ToList(),
            "JTSRD".ToList()
        };
        
        var instructions = ReadData()
            .Select(x => x.Split(" "));

        // move 3 from 5 to 2
        // move 3 from 8 to 4
        
        foreach (var instruction in instructions)
        {
            var count = int.Parse(instruction[1]);
            var from = int.Parse(instruction[3]) - 1;
            var to = int.Parse(instruction[5]) - 1;
            
            var candidates = stacks[from].Take(count).ToList();
            
            stacks[from].RemoveRange(0, count);
            stacks[to].InsertRange(0, candidates);
        }

        var top = stacks.Select(x => x.First());
        
        _output.WriteLine($"Top items: {string.Join("", top)}");

    }
}