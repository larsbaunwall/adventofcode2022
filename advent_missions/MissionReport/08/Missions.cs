using System.Diagnostics;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using missionreport;
using Xunit.Abstractions;

namespace MissionReport._08;

public class Missions
{
    private readonly ITestOutputHelper _output;

    public Missions(ITestOutputHelper output)
    {
        _output = output;
    }

    private int[][] AllTrees()
    {
        return Input.AsLines("08")
            .Select(x => x
                .Select(s => int.Parse(s.ToString()))
                .ToArray())
            .ToArray();
    }

    private (int[] N, int[] E, int[] S, int[] W) GetDirections(int[][] trees, int x, int y)
    {
        var west = trees[y][..x];
        var east = trees[y][(x + 1)..];

        var col = trees
            .Select(elem => elem[x])
            .ToArray();
        var north = col[..y];
        var south = col[(y + 1)..];

        return (north, east, south, west);
    }

    [Fact]
    public void FirstStar()
    {
        var trees = AllTrees();

        var visibleTrees = 0;

        for (var y = 0; y < trees.Length; y++)
        {
            for (var x = 0; x < trees[0].Length; x++)
            {
                if (y == 0 || x == 0 || y == trees.Length - 1 || x == trees[0].Length - 1)
                {
                    visibleTrees += 1;
                }
                else
                {
                    var t = trees[y][x];
                    var directions = GetDirections(trees, x, y);

                    if (directions.N.Max() < t
                        || directions.E.Max() < t
                        || directions.S.Max() < t
                        || directions.W.Max() < t)
                    {
                        visibleTrees += 1;
                    }
                }
            }
        }

        _output.WriteLine($"Visible trees: {visibleTrees}");
    }

    [Fact]
    public void SecondStar()
    {
        var trees = AllTrees();

        var scenicScores = new List<int>();

        for (var y = 0; y < trees.Length; y++)
        {
            for (var x = 0; x < trees[0].Length; x++)
            {
                var t = trees[y][x];
                var directions = GetDirections(trees, x, y);

                var n = directions.N.Reverse().TakeUntil(elem => elem < t).Count();
                var e = directions.E.TakeUntil(elem => elem < t).Count();
                var s = directions.S.TakeUntil(elem => elem < t).Count();
                var w = directions.W.Reverse().TakeUntil(elem => elem < t).Count();

                scenicScores.Add(n * e * s * w);
            }
        }

        _output.WriteLine($"Best scenic score: {scenicScores.Max()}");
    }
}

public static class Extensions
{
    public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach(var item in source)
        {
            if(predicate(item)) 
            {
                yield return item;
            }
            else
            {
                yield return item;
                yield break;
            }
        }    }
}