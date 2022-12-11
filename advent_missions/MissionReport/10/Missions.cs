using System.Diagnostics;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using missionreport;
using Xunit.Abstractions;

namespace MissionReport._10;

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

    }
    
    [Fact]
    public void SecondStar()
    {

    }
}