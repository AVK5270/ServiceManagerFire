using System.ComponentModel.DataAnnotations;


namespace ServiceManagerFire.Models
{
    public class Incident
    {
        public int Id { get; set; }
        public int ObjektId { get; set; }
        [Required]
        public string Description { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public DateTime DateTime { get; set; }

        public Objekt Objekt { get; set; }
        public User User { get; set; }
        public Status Status { get; set; }
    }
}
