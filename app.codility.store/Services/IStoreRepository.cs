using System;
using app.codility.store.Models;

namespace app.codility.store.Services
{
	public interface IStoreRepository
	{
        ICollection<Store> GetStores(Func<Store, bool> filter, bool includeCustomer = false);
        Customer AddCustomer(Customer customer);
        ICollection<Customer> GetCustomers(int storeId);
    }
}

