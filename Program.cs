using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using $safeprojectname$.Data;
using $safeprojectname$.Identity;
using $safeprojectname$.Interface;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

// Add EntityFrameworkCore
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add Identity
builder.Services.AddDefaultIdentity<ProjectIdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<ProjectIdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure Identity Password
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
});

// Configure NToastNotify
builder.Services.AddMvc().AddNToastNotifyToastr(new NToastNotify.ToastrOptions()
{
    ProgressBar = false,
    PositionClass = ToastPositions.TopRight
});

// Add Interfaces
builder.Services.AddSingleton<IDatabaseAccess, DatabaseAccess>();
builder.Services.AddScoped<IDatabaseStartup, DatabaseStartup>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
