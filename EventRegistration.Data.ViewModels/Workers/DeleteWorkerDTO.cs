namespace EventRegistration.Data.ViewModels.Workers
{
    public class DeleteWorkerDTO
    {
        public string WorkerId { get; set; } = null!;
        public string? AdminId { get; set; }
        public string AdminPassword { get; set; } = null!;
    }
}
