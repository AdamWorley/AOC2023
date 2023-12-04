using System.Text.RegularExpressions;

namespace AOC2023;

public partial class Day1
{

    [GeneratedRegex(@"(one)|(two)|(three)|(four)|(five)|(six)|(seven)|(eight)|(nine)")]
    public static partial Regex WordsRegex();

    public static async Task Run()
    {
        string path = Path.Combine(Environment.CurrentDirectory, "Day1/input.txt");

        try
        {
            // Open the text file using a stream reader.
            using var sr = new StreamReader(path);
            // Read the stream as a string, and write the string to the console.

            int result = 0;
            while (!sr.EndOfStream)
            {
                var line = await sr.ReadLineAsync();
                var number = ActualLineValue(line!);

                result += number;
            }

            Console.WriteLine(result);
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }

    public static int LineValue(string? line)
    {
        var chars = line!.ToCharArray();

        var numbers = chars.Where(char.IsDigit).ToArray();

        var tmp = numbers[0].ToString() + numbers[^1].ToString();
        return int.Parse(tmp);
    }

    public static int ActualLineValue(string line)
    {
        string firstDigit = FirstDigit(line);
        string lastDigit = LastDigit(line);

        return int.Parse(firstDigit + lastDigit);
    }

    public static string FirstDigit(string line)
    {
        for (var i = 0; i < line.Length; i++)
        {
            var digits = WordsRegex().Split(line[0..i]);
            if (digits.Length > 1)
            {
                return MapToValue(digits[1]);
            }

            if (char.IsDigit(line[i]))
            {
                return line[i].ToString();
            }
        }

        throw new IndexOutOfRangeException();
    }

    public static string LastDigit(string line)
    {
        for (var i = line.Length - 1; i >= 0; i--)
        {
            var digits = WordsRegex().Split(line[i..^0]);
            if (digits.Length > 1)
            {
                return MapToValue(digits[1]);
            }

            if (char.IsDigit(line[i]))
            {
                return line[i].ToString();
            }
        }

        throw new IndexOutOfRangeException();
    }

    public static string MapToValue(string number) => number switch
    {
        "one" => "1",
        "two" => "2",
        "three" => "3",
        "four" => "4",
        "five" => "5",
        "six" => "6",
        "seven" => "7",
        "eight" => "8",
        "nine" => "9",
        _ => "0"
    };
}
