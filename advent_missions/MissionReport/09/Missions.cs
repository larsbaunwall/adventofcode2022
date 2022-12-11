using System.Collections;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using missionreport;
using Xunit.Abstractions;

namespace MissionReport._09;

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
        var moves = Input.AsLines("09")
            .Select(x => (Direction: x[..1], Count: int.Parse(x[2..])));

        int hx = 0, hy = 0, tx = 0, ty = 0;

        var tailPositions = new List<(int tx, int ty)> { (0, 0) };

        foreach (var move in moves)
        {
            foreach (var head in MovePosition(move.Direction, move.Count))
            {
                if (head.X <= tx + 1 && head.X >= tx - 1 && head.Y <= ty + 1 && head.Y >= ty - 1) continue;

                var newX = hx - tx;
                var newY = hy - ty;

                var vector = (
                    x: Math.Sign(newX) * (int)Math.Ceiling((double)Math.Abs(newX) / 2),
                    y: Math.Sign(newY) * (int)Math.Ceiling((double)Math.Abs(newY) / 2));

                (tx, ty) = (tx + vector.x, ty + vector.y);

                tailPositions.Add((tx, ty));
            }
        }

        IEnumerable<(int X, int Y)> MovePosition(string dir, int count)
        {
            for (var i = 0; i < count; i++)
            {
                var result = dir switch
                {
                    "U" => (hx, hy++),
                    "D" => (hx, hy--),
                    "L" => (hx--, hy),
                    "R" => (hx++, hy),
                    _ => (hx, hy)
                };

                yield return (hx, hy);
            }
        }

        _output.WriteLine($"Number of distinct positions: {tailPositions.Distinct().Count()}");
    }

    [Fact]
    public void SecondStar()
    {
        var moves = Input.AsLines("09")
            .Select(x => (Direction: x[..1], Count: int.Parse(x[2..])));

        var knots = Enumerable.Repeat((X: 0, Y: 0), 10).ToArray();

        var tailPositions = new List<(int knot, int x, int y)>();

        foreach (var head in moves.SelectMany(x => MovePosition(x.Direction, x.Count)))
        {
            for (var i = 1; i < 10; i++)
            {
                var prev = knots[i - 1];
                var curr = knots[i];

                if (prev.X <= curr.X + 1 
                    && prev.X >= curr.X - 1 
                    && prev.Y <= curr.Y + 1 
                    && prev.Y >= curr.Y - 1) continue;

                var newX = prev.X - curr.X;
                var newY = prev.Y - curr.Y;

                var vector = (
                    x: Math.Sign(newX) * (int)Math.Ceiling((double)Math.Abs(newX) / 2),
                    y: Math.Sign(newY) * (int)Math.Ceiling((double)Math.Abs(newY) / 2));

                knots[i] = (curr.X + vector.x, curr.Y + vector.y);

                tailPositions.Add((i, knots[i].X, knots[i].Y));
            }
        }

        IEnumerable<(int X, int Y)> MovePosition(string dir, int count)
        {
            var head = knots[0];

            for (var i = 0; i < count; i++)
            {
                var result = dir switch
                {
                    "U" => (head.X, head.Y++),
                    "D" => (head.X, head.Y--),
                    "L" => (head.X--, head.Y),
                    "R" => (head.X++, head.Y),
                    _ => (head.X, head.Y)
                };

                knots[0] = head;

                yield return head;
            }
        }

        _output.WriteLine($"Number of distinct positions: {tailPositions.Where(x => x.knot == 9).Distinct().Count() + 1}");
    }
}