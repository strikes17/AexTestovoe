using System.Numerics;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            App app = new App();
            //Если нужно перегенерировать БД
            app.FillDatabase();
            var customers = app.GetCustomers(new DateTime(2023,1,1), 1000);
            customers.ForEach(x =>
            {
                Console.WriteLine("----------------");
                Console.WriteLine(x.CustomerName);
                Console.WriteLine(x.ManagerName);
                Console.WriteLine(x.Amount);
                Console.WriteLine("----------------");
            });

        }
    }
}