using System;

namespace HomeworkDelegatesEvents;

public class TimeAndGeometryHelper
{
    public static void ShowCurrentTime()
    {
        Console.WriteLine("Поточний час: " + DateTime.Now.ToLongTimeString());
    }

    public static void ShowCurrentDate()
    {
        Console.WriteLine("Поточна дата: " + DateTime.Now.ToShortDateString());
    }

    public static void ShowCurrentDayOfWeek()
    {
        Console.WriteLine("Поточний день тижня: " + DateTime.Now.DayOfWeek);
    }

    public static bool IsPositive(double value)
    {
        return value > 0;
    }

    public static double CalculateTriangleArea(double baseLength, double height)
    {
        Predicate<double> isValid = IsPositive;
        if (!isValid(baseLength) || !isValid(height))
        {
            throw new ArgumentException("Основа та висота трикутника мають бути більшими за 0.");
        }
        return 0.5 * baseLength * height;
    }

    public static double CalculateRectangleArea(double width, double height)
    {
        Predicate<double> isValid = IsPositive;
        if (!isValid(width) || !isValid(height))
        {
            throw new ArgumentException("Сторони прямокутника мають бути більшими за 0.");
        }
        return width * height;
    }
}