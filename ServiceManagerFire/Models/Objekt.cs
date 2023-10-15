using System.ComponentModel.DataAnnotations;

namespace ServiceManagerFire.Models
{
    public class Objekt
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
