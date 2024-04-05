namespace ConsoleApp1
{
    public class DbInitializer
    {
        public void InitCustomers(List<Customer> customers, List<Manager> managers, List<Order> orders)
        {
            Random random = new Random();
            customers.ForEach(customer =>
            {
                var manager = managers[random.Next(0, managers.Count - 1)];
                customer.Manager = manager;
                customer.ManagerID = manager.ID;
                customer.Orders = orders.Where(order => order.CustomerID == customer.ID).ToList();
            });
        }

    }
}