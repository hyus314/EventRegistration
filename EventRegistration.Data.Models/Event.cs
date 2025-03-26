namespace EventRegistration.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ClientName { get; set; } = null!;
        [Required]
        public DateTime StartDate{ get; set; }
        [Required]
        public DateTime EndDate{ get; set; }
        [MaxLength(100)]
        public string? Decorations { get; set; }
        [Required]
        [MaxLength(100)]
        public string EventType { get; set; } = null!;
        [Required]
        [MaxLength(15)] // universal format
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string ChildrenMenu { get; set; } = null!;
        [Required]
        public string AdultsMenu { get; set; } = null!;
        [Required]
        public decimal MoneyInAdvance { get; set; }
        public int Floor { get; set; }
        // On which floor will the event take place: 1 for First (Lower), 2 for Second (Upper) 
    }
}
