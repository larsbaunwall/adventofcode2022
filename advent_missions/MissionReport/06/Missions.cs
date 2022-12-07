using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Xunit.Abstractions;

namespace MissionReport._06;

public class Missions
{
    private readonly ITestOutputHelper _output;

    private static string ReadData()
    {
        return File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "06", "data.txt"));
    }

    public Missions(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void FirstStar()
    {
        var stream = ReadData();

        var markers = new List<int>();
        
        for (var i = 0; i < stream.Length; i++)
        {
            if (i > 3)
            {
                var segment = stream.Substring(i - 4, 4);

                if (!segment.GroupBy(x => x).Any(x => x.Count() > 1))
                {
                    markers.Add(i);
                }
            }
        }

        _output.WriteLine($"First marker: {string.Join("", markers.First())}");
    }

    [Fact]
    public void SecondStar()
    {
        var stream = ReadData();

        var messages = new List<int>();
        
        for (var i = 0; i < stream.Length; i++)
        {
            if (i > 13)
            {
                var segment = stream.Substring(i - 14, 14);

                if (!segment.GroupBy(x => x).Any(x => x.Count() > 1))
                {
                    messages.Add(i);
                }
            }
        }

        _output.WriteLine($"First marker: {string.Join("", messages.First())}");
    }
}