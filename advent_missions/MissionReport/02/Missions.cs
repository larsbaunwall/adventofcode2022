using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace MissionReport._02;

public class Missions
{
    private static IEnumerable<string> ReadData()
    {
        return File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "02", "data.txt"));
    }
    
    private IEnumerable<(string opponent, string me)> _rounds = ReadData()
        .Select(x => 
            (
                opponent: x[..1],
                me: x[2..]
            )
        );


    [Fact]
    public void FirstStar()
    {
        // A for Rock, B for Paper, and C for Scissors
        // X for Rock, Y for Paper, and Z for Scissors
        
        // The winner of the whole tournament is the player with the highest score.
        // Your total score is the sum of your scores for each round.
        // The score for a single round is the score for the shape you selected
        // (1 for Rock, 2 for Paper, and 3 for Scissors)
        // plus the score for the outcome of the round (0 if you lost, 3 if the round was a draw, and 6 if you won).
        
        int score = 0;
        foreach (var round in _rounds)
        {
            // Rock defeats Scissors, Scissors defeats Paper, and Paper defeats Rock.
            // If both players choose the same shape, the round instead ends in a draw.

            score += round switch  
            {   
                ("A", "X") => 1 + 3,  
                ("A", "Y") => 2 + 6,  
                ("A", "Z") => 3 + 0,  
                ("B", "X") => 1 + 0,  
                ("B", "Y") => 2 + 3,  
                ("B", "Z") => 3 + 6,  
                ("C", "X") => 1 + 6,  
                ("C", "Y") => 2 + 0,  
                ("C", "Z") => 3 + 3,  
                _ => throw new NotImplementedException()  
            };
        }
    }

    [Fact]
    public void SecondStar()
    {
        // The Elf finishes helping with the tent and sneaks back over to you.
        // "Anyway, the second column says how the round needs to end:
        // X means you need to lose,
        // Y means you need to end the round in a draw, and
        // Z means you need to win.
        // Good luck!"

        // Rock defeats Scissors, Scissors defeats Paper, and Paper defeats Rock.
        // If both players choose the same shape, the round instead ends in a draw.

        var strategy = _rounds
            .Select(x =>
                {
                    // A for Rock, B for Paper, and C for Scissors
                    // X for Rock, Y for Paper, and Z for Scissors

                    return x.me switch
                    {
                        //Draw
                        "Y" => x.opponent switch
                        {
                            "A" => (opponent: x.opponent, me: "X"),
                            "B" => (opponent: x.opponent, me: "Y"),
                            "C" => (opponent: x.opponent, me: "Z"),
                        },
                        //Lose
                        "X" => x.opponent switch
                        {
                            "A" => (opponent: x.opponent, me: "Z"),
                            "B" => (opponent: x.opponent, me: "X"),
                            "C" => (opponent: x.opponent, me: "Y"),
                        },
                        // Win
                        "Z" => x.opponent switch
                        {
                            "A" => (opponent: x.opponent, me: "Y"),
                            "B" => (opponent: x.opponent, me: "Z"),
                            "C" => (opponent: x.opponent, me: "X"),
                        },
                        _ => x
                    };
                }
            );

        int score = 0;
        foreach (var round in strategy)
        {
            // Rock defeats Scissors, Scissors defeats Paper, and Paper defeats Rock.
            // If both players choose the same shape, the round instead ends in a draw.


            score += round switch
            {
                ("A", "X") => 1 + 3,
                ("A", "Y") => 2 + 6,
                ("A", "Z") => 3 + 0,
                ("B", "X") => 1 + 0,
                ("B", "Y") => 2 + 3,
                ("B", "Z") => 3 + 6,
                ("C", "X") => 1 + 6,
                ("C", "Y") => 2 + 0,
                ("C", "Z") => 3 + 3,
                _ => throw new NotImplementedException()
            };
        }
    }

}