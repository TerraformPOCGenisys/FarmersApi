using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin() // Allow requests from any origin
            .AllowAnyMethod() // Allow any HTTP method
            .AllowAnyHeader(); // Allow any headers
    });
});

// Register services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable CORS
app.UseCors("AllowAll");

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Sample data for farmers
var farmers = new List<Farmer>
{
    new() {
        Id = 1,
        Name = "John Doe",
        FarmName = "Doe Farm",
        Location = "Green Valley",
        Products = ["Apples", "Carrots", "Honey"],
        Contact = "john@example.com"
    },
    new() {
        Id = 2,
        Name = "Jane Smith",
        FarmName = "Smith Orchard",
        Location = "Sunshine Hills",
        Products = ["Berries", "Peaches", "Jam"],
        Contact = "jane@example.com"
    }
};

// GET /api/farmers - Return the list of farmers
app.MapGet("/api/farmers", () => farmers);

// GET /api/farmers/{id} - Return details of a specific farmer
app.MapGet("/api/farmers/{id}", (int id) =>
{
    var farmer = farmers.FirstOrDefault(f => f.Id == id);
    return farmer is not null ? Results.Ok(farmer) : Results.NotFound();
});

// POST /api/farmers - Add a new farmer
app.MapPost("/api/farmers", (Farmer newFarmer) =>
{
    newFarmer.Id = farmers.Count == 0 ? 1 : farmers.Max(f => f.Id) + 1;
    farmers.Add(newFarmer);
    return Results.Created($"/api/farmers/{newFarmer.Id}", newFarmer);
});

app.Run();
