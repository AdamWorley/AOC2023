namespace AOC2023.Day2;

public sealed class Day2
{
    public static void Run()
    {
        string path = Path.Combine(Environment.CurrentDirectory, "Day2/input.txt");

        using var sr = new StreamReader(path);
        var file = sr.ReadToEnd();

        var lines = ReadFile(file);

        var games = lines.Select(GameFromLine);

        int result = games.Select(FewestCubes).Sum();

        Console.WriteLine(result);
    }

    public static string[] ReadFile(string file) => file.Split(Environment.NewLine);

    public static bool AllRounds(Game x) => x.Rounds.All(AllDrawnColours);

    public static bool AllDrawnColours(Round round) => round.DrawnColours.All(SecondCondition);

    public static bool SecondCondition(DrawnColour colours)
    {
        const int maxRed = 12;
        const int maxGreen = 13;
        const int maxBlue = 14;

        return colours switch
        {
            ("red", <= maxRed) => true,
            ("green", <= maxGreen) => true,
            ("blue", <= maxBlue) => true,
            _ => false
        };
    }

    public static int FewestCubes(Game game)
    {
        var groupedColours = game.Rounds.SelectMany(x => x.DrawnColours)
                                        .GroupBy(x => x.Colour);

        return MaxForColour(groupedColours, "red") * MaxForColour(groupedColours, "green") * MaxForColour(groupedColours, "blue");
    }

    public static int MaxForColour(IEnumerable<IGrouping<string, DrawnColour>> groupedColours, string colour)
    {
        return groupedColours.FirstOrDefault(x => x.Key == colour)?.MaxBy(x => x.Count)?.Count ?? 0;
    }

    public static Game GameFromLine(string game)
    {
        var tmp = game.Split(":", 2);

        var gameNumber = tmp[0];
        var rounds = tmp[1].Trim();

        return new Game(int.Parse(gameNumber.Split(" ")[1].Trim()), GetRounds(rounds));
    }

    public static Round[] GetRounds(string game)
    {
        return game.Split("; ")
                   .Select(x => new Round(ColourBreakdown(x)))
                   .ToArray();
    }

    public static DrawnColour[] ColourBreakdown(string round)
    {
        return round.Split(", ")
                    .Select(x => x.Split(" "))
                    .Select(x => new DrawnColour(x[1], int.Parse(x[0])))
                    .ToArray();
    }

    public sealed record Game(int Number, Round[] Rounds);
    public sealed record Round(DrawnColour[] DrawnColours);
    public sealed record DrawnColour(string Colour, int Count);
}