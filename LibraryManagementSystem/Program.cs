using Library.Mappings;
using Library.Models;
using Library.Repositories;
using Library.Repositories.Implementation;
using Library.Repositories.Interfaces;
using Library.Services;
using Library.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using LibraryManagementSystem.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;  // ? skip e?mail confirmation entirely
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddTransient<ILibraryInfoService, LibraryInfoService>();
builder.Services.AddTransient<ILibraryEventService, LibraryEventService>();
builder.Services.AddTransient<ILanguageService, LanguageService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IPublisherService, PublisherService>();
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<ICountryService, CountryService>();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<INewspaperService, NewspaperService>();
builder.Services.AddTransient<IPeriodicalService, PeriodicalService>();
builder.Services.AddTransient<IJournalService, JournalService>();
builder.Services.AddTransient<IEventRegistrationService, EventRegistrationService>();
builder.Services.AddTransient<IUserCodeService, UserCodeService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddTransient<IFieldOfStudyService, FieldOfStudyService>();
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddTransient<IBorrowingService, BorrowingService>();
builder.Services.AddTransient<IItemCopyService, ItemCopyService>();
builder.Services.AddTransient<IAdminDashboardService, AdminDashboardService>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
DataSeeding();
app.Run();

void DataSeeding()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}
