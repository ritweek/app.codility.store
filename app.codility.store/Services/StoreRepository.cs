using System;
using app.codility.store.Models;

namespace app.codility.store.Services
{
    public class StoreRepository : IStoreRepository
    {
        private readonly List<Store> _stores;

        public StoreRepository()
        {
            _stores = new List<Store>
        {
            new Store
            {
                Id = 1,
                Name = "Store 1",
                CountryCode = "NL",
                Customers = new List<Customer>
                {
                    new Customer { Id = 1, Name = "Customer 1", Email = "customer1@store1.com" },
                    new Customer { Id = 2, Name = "Customer 2", Email = "customer2@store1.com" },
                }
            },
            new Store
            {
                Id = 2,
                Name = "Store 2",
                CountryCode = "PL",
                Customers = new List<Customer>
                {
                    new Customer { Id = 3, Name = "Customer 3", Email = "customer1@store2.com" },
                    new Customer { Id = 4, Name = "Customer 4", Email = "customer2@store2.com" },
                }
            },
            new Store
            {
                Id = 3,
                Name = "Store 3",
                CountryCode = "US",
                Customers = new List<Customer>
                {
                    new Customer { Id = 5, Name = "Customer 5", Email = "customer1@store3.com" },
                    new Customer { Id = 6, Name = "Customer 6", Email = "customer2@store3.com" },
                }
            }
        };
        }

        public ICollection<Store> GetStores(Func<Store, bool> filter, bool includeCustomer = false)
        {
            if (includeCustomer)
            {
                return _stores.Where(filter).ToList();
            }
            else
            {
                return _stores.Where(filter).Select(s => new Store
                {
                    Id = s.Id,
                    Name = s.Name,
                    CountryCode = s.CountryCode
                }).ToList();
            }
        }

        public ICollection<Customer> GetCustomers(int storeId)
        {
            var store = _stores.FirstOrDefault(s => s.Id == storeId);
            if (store == null)
            {
                return null;
            }

            return store.Customers.ToList();
        }

        public Customer AddCustomer(Customer customer)
        {
            var store = _stores.FirstOrDefault(s => s.Id == customer.StoreId);
            if (store == null)
            {
                return null;
            }

            customer.Id = store.Customers.Max(c => c.Id) + 1;
            store.Customers.Add(customer);

            return customer;
        }
    }


}

