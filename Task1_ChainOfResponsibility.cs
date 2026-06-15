using System;

abstract class SupportHandler
{
    protected SupportHandler _next;

    public SupportHandler SetNext(SupportHandler next)
    {
        _next = next;
        return next;
    }

    public abstract void Handle();
}

class TechnicalSupportHandler : SupportHandler
{
    public override void Handle()
    {
        Console.WriteLine("\n[Технічна підтримка]");
        Console.WriteLine("Ваша проблема вирішена технічним відділом. Дякуємо!");
    }
}

class BillingSupportHandler : SupportHandler
{
    public override void Handle()
    {
        Console.WriteLine("\n[Відділ білінгу]");
        Console.WriteLine("Ваше питання щодо оплати вирішено. Дякуємо!");
    }
}

class AccountSupportHandler : SupportHandler
{
    public override void Handle()
    {
        Console.WriteLine("\n[Відділ акаунтів]");
        Console.WriteLine("Ваш акаунт відновлено. Дякуємо!");
    }
}

class GeneralSupportHandler : SupportHandler
{
    public override void Handle()
    {
        Console.WriteLine("\n[Загальна підтримка]");
        Console.WriteLine("Оператор зв'яжеться з вами протягом 24 годин. Дякуємо!");
    }
}

class SupportSystem
{
    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n=============================");
            Console.WriteLine("Ласкаво просимо до підтримки!");
            Console.WriteLine("=============================");
            Console.WriteLine("1. Технічна проблема");
            Console.WriteLine("2. Питання щодо оплати");
            Console.WriteLine("3. Проблема з акаунтом");
            Console.WriteLine("4. Інше");
            Console.Write("Ваш вибір: ");

            string input = Console.ReadLine();

            SupportHandler handler = null;

            switch (input)
            {
                case "1":
                    Console.WriteLine("\n--- Технічна підтримка ---");
                    Console.WriteLine("1. Проблема з інтернетом");
                    Console.WriteLine("2. Проблема з пристроєм");
                    Console.WriteLine("3. Інша технічна проблема");
                    Console.Write("Ваш вибір: ");
                    string techInput = Console.ReadLine();
                    handler = new TechnicalSupportHandler();
                    break;

                case "2":
                    Console.WriteLine("\n--- Білінг ---");
                    Console.WriteLine("1. Помилкове списання");
                    Console.WriteLine("2. Зміна тарифу");
                    Console.WriteLine("3. Інше");
                    Console.Write("Ваш вибір: ");
                    string billInput = Console.ReadLine();
                    handler = new BillingSupportHandler();
                    break;

                case "3":
                    Console.WriteLine("\n--- Акаунт ---");
                    Console.WriteLine("1. Забув пароль");
                    Console.WriteLine("2. Акаунт заблоковано");
                    Console.WriteLine("3. Інше");
                    Console.Write("Ваш вибір: ");
                    string accInput = Console.ReadLine();
                    handler = new AccountSupportHandler();
                    break;

                case "4":
                    handler = new GeneralSupportHandler();
                    break;

                default:
                    Console.WriteLine("Невірний вибір. Спробуйте знову.");
                    continue;
            }

            handler?.Handle();

            Console.WriteLine("\nБажаєте звернутись ще раз? (1 - так / інше - вихід)");
            if (Console.ReadLine() != "1")
                break;
        }
    }
}
