
using EventEaseBooking.Components;
using EventEaseBooking.Interfaces;
using EventEaseBooking.Services;
using EventEaseBooking.Static;
using Microsoft.Extensions.Configuration;
using MudBlazor.Services;
using Radzen;
using System;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddRadzenComponents();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IBookingService, Bookingservice>();
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddScoped<IEventService, Eventservice>();

builder.Services.AddScoped<HttpClient>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();
