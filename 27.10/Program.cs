using System;

namespace HomeworkDelegatesEvents;

internal class Program
{
    static void ShowNotification(string message)
    {
        Console.WriteLine("[Подія] " + message);
    }

    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("=== Завдання 2: Action, Predicate, Func ===");

            Action showInfo = TimeAndGeometryHelper.ShowCurrentTime;
            showInfo += TimeAndGeometryHelper.ShowCurrentDate;
            showInfo += TimeAndGeometryHelper.ShowCurrentDayOfWeek;
            showInfo.Invoke();

            Predicate<double> checkPositive = TimeAndGeometryHelper.IsPositive;
            Console.WriteLine($"Число 5 додатне: {checkPositive(5)}");
            Console.WriteLine($"Число -3 додатне: {checkPositive(-3)}");

            Func<double, double, double> triangleArea = TimeAndGeometryHelper.CalculateTriangleArea;
            Func<double, double, double> rectangleArea = TimeAndGeometryHelper.CalculateRectangleArea;

            Console.WriteLine($"Площа трикутника (10, 5): {triangleArea(10, 5)}");
            Console.WriteLine($"Площа прямокутника (4, 7): {rectangleArea(4, 7)}");

            Console.WriteLine("\n=== Завдання 3: Кредитна картка та події ===");

            CreditCard card = new CreditCard(
                "4149 1234 5678 9012",
                "Іван Петренко",
                new DateTime(2028, 12, 31),
                "1234",
                5000,
                2000,
                5000
            );

            card.OnDeposit += ShowNotification;
            card.OnWithdraw += ShowNotification;
            card.OnCreditUsed += ShowNotification;
            card.OnTargetBalanceReached += ShowNotification;
            card.OnPinChanged += ShowNotification;

            Console.WriteLine(card);
            Console.WriteLine();

            card.Deposit(3500);
            card.Withdraw(6000);
            card.ChangePin("1234", "4321");

            Console.WriteLine("\nСтан картки після операцій:");
            Console.WriteLine(card);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
    }
}