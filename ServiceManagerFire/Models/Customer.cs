using System.ComponentModel.DataAnnotations;



namespace ServiceManagerFire.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
