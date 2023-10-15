using System.ComponentModel.DataAnnotations;

namespace ServiceManagerFire.Models
{
    public class RoleManager
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}