using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddControllers();
//Register services
/**
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<EventTypeService>();
builder.Services.AddScoped<EventVendorService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<VenueService>();
**/

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
