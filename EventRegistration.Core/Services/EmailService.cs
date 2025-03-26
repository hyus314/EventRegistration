using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Microsoft.Extensions.Configuration;

using System.Text;
using System.Globalization;

using EventRegistration.Data.Models;
using EventRegistration.Core.Contracts;
using EventRegistration.Data.ViewModels.Events;

using EventRegistration.Common.Utilities;
using static EventRegistration.Common.EmailMessages.EmailSubjectTitles;
using static EventRegistration.Common.EmailMessages.EmailBodyMessages;
public class EmailService : IEmailService
{
    private readonly MailjetClient client;
    CultureInfo bulgarianCulture;
    EmailApiKeysRetriever apiKeysRetriever;
    public EmailService(IConfiguration configuration, EmailApiKeysRetriever keysRetriever)
    {
        this.apiKeysRetriever = keysRetriever;
        var one = apiKeysRetriever.GetPrivateKey();
        var two = apiKeysRetriever.GetPublicKey();
        this.client = new MailjetClient(
           apiKeysRetriever.GetPublicKey(),
           apiKeysRetriever.GetPrivateKey());

        bulgarianCulture = new CultureInfo("bg-BG");
    }

    public async Task EventEditedEmailAsync(Event oldState, Dictionary<string, Tuple<string, string>> changes, List<string> recipients, EditEventDTO newState)
    {
        StringBuilder body = new();

        var title = string.Format(EventEditedSubjectTitle,
            oldState.ClientName, oldState.PhoneNumber, oldState.StartDate.ToString("f", bulgarianCulture));

        body.AppendLine($"Събитие за името на {oldState.ClientName}, {oldState.PhoneNumber}, с дата и начало {oldState.StartDate.ToString("f", bulgarianCulture)}, има променени стойности: ");
        body.AppendLine();
        if (changes.ContainsKey("Дата") || changes.ContainsKey("Начало") || changes.ContainsKey("Край"))
        {
            if (changes.ContainsKey("Дата"))
            {
                body.AppendLine($"Дата: {changes["Дата"].Item1} -> {changes["Дата"].Item2}");
            }

            if (changes.ContainsKey("Начало"))
            {
                body.AppendLine($"Начало: {changes["Начало"].Item1} -> {changes["Начало"].Item2}");
            }

            if (changes.ContainsKey("Край"))
            {
                body.AppendLine($"Край: {changes["Край"].Item1} -> {changes["Край"].Item2}");
            }

            body.AppendLine();
            body.AppendLine($"Сега събитието има стойности за дата, начало и край: {newState.Date.Date.ToString("D")}, {newState.StartDateValue} - {newState.EndDateValue}");
        }

        body.AppendLine();

        foreach (var change in changes.Where(x => x.Key != "Дата" && x.Key != "Начало" && x.Key != "Край"))
        {
            body.AppendLine($"{change.Key}: {change.Value.Item1} -> {change.Value.Item2}");
        }

        foreach (var recipient in recipients)
        {
            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact("cafekarizma82@gmail.com", "Cafe Karizma"))
                .WithSubject(title)
                .WithTextPart(body.ToString())
                .WithTo(new SendContact(recipient))
                .Build();

            await client.SendTransactionalEmailAsync(email);
        }
    }

    public async Task NewEventEmailAsync(AddEventDTO newEvent, List<string>? recipients)
    {
        if (recipients == null) return;

        TimeZoneInfo bulgariaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");

        var eetStartDate = TimeZoneInfo.ConvertTimeFromUtc(newEvent.StartDate, bulgariaTimeZone);
        var eetEndDate = TimeZoneInfo.ConvertTimeFromUtc(newEvent.EndDate, bulgariaTimeZone);

        var startTime = eetStartDate.ToString("HH:mm");
        var endTime = eetEndDate.ToString("HH:mm");

        var dateTitle = eetStartDate.ToString("f", bulgarianCulture);
        var title = string.Format(NewEventAddedSubjectTitle, newEvent.EventType, dateTitle, newEvent.ClientName);

        var dateBody = newEvent.StartDate.ToString("D", bulgarianCulture);

        var body = string.Format(NewEventBodyMessage, newEvent.ClientName, dateBody, startTime, endTime, newEvent.Decorations, newEvent.EventType, newEvent.PhoneNumber, newEvent.ChildrenMenu, newEvent.ChildrenMenu, newEvent.MoneyInAdvance, newEvent.Floor == 1 ? "Долу" : "Горе");
        foreach (var recipient in recipients)
        {
            var email = new TransactionalEmailBuilder()
            .WithFrom(new SendContact("cafekarizma82@gmail.com", "Cafe Karizma"))
            .WithSubject(title)
            .WithTextPart(body)
            .WithTo(new SendContact(recipient))
            .Build();

            await client.SendTransactionalEmailAsync(email);
        }
    }

    public async Task SendWelcomeEmailAsync(string recipient, string username)
    {

        var email = new TransactionalEmailBuilder()
            .WithFrom(new SendContact("cafekarizma82@gmail.com", "Cafe Karizma"))
            .WithSubject(string.Format(WelcomeSubjectTitle, username))
            .WithTextPart(WelcomeBodyMessage)
            .WithTo(new SendContact(recipient))
            .Build();

        await client.SendTransactionalEmailAsync(email);

    }

}
