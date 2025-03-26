using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using EventRegistration.Core.Contracts;
using EventRegistration.Core.Services;
using EventRegistration.Data;
using EventRegistration.Data.Models;
using Microsoft.AspNetCore.DataProtection;
using EventRegistration.Security;
using EventRegistration.Common.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var deployConnectionString = builder.Configuration.GetConnectionString("DeployConnectionString");

builder.Services.AddDbContext<EventRegistrationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IWorkerService, WorkerService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEventService, EventService>();


builder.Services.AddSingleton(provider =>
{
    var dataProtectionProvider = provider.GetService<IDataProtectionProvider>();
    return dataProtectionProvider!.CreateProtector("ProtectEventData");
});


builder.Services.AddSingleton<EventProtector>();
builder.Services.AddSingleton<EmailApiKeysRetriever>();

builder.Services.AddDefaultIdentity<EventWorker>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredLength = 3;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EventRegistrationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddAntiforgery(options =>
{
    options.FormFieldName = "__RequestVerificationToken";
    options.HeaderName = "X-CSRF-VERIFICATION-TOKEN-E-Registration";
    options.SuppressXFrameOptionsHeader = false;
});

var app = builder.Build();
string? diErrorMessage = null;

try
{
    using (var scope1 = app.Services.CreateScope())
    {
        var services1 = scope1.ServiceProvider;

        //Try resolving the service to detect failures early
        var workerService = services1.GetRequiredService<IWorkerService>();
        //throw new Exception("test message");
    }
}
catch (Exception ex)
{
    diErrorMessage = $"Service resolution failed: {ex.Message}";
}
app.Use(async (context, next) =>
{
    if (!string.IsNullOrEmpty(diErrorMessage))
    {
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync($@"
            <html>
            <head><title>Dependency Injection Error</title></head>
            <body>
                <h1 style='color: red;'>Error: {diErrorMessage}</h1>
                <p>Please check your service registrations.</p>
            </body>
            </html>");
    }
    else
    {
        await next();
    }
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<EventRegistrationDbContext>();

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<EventWorker>>();

    if (!roleManager.RoleExistsAsync("Admin").Result)
    {
        var role = new IdentityRole("Admin");
        var roleResult = roleManager.CreateAsync(role).Result;

        if (roleResult.Succeeded)
        {
            var admin = new EventWorker
            {
                UserName = "admin",
                Email = "admin@gmail.com",
            };

            var adminPassword = "Registrator123";
            var adminUserResult = userManager.CreateAsync(admin, adminPassword).Result;

            if (adminUserResult.Succeeded)
            {
                userManager.AddToRoleAsync(admin, "Admin").Wait();
            }

        }
    }

}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapControllers();

app.Run();

