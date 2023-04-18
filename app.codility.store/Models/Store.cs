using System;
namespace app.codility.store.Models
{
    public class Store
	{

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public ICollection<Customer>? Customers { get; set; }
    }
}

