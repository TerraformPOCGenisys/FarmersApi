using System.Text.Json;
using Genisys.Farmers.Api.Model;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Sample data for farmers and products
var farmers = new List<Farmer>
{
    new() {
        Id = 1,
        Name = "John Doe",
        FarmName = "Doe Farm",
        Location = "Green Valley",
        Contact = "john@example.com",
        Products =
        [
            new Product { Id = 1, Name = "Apples", Price = 1.99m, Description = "Fresh apples", IsInStock = true },
            new Product { Id = 2, Name = "Honey", Price = 5.99m, Description = "Natural honey" }
        ]
    },
    new() {
        Id = 2,
        Name = "Jane Smith",
        FarmName = "Smith Orchard",
        Location = "Sunshine Hills",
        Contact = "jane@example.com",
        Products =
        [
            new Product { Id = 3, Name = "Peaches", Price = 2.99m, Description = "Juicy peaches" },
            new Product { Id = 4, Name = "Jam", Price = 4.99m, Description = "Homemade jam", IsInStock = true }
        ]
    }
};

string filepath = File.Exists("BBProducts.json") ? "BBProducts.json" : Path.Combine(AppContext.BaseDirectory, "publish", "BBProducts.json");
string bbProductJson = File.ReadAllText(filepath);
var bbProducts = JsonSerializer.Deserialize<List<BBProduct>>(bbProductJson, new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true
});

// GET /api/bbProducts - Return the list of farmers
app.MapGet("/api/bbproducts", () => bbProducts);

// POST /api/products/description - Update a product's description
app.MapPost("/api/products/description", (UpdateProductDescription updateProduct) =>
{
    var bbProduct = bbProducts?.FirstOrDefault(p => p.Title == updateProduct.Name);
    if (bbProduct is null)
    {
        return Results.NotFound();
    }

    bbProduct.Body = updateProduct.Description; // Update the description
    return Results.Ok(bbProduct);
});

// POST /api/products/price - Update a product's price
app.MapPost("/api/products/price", (UpdateProductPrice updateProductPrice) =>
{
    var bbProduct = bbProducts?.FirstOrDefault(p => p.Title == updateProductPrice.Name);
    if (bbProduct is null)
    {
        return Results.NotFound();
    }

    bbProduct.Price = updateProductPrice.Price; // Update the description
    return Results.Ok(bbProduct);
});

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
    newFarmer.Id = farmers.Any() ? farmers.Max(f => f.Id) + 1 : 1;
    farmers.Add(newFarmer);
    return Results.Created($"/api/farmers/{newFarmer.Id}", newFarmer);
});

// GET /api/farmers/{farmerId}/products - Get products for a specific farmer
app.MapGet("/api/farmers/{farmerId}/products", (int farmerId) =>
{
    var farmer = farmers.FirstOrDefault(f => f.Id == farmerId);
    return farmer is not null ? Results.Ok(farmer.Products) : Results.NotFound();
});

// POST /api/farmers/{farmerId}/products - Add a product to a specific farmer
app.MapPost("/api/farmers/{farmerId}/products", (int farmerId, Product newProduct) =>
{
    var farmer = farmers.FirstOrDefault(f => f.Id == farmerId);
    if (farmer is null) return Results.NotFound();

    newProduct.Id = farmer.Products.Any() ? farmer.Products.Max(p => p.Id) + 1 : 1;
    newProduct.FarmerId = farmerId;
    farmer.Products.Add(newProduct);
    return Results.Created($"/api/farmers/{farmerId}/products/{newProduct.Id}", newProduct);
});

// GET /api/products - Retrieve all products
app.MapGet("/api/products", () =>
{
    var allProducts = farmers.SelectMany(f => f.Products).ToList();
    return Results.Ok(allProducts);
});

// PUT /api/products/{id}/out-of-stock - Mark a product as out of stock
app.MapPut("/api/products/{id}/out-of-stock", (int id) =>
{
    var product = farmers.SelectMany(f => f.Products).FirstOrDefault(p => p.Id == id);
    if (product is null)
    {
        return Results.NotFound();
    }

    product.IsInStock = false; // Mark as out of stock
    return Results.Ok(product);
});

// PUT /api/products/{id}/description - Update a product's description
app.MapPut("/api/products/{id}/description", (int id, [FromBody] string newDescription) =>
{
    var product = farmers.SelectMany(f => f.Products).FirstOrDefault(p => p.Id == id);
    if (product is null)
    {
        return Results.NotFound();
    }

    product.Description = newDescription; // Update the description
    return Results.Ok(product);
});

app.Run();
