using EventRegistration.Data.Models;
using EventRegistration.Data.ViewModels.Events;

namespace EventRegistration.Core.Contracts
{
    public interface IEmailService
    {
        public Task SendWelcomeEmailAsync(string recipient, string username);
        public Task NewEventEmailAsync(AddEventDTO newEvent, List<string> recipients);
        public Task EventEditedEmailAsync(Event oldState, Dictionary<string, Tuple<string, string>> changes, List<string> recipients, EditEventDTO newState);
    }
}
