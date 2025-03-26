namespace EventRegistration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using EventRegistration.Extensions;

    using EventRegistration.Core.Contracts;
    using EventRegistration.Data.ViewModels.Workers;
    using static EventRegistration.Common.Exceptions.WorkerExceptions;
    using static EventRegistration.Common.Exceptions.AdminExceptions;

    //[AutoValidateAntiforgeryToken]
    //[Authorize(Roles = "Admin")]

    public class WorkersController : Controller
    {
        private readonly IWorkerService workerService;

        public WorkersController(IWorkerService workerService)
        {
            this.workerService = workerService;
        }

        public async Task<IActionResult> AllWorkers()
        {
            var workers = await this.workerService.AllWorkersAsync();
            return View(workers);
        }

        [HttpGet]
        public IActionResult AddWorker()
        {
            ViewBag.ValidationErrors = null;
            return View(new AddWorkerDTO());
        }
        [HttpPost]
        public async Task<IActionResult> AddWorker(AddWorkerDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                ViewBag.ValidationErrors = errorMessages;

                return View(model);
            }
            try
            {
                await this.workerService.AddWorkerAsync(model);
            }
            catch (ValidationsException e)
            {
                var errors = e.Message.Split("@").ToList();
                ViewBag.ValidationErrors = errors;
                return View(model);
            }
            catch (WorkerExistsException e)
            {
                ViewBag.ValidationErrors = new List<string>();
                ViewBag.ValidationErrors.Add(e.Message);
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.ValidationErrors = new List<string>();
                ViewBag.ValidationErrors.Add("Unexpected error occured. Please try again later.");
                return View(model);
            }

            return RedirectToAction(nameof(AllWorkers));
        }

        public async Task<IActionResult> ViewWorker(string id)
        {
            try
            {
                var model = await this.workerService.ViewWorkerDetailsAsync(id);
                return View(model);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> EditNameAsync([FromBody] EditWorkerNameDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return Json(new { success = false, errors });
            }
            try
            {
                await this.workerService.EditWorkerNameAsync(model);
                return Json(new { success = true });
            }
            catch (ValidationsException errors)
            {
                return Json(new { success = false, errors });
            }

        }
        public async Task<IActionResult> EditEmailAsync([FromBody] EditWorkerEmailDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return Json(new { success = false, errors });
            }
            try
            {
                await this.workerService.EditWorkerEmailAsync(model);
                return Json(new { success = true });
            }
            catch (ValidationsException errors)
            {
                return Json(new { success = false, errors });
            }

        }
        [HttpPost]
        public async Task<IActionResult> DeleteWorkerAsync(DeleteWorkerDTO model)
        {
            try
            {
                model.AdminId = this.User.GetId();
                await this.workerService.DeleteWorkerAsync(model);
                return RedirectToAction(nameof(AllWorkers));
            }
            catch (WorkerDoesNotExistException)
            {
                return NotFound();
            }
            catch (AdminIdDoesNotExistException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction(nameof(ViewWorker), new { id = model.WorkerId });
            }
        }

        public async Task<IActionResult> DeleteEvent([FromBody] string temporaryId)
        {
            string userId = this.User.GetId();
            try
            {
                await this.workerService.DeleteEventAsync(temporaryId, userId);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
        }
    }
}
