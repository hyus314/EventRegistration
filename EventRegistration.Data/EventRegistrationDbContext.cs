namespace EventRegistration.Data
{
    using EventRegistration.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class EventRegistrationDbContext : IdentityDbContext<EventWorker>
    {
        public EventRegistrationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<EventWorker> EventsWorker { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Event>()
                .Property(e => e.MoneyInAdvance)
                .HasPrecision(6, 2);

            base.OnModelCreating(builder);
        }
    }
}
