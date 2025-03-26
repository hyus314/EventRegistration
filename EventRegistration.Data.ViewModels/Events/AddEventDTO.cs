namespace EventRegistration.Data.ViewModels.Events
{
    public class AddEventDTO : IEventDTO
    {
        public string ClientName { get; set; } = null!;
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public string? Decorations { get; set; }
        public string EventType { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string ChildrenMenu { get; set; } = null!;
        public string AdultsMenu { get; set; } = null!;
        public decimal MoneyInAdvance { get; set; }
        public int Floor { get; set; }

    }
}
