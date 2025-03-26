namespace EventRegistration.Core.Contracts
{
    using EventRegistration.Data.ViewModels.Workers;
    public interface IWorkerService
    {
        public Task AddWorkerAsync(AddWorkerDTO model);
        public Task<IEnumerable<ViewWorkerDTO>> AllWorkersAsync();
        public Task<DetailsWorkerDTO> ViewWorkerDetailsAsync(string id);
        public Task EditWorkerNameAsync(EditWorkerNameDTO model);
        public Task EditWorkerEmailAsync(EditWorkerEmailDTO model);
        public Task DeleteWorkerAsync(DeleteWorkerDTO model);
        public Task DeleteEventAsync(string eventId, string userId);
    }
}
