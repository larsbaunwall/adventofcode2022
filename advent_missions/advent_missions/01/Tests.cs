namespace advent_missions._01;

public class Tests
{
    private async Task<string[]> ReadData()
    {
        return await File.ReadAllLinesAsync(Path.Combine(Environment.CurrentDirectory, "01", "data.txt"));
    }

    [Fact]
    public async Task FirstStar()
    {
        var data = await ReadData();
        var counts = new List<int>();
        
        var sum = 0;
        foreach (var line in data)
        {
            if (string.IsNullOrEmpty(line))
            {
                counts.Add(sum);
                sum = 0;
                continue;
            }
            
            sum += int.Parse(line);
        }

        var max = counts.Max();
    }
    
    [Fact]
    public async Task SecondStar()
    {
        var data = await ReadData();

        var counts = new List<int>();
        
        var sum = 0;
        foreach (var line in data)
        {
            if (!string.IsNullOrEmpty(line))
            {
                counts.Add(sum);
                sum = 0;
                continue;
            }
            
            sum += int.Parse(line);
        }

        var max = counts.OrderByDescending(x => x).Take(3).Sum();
    }

}