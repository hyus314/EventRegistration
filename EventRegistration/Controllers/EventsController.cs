namespace EventRegistration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using EventRegistration.Data.ViewModels.Events;
    using EventRegistration.Core.Contracts;

    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class EventsController : Controller
    {
        private readonly IEventService eventService;
        public EventsController(IEventService eventService)
        {
            this.eventService = eventService;
        }
        public IActionResult AllEvents()
        {
            return View();
        }
        public IActionResult Details(string date)
        {
            ViewBag.SelectedDate = date;
            return View();
        }
        public async Task<IActionResult> AddEventAsync([FromBody] AddEventDTO model)
        {
            try
            {
                await this.eventService.AddEventAsync(model);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        public async Task<IActionResult> EventsByDate(string date)
        {
            var result = await this.eventService.GetEventsForDateJSONAsync(date);
            return Json(result);
        }

        public async Task<IActionResult> EditEventAsync([FromBody] EditEventDTO model)
        {
            try
            {
                await this.eventService.EditEventAsync(model);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }

        
    }
}
