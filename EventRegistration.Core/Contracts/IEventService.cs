namespace EventRegistration.Core.Contracts
{
    using EventRegistration.Data.ViewModels.Events;
    public interface IEventService
    {
        public Task AddEventAsync(AddEventDTO model);
        public Task<string> GetEventsForDateJSONAsync(string date);
        public Task EditEventAsync(EditEventDTO model);
    }
}
