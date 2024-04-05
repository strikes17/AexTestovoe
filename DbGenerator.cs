namespace ConsoleApp1
{
    public class DbGenerator
    {
        public List<Customer> CreateCustomers()
        {
            string[] names = new string[] { "Oleg", "Alex", "Olga", "Timoty", "Ilyas" };
            List<Customer> customers = new List<Customer>();
            for (int i = 0; i < names.Length; i++)
            {
                Customer customer = new Customer()
                {
                    ID = i + 1,
                    Name = names[i],
                };
                customers.Add(customer);
            }
            return customers;
        }

        public List<Manager> CreateManagers()
        {
            string[] names = new string[] { "Abdul", "Asbek", "Hagimurjon" };
            List<Manager> managers = new List<Manager>();
            for (int i = 0; i < names.Length; i++)
            {
                Manager manager = new Manager()
                {
                    ID = i + 1,
                    Name = names[i]
                };
                managers.Add(manager);
            }
            return managers;
        }

        public List<Order> CreateOrders(List<Customer> customers, List<Manager> managers, int totalOrdersCount,
            (int, int) randomAmountValue, (DateTime, DateTime) randomDateTime)
        {
            List<Order> orders = new List<Order>();
            Random random = new Random();
            for (int i = 0; i < totalOrdersCount; i++)
            {
                var customer = customers[random.Next(0, customers.Count - 1)];
                Order order = new Order()
                {
                    ID = i + 1,
                    Customer = customer,
                    Amount = random.Next(randomAmountValue.Item1, randomAmountValue.Item2),
                    CustomerID = customer.ID,
                    Date = GetRandomDate(randomDateTime.Item1, randomDateTime.Item2)
                };
                orders.Add(order);
            }
            return orders;
        }

        protected DateTime GetRandomDate(DateTime startDate, DateTime endDate)
        {
            Random random = new Random();
            TimeSpan timeSpan = endDate - startDate;
            TimeSpan randomSpan = new TimeSpan(0, random.Next(0, (int)timeSpan.TotalMinutes), 0);
            return startDate + randomSpan;
        }
    }
}