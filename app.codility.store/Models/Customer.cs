using System;
namespace app.codility.store.Models
{
    public class Customer
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int StoreId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

