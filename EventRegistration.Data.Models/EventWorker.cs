using Microsoft.AspNetCore.Identity;

namespace EventRegistration.Data.Models;

public class EventWorker : IdentityUser
{
    public ICollection<Event> AddedEvents{ get; set; } = new HashSet<Event>();
}
