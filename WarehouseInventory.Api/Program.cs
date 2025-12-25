using WarehouseInventory.Api.Models;
using WarehouseInventory.Api.Dtos;
using Microsoft.EntityFrameworkCore;
using WarehouseInventory.Api.Data;
using FluentValidation;
using WarehouseInventory.Api.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WarehouseDbContext>(options =>
{
    options.UseSqlite("Data Source=warehouse.db");
});

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
var app = builder.Build();

app.UseExceptionHandler();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WarehouseDbContext>();

    db.Database.EnsureCreated();

    if (!db.Products.Any())
    {
        db.Products.AddRange(
            new Product
            {
                Name = "Laptop",
                SKU = "WH-1001",
                Price = 1200,
                Quantity = 10,
                CategoryId = 1
            },
            new Product
            {
                Name = "Mouse",
                SKU = "WH-1002",
                Price = 25,
                Quantity = 100,
                CategoryId = 1
            }
        );

        db.SaveChanges();
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var productsGroup = app.MapGroup("/products");


// GET /products
productsGroup.MapGet("/", async (WarehouseDbContext db) =>
{
    var products = await db.Products
        .Select(p => new ProductResponseDto(
            p.Id,
            p.Name,
            p.SKU,
            p.Price,
            p.Quantity,
            p.CategoryId))
        .ToListAsync();

    return Results.Ok(products);
});


// GET /products/{id}
productsGroup.MapGet("/{id:int}", async (int id, WarehouseDbContext db) =>
{
    var product = await db.Products.FindAsync(id);

    if (product is null)
        return Results.NotFound();

    return Results.Ok(new ProductResponseDto(
        product.Id,
        product.Name,
        product.SKU,
        product.Price,
        product.Quantity,
        product.CategoryId));
});


// POST /products
productsGroup.MapPost("/", async (
    CreateProductRequestDto request,
    WarehouseDbContext db) =>
{
    var product = new Product
    {
        Name = request.Name,
        SKU = request.SKU,
        Price = request.Price,
        Quantity = request.Quantity,
        CategoryId = request.CategoryId
    };

    db.Products.Add(product);
    await db.SaveChangesAsync();

    return Results.Created($"/products/{product.Id}",
        new ProductResponseDto(
            product.Id,
            product.Name,
            product.SKU,
            product.Price,
            product.Quantity,
            product.CategoryId));
})
.AddEndpointFilter<ValidationFilter<CreateProductRequestDto>>();


// PUT /products/{id}
productsGroup.MapPut("/{id:int}", async (
    int id,
    UpdateProductRequestDto request,
    WarehouseDbContext db) =>
{
    var product = await db.Products.FindAsync(id);

    if (product is null)
        return Results.NotFound(new
        {
            message = $"Product with id {id} not found."
        });

    product.Price = request.Price;
    product.Quantity = request.Quantity;
    product.Name = request.Name;

    await db.SaveChangesAsync();

    return Results.NoContent();
})
.AddEndpointFilter<ValidationFilter<UpdateProductRequestDto>>();



// DELETE /products/{id}
productsGroup.MapDelete("/{id:int}", async (int id, WarehouseDbContext db) =>
{
    var product = await db.Products.FindAsync(id);

    if (product is null)
        return Results.NotFound();

    db.Products.Remove(product);
    await db.SaveChangesAsync();

    return Results.NoContent();
});


app.Run();
