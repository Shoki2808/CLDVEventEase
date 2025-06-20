using Azure.Storage.Blobs;
using EventEaseAPI.Data.ApplicationDbContext;
using EventEaseAPI.Helpers;
using EventEaseAPI.Interfaces;
using EventEaseAPI.Mapping;
using EventEaseAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton(provider =>
    new BlobServiceClient(builder.Configuration["AzureBlobStorage:ConnectionString"])
);

builder.Services.AddSingleton<AzureSearchClient>();
builder.Services.AddScoped<AzureSearchService>();



//Register services

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventTypeService, EventTypeService>();
builder.Services.AddScoped<IVenueService, VenueService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IBlobService, BlobService>();
builder.Services.AddScoped<IVenueService, VenueService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
