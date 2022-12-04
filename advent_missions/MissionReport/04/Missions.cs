using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Xunit.Abstractions;

namespace MissionReport._04;

public class Missions
{
    private readonly ITestOutputHelper _output;

    private static IEnumerable<string> ReadData()
    {
        return File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "04", "data.txt"));
    }

    public Missions(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void FirstStar()
    {
        var input = ReadData()
            .Select(x => x.Split(","))
            .Select(x => (first: x[0].Split("-"), second: x[1].Split("-")))
            .Select(x => (
                first: Enumerable.Range(int.Parse(x.first[0]), int.Parse(x.first[1]) - int.Parse(x.first[0]) + 1),
                second: Enumerable.Range(int.Parse(x.second[0]), int.Parse(x.second[1]) - int.Parse(x.second[0]) + 1)));

        var count = input.Count(x =>
            x.first.All(itm => x.second.Contains(itm)) ||
            x.second.All(itm => x.first.Contains(itm)));

        _output.WriteLine($"Count: {count}");
    }

    [Fact]
    public void SecondStar()
    {
        var input = ReadData()
            .Select(x => x.Split(","))
            .Select(x => (first: x[0].Split("-"), second: x[1].Split("-")))
            .Select(x => (
                first: Enumerable.Range(int.Parse(x.first[0]), int.Parse(x.first[1]) - int.Parse(x.first[0]) + 1),
                second: Enumerable.Range(int.Parse(x.second[0]), int.Parse(x.second[1]) - int.Parse(x.second[0]) + 1)));

        var count = input.Count(x =>
            x.first.Any(itm => x.second.Contains(itm)) ||
            x.second.Any(itm => x.first.Contains(itm)));

        _output.WriteLine($"Count: {count}");
    }
}