
namespace EventRegistration.Data.ViewModels.Events
{
    public interface IEventDTO
    {
        string ClientName { get; }
        string EventType { get; }
        string PhoneNumber { get; }
        string ChildrenMenu { get; }
        string AdultsMenu { get; }
        decimal MoneyInAdvance { get; }
        int Floor { get; }
    }
}
