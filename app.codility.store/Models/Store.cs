using System;
namespace app.codility.store.Models
{
	public class Store
	{
		
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}

