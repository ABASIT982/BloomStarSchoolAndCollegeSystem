using BloomStarSchoolAndCollegeSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// -------------------------
// Services
// -------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Use AddIdentity instead of AddDefaultIdentity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // or true if you need email confirmation
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// ✅ Tell Identity to use *your* login page
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin/Login";       // custom login page
    options.AccessDeniedPath = "/Admin/Login"; // optional – redirect if not allowed
});

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// -------------------------
// HTTP pipeline
// -------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();   // must be before Authorization
app.UseAuthorization();

// -------------------------
// ✅ Routing configuration
// -------------------------
// If you use MVC controllers, keep the default route:
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ✅ Start site at /Index (not the Identity login)
app.MapRazorPages();
app.MapFallbackToPage("/Index");

// -------------------------
// ✅ Seed roles + default admin (if you already have SeedData)
// -------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}

app.Run();
