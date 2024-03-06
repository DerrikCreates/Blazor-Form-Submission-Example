using BlazorApp;
using Microsoft.AspNetCore.Identity;
using SubmissionExampleSite.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents((options => options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromSeconds(5)));

builder.Services.AddSingleton<Database>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<SubmitLimiter>();
builder.Services.AddSingleton<Posts>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();