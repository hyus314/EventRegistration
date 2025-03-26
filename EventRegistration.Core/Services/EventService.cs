namespace EventRegistration.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    using static Validations.Event.EventValidations;

    using EventRegistration.Data;
    using EventRegistration.Data.Models;
    using EventRegistration.Core.Contracts;
    using EventRegistration.Data.ViewModels.Events;
    using EventRegistration.Security;

    using static EventRegistration.Common.Exceptions.EventExceptions;
    using static EventRegistration.Common.ErrorMessages.EventErrorMessages;
    using static EventRegistration.Common.Utilities.ChangeFinderForEmail;
    using static EventRegistration.Common.Utilities.DeepCopyEditEventExecuter;
    using Microsoft.AspNetCore.Identity;

    public class EventService : IEventService
    {
        private readonly EventRegistrationDbContext data;
        private readonly EventProtector eventProtector;
        private readonly IEmailService emailService;
        private readonly SignInManager<EventWorker> signInManager;
        public EventService(EventRegistrationDbContext data, EventProtector eventProtector, IEmailService emailService, SignInManager<EventWorker> signInManager)
        {
            this.data = data;
            this.eventProtector = eventProtector;
            this.emailService = emailService;
            this.signInManager = signInManager;
        }
        public async Task AddEventAsync(AddEventDTO model)
        {
            try { ValidateEvent(model); } catch (Exception) { throw; }

            TimeZoneInfo bulgariaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");

            var eetStartDate = TimeZoneInfo.ConvertTimeFromUtc(model.StartDate, bulgariaTimeZone);
            var eetEndDate = TimeZoneInfo.ConvertTimeFromUtc(model.EndDate, bulgariaTimeZone);

            if (eetStartDate.Hour < 8 || eetEndDate.Hour < 8 
                || eetStartDate.Hour > 22 || eetEndDate.Hour > 22 )
            {
                throw new InvalidWorkHoursException();
            }

            var entity = new Event()
            {
                ClientName = model.ClientName,
                StartDate = eetStartDate,
                EndDate = eetEndDate,
                Decorations = model.Decorations,
                EventType = model.EventType,
                PhoneNumber = model.PhoneNumber,
                ChildrenMenu = model.ChildrenMenu,
                AdultsMenu = model.AdultsMenu,
                MoneyInAdvance = model.MoneyInAdvance,
                Floor = model.Floor
            };

            try
            {
                await this.data.Events.AddAsync(entity);
                await this.data.SaveChangesAsync();
                var recipients = await this.data.Users.Select(x => x.Email).ToListAsync();
                await this.emailService.NewEventEmailAsync(model, recipients!);
            }
            catch (Exception)
            {
                throw new SomethingWentWrongException(SomethingWentWrongAdding);
            }
}

        public async Task EditEventAsync(EditEventDTO model)
        {
            var entityEvent = await this.data.Events
                 .FindAsync(this.eventProtector.Decrypt(model.EncryptedId))
                 ?? throw new EventNotFoundException();

            if (!TimeSpan.TryParse(model.StartDateValue, out TimeSpan startTime) ||
                !TimeSpan.TryParse(model.EndDateValue, out TimeSpan endTime))
            {
                throw new FormatException("Invalid time format. Expected HH:mm.");
            }

            if (GetChanges(entityEvent, model).Count == 0)
            {
                throw new ChangesHaventBeenMadeEditingException();
            }
            if (startTime.Hours < 8 || endTime.Hours < 8
               || startTime.Hours > 22 || endTime.Hours > 22)
            {
                throw new InvalidWorkHoursException();
            }

            DateTime newStartDate = model.Date.Date.Add(startTime);
            DateTime newEndDate = model.Date.Date.Add(endTime);

            if (newEndDate < newStartDate)
            {
                throw new EndDateSoonerThanStartDateException();
            }

            try { ValidateEvent(model); } catch (Exception) { throw; }

            var oldState = DeepCopy(entityEvent);

            try
            {
                entityEvent.ClientName = model.ClientName;
                entityEvent.StartDate = newStartDate;
                entityEvent.EndDate = newEndDate;
                entityEvent.EventType = model.EventType;
                entityEvent.PhoneNumber = model.PhoneNumber;
                entityEvent.ChildrenMenu = model.ChildrenMenu;
                entityEvent.AdultsMenu = model.AdultsMenu;
                entityEvent.MoneyInAdvance = model.MoneyInAdvance;
                entityEvent.Decorations = model.Decorations;
                entityEvent.Floor = model.Floor;

                await this.data.SaveChangesAsync();
                var recipients = await this.data.Users.Select(x => x.Email).ToListAsync();
                await this.emailService.EventEditedEmailAsync(oldState, GetChanges(oldState, model), recipients!, model);
            }
            catch (Exception)
            {
                throw new SomethingWentWrongException(SomethingWentWrongEditing);
            }
        }

        public async Task<string> GetEventsForDateJSONAsync(string date)
        {
            var dateModel = DateTime.Parse(date);

            var events = await this.data.Events
                 .Where(x => x.StartDate.Date == dateModel.Date)
                 .Select(x => new
                 {
                     title = x.ClientName,
                     start = x.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                     end = x.EndDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                     id = this.eventProtector.Encrypt(x.Id),
                     phoneNumber = x.PhoneNumber,
                     children = x.ChildrenMenu,
                     adult = x.AdultsMenu,
                     floor = x.Floor,
                     decorations = x.Decorations,
                     type = x.EventType,
                     moneyInAdvance = x.MoneyInAdvance
                 })
                 .ToArrayAsync();

            return JsonConvert.SerializeObject(events, Formatting.Indented);
        }
    }
}
