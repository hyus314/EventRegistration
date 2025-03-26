namespace EventRegistration.Core.Services
{
    using System.Threading.Tasks;


    using EventRegistration.Core.Contracts;

    using EventRegistration.Data;
    using EventRegistration.Data.Models;
    using EventRegistration.Data.ViewModels.Workers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using static Validations.Worker.WorkerValidation;
    using static EventRegistration.Common.ErrorMessages.EventErrorMessages;
    using static EventRegistration.Common.Exceptions.WorkerExceptions;
    using static EventRegistration.Common.Exceptions.AdminExceptions;
    using static EventRegistration.Common.Exceptions.EventExceptions;
    using EventRegistration.Security;

    public class WorkerService : IWorkerService
    {
        private readonly EventRegistrationDbContext data;
        private readonly UserManager<EventWorker> userManager;
        private readonly SignInManager<EventWorker> signInManager;
        private readonly IEmailService emailService;
        private readonly EventProtector eventProtector;
        public WorkerService(EventRegistrationDbContext data,
            UserManager<EventWorker> userManager,
            IEmailService emailService,
            SignInManager<EventWorker> signInManager,
            EventProtector eventProtector)
        {
            this.data = data;
            this.userManager = userManager;
            this.emailService = emailService;
            this.signInManager = signInManager;
            this.eventProtector = eventProtector;
        }
        public async Task AddWorkerAsync(AddWorkerDTO model)
        {
            List<string> errors;
            errors = AddWorkerValidations(model);
            if (errors.Count != 0)
            {
                throw new ValidationsException(string.Join("@", errors));
            }
            if (await userManager.FindByNameAsync(model.Username) != null)
            {
                throw new WorkerExistsException("username", model.Username);
            }
            if (await userManager.FindByEmailAsync(model.Email) != null)
            {
                throw new WorkerExistsException("email", model.Email);
            }
            var user = new EventWorker
            {
                UserName = model.Username,
                Email = model.Email,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                errors = result.Errors
                    .Select(e => e.Description)
                    .ToList();

                throw new ValidationsException(string.Join("@", errors));
            }

            await this.emailService.SendWelcomeEmailAsync(model.Email, user.UserName);
        }

        public async Task<IEnumerable<ViewWorkerDTO>> AllWorkersAsync()
        {
            return await this.data.Users.Select(x => new ViewWorkerDTO
            {
                Id = x.Id,
                Username = x.UserName!,
                Email = x.Email!
            })
                .Where(x => x.Username != "admin")
                .ToArrayAsync();

        }

        public async Task DeleteEventAsync(string eventId, string userId)
        {
            var admin = await this.data.Users
                .FindAsync(userId)
                ?? throw new AdminIdDoesNotExistException(userId ?? "none");

            var role = await this.data.Roles
                .FirstOrDefaultAsync(x => x.Name == "Admin")
                ?? throw new AdminRoleDoesNotExistException();

            var check = await this.userManager
                .IsInRoleAsync(admin, "Admin")
                ? 0 : throw new UserNotAdminException(admin.Id);

            var eventEntity = await this.data.Events
                .FindAsync(this.eventProtector.Decrypt(eventId))
                ?? throw new EventNotFoundException();

            try
            {
                this.data.Events.Remove(eventEntity);
                await this.data.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new SomethingWentWrongException(SomethingWentWrongDeleting);
            }
        }

        public async Task DeleteWorkerAsync(DeleteWorkerDTO model)
        {
            var admin = await this.data.Users
                .FindAsync(model.AdminId)
                ?? throw new AdminIdDoesNotExistException(model.AdminId ?? "none");

            var role = await this.data.Roles
                .FirstOrDefaultAsync(x => x.Name == "Admin")
                ?? throw new AdminRoleDoesNotExistException();

            var check = await this.userManager
                .IsInRoleAsync(admin, "Admin")
                ? 0 : throw new UserNotAdminException(admin.Id);

            var adminPassword = await this.userManager
                .CheckPasswordAsync(admin, model.AdminPassword)
                ? 0 : throw new AdminPasswordWrongException();

            var workerToDelete = await this.data.Users
                .FindAsync(model.WorkerId)
                ?? throw new WorkerDoesNotExistException(model.WorkerId ?? "none");

            try
            {
                await this.userManager.UpdateSecurityStampAsync(workerToDelete);
                await this.userManager.DeleteAsync(workerToDelete);
                await this.data.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new SomethingWentWrongDeletingException();
            }
        }

        public async Task EditWorkerEmailAsync(EditWorkerEmailDTO model)
        {
            var errors = EditWorkerEmailValidations(model.NewEmail);
            if (errors.Count != 0)
            {
                throw new ValidationsException(string.Join(" ", errors));
            }

            var worker = await this.data.Users
                .FindAsync(model.WorkerId)
                ?? throw new WorkerDoesNotExistException(model.WorkerId);

            if (worker.Email == model.NewEmail)
            {
                throw new ValidationsException("The new username cannot be the same as the old one.");
            }

            worker.Email = model.NewEmail;
            await this.data.SaveChangesAsync();
        }

        public async Task EditWorkerNameAsync(EditWorkerNameDTO model)
        {
            var errors = EditWorkerNameValidations(model.NewUsername);
            if (errors.Count != 0)
            {
                throw new ValidationsException(string.Join(" ", errors));
            }

            var worker = await this.data.Users
                .FindAsync(model.WorkerId)
                ?? throw new WorkerDoesNotExistException(model.WorkerId);

            if (worker.UserName == model.NewUsername)
            {
                throw new ValidationsException("The new username cannot be the same as the old one.");
            }
            worker.UserName = model.NewUsername;
            await this.data.SaveChangesAsync();
        }

        public async Task<DetailsWorkerDTO> ViewWorkerDetailsAsync(string id)
        {
            var model = await this.data.Users
                .FindAsync(id)
                ?? throw new WorkerDoesNotExistException(id);

            return new DetailsWorkerDTO()
            {
                Id = id,
                Username = model.UserName!,
                Email = model.Email!
            };
        }
    }
}
