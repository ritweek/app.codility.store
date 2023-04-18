using System;
namespace app.codility.store.Models
{
	public class Customer
	{

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int StoreId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

