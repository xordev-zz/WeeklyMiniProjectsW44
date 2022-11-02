namespace UtilityLib;

public static class ConsoleExtention
{
    public static string StringReadNotEmpty(string text)
    {
        string inputString;
        while (true)
        {
            Console.Write($"{text}");
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                WriteLineColor("String can't be empty! Try again!", ConsoleColor.Red);
                continue;
            }
            inputString = input;
            break;
        }
        return inputString;
    }
    public static string StringRead(string? text = null)
    {
        if (text != null)
        {
            Console.Write($"{text}");
        }
        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        return (string)input;
    }
    public static int NumberReadPositive(string text)
    {
        UInt32 inputNumber;

        while (true)
        {
            Console.Write($"{text}");
            var inputString = Console.ReadLine();
            if (!UInt32.TryParse(inputString, out inputNumber))
            {
                WriteLineColor("Must be a positive number! Try again!", ConsoleColor.Red);
                continue;
            }
            break;
        }
        return (int)inputNumber;
    }
    public static void WriteLineColor(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}
