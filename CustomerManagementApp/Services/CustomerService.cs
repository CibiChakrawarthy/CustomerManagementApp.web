using CustomerManagementApp.Interfaces;
using CustomerManagementApp.Models;

namespace CustomerManagementApp.Services
{
    public class CustomerService : ICustomerService
    {
        private static readonly List<Customer> _customers = new();
        private static readonly object _lock = new();

        // Add test data in constructor
        public CustomerService()
        {
            if (_customers.Count == 0)
            {
                _customers.Add(new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Customer",
                    Age = 30,
                    PostCode = "SW1A1AA",
                    Height = 1.75
                });
            }
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await Task.FromResult(_customers);
        }

        public Task<Customer?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_customers.FirstOrDefault(c => c.Id == id));
        }

        public Task AddAsync(Customer customer)
        {
            lock (_lock)
            {
                customer.Id = Guid.NewGuid();
                _customers.Add(customer);
                Console.WriteLine($"Added customer: {customer.Name}, ID: {customer.Id}");
            }
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Customer customer)
        {
            lock (_lock)
            {
                var existing = _customers.FirstOrDefault(c => c.Id == customer.Id);
                if (existing != null)
                {
                    existing.Name = customer.Name;
                    existing.Age = customer.Age;
                    existing.PostCode = customer.PostCode;
                    existing.Height = customer.Height;
                    Console.WriteLine($"Updated customer: {existing.Name}, ID: {existing.Id}");
                }
            }
            return Task.CompletedTask;
        }
    }
}