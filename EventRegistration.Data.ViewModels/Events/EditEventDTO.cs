namespace EventRegistration.Data.ViewModels.Events
{
    public class EditEventDTO : IEventDTO
    {
        public string EncryptedId { get; set; } = null!;
        public DateTime Date{ get; set; }
        public string ClientName { get; set; } = null!;
        public string StartDateValue { get; set; } = null!;
        public string EndDateValue { get; set; } = null!;
        public string EventType { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string ChildrenMenu { get; set; } = null!;
        public string AdultsMenu { get; set; } = null!;
        public decimal MoneyInAdvance { get; set; }
        public string? Decorations { get; set; }
        public int Floor { get; set; }
    }
}
