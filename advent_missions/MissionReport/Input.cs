namespace missionreport;

public static class Input
{
    public static IEnumerable<string> AsLines(string missionDir)
    {
        return File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, missionDir, "data.txt"));
    }
    
    public static string AsText(string missionDir)
    {
        return File.ReadAllText(Path.Combine(Environment.CurrentDirectory, missionDir, "data.txt"));
    }
}