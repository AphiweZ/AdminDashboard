using AdminDashboard.Components;
using AdminDashboard.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Razor Components with interactive server-side rendering.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// App services. MockDataService/ThemeService/CurrentUserService are scoped
// so each user's browser circuit gets its own instance (its own theme,
// its own simulated role, etc).
builder.Services.AddScoped<MockDataService>();
builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<CurrentUserService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
