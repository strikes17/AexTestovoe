using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class App
    {
        public ApplicationContext Context => context;

        protected ApplicationContext context = new ApplicationContext();

        protected DbGenerator dbGenerator = new DbGenerator();

        protected DbInitializer dbInitializer = new DbInitializer();

        public void FillDatabase()
        {
            foreach (var item in context.Customers)
            {
                context.Customers.Remove(item);
            }
            foreach (var item in context.Managers)
            {
                context.Managers.Remove(item);
            }
            foreach (var item in context.Orders)
            {
                context.Orders.Remove(item);
            }
            context.SaveChanges();

            var customers = dbGenerator.CreateCustomers();
            var managers = dbGenerator.CreateManagers();
            var orders = dbGenerator.CreateOrders(customers, managers, 20, (100, 400),
                (new DateTime(2020, 1, 1), new DateTime(2026, 1, 1)));

            dbInitializer.InitCustomers(customers, managers, orders);

            context.Customers.AddRange(customers);
            context.Managers.AddRange(managers);
            context.Orders.AddRange(orders);

            context.SaveChanges();

            context.Database.CloseConnection();
        }

        public List<CustomerViewModel> GetCustomers(DateTime beginDate, int sumAmount)
        {
            List<CustomerViewModel> customerViewModels = new List<CustomerViewModel>();
            foreach (var customer in context.Customers)
            {
                var customerOrders = context.Orders.
                    Where(order => order.CustomerID == customer.ID && order.Date >= beginDate).ToList();
                if (customerOrders == null)
                {
                    continue;
                }

                decimal amountFromAllOrders = 0;
                customerOrders.ForEach(x => amountFromAllOrders += x.Amount);

                if (amountFromAllOrders >= sumAmount)
                {
                    var viewModel = new CustomerViewModel()
                    {
                        CustomerName = customer.Name,
                        ManagerName = context.Managers.FirstOrDefault(x => x.ID == customer.ManagerID).Name,
                        Amount = amountFromAllOrders
                    };
                    customerViewModels.Add(viewModel);
                }
            }
            return customerViewModels;
        }
    }
}