using System.Diagnostics;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using missionreport;
using Xunit.Abstractions;

namespace MissionReport._11;

public class Missions
{
    private readonly ITestOutputHelper _output;

    public Missions(ITestOutputHelper output)
    {
        _output = output;
    }

    private Monkey[] InitializeMonkeys()
    {
        return new[]
        {
            new Monkey
            {
                Items = { 52, 60, 85, 69, 75, 75 },
                Operation = i => i * 17,
                Divisor = 13,
                MonkeyIfTrue = 6,
                MonkeyIfFalse = 7
            },
            new Monkey
            {
                Items = { 96, 82, 61, 99, 82, 84, 85 },
                Operation = i => i + 8,
                Divisor = 7,
                MonkeyIfTrue = 0,
                MonkeyIfFalse = 7
            },
            new Monkey
            {
                Items = { 95, 79 },
                Operation = i => i + 6,
                Divisor = 19,
                MonkeyIfTrue = 5,
                MonkeyIfFalse = 3
            },
            new Monkey
            {
                Items = { 88, 50, 82, 65, 77 },
                Operation = i => i * 19,
                Divisor = 2,
                MonkeyIfTrue = 4,
                MonkeyIfFalse = 1
            },
            new Monkey
            {
                Items = { 66, 90, 59, 90, 87, 63, 53, 88 },
                Operation = i => i + 7,
                Divisor = 5,
                MonkeyIfTrue = 1,
                MonkeyIfFalse = 0
            },
            new Monkey
            {
                Items = { 92, 75, 62 },
                Operation = i => i * i,
                Divisor = 3,
                MonkeyIfTrue = 3,
                MonkeyIfFalse = 4
            },
            new Monkey
            {
                Items = { 94, 86, 76, 67 },
                Operation = i => i + 1,
                Divisor = 11,
                MonkeyIfTrue = 5,
                MonkeyIfFalse = 2
            },
            new Monkey
            {
                Items = { 57 },
                Operation = i => i + 2,
                Divisor = 17,
                MonkeyIfTrue = 6,
                MonkeyIfFalse = 2
            },
        };
    }

    [Fact]
    public void FirstStar()
    {
        var monkeys = InitializeMonkeys();

        for (int i = 0; i < 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                monkey.InspectItems(monkeys, item => item / 3);
            }
        }

        var top = monkeys
            .Select(x => x.InspectionCounter)
            .OrderByDescending(i => i)
            .ToArray();

        _output.WriteLine($"Monkey business: {top[0] * top[1]}");
    }

    [Fact]
    public void SecondStar()
    {
        var monkeys = InitializeMonkeys();

        // https://en.wikipedia.org/wiki/Chinese_remainder_theorem
        var modulus = monkeys.Select(x => x.Divisor).Aggregate(1L, (i, j) => i * j);

        for (int i = 0; i < 10000; i++)
        {
            foreach (var monkey in monkeys)
            {
                monkey.InspectItems(monkeys, item => item % modulus);
            }
        }

        var top = monkeys
            .Select(x => x.InspectionCounter)
            .OrderByDescending(i => i)
            .ToArray();

        _output.WriteLine($"Monkey business: {top[0] * top[1]}");
    }

    class Monkey
    {
        public List<long> Items { get; set; } = new List<long>();
        public Func<long, long> Operation { get; set; }
        public long Divisor { get; set; }
        public int MonkeyIfTrue { get; set; }
        public int MonkeyIfFalse { get; set; }
        public long InspectionCounter { get; private set; }

        public void InspectItems(Monkey[] allMonkeys, Func<long,long> reduceWorry)
        {
            foreach (var item in Items.Select(item => Operation(item)))
            {
                allMonkeys[reduceWorry(item) % Divisor == 0 ? MonkeyIfTrue : MonkeyIfFalse]
                    .Items.Add(reduceWorry(item));
                InspectionCounter += 1;
            }

            Items.Clear();
        }
    }
}