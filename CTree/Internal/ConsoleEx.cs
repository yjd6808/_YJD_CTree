/* * * * * * * * * * * * * 
 * 작성자: 윤정도
 * 생성일: 11/17/2022 3:18:23 PM
 * * * * * * * * * * * * *
 */

namespace CTree.Internal;

internal class ConsoleEx
{
    private static ConsoleColor s_previousForegroundColor = ConsoleColor.Gray;
    private static ConsoleColor s_previousBackgroundColor = ConsoleColor.Black;

    public static void SetForegroundColor(ConsoleColor color)
    {
        s_previousForegroundColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
    }

    public static void SetBackgroundColor(ConsoleColor color)
    {
        s_previousBackgroundColor = Console.BackgroundColor;
        Console.BackgroundColor = color;
    }

    public static void ResetForegroundColor() => Console.ForegroundColor = s_previousForegroundColor;
    public static void ResetBackgroundColor() => Console.BackgroundColor = s_previousBackgroundColor;

    public static void WriteLine<T>(T msg)
    {
        Console.WriteLine(msg);
    }

    public static void WriteLine<T>(T msg, ConsoleColor foregroundColor)
    {
        SetForegroundColor(foregroundColor);
        Console.WriteLine(msg);
        ResetForegroundColor();
    }

    public static void WriteLine<T>(T msg, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        SetForegroundColor(foregroundColor);
        SetBackgroundColor(backgroundColor);
        Console.WriteLine(msg);
        ResetForegroundColor();
        ResetBackgroundColor();
    }

    public static void Write<T>(T msg)
    {
        Console.Write(msg);
    }

    public static void Write<T>(T msg, ConsoleColor foregroundColor)
    {
        SetForegroundColor(foregroundColor);
        Console.Write(msg);
        ResetForegroundColor();
    }

    public static void Write<T>(T msg, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
    {
        SetForegroundColor(foregroundColor);
        SetBackgroundColor(backgroundColor);
        Console.Write(msg);
        ResetForegroundColor();
        ResetBackgroundColor();
    }
}