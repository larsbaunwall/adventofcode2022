using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace MissionReport._03;

public class Missions
{
    private static IEnumerable<string> ReadData()
    {
        return File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "03", "data.txt"));
    }

    [Fact]
    public void FirstStar()
    {
        var items = Enumerable.Range('a', 26)
            .Concat(Enumerable.Range('A', 26))
            .Select(x => (char)x)
            .ToList();

        var compartments = ReadData()
            .Select(x => x.ToCharArray())
            .Select(x => (first: x[..(x.Length / 2)], second: x[(x.Length / 2)..]));

        var sum = compartments
            .Sum(x => x.first.Distinct().Sum(c => x.second.Contains(c) ? items.IndexOf(c) + 1 : 0));
    }

    [Fact]
    public void SecondStar()
    {
        var items = Enumerable.Range('a', 26)
            .Concat(Enumerable.Range('A', 26))
            .Select(x => (char)x)
            .ToList();

        var rucksacks = ReadData()
            .Select(x => x.ToCharArray())
            .Chunk(3);

        var sum = rucksacks.Sum(x =>
        {
            var item = x[0].Distinct().Single(i => x[1].Contains(i) && x[2].Contains(i));

            return items.IndexOf(item) + 1;
        });
    }

}